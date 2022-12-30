using System;
using System.IO;
using Telnyx;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Telnyx.net.Services.Calls.CallCommands;

namespace asp_net_IVR {
    public class WebhookHelpers
    {
        public static async Task<CallControlWebhook> deserializeWebhook(HttpRequest request)
        {
            string json;
            using (var reader = new StreamReader(request.Body))
            {
                json = await reader.ReadToEndAsync();
            }
            CallControlWebhook myDeserializedClass = JsonSerializer.Deserialize<CallControlWebhook>(json);
            return myDeserializedClass;
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            if (base64EncodedData == null) {
                return "";
            }
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }

    [ApiController]
    [Route("call-control/[controller]")]
    public class OutboundController : ControllerBase
    {
        [HttpPost]
        [Consumes("application/json")]
        public string CallControlOutboundWebhook(CallControlWebhook webhook)
        {
            Console.WriteLine($"Received outbound event: {webhook.data.event_type}");
            return "";
        }
    }

    [ApiController]
    [Route("call-control/[controller]")]
    public class InboundController : ControllerBase
    {

        [HttpPost]
        [Consumes("application/json")]
        public async Task<string> CallControlInboundWebhook(CallControlWebhook webhook)
        {
            String callControlId = webhook.data.payload.call_control_id;
            CallControlService callControlService = new CallControlService();
            callControlService.CallControlId = callControlId;
            Guid commandId = Guid.NewGuid();
            String webhookClientState = WebhookHelpers.Base64Decode(webhook.data.payload.client_state);
            try {
                switch (webhook.data.event_type){
                    case "call.initiated":
                        CallControlAnswerOptions answerOptions = new CallControlAnswerOptions() {
                            CommandId = commandId
                        };
                        CallAnswerResponse answerResponse = await callControlService.AnswerAsync(answerOptions);
                        Console.WriteLine($"Answer Response: {answerResponse.TelnyxResponse.ResponseJson.ToString()}");
                        return "answer-sent";
                    case "call.answered":
                        CallControlGatherUsingSpeakOptions gatherUsingSpeakOptions = new CallControlGatherUsingSpeakOptions(){
                            Language = "en-US",
                            Voice = "female",
                            Payload = "Please enter the 10 digit phone number you would like to dial, followed by the pound sign",
                            InvalidPayload = "Sorry, I didn't get that",
                            MaximumDigits = 11,
                            MinimumDigits = 10,
                            ValidDigits = "0123456789",
                            CommandId = commandId
                        };
                        CallGatherUsingSpeakResponse gatherResponse = await callControlService.GatherUsingSpeakAsync(gatherUsingSpeakOptions);
                        Console.WriteLine($"Gather Response: {gatherResponse.TelnyxResponse.ResponseJson.ToString()}");
                        return "gather-sent";
                    case "call.gather.ended":
                        String reason = webhook.data.payload.status;
                        if (reason == "call_hangup") {
                            Console.WriteLine($"Call: {callControlId} hung up during gather");
                        }
                        String digits = webhook.data.payload.digits;
                        String phoneNumber = $"+1{digits}";
                        UriBuilder uriBuilder = new UriBuilder(Request.Scheme, Request.Host.ToString());
                        uriBuilder.Path = "call-control/outbound";
                        string transferUri = uriBuilder.ToString();
                        CallControlTransferOptions transferOptions = new CallControlTransferOptions(){
                            To = phoneNumber,
                            WebhookUrl = transferUri,
                            CommandId = commandId
                        };
                        CallTransferResponse transferResponse = await callControlService.TransferAsync(transferOptions);
                        Console.WriteLine($"Transfer Response: {transferResponse.TelnyxResponse.ResponseJson.ToString()}");
                        return "transfer-sent";
                    default:
                        Console.WriteLine($"Non-handled Event: {webhook.data.event_type}");
                        return "default-non-event";
                    }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            return "";
        }
    }
}
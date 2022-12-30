using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using Telnyx;
using Telnyx.net.Entities;
using Microsoft.AspNetCore.Http;
using dotnet_starter.Models;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon;

namespace dotnet_starter.Controllers
{
  public class WebhookHelpers
  {
    public static async Task<InboundWebhook> deserializeInboundMessage(HttpRequest request)
    {
      string json;
      using (var reader = new StreamReader(request.Body))
      {
        json = await reader.ReadToEndAsync();
      }
      InboundWebhook myDeserializedClass = JsonConvert.DeserializeObject<InboundWebhook>(json);
      return myDeserializedClass;
    }

    public static async Task<OutboundWebhook> deserializeOutboundMessage(HttpRequest request)
    {
      string json;
      using (var reader = new StreamReader(request.Body))
      {
        json = await reader.ReadToEndAsync();
      }
      OutboundWebhook myDeserializedClass = JsonConvert.DeserializeObject<OutboundWebhook>(json);
      return myDeserializedClass;
    }

    public static async Task<String> UploadFileAsync(string filePath)
    {
      string bucketName = System.Environment.GetEnvironmentVariable("TELNYX_MMS_S3_BUCKET");
      RegionEndpoint bucketRegion = RegionEndpoint.USEast2;
      IAmazonS3 s3Client = new AmazonS3Client(bucketRegion);
      TransferUtility fileTransferUtility = new TransferUtility(s3Client);
      string fileName = System.IO.Path.GetFileName(filePath);
      string mediaUrl = "";
      try
      {
        TransferUtilityUploadRequest fileTransferUtilityRequest = new TransferUtilityUploadRequest
        {
          BucketName = bucketName,
          FilePath = filePath,
          CannedACL = S3CannedACL.PublicRead
        };
        await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
        Console.WriteLine("Upload completed");
        mediaUrl = $"https://{bucketName}.s3.amazonaws.com/{fileName}";
      }
      catch (AmazonS3Exception e)
      {
        Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
      }
      catch (Exception e)
      {
        Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
      }
      return mediaUrl;
    }

    public static async Task<string> downloadMediaAsync(string directoryPath, string fileName, Uri uri)
    {
      HttpClient httpClient = new HttpClient();
      string uriWithoutQuery = uri.GetLeftPart(UriPartial.Path);
      string fileExtension = Path.GetExtension(uriWithoutQuery);
      string path = Path.Combine(directoryPath, $"{fileName}{fileExtension}");
      Directory.CreateDirectory(directoryPath);
      byte[] imageBytes = await httpClient.GetByteArrayAsync(uri);
      await File.WriteAllBytesAsync(path, imageBytes);
      return path;
    }
  }

  [ApiController]
  [Route("messaging/[controller]")]
  public class OutboundController : ControllerBase
  {
    // POST messaging/Inbound
    [HttpPost]
    [Consumes("application/json")]
    public async Task<string> MessageDLRCallback()
    {
      OutboundWebhook webhook = await WebhookHelpers.deserializeOutboundMessage(this.Request);
      Console.WriteLine($"Received DLR for message with ID: {webhook.data.payload.id}");
      return "";
    }
  }

  [ApiController]
  [Route("messaging/[controller]")]
  public class InboundController : ControllerBase
  {

    private string TELNYX_API_KEY = System.Environment.GetEnvironmentVariable("TELNYX_API_KEY");
    // POST messaging/Inbound
    [HttpPost]
    [Consumes("application/json")]
    public async Task<string> MessageInboundCallback()
    {
      InboundWebhook webhook = await WebhookHelpers.deserializeInboundMessage(this.Request);
      UriBuilder uriBuilder = new UriBuilder(Request.Scheme, Request.Host.ToString());
      uriBuilder.Path = "messaging/outbound";
      string dlrUri = uriBuilder.ToString();
      string to = webhook.data.payload.to[0].phone_number;
      string from = webhook.data.payload.from.phone_number;
      List<MediaItem> media = webhook.data.payload.media;
      List<string> files = new List<string>();
      List<string> mediaUrls = new List<string>();
      if (media != null)
      {
        foreach (var item in media)
        {
          string path = await WebhookHelpers.downloadMediaAsync("./", item.hash_sha256, new Uri(item.url));
          files.Add(path);
          string mediaUrl = await WebhookHelpers.UploadFileAsync(path);
          mediaUrls.Add(mediaUrl);
        }
      }
      TelnyxConfiguration.SetApiKey(TELNYX_API_KEY);
      MessagingSenderIdService service = new MessagingSenderIdService();
      NewMessagingSenderId options = new NewMessagingSenderId
      {
        From = to,
        To = from,
        Text = "Hello, World!",
        WebhookUrl = dlrUri,
        UseProfileWebhooks = false,
        MediaUrls = mediaUrls
      };
      MessagingSenderId messageResponse = await service.CreateAsync(options);
      Console.WriteLine($"Sent message with ID: {messageResponse.Id}");
      return "";
    }
  }
}
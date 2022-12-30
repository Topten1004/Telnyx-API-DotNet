using System;
using Telnyx.net.Services.VerifyAPI;

namespace Verify
{
    class Program
    {
        private static string TELNYX_API_KEY = DotNetEnv.Env.GetString("TELNYX_API_KEY", "VarNotFound");
        private static string TELNYX_VERIFY_PROFILE_ID = DotNetEnv.Env.GetString("TELNYX_VERIFY_PROFILE_ID", "VarNotFound");
        static void Main(string[] args)
        {
            // DotNetEnv load and paste some of our variables, check if fetching correctly
            DotNetEnv.Env.Load();
            if (TELNYX_API_KEY == "VarNotFound" || TELNYX_VERIFY_PROFILE_ID == "VarNotFound")
            {
                Console.WriteLine("Variable not found, check your .env");
                Environment.Exit(-1);
            }   

            // Configure Telnyx API Key
            Telnyx.TelnyxConfiguration.SetApiKey(TELNYX_API_KEY);
            VerificationService verifyService = new VerificationService();

            GetAppInfo(); // Runs GetAppInfo, generic header ahoy

            Console.WriteLine("Phone Number (+E164 Format) to Verify?: ");
            string numberInput = Console.ReadLine();
            
            Console.WriteLine(numberInput);

            SendVerificationCode(numberInput); // Sends verification code to specified number

            CodeVerify(numberInput); // Submits verification code
        }

        static void PrintColorMessage(ConsoleColor color, string message){
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        // Telnyx Header (Arbitrary)
        static void GetAppInfo() {
            string appName = "Telnyx Console Verify-er";
            string appVersion = "1.0.0";

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("{0}: Version {1}", appName, appVersion);
            Console.ResetColor();
        }

        // Telnyx create verification request 
        static void SendVerificationCode(string numberInput) {
            VerificationService verifyService = new VerificationService();
            VerifyOptions verifyOptions = new VerifyOptions
            {
                PhoneNumber = numberInput,
                VerifyProfileId = Guid.Parse(TELNYX_VERIFY_PROFILE_ID),
                Type = "sms",
                TimeoutSecs = 300,
            };
            try
            {
                verifyService.CreateVerification(verifyOptions);
                PrintColorMessage(ConsoleColor.Green, "Verification code sent to " + numberInput);
            }
            catch (Exception e)
            {
                PrintColorMessage(ConsoleColor.Red, e.Message);
            }
        }
    
        static void CodeVerify (string phoneNumber){
            int attempts = 0;
            int maxAttempts = 5;
            VerificationService verifyService = new VerificationService();
            while(attempts < maxAttempts){
                //Get input, increment attempts
                Console.WriteLine("Verification code?: ");
                string inputVerify = Console.ReadLine();
                attempts++;
                try
                {
                    VerifyCodeOptions codeOptions = new VerifyCodeOptions
                    {
                        Code = inputVerify
                    };
                    var verification = verifyService.SubmitVerificationCode(phoneNumber, codeOptions);
                    if (verification.ResponseCode == "accepted")
                    {
                        PrintColorMessage(ConsoleColor.Green, "Success! Code Verified!");
                        break;
                    }
                    else
                    {
                        PrintColorMessage(ConsoleColor.Red, "Verification failed");
                        if (attempts >= maxAttempts){
                            PrintColorMessage(ConsoleColor.Red, "Verification max attempts reached");
                        }
                    };
                }
                catch (Exception e)
                {
                    PrintColorMessage(ConsoleColor.Red, "Error verifying code");
                    Console.WriteLine(e);
                }
            }
        }
    }
}

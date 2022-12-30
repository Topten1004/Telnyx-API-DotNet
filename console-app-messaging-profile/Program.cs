using System;
using Telnyx;
using dotenv.net;
using Newtonsoft.Json;
using Telnyx.net.Entities.Messaging.Messaging_Profiles;
using System.Collections.Generic;

namespace console_app_messaging_profile
{
  class Program
  {
    private const string webhookUrl = "";

    static void Main(string[] args)
    {
      DotEnv.Config();
      string TELNYX_API_KEY = System.Environment.GetEnvironmentVariable("TELNYX_API_KEY");
      TelnyxConfiguration.SetApiKey(TELNYX_API_KEY);
      UrlShortenerSettings urlShortenerSettings = new UrlShortenerSettings()
      {
        Domain = "aptrmdr.cc",
        ReplaceBlackListOnly = false,
        SendWebhooks = false
      };
      NewMessagingProfile createOptions = new NewMessagingProfile
      {
        Name = "Summer Campaign asdf",
        WebhookUrl = webhookUrl,
        UrlShortenerSettings = urlShortenerSettings
      };

      Console.WriteLine(JsonConvert.SerializeObject(createOptions)); ;
      MessagingProfileService service = new MessagingProfileService();
      MessagingProfile messagingProfile = service.Create(createOptions);
      Console.WriteLine(JsonConvert.SerializeObject(messagingProfile));
    }
  }
}

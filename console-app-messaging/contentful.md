Get up and running with our Client libraries and start developing your Telnyx integration.

Integrating Telnyx into your app or website can begin as soon as you create a Telnyx account, requiring only two steps:
1. Obtain your API key so Telnyx can authenticate your integration’s API requests
2. Install a Client library so your integration can interact with the Telnyx API

***

## Step 1: Obtain your API keys

Telnyx authenticates your API requests using your account’s API Key. If you do not include your key when making an API request, or use one that is incorrect or outdated, Telnyx returns an error.

Your API Keys are available in the ["Auth"](https://portal.telnyx.com/#/app/auth/v2) section of the Portal. Once you create an API Key you must save it for future use. You will only see your API Key once, on creation.

We include `YOUR_API_KEY` in our code examples. Replace this text with your own API Key.

If you cannot see your API keys in the Portal, this means you do not have access to them. Only Telnyx account owners can access API Keys.


## Step 2: Install a Client library

You'll need to ensure that you have DotNet installed on your computer. If DotNet (core or framework) isn’t installed, follow the [official installation instructions](https://dotnet.microsoft.com/download) for your operating system to install it. You can check this by running the following:

Telnyx targets the [.NET Standard 2.0](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md). You should be running a supported verison of dotnet/Xamarin/Mono/Framework to leverage the SDK.

```bash
dotnet --version
```

Install the [Telnyx.net SDK](https://github.com/team-telnyx/telnyx-dotnet) by running the following:

```bash
dotnet add package Telnyx.net
```

Check out the [dotnet/C# API docs](/docs/api/v2/overview?lang=net), see the source on [GitHub](https://github.com/team-telnyx/telnyx-dotnet), or see Package on [NuGet](https://www.nuget.org/packages/Telnyx.net/)

## Send Message Async Example

The C# SDK supports Task based API calls.

By updating the Main method to support async calls, we can build a quick example.

Demo below instanciates the Telnyx client and sends a "Hello, World!" message to the outbound phone number then prints the message ID.

Be sure to update the `From` & `To` numbers to your Telnyx number and destination phone number respectively.

```csharp
using System;
using Telnyx;
using System.Threading.Tasks;

namespace demo_dotnet_telnyx
{
    class Program
    {
        private static string TELNYX_API_KEY = System.Environment.GetEnvironmentVariable("TELNYX_API_KEY");
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TelnyxConfiguration.SetApiKey(TELNYX_API_KEY);
            MessagingSenderIdService service = new MessagingSenderIdService();
            NewMessagingSenderId options = new NewMessagingSenderId
            {
                From = "+19198675309", // alphanumeric sender id
                To = "+19198675310",
                Text = "Hello, World!"
            };
            MessagingSenderId messageResponse = await service.CreateAsync(options);
            Console.WriteLine(messageResponse.Id);
        }
    }
}
```

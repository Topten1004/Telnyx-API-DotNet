In this tutorial, you’ll learn how to send SMS and MMS messages using the Telnyx
v2 API
and the [Telnxy.net Library](https://github.com/team-telnyx/telnyx-dotnet).

---

## Portal Setup

Follow the [setup guide](/docs/v2/messaging/quickstarts/portal-setup) to
configure your Portal for sending and receiving messages.

## Development Environment Setup

Check out the [Development Environment
Setup](/docs/v2/messaging/quickstarts/dev-env-setup?lang=.net) guide to set up
the Telnyx.net SDK and your development environment for this guide.

You can paste the below snippets into your Terminal on Mac and Linux computers.
Windows users should install bash on Windows.

---

## Send an SMS

Follow the steps below to send SMS messages with dotnet core v3.1+

1. Using the dotnet CLI create a new console application and change directories to the newly created folder.  Then add the Telnyx.net package.

```bash
$ dotnet new console --output send-sms
$ cd send-sms
$ dotnet add package Telnyx.net
```


2. Open the `Program.cs` file created for you in the directory. It should look something like the code below:

#### Original Program.cs

```csharp
using System;

namespace send_sms
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}

```

3. Tell the application to use Telnyx.net by adding: `using Telnyx;` before the `namespace`

4. In order to use the Asynchronous methods, we need to modify our Main method to return a Task and make it async.

  4.1. `static void Main(string[] args)` should be `static async Task Main(string[] args)`.

  4.2. Add `using System.Threading.Tasks;` before the `namespace`.

5. Modify the Main method to look something like the following code:

> **Notes:**
> * Don’t forget to set `YOUR_API_KEY` as an environment variable (or set at runtime)
> * Make sure that the source (`from`) is eligible to send messages towards the destination (`to`) phone number. Read more about this [here](/docs/v2/messaging/features/traffic-type).
> * If sending an alphanumeric message, make sure to specify the `messaging_profile_id` parameter in the body of the request.

#### Updated Program.cs

```csharp
using System;
using Telnyx;
using System.Threading.Tasks;

namespace send_sms
{
  class Program
  {
    private static string TELNYX_API_KEY = System.Environment.GetEnvironmentVariable("TELNYX_API_KEY");
    private static string telnyxNumber = "+19194150467";
    private static string destinationNumber = "+19198675309";


    static async Task Main(string[] args)
    {
      Console.WriteLine("Hello World!");
      TelnyxConfiguration.SetApiKey(TELNYX_API_KEY);
      MessagingSenderIdService service = new MessagingSenderIdService();
      NewMessagingSenderId options = new NewMessagingSenderId
      {
        From = telnyxNumber,
        To = destinationNumber,
        Text = "Hello, World!"
      };
      try
      {
        MessagingSenderId messageResponse = await service.CreateAsync(options);
        Console.WriteLine(messageResponse.Id);

      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
    }
  }
}
```

#### Run the program

6. Finally, restore and run the application

```bash
$ dotnet restore
$ TELNYX_API_KEY="YOUR_API_KEY" dotnet run
$ TELNYX_API_KEY="YOUR_API_KEY" dotnet run
Hello World!
4031742c-d288-49f0-b4b9-809931dbd27c
```

> **Notes:**
> * Make sure you’re using the full +E.164 formatted number for your `to` and
>   `from` numbers.
>   In the US and Canada, this typically means adding +1 to the beginning of your
>   10-digit phone number.
> * The webhook URLs for this message are taken from the messaging profile.
>   If you want to override them, use the `webhook_url` and
>   `webhook_failover_url` request fields.
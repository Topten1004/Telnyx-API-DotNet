<div align="center">

# Telnyx.net Call Control Getting Started

![Telnyx](../logo-dark.png)

Sample application demonstrating Telnyx.net Gathering IVR Digits and Transferring a call

</div>

## Documentation & Tutorial

The full documentation and tutorial is available on [developers.telnyx.com](https://developers.telnyx.com/docs/v2/development/dev-env-setup?lang=dotnet&utm_source=referral&utm_medium=github_referral&utm_campaign=cross-site-link)

## Pre-Reqs

You will need to set up:

* [Telnyx Account](https://telnyx.com/sign-up?utm_source=referral&utm_medium=github_referral&utm_campaign=cross-site-link)
* [Telnyx Phone Number](https://portal.telnyx.com/#/app/numbers/my-numbers?utm_source=referral&utm_medium=github_referral&utm_campaign=cross-site-link) enabled with:
  * [Telnyx Call Control Application](https://portal.telnyx.com/#/app/call-control/applications?utm_source=referral&utm_medium=github_referral&utm_campaign=cross-site-link)
  * [Telnyx Outbound Voice Profile](https://portal.telnyx.com/#/app/outbound-profiles?utm_source=referral&utm_medium=github_referral&utm_campaign=cross-site-link)
* Ability to receive webhooks (with something like [ngrok](https://developers.telnyx.com/docs/v2/development/ngrok?utm_source=referral&utm_medium=github_referral&utm_campaign=cross-site-link))
* [DotNet Core](https://developers.telnyx.com/docs/v2/development/dev-env-setup?lang=net) installed

## What you can do

* Call the Telnyx Number and enter the digits to get transferred.

## Usage

The following environmental variables need to be set

| Variable               | Description                                                                                                                                              |
|:-----------------------|:---------------------------------------------------------------------------------------------------------------------------------------------------------|
| `TELNYX_API_KEY`       | Your [Telnyx API Key](https://portal.telnyx.com/#/app/api-keys?utm_source=referral&utm_medium=github_referral&utm_campaign=cross-site-link)              |
| `TELNYX_PUBLIC_KEY`    | Your [Telnyx Public Key](https://portal.telnyx.com/#/app/account/public-key?utm_source=referral&utm_medium=github_referral&utm_campaign=cross-site-link) |
| `PORT`      | **Defaults to `8000`** The port the app will be served                                                                                                   |

### .env file

This app uses the excellent [dotenv.net](https://github.com/bolorundurowb/dotenv.net) package to manage environment variables.

Make a copy of [`.env.sample`](./.env.sample) and save as `.env` and update the variables to match your creds.

```
TELNYX_API_KEY=
TELNYX_PUBLIC_KEY=
PORT=8000
```

### Callback URLs For Telnyx Applications

| Callback Type                    | URL                              |
|:---------------------------------|:---------------------------------|
| Inbound Calls Callback         | `{ngrok-url}/call-control/inbound`  |
| Outbound Calls Callback | `{ngrok-url}/call-control/outbound` |

### Install

Run the following commands to get started

```
$ git clone https://github.com/team-telnyx/demo-dotnet-telnyx.git
$ cd asp_net-IVR
```

### Ngrok

This application is served on the port defined in the runtime environment (or in the `.env` file). Be sure to launch [ngrok](https://developers.telnyx.com/docs/v2/development/ngrok?utm_source=referral&utm_medium=github_referral&utm_campaign=cross-site-link) for that port

```
./ngrok http 8000
```

> Terminal should look _something_ like

```
ngrok by @inconshreveable                                                                                                                               (Ctrl+C to quit)

Session Status                online
Account                       Little Bobby Tables (Plan: Free)
Version                       2.3.35
Region                        United States (us)
Web Interface                 http://127.0.0.1:4040
Forwarding                    http://your-url.ngrok.io -> http://localhost:8000
Forwarding                    https://your-url.ngrok.io -> http://localhost:8000

Connections                   ttl     opn     rt1     rt5     p50     p90
                              0       0       0.00    0.00    0.00    0.00
```

At this point you can point your [call-control application](https://portal.telnyx.com/#/app/call-control/applications) to generated ngrok URL + path  (Example: `http://{your-url}.ngrok.io/call-control/inbound`).

### Assign Phone Number to the Application

Update one of your [phone numbers](https://portal.telnyx.com/#/app/numbers/my-numbers) to point to the [call-control application](https://portal.telnyx.com/#/app/call-control/applications) you've updated with your ngrok URL.

### Run

Open your IDE and run the application within the IDE or using the dotnet CLI

#### CLI Commands

```bash
$ dotnet restore
$ dotnet run
```

#### Call your phone number

1. Call the phone number associated you with your call control application.
2. You'll be prompted to enter a 10 digit phone number then the app will transfer you to the phone number you entered. _Try `9198675309` if you need a demo number_
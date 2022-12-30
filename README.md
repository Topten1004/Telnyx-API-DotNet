<div align="center">

# Telnyx C# Getting Started

![Telnyx](logo-dark.png)

Sample application demonstrating C# SDK Basics

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
* [DotNet Core](https://developers.telnyx.com/docs/v2/development/dev-env-setup?lang=java&utm_source=referral&utm_medium=github_referral&utm_campaign=cross-site-link) installed

## What you can do

| Example                                        | Description                                                                                                         |
|:-----------------------------------------------|:--------------------------------------------------------------------------------------------------------------------|
| [Console App Messaging](console-app-messaging) | Quick example demonstrating how to send an SMS with Telnyx.net from the dotnet CLI: `dotnet new console`            |
| [ASP.NET Messaging](asp.net-messaging)         | Example working with inbound MMS & SMS messages, downloading media from inbound MMS, and uploading media to AWS S3. |

### Install

Run the following commands to get started

```
$ git clone https://github.com/d-telnyx/demo-dotnet-telnyx.git
```

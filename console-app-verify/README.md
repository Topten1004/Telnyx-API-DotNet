<div align="center">

# Telnyx C# Getting Started

![Telnyx](../logo-dark.png)

Sample application demonstrating C# SDK Messaging Profile Settings

</div>

## Documentation & Tutorial

The full documentation and tutorial is available on [developers.telnyx.com](https://developers.telnyx.com/docs/v2/development/dev-env-setup?lang=dotnet&utm_source=referral&utm_medium=github_referral&utm_campaign=cross-site-link)

## Pre-Reqs

You will need to set up:

* [Telnyx Account](https://telnyx.com/sign-up?utm_source=referral&utm_medium=github_referral&utm_campaign=cross-site-link)
* [Verifty Profile](https://portal.telnyx.com/#/app/verify/profiles)
* [DotNet Core](https://developers.telnyx.com/docs/v2/development/dev-env-setup?lang=java&utm_source=referral&utm_medium=github_referral&utm_campaign=cross-site-link) installed

## What you can do

* Input phone number to send verification code to
* Verify said number with verification code

## Usage

The following environmental variables need to be set

| Variable            | Description                                                                                                                                              |
|:--------------------|:---------------------------------------------------------------------------------------------------------------------------------------------------------|
| `TELNYX_API_KEY`    | Your [Telnyx API Key](https://portal.telnyx.com/#/app/api-keys?utm_source=referral&utm_medium=github_referral&utm_campaign=cross-site-link)              |
| `TELNYX_VERIFY_PROFILE_ID` | Your [Telnyx Verify Profile Key](https://portal.telnyx.com/#/app/verify/profiles) |

### .env file

This app uses the excellent [dotnet-env](https://github.com/tonerdo/dotnet-env) package to manage environment variables.

Make a copy of [`.env.sample`](./.env.sample) and save as `.env` and update the variables to match your creds.

```
TELNYX_API_KEY=
TELNYX_VERIFY_PROFILE_ID=
```

### Install

Run the following commands to get started

```
$ git clone https://github.com/d-telnyx/demo-dotnet-telnyx.git
```

### Run

Open your IDE and run the application

When the application is started:
* Enter your phone number to send the verification code.
* ⚠️ (Note: You must enter phone number in E.164 format (i.e. +12345678910) for the code to be sent correctly.)
* Receive the verification code
* Enter the code into the console app, you'll have 5 tries to get it right!
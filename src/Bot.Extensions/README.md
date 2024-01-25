---

title: Telegram Bot Extensions
lastmod: 2023-07-27T02:03:09:00.894Z
date: 2023-12-26T12:09:00:00.0000+05:00Z
license: MIT
keywords:
- telegram
- bot
- api
- bot-api-token
type: readme
slug: telegram-bot-extensions
description: This library provides a set of extensions for the `Telegram.Bot` library.  It primarily consists of the `BotApiToken` class right now, which parses a bot API token, and the `ValidateTelegramBotRequestAttribute`, which validates that a request is from Telegram, as well as several dependency injection extensions.
project: telegram
authors:
- dgmjr
version: 0.0.1
preview: /src/Telegram/src/Identity/Icon.png
--------------------------------------------

# Telegram Bot Extensions

This library provides a set of extensions for the `Telegram.Bot` library.  It primarily consists of the [`BotApiToken`](https://github.com/dgmjr-io/Telegram/blob/main/src/Bot.Extensions/Types/BotApiToken.cs) class right now, which parses a bot API token, and the [`ValidateTelegramBotRequestAttribute`](https://github.com/dgmjr-io/Telegram/blob/main/src/Bot.Extensions/Filters/ValidateTelegramBotRequestAttribute.cs) class, which validates that a request is from Telegram, as well as several dependency injection extensions.

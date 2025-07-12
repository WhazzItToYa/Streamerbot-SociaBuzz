# SociaBuzz Integration for Streamer.bot

This is a [Streamer.bot](https://streamer.bot) extension which integrates the [SociaBuzz](https://sociabuzz.com) monetization platform with Streamer.bot. It currently supports a trigger for receiving TRIBE donations.

## Installation and Setup

There are two different ways to integrate SociaBuzz into Streamer.bot: A Streamer.bot webhook, or Discord notification messages.

1. [Custom Streamer.bot Webhooks](Webhook.md): Uses Streamer.bot custom webhooks
    * Much simpler to set up than the Discord option.
    * Requires Streamer.bot 1.0.0 or newer.
    * Requires a subscription to one of Streamer.bot's Patreon support tiers
2. [Discord Notifications](Discord.md): Monitors for SociaBuzz notifications in your Discord server
    * More complicated Discord setup.
    * Can be used with Streamer.bot 0.2.8 or 1.0.0.

## Usage

### Donation Trigger

The extension adds a **Custom > SociaBuzz > Donation** trigger to Streamer.bot which fires every time SociaBuzz receives a donation and sends a message to your Discord.

The trigger sets the folowing arguments:

| name | description | example |
|------|-------------|----|
| `donationAmount` | The numeric amount of the donation | 3, 4.1 |
| `donationCurrency` | The currency code of the donation | "USD", "IDR" |
| `donationFrom` | The name appearing in the donation. Note: These are not Twitch usernames. | "Jessica" |

The extension contains an "Example SociaBuzz Donation" action which demonstrates uses the trigger to send the chat message.

### Changing the Discord message

SociaBuzz lets you customize the Discord message that it sends to your server. If you do change it, then you must go back to the extension's configuration page (see instructions above for how to open it) and update "SociaBuzz Discord Message" to match.

Note that the message must contain the `{amount}` and `{supporter}` placeholders, in order for the extension to parse the `donationAmount` and `donationFrom` arguments out of the messages.

## Notes/Caveats

**No Offline Donations**: This extension only monitors donations when Streamer.bot is running. Any donations that occur while Streamer.bot is closed, will still be delivered to your Discord server, but will not fire the donation trigger in Streamer.bot, even after Streamer.bot is restarted. Perhaps a future version will be able to catch up on missed donations.

## Acknowledgements

Thank you to [Mustached Maniac](https://mustachedmaniac.com/) for the awesome DiscoBot, and the changes he made specifically to support this extension.

Thanks to Nate for creating the Streamer.bot tool, and the community that supports it.

## Support, Contact

* For feature requests or bug reports: https://github.com/WhazzItToYa/Streamerbot-SociaBuzz/issues 
* Or submit a pull request
* Or if you can't do either of those things, ping me through the Streamer.bot Discord in the [SociaBuzz post](https://discord.com/channels/834650675224248362/1373459135663968327/1373459135663968327)



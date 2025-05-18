using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class CPHInline
{
    public void Init()
    {
        CPH.RegisterCustomTrigger("Donation", "SociaBuzzDonation", new string[]{"SociaBuzz"});
    }

    public bool DonationReceived()
    {
        // See if the message is from the sociaBuzz notification user, if supplied.
        string? userId = CPH.GetGlobalVar<string?>("sociaBuzzUserId", true);
        if (userId != null)
        {
            CPH.TryGetArg("discord.UserID", out string? messageUserId);
            if (userId != messageUserId) return false;
        }

        // Get the title of the sociaBuzz rich text embed.
        if (!CPH.TryGetArg("discord.Embed1.Title", out string title))
        {
            CPH.LogError("discord message does not contain discord.Embed1.Title as expected.  Aborting");
            return false;
        }

        // Use SocaiBuzz's own template string as the basis for the regexp to parse the discord string.
        string donationMsg = CPH.GetGlobalVar<string?>("sociaBuzzDonationMessage", true) ?? "Yay! {amount} from {supporter}";
        string donationPattern = donationMsg
            .Replace(@"\", @"\\")
            .Replace(".", @"\.")
            .Replace("*", @"\*")
            .Replace("+", @"\+")
            .Replace("[", @"\[")
            .Replace("]", @"\]")
            .Replace("(", @"\(")
            .Replace(")", @"\)")
            .Replace("|", @"\|")
            .Replace("?", @"\?")
            .Replace("^", @"\^")
            .Replace("$", @"\$")
            .Replace("{amount}", @"(?<currency>\D+)(?<amount>[\d\S]+)")
            .Replace("{supporter}", @"(?<supporter>.+)")
            .Replace("{", @"\{")
            .Replace("}", @"\}");

        var match = Regex.Match(title, donationPattern);
        if (!match.Success)
        {
            CPH.LogDebug($"""Discord Message "{title}" does not match template "{donationMsg}" (regex="{donationPattern}") """);
            return false;
        }
        
        // Extract the args to pass, and fire the trigger.
        var currency = match.Groups["currency"].Value;
        var amount = float.Parse(match.Groups["amount"].Value);
        var user = match.Groups["supporter"].Value;
        
        CPH.TriggerCodeEvent(
            "SocialBuzz Donation",
            new Dictionary<string, object> {
                {"donationAmount", amount},
                {"donationCurrency", currency},
                {"donationFrom", user}
            }
        );
        return true;
    }
}
    

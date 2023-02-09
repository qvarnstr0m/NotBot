//Discord bot with connection to Notion DB
//Scans for lectures for our class and sends message at 08:00 in Discord
//Martin Qvarnström SUT22, Campus Varberg

//VoiceNotification.cs, handles notifications when user enters the Lounge voice channel

using Discord.WebSocket;
using NotBot.Service;

namespace NotBot.Modules
{
    internal class VoiceNotification
    {
        private static DiscordSocketClient _client = DiscordClientSingleton.Instance;
        internal static async Task UserVoiceStateUpdatedAsync()
        {
            ulong guild = Convert.ToUInt64(ConfigSingleton.Instance.GetSection("Adresses")["DiscordGuild"]);
            ulong channel = Convert.ToUInt64(ConfigSingleton.Instance.GetSection("Adresses")["DiscordMainChannel"]);

            _client.UserVoiceStateUpdated += async (user, from, to) =>
            {
                if (from.VoiceChannel == null && to.VoiceChannel?.Name == "Lounge" && to.VoiceChannel.Guild.Id == guild)
                {
                    await _client.GetGuild(guild).GetTextChannel(channel).
                        SendMessageAsync($"{user.Username} har smygit in i loungen, in och tjöta 📣📣📣");
                }
                
            };

            await Task.CompletedTask;
        }
    }
}

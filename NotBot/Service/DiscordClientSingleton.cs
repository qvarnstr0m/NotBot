//Discord bot with connection to Notion DB
//Scans for lectures for our class and sends message at 08:00 in Discord
//Martin Qvarnström SUT22, Campus Varberg

//DiscordClientSingleton.cs, to make sure only one client is instantiated

using Discord.WebSocket;

namespace NotBot.Service
{
    internal class DiscordClientSingleton
    {
        private static DiscordSocketClient _instance;
        private static readonly object _lock = new object();

        private DiscordClientSingleton() { }

        public static DiscordSocketClient Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DiscordSocketClient();
                    }
                    return _instance;
                }
            }
        }
    }
}

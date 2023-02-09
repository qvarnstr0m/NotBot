//Discord bot with connection to Notion DB
//Scans for lectures for our class and sends message at 08:00 in Discord
//Martin Qvarnström SUT22, Campus Varberg

//Bot.cs with RunBotAsync method, connects to Discord and starts asyncronous tasks

using Discord.WebSocket;
using Discord;
using NotBot.Modules;
using NotBot.Service;

namespace NotBot
{
    internal class Bot
    {
        public async Task RunBotAsync()
        {
            //Set up client and config
            DiscordSocketClient client = DiscordClientSingleton.Instance;

            var config = new DiscordSocketConfig()
            {
                GatewayIntents = GatewayIntents.All
            };

            //Get token from json file
            string discordToken = ConfigSingleton.Instance.GetSection("Secrets")["DiscordBotToken"];

            client.Log += Modules.ConsoleLog._client_Log; //Handles event logs to console
            client.MessageReceived += MessageHandler.HandleMessageAsync; //Handles message I/O

            //Login bot to Discord
            await client.LoginAsync(TokenType.Bot, discordToken);
            await client.StartAsync();

            await VoiceNotification.UserVoiceStateUpdatedAsync(); //Handles notification on voicechannel join
            await Modules.Timer.TimerInterval(); //Handles the timer for the scan for new events in Notion DB

            await Task.Delay(-1); //Task waits indefinitely
            //Bot is online
        }
    }
}

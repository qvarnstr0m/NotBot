//Discord bot with connection to Notion DB
//Scans for lectures for our class and sends message at 08:00
//Martin Qvarnström SUT22, Campus Varberg

//Program.cs class with Main method to handle runtime

using Discord.Commands;
using Discord.WebSocket;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace NotBot
{
    internal class Program
    {
        //String to hold return message from NotionLogic.ScanNotionDB()
        string returnMessage = "";

        public ulong discordServer; //Assign with server id
        public ulong discordChannel; //Assign in with channel id

        //Main method to run client
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        //Fields to handle client connections and commands
        private DiscordSocketClient _client;
        public CommandService _commands;
        private IServiceProvider _services;
        private SocketGuild guild;

        //Get token through .txt file and StreamReader
        StreamReader readToken = new StreamReader("");

        //Log channel info
        private ulong LogChannelID;
        private SocketTextChannel LogChannel;

        //Connect bot
        public async Task RunBotAsync()
        {
            var config = new DiscordSocketConfig()
            {
                GatewayIntents = GatewayIntents.All
            };

            _client = new DiscordSocketClient(config);
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            string token = readToken.ReadToEnd();

            _client.Log += _client_Log;

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, token);

            await _client.StartAsync();

            await TimerInterval();

            await Task.Delay(-1);

            //Bot is online
        }

        //Timer to check if it is time to scan html in string fullUrl
        private async Task TimerInterval()
        {
            //Set timespan to 10 minutes and create timer object w. param of timespan
            TimeSpan time = new TimeSpan(0, 10, 0);
            PeriodicTimer timer = new PeriodicTimer(time);

            //New timespan to hold logic after first run at hour 08
            TimeSpan delayOneHour = new TimeSpan(1, 0, 0);

            while (await timer.WaitForNextTickAsync())
            {
                //Only run at hour 08
                if (ReturnSwedishTime().Hour == 8)
                {
                    timer = new PeriodicTimer(delayOneHour); //When ran hold logic one hour to prevent multiple messages at hour 08
                    returnMessage = await NotionLogic.ScanNotionDB();
                }
                else if (ReturnSwedishTime().Hour == 9)
                {
                    timer = new PeriodicTimer(time); //Set timer back to normal after hour 08
                }

                if (returnMessage.Length > 0)
                {
                    await _client.GetGuild(discordServer).GetTextChannel(discordChannel).
                        SendMessageAsync(returnMessage);
                    returnMessage = "";
                }
            }
        }

        //Method to return local swedish time, daylight savings time adjusted up to year 2024
        private static DateTime ReturnSwedishTime()
        {
            DateTime nowUTC = DateTime.UtcNow;
            TimeSpan oneHour = new TimeSpan(1, 0, 0);
            DateTime nowCET = nowUTC.Add(oneHour);

            DateTime startSummerTime2022 = new DateTime(2022, 03, 27);
            DateTime endSummerTime2022 = new DateTime(2022, 10, 30);
            DateTime startSummerTime2023 = new DateTime(2023, 03, 26);
            DateTime endSummerTime2023 = new DateTime(2023, 10, 29);
            DateTime startSummerTime2024 = new DateTime(2024, 03, 31);
            DateTime endSummerTime2024 = new DateTime(2024, 10, 27);

            if (nowCET >= startSummerTime2022 && nowCET <= endSummerTime2022 || nowCET >= startSummerTime2023 && nowCET <= endSummerTime2023 ||
                nowCET >= startSummerTime2024 && nowCET <= endSummerTime2024)
            {
                nowCET = nowCET.Add(oneHour);
            }

            return nowCET;
        }

        //Log events to console
        private Task _client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        //Register Commands Async
        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        //Input and output logic
        public async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);
            var channel = _client.GetChannel(LogChannelID) as SocketTextChannel;

            //Log messages to console
            Console.WriteLine($"User {message.Author.Username} ({message.Author.Id}) wrote:\n{message.ToString()}");

            //Make client ignore own output
            if (message.Author.IsBot) return;

            int argPos = 0;

            //Set / char to execute commands in Commands class
            if (message.HasStringPrefix("/", ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
                if (result.Error.Equals(CommandError.UnmetPrecondition)) await message.Channel.SendMessageAsync(result.ErrorReason);
            }

            //Make input text lowercase
            var text = message.ToString().ToLower();

            //Respond to certain words and phrases
            switch (text)
            {
                case "hej bot":
                    await message.Channel.SendMessageAsync("Hej " + message.Author.Username + "!");
                    break;
                case "blipp":
                    await message.Channel.SendMessageAsync("Blopp!");
                    break;
            }

            if (text.Contains("diesel") || text.Contains("bensin"))
            {
                await message.Channel.SendMessageAsync("Don't mention the bränslepriser...");
            }
        }
    }
}
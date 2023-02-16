//Discord bot with connection to Notion DB
//Scans for lectures for our class and sends message at 08:00 in Discord
//Martin Qvarnström SUT22, Campus Varberg

//MessageHandler.cs, handles the I/O text messages logic

using Discord.WebSocket;

namespace NotBot.Modules
{
    internal class MessageHandler
    {
        internal static async Task HandleMessageAsync(SocketMessage message)
        {
            if (message.Author.IsBot) return;

            //This resolves the "gets empty strings except from when from itself" cases
            var fullMessage = await message.Channel.GetMessageAsync(message.Id);

            //Check if message is command
            if (fullMessage.ToString()[0] == '!')
            {
                await Commands.ExecuteCommands(fullMessage);
                return;
            }

            //Log messages to console
            Console.WriteLine($"User {fullMessage.Author.Username} ({fullMessage.Author.Id}) wrote:\n{fullMessage.ToString()}");

            //Respond to various words and phrases
            switch (fullMessage.ToString().ToLower())
            {
                case "hej bot":
                    await message.Channel.SendMessageAsync($"Hej {message.Author.Username}!");
                    break;
                case "blipp":
                    await message.Channel.SendMessageAsync("Blopp!");
                    break;
            }

            if (fullMessage.ToString().ToLower().Contains("diesel") || fullMessage.ToString().ToLower().Contains("bensin"))
            {
                await message.Channel.SendMessageAsync("Don't mention the bränslepriser...");
            }
        }
    }
}

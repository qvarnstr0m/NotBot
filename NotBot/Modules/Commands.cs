//Discord bot with connection to Notion DB
//Scans for lectures for our class and sends message at 08:00 in Discord
//Martin Qvarnström SUT22, Campus Varberg

//Commands.cs class to handle custom /commands

using Discord.Commands;

namespace NotBot.Modules
{
    public class Commands
    {
        public static async Task ExecuteCommands(string command)
        {
            //Not yet implemented
            Console.WriteLine($"the command: {command}");
            await Task.CompletedTask;
        }
    }
}
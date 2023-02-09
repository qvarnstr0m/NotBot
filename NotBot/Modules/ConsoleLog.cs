//Discord bot with connection to Notion DB
//Scans for lectures for our class and sends message at 08:00 in Discord
//Martin Qvarnström SUT22, Campus Varberg

//ConsoleLog.cs, log events to console

using Discord;

namespace NotBot.Modules
{
    internal class ConsoleLog
    {
        //Log events to console
        internal static Task _client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }
    }
}

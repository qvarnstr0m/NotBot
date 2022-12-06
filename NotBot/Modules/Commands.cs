//Discord bot with connection to Notion DB
//Scans for lectures for our class and sends message at 08:00
//Martin Qvarnström SUT22, Campus Varberg

//Commands.cs class to handle custom /commands

using Discord.Commands;

namespace NotBot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("testcommand")]
        public async Task Comandi()
        {
            await ReplyAsync("This a /testcommand");
        }
    }
}
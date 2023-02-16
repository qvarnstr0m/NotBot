//Discord bot with connection to Notion DB
//Scans for lectures for our class and sends message at 08:00 in Discord
//Martin Qvarnström SUT22, Campus Varberg

//Commands.cs class to handle custom /commands

using NotBot.Service;

namespace NotBot.Modules
{
    public class Commands
    {
        public static async Task ExecuteCommands(Discord.IMessage message)
        {
            string[] commandArray = message.ToString().Split(' ');

            switch (commandArray[0])
            {
                case "!trello":
                    {
                        await message.Channel.SendMessageAsync($"Go to {ConfigSingleton.Instance.GetSection("Adresses")["TrelloURL"]} " +
                            $"to see the kanban board. To add a suggestion use the command !addcard, followed by a space and your suggestion");
                        break;
                    }

                case "!addcard":
                    {
                        if (commandArray.Length > 1)
                        {
                            await Trello.AddTrelloCardAsync(message);
                        }
                        else
                        {
                            await message.Channel.SendMessageAsync("You need to add a suggestion to this command!");
                        }
                        break;
                    }

                default:
                    {
                        await message.Channel.SendMessageAsync($"Not a valid command, beep. 🤖");
                        break;
                    }
            }
        }
    }
}
using Manatee.Trello;
using NotBot.Service;

namespace NotBot.Modules
{
    internal class Trello
    {
        internal static async Task AddTrelloCardAsync(Discord.IMessage message)
        {
            // Get api keys and tokens from json file
            TrelloAuthorization.Default.AppKey = ConfigSingleton.Instance.GetSection("Secrets")["TrelloApiKey"];
            TrelloAuthorization.Default.UserToken = ConfigSingleton.Instance.GetSection("Secrets")["TrelloUserToken"];
            var board = new Board(ConfigSingleton.Instance.GetSection("Secrets")["TrelloBoardId"]);

            var lists = board.Lists;
            await lists.Refresh();

            var backlog = board.Lists.FirstOrDefault(l => l.Name == ConfigSingleton.Instance.GetSection("Adresses")["TrelloColumn"]);
            var newCard = await backlog.Cards.Add(message.ToString().Substring(9));



            if (newCard.ToString().Length > 0)
            {
                await message.Channel.SendMessageAsync("Card added!");
                return;
            }

            await message.Channel.SendMessageAsync("Something went wrong, try again?");
        }
    }
}

# NotBot 🤖  

## A Discord bot that connects to Notion  

Developed with .NET 6.0 C# 10.0  
Packages: Discord.NET, Microsoft.Extentions.DependencyInjection, Notion.NET  
  
Screenshot:  
<img width="453" alt="NotBotScreenshot" src="https://user-images.githubusercontent.com/70780322/212863354-6bfc24c4-b5b8-485b-bfc9-be71b2bc1102.png">

### Why    
The main feature for this bot is the connection to Notion. Most of our(SUT22, Campus Varberg) course is handled through Notion, including information on lessons. Our internal communication is mostly on our Discord server, so I connected the two with this bot. It scans the Notion classes DB for events once a day and sends a message with the relevant parsed data.  
  
The bot also has some easter egg type functions in that it responds to certain words and phrases. More features may be added in the future, feel free to <a href ="#contribute">contribute</a>!

### How  
#### Program.cs  
The app runs through the Main method where it sets up a client that connects to the Discord server with an asyncronous task.  
This class also holds the async timer task that checks the Notion DB once a day, set to 08:00.  
Program.cs also hold the logic for logging data and responding to user messages.  
Tokens to access Discord bot and Notion DB are placed in .txt files to avoid giving out sensetive data.  
  
#### NotionLogic.cs  
This class holds the logic that sets up a connection to the Notion DB, scans for relevant data and parses it to a readable message to send to the Discord channel, which is done in TimerInterval async task in Program.cs

#### Modules/Commands.cs
This class handles the logic for / messages
  
### Installation & running  
Discord bots are initially set up in the Discord developer portal: https://discord.com/developers/applications and connected to the application with the token that are generated there.  
To get the bot running deploy the app with a host that supports .NET applications, for example https://serverstarter.host/  
  
For a more thorough guide to setting up a Discord bot with the Discord.NET package visit https://discordnet.dev/ or https://medium.com/medialesson/how-to-write-your-own-discord-bot-on-net-6-ac96e40467b8  
  
### Contribute  
Please feel free to contribute with new features and improvments:
1. Clone repo and create a new branch in terminal: $ git checkout https://github.com/qvarnstr0m/NotBot -b name_for_new_branch.  
2. Make changes  
3. Submit Pull Request with description of changes  

## NotBot 🤖  
  
<img width="453" alt="NotBotScreenshot" src="https://user-images.githubusercontent.com/70780322/212863354-6bfc24c4-b5b8-485b-bfc9-be71b2bc1102.png">  
  
### Table of contents  
+ Introduction  
+ Features  
+ Tech stack  
+ Contribute  
+ Run locally  
+ Run on server  
  
### Introduction  
  The first idea for this bot was to scrape a webpage for lectures for our class and notify us in Discord every morning on time, place and subject. However that webpage wasn't always very up to date, so I had to give up that idea. I did learn web scraping in .NET though. Most of the info for our class courses and lectures are in published in Notion, and when I got access to that DB the idea worked much better, each morning the bot looks for lectures and notifies us. Other features have been added, like notifications on when a voice channel is joined and responding to certain words and phrases.  
    
📌 The bot has it's [own Trello kanban board](https://trello.com/b/4FoiqnSs/notbotboard) with features to add cards in suggestion column in Discord via !addcard command. 📌
    
### Features    
  + Notification on lectures each morning
  + Own Trello Kanban board w. features to add cards through Discord
  + Alert on when the Lounge voice channel is joined
  + Responding to certain words and phrases
    
### Tech stack  
  + .NET 6.0
  + C# 10.0
  + Discord.NET
  + Microsoft.Extensions.Configuration
  + Microsoft.Extensions.Configuration.Json
  + Microsoft.Extensions.DependencyInjection
  + Notion.NET
  + Manatee.Trello

### Contribute   
  
  Contributions of any kind are welcome!   
    
  Make sure you have [dotnet installed](https://dotnet.microsoft.com/en-us/download).  
    
Clone the project on a CLI  
```git clone https://github.com/qvarnstr0m/NotBot/```  

  Install all the Nuget packages listed in the Tech Stack  
  
### Run on a server  
This bot needs to run in an enviroment that can handle .NET 6.0 apps, I have yet to find a good free one and this one runs on [serverstarter.host](https://serverstarter.host/) for a couple of $ a month. Feel free to contribute and add a list of decent bot hosting servers, free or not.

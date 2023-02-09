//Discord bot with connection to Notion DB
//Scans for lectures for our class and sends message at 08:00 in Discord
//Martin Qvarnström SUT22, Campus Varberg

//Program.cs with Main method, entry point of application

using Discord.Commands;
using Discord.WebSocket;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using NotBot.Service;

namespace NotBot
{
    internal class Program
    {
        static void Main(string[] args) => new Bot().RunBotAsync().GetAwaiter().GetResult();
    }
}
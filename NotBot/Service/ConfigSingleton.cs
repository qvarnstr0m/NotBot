//Discord bot with connection to Notion DB
//Scans for lectures for our class and sends message at 08:00 in Discord
//Martin Qvarnström SUT22, Campus Varberg

//ConfigSingleton.cs, to make sure only one json config is instantiated

using Microsoft.Extensions.Configuration;

namespace NotBot.Service
{
    internal class ConfigSingleton
    {
        private static readonly Lazy<IConfigurationRoot> _lazyInstance =
            new Lazy<IConfigurationRoot>(() =>
            {
                return new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json") // ..\Debug\net6.0\ folder
                    .Build();
            });

        public static IConfigurationRoot Instance => _lazyInstance.Value;
    }
}

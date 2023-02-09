//Discord bot with connection to Notion DB
//Scans for lectures for our class and sends message at 08:00 in Discord
//Martin Qvarnström SUT22, Campus Varberg

//Timer.cs, handles the periodic retrival of events in Notion DB

using NotBot.Service;

namespace NotBot.Modules
{
    internal class Timer
    {
        //String to hold return message from NotionLogic.ScanNotionDB()
        public static string returnMessage = "";

        //Timer to check if it is time to check Notion DB for lecture
        internal static async Task TimerInterval()
        {
            //Set timespan to 10 minutes and create timer object w. param of timespan
            TimeSpan time = new TimeSpan(0, 15, 0);
            PeriodicTimer timer = new PeriodicTimer(time);

            //New timespan to hold logic after first run at hour 08
            TimeSpan delayOneHour = new TimeSpan(1, 0, 0);

            while (await timer.WaitForNextTickAsync())
            {
                //Only run at hour 08
                if (ReturnSwedishTime().Hour == 8)
                {
                    timer = new PeriodicTimer(delayOneHour); //When ran hold logic one hour to prevent multiple messages at hour 08
                    returnMessage = await Notion.ScanNotionDB();
                }

                else if (ReturnSwedishTime().Hour == 9)
                {
                    timer = new PeriodicTimer(time); //Set timer back to normal after hour 08
                }

                if (returnMessage.Length > 0)
                {
                    //Get Guild and main channel
                    ulong guild = Convert.ToUInt64(ConfigSingleton.Instance.GetSection("Adresses")["DiscordGuild"]);
                    ulong channel = Convert.ToUInt64(ConfigSingleton.Instance.GetSection("Adresses")["DiscordMainChannel"]);

                    await DiscordClientSingleton.Instance.GetGuild(guild).GetTextChannel(channel).
                        SendMessageAsync(returnMessage);
                    returnMessage = "";
                }
            }
        }

        //Method to return local swedish time, daylight savings time adjusted up to year 2024
        //Bit of a homemade solution, but couldnt find anything better
        private static DateTime ReturnSwedishTime()
        {
            DateTime nowUTC = DateTime.UtcNow;
            TimeSpan oneHour = new TimeSpan(1, 0, 0);
            DateTime nowCET = nowUTC.Add(oneHour);

            DateTime startSummerTime2022 = new DateTime(2022, 03, 27);
            DateTime endSummerTime2022 = new DateTime(2022, 10, 30);
            DateTime startSummerTime2023 = new DateTime(2023, 03, 26);
            DateTime endSummerTime2023 = new DateTime(2023, 10, 29);
            DateTime startSummerTime2024 = new DateTime(2024, 03, 31);
            DateTime endSummerTime2024 = new DateTime(2024, 10, 27);

            if (nowCET >= startSummerTime2022 && nowCET <= endSummerTime2022 || nowCET >= startSummerTime2023 && nowCET <= endSummerTime2023 ||
                nowCET >= startSummerTime2024 && nowCET <= endSummerTime2024)
            {
                nowCET = nowCET.Add(oneHour);
            }

            return nowCET;
        }
    }
}

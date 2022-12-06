//Discord bot with connection to Notion DB
//Scans for lectures for our class and sends message at 08:00
//Martin Qvarnström SUT22, Campus Varberg

//NotionLogic.cs to handle connection and retrival of data from Notion DB

using Notion.Client;
using System.Text;

namespace NotBot
{
    internal static class NotionLogic
    {
        internal static async Task<string> ScanNotionDB()
        {
            //Get token and DB through .txt files and StreamReader
            StreamReader readToken = new StreamReader("");
            StreamReader readDB = new StreamReader("");

            //String to hold return message
            string returnMessage = "";

            //Set up client with token reference to a Notion DB
            var client = NotionClientFactory.Create(new ClientOptions
            {
                AuthToken = readToken.ReadToEnd()
            });

            //Set up filter and to query to DB with client to get return of pages
            var dateFilter = new DateFilter("Datum", onOrAfter: DateTime.Today);
            var queryParams = new DatabasesQueryParameters { Filter = dateFilter };
            var pages = await client.Databases.QueryAsync(readDB.ReadToEnd(), queryParams);

            //Loop through all pages
            foreach (var result in pages.Results)
            {
                try
                {
                    //Get date from event page and only use those with todays date
                    string eventDate = GetValue(result.Properties["Datum"]).ToString().Substring(0, 10);
                    string todaysDate = DateTime.Today.ToString().Substring(0, 10);
                    var todaysActivity = GetValue(result.Properties["Aktivitet"]);

                    //Different output depending on if activity is regular class or deadline
                    if (eventDate == todaysDate && !todaysActivity.ToString().ToLower().Contains("deadline"))
                    {
                        returnMessage = $"God Morgon SUT22! Idag har vi: {GetValue(result.Properties["Aktivitet"])} med {GetValue(result.Properties["Lärare"])}. " +
                            $"Tider: {GetValue(result.Properties["Tider"])}. Dagens innehåll är {GetValue(result.Properties["Innehåll"])}";
                        break;
                    }
                    else if (eventDate == todaysDate)
                    {
                        returnMessage = $"Idag har vi: {GetValue(result.Properties["Aktivitet"])} på en labb, {result.Url}";
                        break;
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }

                //Leave below in for future debugging

                //foreach (var property in result.Properties)
                //{
                //    Console.WriteLine($"{property.Key} {GetValue(property.Value)}");
                //}
                //Console.WriteLine();
            }

            //Method to return correct type of data in properties
            object GetValue(PropertyValue p)
            {
                try
                {
                    switch (p)
                    {
                        case RichTextPropertyValue richTextPropertyValue:
                            return richTextPropertyValue.RichText.FirstOrDefault()?.PlainText;

                        case DatePropertyValue datePropertyValue:
                            return datePropertyValue.Date.Start;

                        case MultiSelectPropertyValue multiSelectPropertyValue:
                            var teacherList = multiSelectPropertyValue.MultiSelect.ToList();
                            StringBuilder teacherResult = new StringBuilder();
                            foreach (var item in teacherList)
                            {
                                if (teacherList.Count > 1)
                                {
                                    teacherResult.Append(item.Name + " ");
                                }
                                else
                                {
                                    teacherResult.Append(item.Name);
                                }

                            }
                            string teacherResultString = teacherResult.ToString().TrimEnd();
                            return teacherResultString.Replace(" ", " och ");

                        case TitlePropertyValue titlePropertyValue:
                            return titlePropertyValue.Title[0].PlainText;

                        default:
                            return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            return returnMessage;
        }
    }
}

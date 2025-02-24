using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webScrapping.Models;
using static webScrapping.Program;
using webScrapping.context;

namespace webScrapping.auto
{
    public class Auto
    {
        public static async void RunWebScrapping(object state)
        {
            bool success = false;
            while (!success)
            {
                try
                {
                    Console.WriteLine($"Start {DateTime.Now}");
                    Match match = new Match();
                    var broker = new StorageBroker();
                    var matchList = match.MatchListGenerate();
                    await match.ShowMatchs(matchList);
                    await broker.SaveOnDb(matchList, broker);
                    Console.WriteLine($"Finish {DateTime.Now}");
                    Console.WriteLine("Press a key to close the system...");
                    Console.ReadKey();
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error : {ex.Message}");
                    await Task.Delay(TimeSpan.FromSeconds(2));


                }
            }
        }
    }
}

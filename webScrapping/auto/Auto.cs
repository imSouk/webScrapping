using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webScrapping.Models;
using static webScrapping.Program;
using webScrapping.context;
using webScrapping.services;

namespace webScrapping.auto
{
    public class Auto
    {
        private readonly MatchServices _matchServices;
        public Auto(MatchServices matchServices ) 
        {
            _matchServices = matchServices;
        }
        public Auto()
        {
            
        }
        public  async void RunWebScrapping(object state)
        {
            bool success = false;
            while (!success)
            {
                try
                {
                    Console.WriteLine($"Start {DateTime.Now}");
                    Match match = new Match();
                   
                    var matchList = match.MatchListGenerate();
                    await match.ShowMatchs(matchList);
                    await _matchServices.SaveOnDb(matchList);
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

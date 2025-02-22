using System;
using System.Text.Json.Nodes;
using Azure.Core;
using HtmlAgilityPack;
using webScrapping.Models;

namespace webScrapping
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            
            Match match = new Match();
            var matchList = match.MatchListGenerate();
            await match.ShowMatchs(matchList);
            await match.FormatDetails(matchList[0]);
            //var link = "r/9553441/Tomasz-Lojtek-vs-Mariusz-Koczyba";
            //string baseURl = $"https://pt.betsapi.com/";
            //string searchMatch ="r/9553441/Tomasz-Lojtek-vs-Mariusz-Koczyba";
            //string matchResult = baseURl  + searchMatch;
            //Console.WriteLine(matchResult);
            Console.ReadKey();
        }
        
       


    }
}

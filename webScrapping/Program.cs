using System;
using System.Text.Json.Nodes;
using Azure.Core;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webScrapping.Models;
using webScrapping.auto;
using webScrapping.context;

namespace webScrapping
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var auto = new Auto();
            var timer = new Timer(Auto.RunWebScrapping, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));         
            Console.ReadKey();
        }

        internal class StorageBroker1 : StorageBroker
        {
        }
    }
}

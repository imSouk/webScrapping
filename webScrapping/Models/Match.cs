using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace webScrapping.Models
{
    public class Match 
    {
        public string Date { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public string LinkP1 { get; set; }
        public string LinkP2 { get; set; }
        public string LinkMatch { get; set; }
        public int ScoreP1 { get; set; }
        public int ScoreP2 { get; set; }
        public Match()
        {

        }
        public Match(string date, string player1, string player2, int scoreP1, int scoreP2, string linkP1, string linkP2, string linkMatch)
        {
            Date = date;
            Player1 = player1;
            Player2 = player2;
            LinkP1 = linkP1;
            LinkP2 = linkP2;
            LinkMatch = linkMatch;
            ScoreP1 = scoreP1;
            ScoreP2 = scoreP2;
        }
        public async Task<HtmlAgilityPack.HtmlNodeCollection> RequestMatchs()
        {
            HttpClient httpClient = new HttpClient();
            string url = $"https://pt.betsapi.com/le/29128/TT-Elite-Series";
            string html = await httpClient.GetStringAsync(url);
            
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            string xpath = "//table[@class ='table table-sm']//tbody//tr";

            HtmlNodeCollection linhas = htmlDocument.DocumentNode.SelectNodes(xpath);



            if (linhas == null)
            {
                return null;
            }
            else
            {

                return linhas;
            }


        }
        public async Task<List<Match>> FormatAndCreateList(List<Match> lista) 
        
        {
            var partida = new Match();
            var result = await partida.RequestMatchs();

            foreach (var match in result)
            {
                var tableRow = match.Descendants("td").ToList().Where(e => !e.InnerText.Equals("-")).ToList();
                var data = tableRow[0].InnerText;
                var playerHome = tableRow[1].InnerText.Split('\n')[1].Trim().Split(" v ")[0];
                var playerAway = tableRow[1].InnerText.Split('\n')[1].Trim().Split(" v ")[1];
                var linkP1 = tableRow[1].InnerHtml.Replace("\n", "").Split(">")[0].TrimStart().Replace("<a href=", "");
                var linkP2 = tableRow[1].InnerHtml.Replace("\n", "").Split(">")[2].TrimStart().Replace("<a href=", "").Replace("v", "");
                var linkMatch = tableRow[2].InnerHtml.Replace("\n", "").TrimStart().Split(">")[0].Replace("<a href=", "");
                var score = tableRow[2].InnerText.Replace("\n", "").TrimStart().TrimEnd().Split("-");
                var scoreP1 = Convert.ToInt32(score[0]);
                var scoreP2 = Convert.ToInt32(score[1]);
                var novaMatch = new Match(data, playerHome, playerAway, scoreP1, scoreP2,linkP1,linkP2, linkMatch);
                lista.Add(novaMatch);

            }
            return lista; 

        }
        public async Task ConsoleLogPartidas(List<Match> lista) 
        
        {
            int i = 1;
            foreach (var match in lista)
            {
                
                Console.WriteLine("PARTIDA:" + i);
                Console.WriteLine(match.Date);
                Console.WriteLine(match.Player1 + " v " + match.Player2);
                Console.WriteLine("Resultado:" + match.ScoreP1 + "-" + match.ScoreP2);
                Console.Write("\n\n");
                i++;
            }
            await Task.CompletedTask;

        }
        public async Task ShowMatchs() 
        
        {

           var matchList = new List<Match>();
           await FormatAndCreateList(matchList);
           await ConsoleLogPartidas(matchList);
        
        }

        public async Task<HtmlAgilityPack.HtmlNodeCollection> GetMatchDetails(string linkMatch) 
        {

            HttpClient httpClient = new HttpClient();
            string baseURl = $"https://pt.betsapi.com/";
            string matchURL = baseURl + linkMatch;
            string html = await httpClient.GetStringAsync(matchURL);

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
        
            string xpath = "//table[@class ='table']//tr";
            HtmlNodeCollection jogadorDetails = htmlDocument.DocumentNode.SelectNodes(xpath);

            if (jogadorDetails == null)
            {
                return null;
            }
            else
            {

                return jogadorDetails;
            }

        }
        public async Task<> FormatDetails() 
        { 
        
        }
    }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
        public int SetScoreP1 { get; set; }
        public int SetScoreP2 { get; set; }
        public int P1Id  { get; set; }
        public int P2Id  { get; set; }
        public string P1Name { get; set; }
        public string P2Name { get; set; }
        public Match()
        {

        }
        public Match(string date, string player1, string player2, int scoreP1, int scoreP2, string linkP1, string linkP2, string linkMatch, int p1Id,int p2Id,string p1Name,string p2Name)
        {
            Date = date;
            Player1 = player1;
            Player2 = player2;
            P1Id = p1Id;
            P2Id = p2Id;
            P1Name = p1Name;
            P2Name = p2Name;
            LinkP1 = linkP1;
            LinkP2 = linkP2;
            LinkMatch = linkMatch;
            SetScoreP1 = scoreP1;
            SetScoreP2 = scoreP2;
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
                var p1Id = Convert.ToInt32(tableRow[1].InnerHtml.TrimStart().Split("/")[2]);
                var p2Id = Convert.ToInt32(tableRow[1].InnerHtml.TrimStart().Split("/")[6]);
                var p1Name = tableRow[1].InnerHtml.TrimStart().Split("/")[3].Split("\"")[0];
                var p2Name = tableRow[1].InnerHtml.TrimStart().Split("/")[7].Split("\"")[0];
                var linkP1 = "/t/"+ tableRow[1].InnerHtml.TrimStart().Split("/")[2] + "/" + tableRow[1].InnerHtml.TrimStart().Split("/")[3].Split("\"")[0];
                var linkP2 = "/t/" + tableRow[1].InnerHtml.TrimStart().Split("/")[6] + "/" + tableRow[1].InnerHtml.TrimStart().Split("/")[7].Split("\"")[0];
                var linkMatch = tableRow[2].InnerHtml.Replace("\n", "").TrimStart().Split(">")[0].Replace("<a href=", "").Replace("\"", "");
                var SetScore = tableRow[2].InnerText.Replace("\n", "").TrimStart().TrimEnd().Split("-");
                var setScoreP1 = Convert.ToInt32(SetScore[0]);
                var setScoreP2 = Convert.ToInt32(SetScore[1]);
                var novaMatch = new Match(data, playerHome, playerAway, setScoreP1, setScoreP2,linkP1,linkP2, linkMatch,p1Id, p2Id,p1Name,p2Name);
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
                Console.WriteLine("Resultado:" + match.SetScoreP1 + "-" + match.SetScoreP2);
                Console.Write("\n\n");
                i++;
            }
            await Task.CompletedTask;

        }
        public List<Match> MatchListGenerate()
        {
            var matchList = new List<Match>();
            return matchList;
        }
        
        public async Task ShowMatchs(List<Match> matchlist) 
        {
           await FormatAndCreateList(matchlist);
           await ConsoleLogPartidas(matchlist);
           
        
        }
        public async Task SetMatchDetails(Match match)
        {
            var result = await GetMatchDetails(match);
            var sets= result[0].InnerText.Replace("\n", "").TrimStart().Replace("  ", "").Last();
            var ptsP1PSet = result[1].InnerText.TrimStart().Replace(" ", "").Replace("\n", " ").Split(' ').Where(e => int.TryParse(e, out _)).ToList();
            var ptsP2PSet = result[2].InnerText.TrimStart().Replace(" ", "").Replace("\n", " ").Split(' ').Where(e => int.TryParse(e, out _)).ToList();
            Console.WriteLine("Resultado por set da partida selecionada");
            Console.WriteLine("Foram " + sets + " sets");
            Console.Write(match.P1Name + ":");
            foreach (var i in ptsP1PSet) { Console.Write(i + " "); }
            Console.WriteLine();
            Console.Write(match.P2Name + ":");
            foreach (var i in ptsP2PSet) { Console.Write(i + " "); }
            Console.WriteLine("Link para a partida: "+ match.LinkMatch);
            

        }

        //public async Task ShowMatchDetails(Match match) 
        //{
        
        
        
        //}
        public async Task<HtmlAgilityPack.HtmlNodeCollection> GetMatchDetails(Match match)
        {

            HttpClient httpClient = new HttpClient();
            string baseURl = $"https://pt.betsapi.com/";
            string matchURL = baseURl + match.LinkMatch.Replace("\"","");
            string html = await httpClient.GetStringAsync(matchURL);

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            string xpath = "//table[@class ='table']//tr";
            HtmlNodeCollection matchDetails = htmlDocument.DocumentNode.SelectNodes(xpath);

            if (matchDetails == null)
            {
                return null;
            }
            else
            {

                return matchDetails;
            }
            

         }
        
    }
    
}

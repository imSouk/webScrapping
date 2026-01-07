using HtmlAgilityPack;
using webScrapping.DTOs;
using webScrapping.Interfaces;
using webScrapping.Models;

namespace webScrapping.Services;

public class MatchScraperService : IMatchScraper
{
    private static readonly string BaseUrl = "https://pt.betsapi.com";
    private static readonly string MatchesUrl = $"{BaseUrl}/le/29128/TT-Elite-Series";
    private static readonly string MatchesXPath = "//table[@class ='table table-sm']//tbody//tr";
    private static readonly string MatchDetailsXPath = "//table[@class ='table']//tr";
    private readonly HttpClient _httpClient;

    public MatchScraperService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ScrapedMatchDto>> ScrapeMatchesAsync()
    {
        var html = await _httpClient.GetStringAsync(MatchesUrl);
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);
        var linhas = htmlDocument.DocumentNode.SelectNodes(MatchesXPath);

        if (linhas == null)
        {
            return new List<ScrapedMatchDto>();
        }

        var matches = new List<ScrapedMatchDto>();

        foreach (var match in linhas)
        {
            var tableRow = match.Descendants("td").ToList().Where(e => !e.InnerText.Equals("-")).ToList();
            
            var data = tableRow[0].InnerText;
            var playerHome = tableRow[1].InnerText.Split('\n')[1].Trim().Split(" v ")[0];
            var playerAway = tableRow[1].InnerText.Split('\n')[1].Trim().Split(" v ")[1];
            
            var htmlContent = tableRow[1].InnerHtml.TrimStart();
            var p1Id = Convert.ToInt32(htmlContent.Split("/")[2]);
            var p2Id = Convert.ToInt32(htmlContent.Split("/")[6]);
            var p1Name = htmlContent.Split("/")[3].Split("\"")[0];
            var p2Name = htmlContent.Split("/")[7].Split("\"")[0];
            
            var linkP1 = "/t/" + htmlContent.Split("/")[2] + "/" + htmlContent.Split("/")[3].Split("\"")[0];
            var linkP2 = "/t/" + htmlContent.Split("/")[6] + "/" + htmlContent.Split("/")[7].Split("\"")[0];
            
            var linkMatch = tableRow[2].InnerHtml.Replace("\n", "").TrimStart().Split(">")[0].Replace("<a href=", "").Replace("\"", "");
            
            var setScore = tableRow[2].InnerText.Replace("\n", "").TrimStart().TrimEnd().Split("-");
            var setScoreP1 = Convert.ToInt32(setScore[0]);
            var setScoreP2 = Convert.ToInt32(setScore[1]);

            var scrapedMatch = new ScrapedMatchDto
            {
                Date = data,
                PlayerHome = playerHome,
                PlayerAway = playerAway,
                P1Id = p1Id,
                P2Id = p2Id,
                P1Name = p1Name,
                P2Name = p2Name,
                LinkP1 = linkP1,
                LinkP2 = linkP2,
                LinkMatch = linkMatch,
                SetScoreP1 = setScoreP1,
                SetScoreP2 = setScoreP2
            };

            matches.Add(scrapedMatch);
        }

        return matches;
    }

    public async Task<MatchDetailsDto?> GetMatchDetailsAsync(string linkMatch)
    {
        var matchUrl = $"{BaseUrl}/{linkMatch.Replace("\"", "")}";
        var html = await _httpClient.GetStringAsync(matchUrl);
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);
        var matchDetails = htmlDocument.DocumentNode.SelectNodes(MatchDetailsXPath);

        if (matchDetails == null)
        {
            return null;
        }

        var sets = matchDetails[0].InnerText.Replace("\n", "").TrimStart().Replace("  ", "").Last();
        var pointsP1 = matchDetails[1].InnerText.TrimStart().Replace(" ", "").Replace("\n", " ")
            .Split(' ').Where(e => int.TryParse(e, out _)).Select(int.Parse).ToList();
        var pointsP2 = matchDetails[2].InnerText.TrimStart().Replace(" ", "").Replace("\n", " ")
            .Split(' ').Where(e => int.TryParse(e, out _)).Select(int.Parse).ToList();

        return new MatchDetailsDto
        {
            NumberOfSets = int.Parse(sets.ToString()),
            PointsP1 = pointsP1,
            PointsP2 = pointsP2,
            MatchLink = matchUrl
        };
    }
}

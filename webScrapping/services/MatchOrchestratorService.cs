using webScrapping.DTOs;
using webScrapping.Interfaces;
using webScrapping.Models;

namespace webScrapping.Services;

public class MatchOrchestratorService
{
    private readonly IMatchScraper _scraper;
    private readonly IMatchRepository _repository;

    public MatchOrchestratorService(IMatchScraper scraper, IMatchRepository repository)
    {
        _scraper = scraper;
        _repository = repository;
    }

    public async Task RunScrapingAsync()
    {
        Console.WriteLine($"Start {DateTime.Now}");
        
        var scrapedMatches = await _scraper.ScrapeMatchesAsync();
        Console.WriteLine($"Scraped {scrapedMatches.Count} matches from website");
        
        var newMatches = new List<Match>();
        
        foreach (var scrapedMatch in scrapedMatches)
        {
            var exists = await _repository.ExistsByLinkAsync(scrapedMatch.LinkMatch);
            
            if (!exists)
            {
                Console.WriteLine($"Saving new match: {scrapedMatch.PlayerHome} v {scrapedMatch.PlayerAway}");
                var match = new Match(
                    scrapedMatch.Date,
                    scrapedMatch.PlayerHome,
                    scrapedMatch.PlayerAway,
                    scrapedMatch.SetScoreP1,
                    scrapedMatch.SetScoreP2,
                    scrapedMatch.LinkP1,
                    scrapedMatch.LinkP2,
                    scrapedMatch.LinkMatch,
                    scrapedMatch.P1Id,
                    scrapedMatch.P2Id,
                    scrapedMatch.P1Name,
                    scrapedMatch.P2Name
                );
                
                newMatches.Add(match);
            }
        }
        
        if (newMatches.Any())
        {
            await _repository.AddRangeAsync(newMatches);
            await _repository.SaveChangesAsync();
            Console.WriteLine($"[SALVOU] {newMatches.Count} new matches saved to database!");
        }
        else
        {
            Console.WriteLine("[INFO] No new matches found - all matches already exist in database");
        }
        
        Console.WriteLine($"Finish {DateTime.Now}");
    }

    public void PrintMatches(List<ScrapedMatchDto> matches)
    {
        for (int i = matches.Count - 1; i >= Math.Max(0, matches.Count - 30); i--)
        {
            var match = matches[i];
            Console.WriteLine($"PARTIDA: {matches.Count - i}");
            Console.WriteLine(match.Date);
            Console.WriteLine($"{match.PlayerHome} v {match.PlayerAway}");
            Console.WriteLine($"Resultado: {match.SetScoreP1}-{match.SetScoreP2}");
            Console.WriteLine();
        }
    }
}

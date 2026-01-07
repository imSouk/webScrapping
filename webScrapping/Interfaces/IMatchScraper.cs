using webScrapping.DTOs;

namespace webScrapping.Interfaces;

public interface IMatchScraper
{
    Task<List<ScrapedMatchDto>> ScrapeMatchesAsync();
    Task<MatchDetailsDto?> GetMatchDetailsAsync(string linkMatch);
}

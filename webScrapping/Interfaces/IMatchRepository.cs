using webScrapping.Models;

namespace webScrapping.Interfaces;

public interface IMatchRepository
{
    Task<bool> ExistsByLinkAsync(string linkMatch);
    Task AddAsync(Match match);
    Task AddRangeAsync(IEnumerable<Match> matches);
    Task<int> SaveChangesAsync();
}

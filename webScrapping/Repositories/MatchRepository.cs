using Microsoft.EntityFrameworkCore;
using webScrapping.context;
using webScrapping.Interfaces;
using webScrapping.Models;

namespace webScrapping.Repositories;

public class MatchRepository : IMatchRepository
{
    private readonly StorageBroker _broker;

    public MatchRepository(StorageBroker broker)
    {
        _broker = broker;
    }

    public Task<bool> ExistsByLinkAsync(string linkMatch)
    {
        return _broker.Matchs.AnyAsync(m => m.LinkMatch == linkMatch);
    }

    public Task AddAsync(Match match)
    {
        return Task.Run(() => _broker.Add(match));
    }

    public Task AddRangeAsync(IEnumerable<Match> matches)
    {
        return Task.Run(() => _broker.AddRange(matches));
    }

    public Task<int> SaveChangesAsync()
    {
        return _broker.SaveChangesAsync();
    }
}

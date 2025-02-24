using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webScrapping.Models;

namespace webScrapping.context
{
    public class StorageBroker : DbContext
        {
            public StorageBroker() => this.Database.Migrate();
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Server=DESKTOP-0M45M7S\\SQLEXPRESS;Database=TTEliteSeries;Trusted_Connection=True;TrustServerCertificate=True;");
            //Create a table call Match
            public DbSet<Match> Matchs { get; set; }

            public async Task SaveOnDb(List<Match> matchList, StorageBroker broker)
            {
                foreach (Match partida in matchList)
                {
                    if (!broker.Matchs.Any(m => m.LinkMatch == partida.LinkMatch))
                    {
                        broker.Add(partida);
                        broker.SaveChanges();
                    }
                }
                await Task.CompletedTask;
            }

        }
    
}

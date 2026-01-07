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
        public StorageBroker(DbContextOptions<StorageBroker> options) : base(options)
        {
        }

        public DbSet<Match> Matchs { get; set; }
    }
}

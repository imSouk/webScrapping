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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Server=PEDRO-DESKTOP;Database=TTEliteSeriesD;Trusted_Connection=True;TrustServerCertificate=True;");
        //Create a table call Match
        public DbSet<Match> Matchs { get; set; }


        }
    
}

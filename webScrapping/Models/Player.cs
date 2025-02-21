using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webScrapping.Models
{
    internal class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PlayeUrl { get; set; }
        public int[] GameScore { get; set; }
    }
}

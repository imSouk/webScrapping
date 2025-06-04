using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webScrapping.context;
using webScrapping.Models;

namespace webScrapping.services
{
    public class MatchServices
    {
        private readonly StorageBroker _broker;
        public MatchServices(StorageBroker storageBroker) 
        {
            _broker = storageBroker;
        }

        public async Task SaveOnDb(List<Match> matchList)
        {
            foreach (Match partida in matchList)
            {
                if (!_broker.Matchs.Any(m => m.LinkMatch == partida.LinkMatch))
                {
                    _broker.Add(partida);
                    _broker.SaveChanges();
                }
            }
            await Task.CompletedTask;
        }
    }
}

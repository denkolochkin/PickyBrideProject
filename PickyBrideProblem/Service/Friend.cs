using System;
using log4net;
using PickyBrideProblem.Entity;
using PickyBrideProblem.Service;

namespace PickyBrideProblem.Service
{
    public class Friend
    {

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public List<Contender> ProcessedContenders = new();

        public Contender Compare(Contender first, Contender second)
        {
            if (ProcessedContenders.Contains(first) && ProcessedContenders.Contains(second))
            {
                return first.Quality > second.Quality ? first : second;
            }
            log.Error("Comparing error");
            throw new Exception("Someone hasn't dated!");
        }
    }
}


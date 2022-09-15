using PickyBrideProblem.Entity;
using PickyBrideProblem.Dto;

using System.Configuration;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace PickyBrideProblem.Service
{
    public class Princess
    {

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private int DatesCount = 1;

        private readonly int ContendersCount =
            int.Parse(ConfigurationManager.AppSettings["ContendersCount"]);

        private readonly List<Contender> FirstContenders = new();

        public PrincessAnswer DateContender(Contender contender, Friend friend)
        {
            PrintStatus(contender);

            friend.ProcessedContenders.Add(contender);

            if (DatesCount < (ContendersCount / Math.E))
            {
                FirstContenders.Add(contender);
            }
            else 
            {
                Contender bestOfFirst = FindBestOfFirst(friend);

                if (!friend.Compare(contender, bestOfFirst).Equals(bestOfFirst)
                    || DatesCount.Equals(ContendersCount))
                {
                    PrintResult(contender);
                    return new PrincessAnswer
                    {
                        Answer = ConfigurationManager.AppSettings["PositiveAnswer"],
                        Quality = contender.Quality
                    };
                }
            }

            DatesCount++;

            return new PrincessAnswer
            {
                Answer = ConfigurationManager.AppSettings["NegativeAnswer"]
            };
        }

        private Contender FindBestOfFirst(Friend friend)
        {
            Contender bestContenter = FirstContenders.First();
            for (int i = 0; i < FirstContenders.Count; i++)
            {
                if (!bestContenter.Equals(friend.Compare(bestContenter, FirstContenders[i])))
                {
                    bestContenter = FirstContenders[i];
                }
            }
            return bestContenter;
        }

        private void PrintStatus(Contender contender)
        {
            log.Info("Date #" + DatesCount
                + " : " + contender.Name + " " + contender.Lastname);
        }

        private static void PrintResult(Contender contender)
        {
            log.Info(contender.Quality);
        }
    }
}


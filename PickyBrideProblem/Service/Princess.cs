using PickyBrideProblem.Entity;
using PickyBrideProblem.Dto;

namespace PickyBrideProblem.Service
{
    public class Princess
    {

        private int DatesCount = 1;

        private readonly List<Contender> FirstContenders = new();

        public PrincessAnswer DateContender(Contender contender, Friend friend)
        {
            PrintStatus(contender);

            friend.ProcessedContenders.Add(contender);

            if (DatesCount < (Properties.ContendersCount / Math.E))
            {
                FirstContenders.Add(contender);
            }
            else 
            {
                Contender bestOfFirst = FindBestOfFirst(friend);

                if (!friend.Compare(contender, bestOfFirst).Equals(bestOfFirst)
                    || DatesCount.Equals(Properties.ContendersCount))
                {
                    PrintResult(contender);
                    return new PrincessAnswer
                    {
                        Answer = Properties.PositiveAnswer,
                        Quality = contender.Quality
                    };
                }
            }

            DatesCount++;

            return new PrincessAnswer
            {
                Answer = Properties.NegativeAnswer
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
            Console.WriteLine("Date #" + DatesCount
                + " : " + contender.Name + " " + contender.Lastname);
        }

        private static void PrintResult(Contender contender)
        {
            Console.WriteLine(contender.Quality);
        }
    }
}


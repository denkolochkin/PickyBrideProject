using PickyBrideProblem.Entity;
using PickyBrideProblem.Dto;
using System.Configuration;
using log4net;

namespace PickyBrideProblem.Service
{
    public class Hall
    {

        private readonly int ContendersCount =
            int.Parse(ConfigurationManager.AppSettings["ContendersCount"]);

        readonly Friend friend = new();

        public List<Contender> contenders;

        public Hall(List<Contender> contenders)
        {
            this.contenders = contenders;
        }

        public void DoInterview()
        {
            Princess princess = new();


            for (int i = 0; i < ContendersCount; i++)
            {
                Contender contender = contenders[new Random().Next(contenders.Count)];
                friend.ProcessedContenders.Add(contender);
                PrincessAnswer princessAnswer = princess.DateContender(contender, friend);
                if (ConfigurationManager.AppSettings["PositiveAnswer"].Equals(princessAnswer.Answer))
                {
                    break;
                }
                contenders.Remove(contender);
            }
        }
    }
}


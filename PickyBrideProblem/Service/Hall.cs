using System;
using PickyBrideProblem.Entity;
using PickyBrideProblem.Dto;

namespace PickyBrideProblem.Service
{
    public class Hall
    {
        readonly Friend friend = new();

        public List<Contender> contenders;

        public Hall(List<Contender> contenders)
        {
            this.contenders = contenders;
        }

        public void DoInterview()
        {
            Princess princess = new();

            for (int i = 0; i < Properties.ContendersCount; i++)
            {
                Contender contender = contenders[new Random().Next(contenders.Count)];
                friend.ProcessedContenders.Add(contender);
                PrincessAnswer princessAnswer = princess.DateContender(contender, friend);
                if (princessAnswer.Answer.Equals(Properties.PositiveAnswer))
                {
                    break;
                }
                contenders.Remove(contender);
            }
        }
    }
}


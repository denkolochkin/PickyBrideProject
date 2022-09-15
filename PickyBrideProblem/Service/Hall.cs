using PickyBrideProblem.Entity;
using PickyBrideProblem.Dto;

using System.Configuration;

namespace PickyBrideProblem.Service
{
    public class Hall
    {
        /// <summary>
        /// Заполняется ContenderGenerator и присваивается перед началом оды.
        /// </summary>
        public List<Contender> contenders = new();

        public Contender GetNextContender()
        {
            Contender contender = contenders[new Random().Next(contenders.Count)];
            contenders.Remove(contender);
            return contender;
        }
    }
}


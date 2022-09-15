using System.Configuration;
using Faker;

using PickyBrideProblem.Entity;

namespace PickyBrideProblem.Service
{
    public class ContenderGenerator
    {

        private readonly int ContendersCount =
            int.Parse(ConfigurationManager.AppSettings["ContendersCount"]);


        public List<Contender> GenerateContenders()
        {
            List<Contender> contenders = new();

            for (int i = 0; i < ContendersCount; i++)
            {
                contenders.Add(new Contender
                {
                    Name = NameFaker.MaleFirstName(),
                    Lastname = NameFaker.LastName(),
                    Quality = i + 1
                });
            }

            return contenders;
        }
    }
}


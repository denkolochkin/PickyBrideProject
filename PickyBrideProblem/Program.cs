using PickyBrideProblem;
using PickyBrideProblem.Entity;
using PickyBrideProblem.Service;

using Faker;

class MainClass
{
    static void Main()
    {
        List<Contender> contenders = new();
        for (int i = 0; i < Properties.ContendersCount; i++)
        {
            contenders.Add(new Contender
            {
                Name = NameFaker.MaleFirstName(),
                Lastname = NameFaker.LastName(),
                Quality = i + 1 
            });
        }

        Hall hall = new(contenders);
        hall.DoInterview();
    }
}
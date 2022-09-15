using PickyBrideProblem;
using PickyBrideProblem.Entity;
using PickyBrideProblem.Service;

using Faker;

class MainClass
{
    static void Main()
    {
        ContenderGenerator contenderGenerator = new();
        Hall hall = new(contenderGenerator.GetContenders());
        hall.DoInterview();
    }
}
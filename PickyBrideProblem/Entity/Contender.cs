namespace PickyBrideProblem.Entity

{
    public class Contender
    {
        public Guid Id = Guid.NewGuid();

        public int Quality { get; set; }

        public string? Name { get; set; }
        public string? Lastname { get; set; }
    }
}


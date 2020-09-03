namespace ConsoleApp1
{
    public class Player
    {
        public string Name { get; set; }
        public string Points { get; set; }

        public Player(string name, string points)
        {
            Name = name;
            Points = points;
        }
    }
}

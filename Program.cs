namespace OptiRoute
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            printStartingStations();
        }

        static void printStartingStations()
        {
            var tramStations = StaticData.tramStations;
            Console.WriteLine("Choose a station:");
            for (int i = 0; i < tramStations.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tramStations[i].Name}");
            }
        }
    }
}

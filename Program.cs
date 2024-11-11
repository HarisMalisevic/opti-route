namespace OptiRoute
{
    internal class Program
    {

        public static List<ITransportationMethod> transportationMethods = new List<ITransportationMethod>{
            new Tram(StaticData.orderedTramStations)
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            printStartingStations();
        }

        static void printStartingStations()
        {
            var startingStations = new SortedSet<Station>(new StationLexicographicComparer());

            for (int i = 0; i < transportationMethods.Count; i++)
            {
            var method = transportationMethods[i];
            var stations = method.getStartingStations();
            startingStations.UnionWith(stations);
            }

            foreach (var station in startingStations)
            {
            Console.WriteLine(station.Name);
            }
        }
    }
}

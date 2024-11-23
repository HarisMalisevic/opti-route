using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;

namespace OptiRoute
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {

        private static double TROLLEYBUS_PRICE_PER_ZONE_KM = 0.5;

        public static List<ITransportationMethod> staticTransportationMethods = new List<ITransportationMethod>{
            new Tram(StaticData.orderedTramStations),
            new Bus(StaticData.orderedBusStations, StaticData.busTravelTimesMinutes, StaticData.busTravelPricingKM),
            new Trolleybus(StaticData.orderedTrolleybusStations, StaticData.trolleybusTravelTimesMinutes, TROLLEYBUS_PRICE_PER_ZONE_KM)
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // Prepare starting stations
            List<Station> startingStations = prepareStartingStations(staticTransportationMethods);

            // Print starting stations
            int index = 1;
            foreach (var station in startingStations)
            {
                Console.WriteLine($"{index}. {station.Name}");
                index++;
            }

            // User selects starting station
            Console.WriteLine("Enter the number of the station you want to start from:");
            int selectedStationIndex;
            while (!int.TryParse(Console.ReadLine(), out selectedStationIndex) || selectedStationIndex < 1 || selectedStationIndex > startingStations.Count)
            {
                Console.WriteLine("Invalid input. Please enter a valid station number:");
            }

            Station selectedStartingStation = startingStations[selectedStationIndex - 1];
            Console.WriteLine($"You have selected: {selectedStartingStation.Name}");

            // Prepare destination stations
            List<Station> destinationStations = prepareDestinationStations(staticTransportationMethods, selectedStartingStation);

            // Print destination stations
            index = 1;
            foreach (var station in destinationStations)
            {
                Console.WriteLine($"{index}. {station.Name}");
                index++;
            }

            // User selects destination station
            Console.WriteLine("Enter the number of the station you want to go to:");
            while (!int.TryParse(Console.ReadLine(), out selectedStationIndex) || selectedStationIndex < 1 || selectedStationIndex > destinationStations.Count)
            {
                Console.WriteLine("Invalid input. Please enter a valid station number:");
            }

            Station selectedDestinationStation = destinationStations[selectedStationIndex - 1];
            Console.WriteLine($"You have selected: {selectedDestinationStation.Name}");

            //Find the best route

            double optimalTravelTime = TravelTimeOptimizer.getOptimalTravelTimeMinutes(staticTransportationMethods, selectedStartingStation, selectedDestinationStation);

            Console.WriteLine($"The optimal travel time is: {optimalTravelTime} minutes.");

            // Prompt the user to try again or exit
            Console.WriteLine("Would you like to find another route? (yes/no)");
            string userResponse = Console.ReadLine().Trim().ToLower();

            if (userResponse == "yes")
            {
                Main(args); // Restart the process
            }
            else
            {
                Console.WriteLine("Thank you for using OptiRoute. Goodbye!");
                Environment.Exit(0); // Exit the program
            }

        }

        static List<Station> prepareStartingStations(List<ITransportationMethod> transportationMethods)
        {
            var startingStations = new SortedSet<Station>(new StationLexicographicComparer());

            for (int i = 0; i < transportationMethods.Count; i++)
            {
                var method = transportationMethods[i];
                var stations = method.getStartingStations();
                startingStations.UnionWith(stations);
            }

            return startingStations.ToList();
        }

        static List<Station> prepareDestinationStations(List<ITransportationMethod> transportationMethods, Station destinationStation)
        {
            var destinationStations = new SortedSet<Station>(new StationLexicographicComparer());

            for (int i = 0; i < transportationMethods.Count; i++)
            {
                var method = transportationMethods[i];
                var stations = new SortedSet<Station>();
                try
                {
                    stations = method.getDestinationStations(destinationStation);
                }
                catch (ArgumentException)
                {
                    // Skip this method if it doesn't support the destination station
                    continue;

                }
                destinationStations.UnionWith(stations);
            }

            return destinationStations.ToList();
        }
    }
}

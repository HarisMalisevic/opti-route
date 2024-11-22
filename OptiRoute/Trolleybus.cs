
namespace OptiRoute
{
    public class Trolleybus : ITransportationMethod
    {

        private List<Station> supportedStations;

        private int[,] travelTimesMinutes;

        private double pricePerZoneTraveledKM { get; set; }

        public Trolleybus(List<Station> supportedStations, int[,] travelTimesMinutes, double pricePerZoneTraveledKM)
        {

            if (supportedStations.Distinct().Count() != supportedStations.Count)
            {
                throw new ArgumentException("The list of supported stations contains duplicates.");
            }
            
            if (travelTimesMinutes.GetLength(0) != supportedStations.Count || travelTimesMinutes.GetLength(1) != supportedStations.Count)
            {
                throw new ArgumentException("The dimensions of travelTimesMinutes must match the number of supported stations.");
            }
            
            this.supportedStations = supportedStations;
            this.travelTimesMinutes = travelTimesMinutes;
            this.pricePerZoneTraveledKM = pricePerZoneTraveledKM;
        }
        public double getCommuteDurationMinutes(Station startingStation, Station destinationStation)
        {
            var startIndex = supportedStations.IndexOf(startingStation);
            var destinationIndex = supportedStations.IndexOf(destinationStation);

            if (startIndex == -1)
            {
                throw new ArgumentException(message: $"Starting station {startingStation.Name} not supported.");
            }

            if (destinationIndex == -1)
            {
                throw new ArgumentException(message: $"Destination station {destinationStation.Name} not supported.");
            }

            if (startIndex == destinationIndex)
            {
                throw new ArgumentException("Starting and destination stations are the same");
            }

            return travelTimesMinutes[startIndex, destinationIndex];
        }

        public SortedSet<Station> getDestinationStations(Station startingStation)
        {
            SortedSet<Station> destinationStations = new SortedSet<Station>(supportedStations, new StationLexicographicComparer());

            if (!supportedStations.Contains(startingStation))
            {
                throw new ArgumentException(message: $"Starting station {startingStation.Name} not supported.");
            }

            destinationStations.Remove(startingStation);

            return destinationStations;
        }

        public double getPriceKM(Station startingStation, Station destinationStation)
        {
            int startIndex = supportedStations.IndexOf(startingStation);
            int destinationIndex = supportedStations.IndexOf(destinationStation);

            if (startIndex == -1)
            {
                throw new ArgumentException(message: $"Starting station {startingStation.Name} not supported.");
            }

            if (destinationIndex == -1)
            {
                throw new ArgumentException(message: $"Destination station {destinationStation.Name} not supported.");
            }

            if (startIndex == destinationIndex)
            {
                throw new ArgumentException("Starting and destination stations are the same");
            }


            int numberOfZonesTraveled = Math.Abs(destinationStation.Zone - startingStation.Zone) + 1; // +1 because driving within one zone is still considered as traveling through one zone

            return pricePerZoneTraveledKM * numberOfZonesTraveled;
        }

        public SortedSet<Station> getStartingStations()
        {
            SortedSet<Station> startingStations = new SortedSet<Station>(supportedStations, new StationLexicographicComparer());

            return startingStations;
        }
    }
}
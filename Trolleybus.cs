
namespace OptiRoute
{
    public class Trolleybus : ITransportationMethod
    {

        private List<Station> supportedStations;

        private int[,] travelTimesMinutes;

        private double pricePerZoneTraveledKM { get; set; }

        public Trolleybus(List<Station> supportedStations, int[,] travelTimesMinutes, double pricePerZoneTraveledKM)
        {
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
            SortedSet<Station> destinationStations = new SortedSet<Station>();
            int startIndex = supportedStations.IndexOf(startingStation);

            if (startIndex == -1)
            {
                throw new ArgumentException("The starting station is not supported by this bus.");
            }

            for (int i = 0; i < travelTimesMinutes.GetLength(1); i++)
            {
                if (travelTimesMinutes[startIndex, i] != -1)
                {
                    destinationStations.Add(supportedStations[i]);
                }
            }

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
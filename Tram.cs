namespace OptiRoute
{
    public class Tram : ITransportationMethod, ITransportationPricing
    {

        const int TIME_BETWEEN_STATIONS_MINUTES = 5;
        const double PRICE_PER_STATION = 0.5;
        private List<Station> supportedStations;

        public Tram(List<Station> supportedStations)
        {
            this.supportedStations = supportedStations;
        }

        public SortedSet<Station> getStartingStations()
        {
            var startingStations = new SortedSet<Station>(supportedStations, new StationLexicographicComparer());
            return startingStations;
        }

        public SortedSet<Station> getDestinationStations(Station startingStation)
        {
            if (!supportedStations.Contains(startingStation))
            {
                throw new ArgumentException(message: $"Station {startingStation.Name} not supported.");
            }

            var destinationStation = new SortedSet<Station>(supportedStations, new StationLexicographicComparer());
            destinationStation.Remove(startingStation);

            return destinationStation;
        }

        public double getCommuteDurationMinutes(Station startingStation, Station destinationStation)
        {


            var startingStationIndex = supportedStations.IndexOf(startingStation);
            var destinationStationIndex = supportedStations.IndexOf(destinationStation);

            if (startingStationIndex == -1)
            {
                throw new ArgumentException(message: $"Starting station {startingStation.Name} not supported.");
            }

            if (destinationStationIndex == -1)
            {
                throw new ArgumentException(message: $"Destination station {destinationStation.Name} not supported.");
            }

            return Math.Abs(startingStationIndex - destinationStationIndex) * TIME_BETWEEN_STATIONS_MINUTES;
        }

        public double getPriceKM(Station startingStation, Station destinationStation)
        {

            var startingStationIndex = supportedStations.IndexOf(startingStation);
            var destinationStationIndex = supportedStations.IndexOf(destinationStation);

            if (startingStationIndex == -1)
            {
                throw new ArgumentException(message: $"Starting station {startingStation.Name} not supported.");
            }

            if (destinationStationIndex == -1)
            {
                throw new ArgumentException(message: $"Destination station {destinationStation.Name} not supported.");
            }

            return Math.Abs(startingStationIndex - destinationStationIndex) * PRICE_PER_STATION;

        }


    }
}
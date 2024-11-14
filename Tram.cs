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

            return Math.Abs(startIndex - destinationIndex) * TIME_BETWEEN_STATIONS_MINUTES;
        }

        public double getPriceKM(Station startingStation, Station destinationStation)
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

            return Math.Abs(startIndex - destinationIndex) * PRICE_PER_STATION;

        }


    }
}
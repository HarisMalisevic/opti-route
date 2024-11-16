namespace OptiRoute
{
    public class Tram : ITransportationMethod
    {

        private int timeBetweenStationsMinutes;
        private double pricePerStationKM;
        public int TimeBetweenStationsMinutes
        {
            get { return timeBetweenStationsMinutes; }
            set { timeBetweenStationsMinutes = value; }
        }

        public double PricePerStationKM
        {
            get { return pricePerStationKM; }
            set { pricePerStationKM = value; }
        }

        public List<Station> SupportedStations
        {
            get { return supportedStations; }
            set { supportedStations = value; }
        }
        private List<Station> supportedStations;

        public Tram(List<Station> supportedStations, int timeBetweenStationsMinutes = 5, double pricePerStationKM = 0.5)
        {
            this.supportedStations = supportedStations;
            this.timeBetweenStationsMinutes = timeBetweenStationsMinutes;
            this.pricePerStationKM = pricePerStationKM;
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

            return Math.Abs(startIndex - destinationIndex) * timeBetweenStationsMinutes;
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

            return Math.Abs(startIndex - destinationIndex) * pricePerStationKM;

        }


    }
}
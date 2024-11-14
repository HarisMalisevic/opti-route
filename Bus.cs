
namespace OptiRoute
{
    public class Bus : ITransportationMethod, ITransportationPricing
    {
        private List<Station> supportedStations;

        private int[,] travelTimesMinutes;
        private double[,] travelPricesKM;



        public Bus(List<Station> supportedStations, int[,] travelTimesMinutes, double[,] travelPricesKM)
        {
            if (travelTimesMinutes.GetLength(0) != supportedStations.Count || travelTimesMinutes.GetLength(1) != supportedStations.Count)
            {
                throw new ArgumentException("The dimensions of travelTimesMinutes must match the number of supported stations.");
            }

            if (travelPricesKM.GetLength(0) != supportedStations.Count || travelPricesKM.GetLength(1) != supportedStations.Count)
            {
                throw new ArgumentException("The dimensions of travelPricesKM must match the number of supported stations.");
            }

            this.supportedStations = supportedStations;
            this.travelTimesMinutes = travelTimesMinutes;
            this.travelPricesKM = travelPricesKM;
        }

        public double getCommuteDurationMinutes(Station startingStation, Station destinationStation)
        {
            int startIndex = supportedStations.IndexOf(startingStation);
            int destinationIndex = supportedStations.IndexOf(destinationStation);

            if (startIndex == -1 || destinationIndex == -1)
            {
                throw new ArgumentException("One or both of the stations are not supported by this bus.");
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

            if (startIndex == -1 || destinationIndex == -1)
            {
                throw new ArgumentException("One or both of the stations are not supported by this bus.");
            }

            return travelPricesKM[startIndex, destinationIndex];
        }

        public SortedSet<Station> getStartingStations()
        {
            SortedSet<Station> startingStations = new SortedSet<Station>(supportedStations, new StationLexicographicComparer());

            return startingStations;
        }
    }
}
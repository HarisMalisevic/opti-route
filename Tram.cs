namespace OptiRoute
{
    public class Tram : ITransportationMethod, ICjenovnik
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
            var startingStations = new SortedSet<Station>(supportedStations, new StanicaAbecednoComparer());
            return startingStations;
        }

        public SortedSet<Station> getDestinationStations(Station startingStation)
        {
            if (!supportedStations.Contains(startingStation))
            {
                throw new ArgumentException(message: $"Station {startingStation.Name} not supported.");
            }

            var destinationStation = new SortedSet<Station>(supportedStations, new StanicaAbecednoComparer());
            destinationStation.Remove(startingStation);

            return destinationStation;
        }

        public double getCommuteDurationMinutes(Station polaznaStanica, Station odredisnaStanica)
        {


            var polaznaStanicaIndex = supportedStations.IndexOf(polaznaStanica);
            var odredisnaStanicaIndex = supportedStations.IndexOf(odredisnaStanica);

            return Math.Abs(polaznaStanicaIndex - odredisnaStanicaIndex) * TIME_BETWEEN_STATIONS_MINUTES;
        }

        public double getPriceKM(Station polaznaStanica, Station odredisnaStanica)
        {

            var polaznaStanicaIndex = supportedStations.IndexOf(polaznaStanica);
            var odredisnaStanicaIndex = supportedStations.IndexOf(odredisnaStanica);

            return Math.Abs(polaznaStanicaIndex - odredisnaStanicaIndex) * PRICE_PER_STATION;

        }


    }
}
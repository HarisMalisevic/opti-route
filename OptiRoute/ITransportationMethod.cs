namespace OptiRoute {
    public interface ITransportationMethod {
        public double getCommuteDurationMinutes(Station startingStation, Station destinationStation);

        public SortedSet<Station> getStartingStations();
        public SortedSet<Station> getDestinationStations(Station startingStation);

        public double getPriceKM(Station startingStation, Station destinationStation);
    }

}
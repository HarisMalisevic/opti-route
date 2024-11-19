namespace OptiRoute {
    public interface ITransportationMethod {
        public double getCommuteDurationMinutes(Station startingStation, Station destinationStation);

        public SortedSet<Station> getStartingStations();
        public SortedSet<Station> getDestinationStations(Station startingStation);
        //TODO: Potrebno proslijediti IComparer<Stanica> unutar implementacije metode dajOdredisneStanice

        public double getPriceKM(Station startingStation, Station destinationStation);
    }

}
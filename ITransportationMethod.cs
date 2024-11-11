namespace OptiRoute {
    public interface ITransportationMethod {
        public double getCommuteDurationMinutes(Station polaznaStanica, Station odredisnaStanica);

        public SortedSet<Station> getStartingStations();
        public SortedSet<Station> dajOdredisneStanice(Station polaznaStanica);
        //TODO: Potrebno proslijediti IComparer<Stanica> unutar implementacije metode dajOdredisneStanice
    }

}
namespace OptiRoute {
    public class Tram : ITransportationMethod, ICjenovnik {
        private List<Station> supportedStations;

        public Tram(List<Station> supportedStations) {
            this.supportedStations = supportedStations;
        }

        public SortedSet<Station> getStartingStations() {
            var startingStations = new SortedSet<Station>(supportedStations, new StanicaAbecednoComparer());
            return startingStations;
        }

        public SortedSet<Station> dajOdredisneStanice(Station startingStation) {
            if (!supportedStations.Contains(startingStation)) {
                throw new ArgumentException(message: $"Station {startingStation.Name} not supported.");
            }

            var destinationStation = new SortedSet<Station>(supportedStations, new StanicaAbecednoComparer());
            destinationStation.Remove(startingStation);

            return destinationStation;
        }

        public double getCommuteDurationMinutes(Station polaznaStanica, Station odredisnaStanica) {
            //TODO: Implementirati
            throw new NotImplementedException();
        }

        public double getPriceKM(Station polaznaStanica, Station odredisnaStanica) {
            //TODO: Implementirati
            throw new NotImplementedException();
        }


    }
}
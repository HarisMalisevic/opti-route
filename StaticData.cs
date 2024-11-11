namespace OptiRoute {
    public static class StaticData {

        public static Station predsjednistvo = new Station("Predsjednistvo", Zone.A_CentarGrada);
        public static Station tehnickaSkola = new Station("Tehnicka skola", Zone.A_CentarGrada);
        public static Station kampus = new Station("Kampus", Zone.A_CentarGrada);
        public static Station pofalici = new Station("Pofalici", Zone.A_CentarGrada);

        public static Station ilidza = new Station("Ilidza", Zone.B_Predgradje);
        public static Station sutjeska = new Station("Sutjeska", Zone.A_CentarGrada);
        public static Station ciglane = new Station("Ciglane", Zone.A_CentarGrada);
        public static Station bare = new Station("Bare", Zone.A_CentarGrada);
        public static Station vogosca = new Station("Vogosca", Zone.B_Predgradje);
        public static Station ilijas = new Station("Ilijas", Zone.B_Predgradje);
        public static Station skenderija = new Station("Skenderija", Zone.A_CentarGrada);
        public static Station jezero = new Station("Jezero", Zone.A_CentarGrada);

        public static List<Station> orderedTramStations = new List<Station>
        {
            predsjednistvo,
            tehnickaSkola,
            kampus,
            pofalici,
            ilidza
        };


        // public static Dictionary<(Stanica, Stanica), double> autobuskeLinije = new Dictionary<(Stanica, Stanica), double>{};

        // public static Dictionary<(Stanica, Stanica), double> trolejbuskeLinije = new Dictionary<(Stanica, Stanica), double>{};
    }
}
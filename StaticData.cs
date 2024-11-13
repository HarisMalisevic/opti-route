namespace OptiRoute
{
    public static class StaticData
    {

        public static Station predsjednistvo = new Station("Predsjednistvo", Zone.A_CITY_CENTER);
        public static Station tehnickaSkola = new Station("Tehnicka skola", Zone.A_CITY_CENTER);
        public static Station kampus = new Station("Kampus", Zone.A_CITY_CENTER);
        public static Station pofalici = new Station("Pofalici", Zone.A_CITY_CENTER);

        public static Station ilidza = new Station("Ilidza", Zone.B_SUBURBS);
        public static Station sutjeska = new Station("Sutjeska", Zone.A_CITY_CENTER);
        public static Station ciglane = new Station("Ciglane", Zone.A_CITY_CENTER);
        public static Station bare = new Station("Bare", Zone.A_CITY_CENTER);
        public static Station vogosca = new Station("Vogosca", Zone.B_SUBURBS);
        public static Station ilijas = new Station("Ilijas", Zone.B_SUBURBS);
        public static Station skenderija = new Station("Skenderija", Zone.A_CITY_CENTER);
        public static Station jezero = new Station("Jezero", Zone.A_CITY_CENTER);

        public static List<Station> orderedTramStations = new List<Station>
        {
            predsjednistvo,
            tehnickaSkola,
            kampus,
            pofalici,
            ilidza
        };

        public static List<Station> orderedBusStations = new List<Station>
        {
            sutjeska,
            ciglane,
            bare,
            vogosca,
            ilijas
        };

        public static int[,] busTravelTimesMinutes = {
            {0, 6, 12, 22, 47},
            {6, 0, 8, 11, 27},
            {12, 6, 0, 6, 12},
            {24, 11, 6, 0, 6},
            {51, 16, 12, 6, 0}
        };

        public static double[,] busTravelPricingKM = {
            {0, -1, 1.2, 2.0, 3.4},
            {-1, 0, 1.2, 2.0, 3.4},
            {1.2, 1.0, 0, 1.2, 2.5},
            {2.4, 1.4, 1.2, 0, 2.5},
            {3.4, 1.6, 1.2, 1.0, 0}
        };


        // public static Dictionary<(Stanica, Stanica), double> autobuskeLinije = new Dictionary<(Stanica, Stanica), double>{};

        // public static Dictionary<(Stanica, Stanica), double> trolejbuskeLinije = new Dictionary<(Stanica, Stanica), double>{};
    }
}
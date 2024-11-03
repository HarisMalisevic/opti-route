namespace OptiRoute {
    public static class StaticData {

        public static Stanica predsjednistvo = new Stanica("Predsjednistvo", Zona.A_CentarGrada);
        public static Stanica tehnickaSkola = new Stanica("Tehnicka skola", Zona.A_CentarGrada);
        public static Stanica kampus = new Stanica("Kampus", Zona.A_CentarGrada);
        public static Stanica pofalici = new Stanica("Pofalici", Zona.A_CentarGrada);
        public static Stanica sutjeska = new Stanica("Sutjeska", Zona.A_CentarGrada);
        public static Stanica ciglane = new Stanica("Ciglane", Zona.A_CentarGrada);
        public static Stanica bare = new Stanica("Bare", Zona.A_CentarGrada);
        public static Stanica vogosca = new Stanica("Vogosca", Zona.B_Predgradje);
        public static Stanica ilijas = new Stanica("Ilijas", Zona.B_Predgradje);
        public static Stanica skenderija = new Stanica("Skenderija", Zona.A_CentarGrada);
        public static Stanica jezero = new Stanica("Jezero", Zona.A_CentarGrada);

        public static Dictionary<(Stanica, Stanica), double> staticTramvajskeLinije = new Dictionary<(Stanica, Stanica), double>
            {
                // Ka Ilidzi
                {(predsjednistvo, tehnickaSkola), 5},
                {(predsjednistvo, kampus), 9},
                {(predsjednistvo, pofalici), 13},

                {(tehnickaSkola, predsjednistvo), 13},
                {(tehnickaSkola, kampus), 4},
                {(tehnickaSkola, pofalici), 7},

                {(kampus, predsjednistvo), 9},
                {(kampus, tehnickaSkola), 3},
                {(kampus, pofalici), 3},

                {(pofalici, predsjednistvo), 12},
                {(pofalici, tehnickaSkola), 6},
                {(pofalici, kampus), 3},
            };


        // public static Dictionary<(Stanica, Stanica), double> autobuskeLinije = new Dictionary<(Stanica, Stanica), double>{};

        // public static Dictionary<(Stanica, Stanica), double> trolejbuskeLinije = new Dictionary<(Stanica, Stanica), double>{};
    }
}
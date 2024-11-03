namespace OptiRoute {
    public interface IPrevoznoSredstvo {
        public double dajTrajanjeVoznjeMinute(Stanica polaznaStanica, Stanica odredisnaStanica);

        public SortedSet<Stanica> dajPolazneStanice();
        public SortedSet<Stanica> dajOdredisneStanice(Stanica polaznaStanica);
        //TODO: Potrebno proslijediti IComparer<Stanica> unutar implementacije metode dajOdredisneStanice
    }

}
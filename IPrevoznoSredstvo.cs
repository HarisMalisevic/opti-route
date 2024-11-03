namespace OptiRoute
{
    public interface IPrevoznoSredstvo
    {
        private List<Stanica> podrzaneStanice;
        public double dajTrajanjeVoznjeMinute(Stanica polaznaStanica, Stanica odredisnaStanica);

        public SortedSet<Stanica> dajPolazneStanice();
        public SortedSet<Stanica> dajOdredisneStanice(Stanica polaznaStanica);
        //TODO: Potrebno proslijediti IComparer<Stanica> unutar implementacije metode dajOdredisneStanice
    }

}
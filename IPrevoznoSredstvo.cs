namespace OptiRoute
{
    public interface IPrevoznoSredstvo
    {
        public double dajTrajanjeVoznje(Stanica polaznaStanica, Stanica odredisnaStanica);
        public SortedSet<Stanica> dajOdredisneStanice(Stanica polaznaStanica);
        //TODO: Potrebno proslijediti IComparer<Stanica> unutar implementacije metode dajOdredisneStanice
    }

}
namespace OptiRoute
{
    public class Tramvaj : IPrevoznoSredstvo, ICjenovnik
    {
        private List<Stanica> podrzaneStanice;

        

        public Tramvaj (List<Stanica> podrzaneStanice)
        {
            this.podrzaneStanice = podrzaneStanice;
        }

        public SortedSet<Stanica> dajPolazneStanice()
        {
            var odredisneStanice = new SortedSet<Stanica>(podrzaneStanice, new StanicaAbecednoComparer());
            return odredisneStanice;
        }

        public SortedSet<Stanica> dajOdredisneStanice(Stanica polaznaStanica)
        {
            if (!podrzaneStanice.Contains(polaznaStanica))
            {
                throw new ArgumentException(message: $"Stanica{polaznaStanica.Naziv}nije u podrzanim stanicama.");
            }

            var odredisneStanice = new SortedSet<Stanica>(podrzaneStanice, new StanicaAbecednoComparer());
            odredisneStanice.Remove(polaznaStanica);

            return odredisneStanice;
        }

        public double dajTrajanjeVoznjeMinute(Stanica polaznaStanica, Stanica odredisnaStanica)
        {
            //TODO: Implementirati
            throw new NotImplementedException();
        }

        public double dajCijenuKM(Stanica polaznaStanica, Stanica odredisnaStanica)
        {
            //TODO: Implementirati
            throw new NotImplementedException();
        }


    }
}
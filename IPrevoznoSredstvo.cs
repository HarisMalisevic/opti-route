using System.Dynamic;

namespace OptiRoute
{
    public interface IPrevoznoSredstvo
    {
        public double dajTrajanjeVoznje(Stanica polaznaStanica, Stanica odredisnaStanica);
        public List<Stanica> dajOdredisneStanice(Stanica polaznaStanica);
    }

}
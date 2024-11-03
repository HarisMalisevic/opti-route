using Microsoft.Win32.SafeHandles;

namespace OptiRoute
{
    public class Stanica
    {
        private string naziv;

        public Zona zona;
        public string Naziv
        {
            get { return naziv; }
        }

        public Zona Zona
        {
            get { return zona; }
        }
        public Stanica(string naziv, Zona zona)
        {
            this.naziv = naziv;
            this.zona = zona;
        }
    }
}
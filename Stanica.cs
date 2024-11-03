using Microsoft.Win32.SafeHandles;

namespace OptiRoute
{
    public class Stanica
    {
        private string naziv;

        private Zona zona;
        public Stanica(string naziv, Zona zona)
        {
            this.naziv = naziv;
            this.zona = zona;
        }
    }
}
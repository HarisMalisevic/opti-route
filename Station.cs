using Microsoft.Win32.SafeHandles;

namespace OptiRoute
{
    public class Station
    {
        private string name;
        private Zone zone;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Zone Zone
        {
            get { return zone; }
            set { zone = value; }
        }

        public Station(string name, Zone zone)
        {
            this.name = name;
            this.zone = zone;
        }
    }
}
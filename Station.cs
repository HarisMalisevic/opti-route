using Microsoft.Win32.SafeHandles;

namespace OptiRoute
{
    public class Station
    {
        private string name;

        public Zone zone;
        public string Name
        {
            get { return name; }
        }

        public Zone Zone
        {
            get { return zone; }
        }
        public Station(string name, Zone zone)
        {
            this.name = name;
            this.zone = zone;
        }
    }
}
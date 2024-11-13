using Microsoft.Win32.SafeHandles;

namespace OptiRoute
{
    public class Station
    {
        public string Name { get; set; }
        public Zone Zone { get; set; }

        public Station(string name, Zone zone)
        {
            Name = name;
            Zone = zone;
        }
    }
}
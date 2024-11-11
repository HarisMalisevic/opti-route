namespace OptiRoute
{
    public class StationLexicographicComparer : IComparer<Station>
    {
        public int Compare(Station? x, Station? y)
        {
            return string.Compare(x?.Name, y?.Name, StringComparison.Ordinal);
        }
    }
}
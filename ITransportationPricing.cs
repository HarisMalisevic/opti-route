namespace OptiRoute
{
    public interface ITransportationPricing
    {
        public double getPriceKM(Station startingStation, Station destinationStation);
    }
}
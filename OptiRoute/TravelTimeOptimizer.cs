namespace OptiRoute;

public static class TravelTimeOptimizer
{

    public static double getOptimalTravelTimeMinutes(List<ITransportationMethod> transportationMethod, Station startingStation, Station destinationStation)
    {
        if (transportationMethod == null)
        {
            throw new ArgumentException("Transportation methods must not be null.");
        }

        if (transportationMethod.Count == 0)
        {
            throw new ArgumentException("At least one transportation method must be provided.");
        }

        if (startingStation == null || destinationStation == null)
        {
            throw new ArgumentException("Starting and destination stations must not be null.");
        }

        double optimalTravelTime = double.MaxValue;

        foreach (var method in transportationMethod)
        {
            double travelTime;
            try
            {
                travelTime = method.getCommuteDurationMinutes(startingStation, destinationStation);
            }
            catch (ArgumentException)
            {

                continue;
            }

            if (travelTime < optimalTravelTime)
            {
                optimalTravelTime = travelTime;
            }
        }
        return optimalTravelTime;
    }

}

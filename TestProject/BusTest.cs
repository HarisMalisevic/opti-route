using System.Runtime.CompilerServices;
using OptiRoute;

namespace TestProject;

[TestClass]
public sealed class BusTest
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Bus_TimesMatrixNotSquare_ThrowsArgumentException()
    {
        // Arrange
        List<Station> supportedStations = new List<Station> {
            new Station("First Station", Zone.A_CITY_CENTER), new Station("Second Station", Zone.B_SUBURBS)
        };

        int[,] travelTimesMinutes = new int[1, 2];
        double[,] travelPricesKM = new double[,] { { 0, 2.0 }, { 3.0, 0 } };

        // Act
        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Bus_PricesMatrixNotSquare_ThrowsArgumentException()
    {
        // Arrange
        List<Station> supportedStations = new List<Station> {
            new Station("First Station", Zone.A_CITY_CENTER), new Station("Second Station", Zone.B_SUBURBS)
        };

        int[,] travelTimesMinutes = new int[,] { { 0, 2 }, { 3, 0 } };
        double[,] travelPricesKM = new double[1, 2];

        // Act
        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void getCommuteDurationMinutes_UnsupportedStartingStation_ThrowsArgumentException()
    {
        // Arrange
        Station stationA = new Station("First Station", Zone.A_CITY_CENTER);
        Station stationB = new Station("Second Station", Zone.B_SUBURBS);

        List<Station> supportedStations = new List<Station> { stationA, stationB };

        int[,] travelTimesMinutes = new int[,] { { 0, 2 }, { 3, 0 } };
        double[,] travelPricesKM = new double[,] { { 0, 1.2 }, { 1.3, 0 } };

        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        Station stationC = new Station("Third station", Zone.B_SUBURBS);

        // Act
        double result = bus.getCommuteDurationMinutes(stationC, stationA);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void getCommuteDurationMinutes_UnsupportedDestinationStation_ThrowsArgumentException()
    {
        // Arrange
        Station stationA = new Station("First Station", Zone.A_CITY_CENTER);
        Station stationB = new Station("Second Station", Zone.B_SUBURBS);

        List<Station> supportedStations = new List<Station> { stationA, stationB };

        int[,] travelTimesMinutes = new int[,] { { 0, 2 }, { 3, 0 } };
        double[,] travelPricesKM = new double[,] { { 0, 1.2 }, { 1.3, 0 } };

        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        Station stationC = new Station("Third station", Zone.B_SUBURBS);

        // Act
        double result = bus.getCommuteDurationMinutes(stationA, stationC);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    public void getCommuteDurationMinutes_Stations5MinutesAway_Returns5()
    {
        // Arrange

        const double EXPECTED_MINUTES = 5.0;

        Station stationA = new Station("First Station", Zone.A_CITY_CENTER);
        Station stationB = new Station("Second Station", Zone.B_SUBURBS);

        List<Station> supportedStations = new List<Station> { stationA, stationB };

        int[,] travelTimesMinutes = new int[,] { { 0, 5 }, { 4, 0 } };
        double[,] travelPricesKM = new double[,] { { 0, 1.2 }, { 1.3, 0 } };

        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        // Act
        double result = bus.getCommuteDurationMinutes(stationA, stationB);

        // Assert
        Assert.AreEqual(result, EXPECTED_MINUTES);
    }

    [TestMethod]
    public void getCommuteDurationMinutes_Stations4MinutesAway_Returns4() //TODO: RENAME!
    {
        // Arrange

        const double EXPECTED_MINUTES = 4.0;

        Station stationA = new Station("First Station", Zone.A_CITY_CENTER);
        Station stationB = new Station("Second Station", Zone.B_SUBURBS);

        List<Station> supportedStations = new List<Station> { stationA, stationB };

        int[,] travelTimesMinutes = new int[,] { { 0, 5 }, { 4, 0 } };
        double[,] travelPricesKM = new double[,] { { 0, 1.2 }, { 1.3, 0 } };

        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        // Act
        double result = bus.getCommuteDurationMinutes(stationB, stationA);

        // Assert
        Assert.AreEqual(result, EXPECTED_MINUTES);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void getDestinationStations_UnsupportedStartingStation_ThrowsArgumentExcpetion()
    {
        // Arrange

        Station stationA = new Station("First Station", Zone.A_CITY_CENTER);
        Station stationB = new Station("Second Station", Zone.B_SUBURBS);
        Station stationC = new Station("Third Station", Zone.C_OUTSKIRT);

        List<Station> supportedStations = new List<Station> { stationA, stationB, stationC };

        int[,] travelTimesMinutes = new int[,] { { 0, 5, 7 }, { 4, 0, 3 }, { 6, 4, 0 } };
        double[,] travelPricesKM = new double[,] { { 0, 1.2, 1.3 }, { 1.2, 0, 1.1 }, { 1.2, 1.1, 0 } };

        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        Station stationD = new Station("Fourth Station", Zone.C_OUTSKIRT);

        // Act
        SortedSet<Station> result = bus.getDestinationStations(stationD);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    public void getDestinationStations_SupportedStartingStation_ReturnsTwoStations()
    {
        // Arrange

        Station stationA = new Station("First Station", Zone.A_CITY_CENTER);
        Station stationB = new Station("Second Station", Zone.B_SUBURBS);
        Station stationC = new Station("Third Station", Zone.C_OUTSKIRT);

        List<Station> supportedStations = new List<Station> { stationA, stationB, stationC };

        int[,] travelTimesMinutes = new int[,] { { 0, 5, 7 }, { 4, 0, 3 }, { 6, 4, 0 } };
        double[,] travelPricesKM = new double[,] { { 0, 1.2, 1.3 }, { 1.2, 0, 1.1 }, { 1.2, 1.1, 0 } };

        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        Station stationD = new Station("Fourth Station", Zone.C_OUTSKIRT);

        SortedSet<Station> EXPECTED_RESAULT = new SortedSet<Station>(new StationLexicographicComparer()) { stationB, stationC };

        // Act
        SortedSet<Station> result = bus.getDestinationStations(stationA);

        // Assert        
        Assert.AreEqual(result, EXPECTED_RESAULT);
    }
}
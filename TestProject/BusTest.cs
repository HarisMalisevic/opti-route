using OptiRoute;

namespace TestProject;

[TestClass]
public sealed class BusTest
{

    public static IEnumerable<object[]> ValidBusTravelData
    {
        get
        {
            return new[]
            {
                new object[] {StaticTestData.vogosca, StaticTestData.ilijas, 6, 2.5},
                new object[] {StaticTestData.sutjeska, StaticTestData.bare, 12, 1.2},
                new object[] {StaticTestData.bare, StaticTestData.ilijas, 12, 2.5},
            };
        }
    }

    private Station stationA = new Station("A", Zone.A_CITY_CENTER);
    private Station stationB = new Station("B", Zone.A_CITY_CENTER);
    private Station stationC = new Station("C", Zone.A_CITY_CENTER);

    private int[,] travelTimesMinutes = new int[,] { { 0, 5, 7 }, { 4, 0, 3 }, { 6, 4, 0 } };
    private double[,] travelPricesKM = new double[,] { { 0, 1.2, 1.3 }, { 1.2, 0, 1.1 }, { 1.2, 1.1, 0 } };


    [TestInitialize]
    public void TestInitialize()
    {
        stationA = new Station("A", Zone.A_CITY_CENTER);
        stationB = new Station("B", Zone.A_CITY_CENTER);
        stationC = new Station("C", Zone.A_CITY_CENTER);
        travelTimesMinutes = new int[,] { { 0, 5, 7 }, { 4, 0, 3 }, { 6, 4, 0 } };
        travelPricesKM = new double[,] { { 0, 1.2, 1.3 }, { 1.2, 0, 1.1 }, { 1.2, 1.1, 0 } };
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Bus_TimesMatrixNotSquare_ThrowsArgumentException()
    {
        // Arrange
        List<Station> supportedStations = new List<Station> { stationA, stationB, stationC };

        travelTimesMinutes = new int[1, 2];

        // Act
        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Bus_PricesMatrixNotSquare_ThrowsArgumentException()
    {
        // 
        List<Station> supportedStations = new List<Station> { stationA, stationB, stationC };

        travelPricesKM = new double[1, 2];

        // Act
        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Bus_MoreTravelTimesThanStations_ThrowsArgumentException()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB };

        // Act
        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        // Assert        
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Bus_DuplicatesInSupportedStations_ThrowsArgumentException()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB, stationA };

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

        List<Station> supportedStations = new List<Station> { stationA, stationB, stationC };

        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        // Act
        double result = bus.getCommuteDurationMinutes(new Station("Unsupported station", Zone.B_SUBURBS), stationA);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void getCommuteDurationMinutes_UnsupportedDestinationStation_ThrowsArgumentException()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB, stationC };

        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        // Act
        double result = bus.getCommuteDurationMinutes(stationA, new Station("Unsupported station", Zone.B_SUBURBS));

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [DataRow(0, 2)]
    [DataRow(0, 3)]
    [DataRow(0, 4)]
    [DataRow(1, 3)]
    [DataRow(1, 4)]
    [DataRow(2, 3)]
    [DataRow(3, 2)]
    [DataRow(4, 2)]
    [DataRow(4, 1)]
    public void getCommuteDurationAndPrice_ValidStationsDataRow_ReturnsExpectedValues(int startIndex, int destinationIndex)
    {
        // Arrange

        Station startingStation;
        Station destinationStation;
        double expectedTravelTime;
        double expectedTravelPrice;

        (startingStation, destinationStation, expectedTravelTime, expectedTravelPrice) = StaticTestData.GetBusTravelData(startIndex, destinationIndex);

        Bus bus = new Bus(StaticTestData.orderedBusStations, StaticTestData.busTravelTimesMinutes, StaticTestData.busTravelPricingKM);

        // Act

        double resultTravelTime = bus.getCommuteDurationMinutes(startingStation, destinationStation);
        double resultTravelPrice = bus.getPriceKM(startingStation, destinationStation);

        // Assert

        Assert.AreEqual(expectedTravelTime, resultTravelTime);
        Assert.AreEqual(expectedTravelPrice, resultTravelPrice);

    }



    [TestMethod]
    [DynamicData(nameof(ValidBusTravelData))]
    public void getCommuteDurationAndPrice_ValidStationsDynamicData_ReturnsExpectedValues(Station startingStation, Station destinationStation, double expectedTravelTime, double expectedTravelPrice)
    {
        // Arrange

        Bus bus = new Bus(StaticTestData.orderedBusStations, StaticTestData.busTravelTimesMinutes, StaticTestData.busTravelPricingKM);

        // Act

        double resultTravelTime = bus.getCommuteDurationMinutes(startingStation, destinationStation);

        // Assert

        Assert.AreEqual(expectedTravelTime, resultTravelTime);

    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void getDestinationStations_UnsupportedStartingStation_ThrowsArgumentExcpetion()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB, stationC };

        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        Station stationD = new Station("Unsupported station", Zone.B_SUBURBS);

        // Act
        SortedSet<Station> result = bus.getDestinationStations(stationD);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    public void getDestinationStations_SupportedStartingStation_ReturnsTwoStations()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB, stationC };

        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        Station stationD = new Station("Unsupported station", Zone.B_SUBURBS);

        var EXPECTED_RESAULT = new SortedSet<Station>(new StationLexicographicComparer()) { stationB, stationC };

        // Act
        SortedSet<Station> result = bus.getDestinationStations(stationA);

        // Assert        
        CollectionAssert.AreEqual(EXPECTED_RESAULT, result);
    }

    [TestMethod]
    public void getDestinationStations_OnlyOneStationSupported_ReturnsEmptyCollection()
    {
        // Arrange


        List<Station> supportedStations = new List<Station> { stationA };

        travelTimesMinutes = new int[,] { { 0 } };
        travelPricesKM = new double[,] { { 0 } };

        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        // Act
        SortedSet<Station> result = bus.getDestinationStations(stationA);

        // Assert        
        Assert.AreEqual(result.Count, 0);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void getPriceKM_UnsupportedStartingStation_ThrowsArgumentException()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB };

        travelTimesMinutes = new int[,] { { 0, 5 }, { 4, 0 } };
        travelPricesKM = new double[,] { { 0, 1.2 }, { 1.3, 0 } };

        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        Station stationD = new Station("Unsupported station", Zone.B_SUBURBS);

        // Act
        double result = bus.getPriceKM(stationD, stationA);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void getPriceKM_UnsupportedDestinationStation_ThrowsArgumentException()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB };

        travelTimesMinutes = new int[,] { { 0, 5 }, { 4, 0 } };
        travelPricesKM = new double[,] { { 0, 1.2 }, { 1.3, 0 } };

        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        Station stationD = new Station("Unsupported station", Zone.B_SUBURBS);

        // Act
        double result = bus.getPriceKM(stationA, stationD);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void getPriceKM_SameStartingAndDestination_ThrowsArgumentException()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB };

        travelTimesMinutes = new int[,] { { 0, 5 }, { 4, 0 } };
        travelPricesKM = new double[,] { { 0, 1.2 }, { 1.3, 0 } };

        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        // Act
        double result = bus.getPriceKM(stationA, stationA);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [DynamicData(nameof(ValidBusTravelData))]
    public void getPriceKM_CommuteDurationAndPrice_ValidStationsDynamicData_ReturnsExpectedValues(Station startingStation, Station destinationStation, double expectedTravelTime, double expectedTravelPrice)
    {
        // Arrange

        Bus bus = new Bus(StaticTestData.orderedBusStations, StaticTestData.busTravelTimesMinutes, StaticTestData.busTravelPricingKM);

        // Act

        double resultTravelTime = bus.getCommuteDurationMinutes(startingStation, destinationStation);
        double resultTravelPrice = bus.getPriceKM(startingStation, destinationStation);

        // Assert

        Assert.AreEqual(expectedTravelPrice, resultTravelPrice);

    }

    [TestMethod]
    public void getStartingStations_ThreeStations_ContainsThreeStations()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB, stationC };

        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        var EXPECTED_RESAULT = new SortedSet<Station>(new StationLexicographicComparer()) { stationA, stationB, stationC };

        // Act
        SortedSet<Station> result = bus.getStartingStations();

        // Assert        
        CollectionAssert.AreEqual(EXPECTED_RESAULT, result);
    }

    [TestMethod]
    public void getStartingStations_NotOrderedStations_LexicographicallySortedStations()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationC, stationA, stationB };

        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        var EXPECTED_RESAULT = new SortedSet<Station>(new StationLexicographicComparer()) { stationA, stationB, stationC };

        // Act
        SortedSet<Station> result = bus.getStartingStations();

        // Assert
        CollectionAssert.AreEqual(EXPECTED_RESAULT, result);
    }
}
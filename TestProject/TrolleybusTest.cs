using System.Diagnostics.CodeAnalysis;
using OptiRoute;

namespace TestProject;

[TestClass]
[ExcludeFromCodeCoverage]
public sealed class TrolleybusTest
{

    public static IEnumerable<object[]> ValidTrolleybusTravelData
    {
        get
        {
            return new[]
            {
                new object[] {StaticTestData.vogosca, StaticTestData.bare, 7, 2.4},
                new object[] {StaticTestData.sutjeska, StaticTestData.bare, 10, 1.2},
                new object[] {StaticTestData.bare, StaticTestData.grbavica, 39, 1.2},
                new object[] {StaticTestData.grbavica, StaticTestData.sutjeska, 29, 1.2}
            };
        }
    }

    private Station stationA = new Station("A", Zone.A_CITY_CENTER);
    private Station stationB = new Station("B", Zone.B_SUBURBS);
    private Station stationC = new Station("C", Zone.C_OUTSKIRT);

    private int[,] travelTimesMinutes = new int[,] { { 0, 5, 7 }, { 4, 0, 3 }, { 6, 4, 0 } };

    private double pricePerZoneTraveledKM = 1.2;


    [TestInitialize]
    public void TestInitialize()
    {
        stationA = new Station("A", Zone.A_CITY_CENTER);
        stationB = new Station("B", Zone.B_SUBURBS);
        stationC = new Station("C", Zone.C_OUTSKIRT);
        travelTimesMinutes = new int[,] { { 0, 5, 7 }, { 4, 0, 3 }, { 6, 4, 0 } };
        pricePerZoneTraveledKM = 1.2;
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Trolleybus_TimesMatrixNotSquare_ThrowsArgumentException()
    {
        // Arrange
        List<Station> supportedStations = new List<Station> { stationA, stationB, stationC };

        travelTimesMinutes = new int[1, 2] { { 0, 5 } }; // Not square matrix

        // Act
        Trolleybus trolleybus = new Trolleybus(supportedStations, travelTimesMinutes, pricePerZoneTraveledKM);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Trolleybus_MoreTravelTimesThanStations_ThrowsArgumentException()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB };

        // Act
        Trolleybus Trolleybus = new Trolleybus(supportedStations, travelTimesMinutes, pricePerZoneTraveledKM);

        // Assert        
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Trolleybus_DuplicatesInSupportedStations_ThrowsArgumentException()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB, stationA };

        // Act
        Trolleybus Trolleybus = new Trolleybus(supportedStations, travelTimesMinutes, pricePerZoneTraveledKM);

        // Assert        
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void getCommuteDurationMinutes_UnsupportedStartingStation_ThrowsArgumentException()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB, stationC };

        Trolleybus Trolleybus = new Trolleybus(supportedStations, travelTimesMinutes, pricePerZoneTraveledKM);

        // Act
        double result = Trolleybus.getCommuteDurationMinutes(new Station("Unsupported station", Zone.B_SUBURBS), stationA);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void getCommuteDurationMinutes_UnsupportedDestinationStation_ThrowsArgumentException()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB, stationC };

        Trolleybus Trolleybus = new Trolleybus(supportedStations, travelTimesMinutes, pricePerZoneTraveledKM);

        // Act
        double result = Trolleybus.getCommuteDurationMinutes(stationA, new Station("Unsupported station", Zone.B_SUBURBS));

        // Assert
        // No assert needed, the exception is expected
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void getCommuteDurationMinutes_SameStartingAndDestination_ThrowsArgumentException()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB };

        travelTimesMinutes = new int[,] { { 0, 5 }, { 4, 0 } };
        pricePerZoneTraveledKM = 1.2;

        Trolleybus Trolleybus = new Trolleybus(supportedStations, travelTimesMinutes, pricePerZoneTraveledKM);

        // Act
        double result = Trolleybus.getCommuteDurationMinutes(stationA, stationA);

        // Assert
        // No assert needed, the exception is expected
    }


    [TestMethod]
    [DataRow(0, 1)]
    [DataRow(1, 0)]
    [DataRow(1, 3)]
    [DataRow(1, 4)]
    [DataRow(3, 1)]
    public void getCommuteDurationMinutes_ValidStationsDataRow_ReturnsExpectedValues(int startIndex, int destinationIndex)
    {
        // Arrange

       Station startingStation;
       Station destinationStation;
       int expectedTravelTimeMinutes;

       (startingStation, destinationStation, expectedTravelTimeMinutes) = StaticTestData.GetTrolleybusTravelData(startIndex, destinationIndex);

       Trolleybus trolleybus = new Trolleybus(StaticTestData.orderedTrolleybusStations, StaticTestData.trolleybusTravelTimesMinutes, pricePerZoneTraveledKM);

        // Act
        double resultTravelTime = trolleybus.getCommuteDurationMinutes(startingStation, destinationStation);

        // Assert
        Assert.AreEqual(expectedTravelTimeMinutes, resultTravelTime);

    }


    [TestMethod]
    [DynamicData(nameof(ValidTrolleybusTravelData))]
    public void getCommuteDurationKM_ValidStationsDynamicData_ReturnsExpectedValues(Station startingStation, Station destinationStation, double expectedTravelTime, double expectedTravelPrice)
    {
        // Arrange

        Trolleybus Trolleybus = new Trolleybus(StaticTestData.orderedTrolleybusStations, StaticTestData.trolleybusTravelTimesMinutes, pricePerZoneTraveledKM);

        // Act

        double resultTravelTime = Trolleybus.getCommuteDurationMinutes(startingStation, destinationStation);

        // Assert

        Assert.AreEqual(expectedTravelTime, resultTravelTime);

    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void getDestinationStations_UnsupportedStartingStation_ThrowsArgumentExcpetion()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB, stationC };

        Trolleybus Trolleybus = new Trolleybus(supportedStations, travelTimesMinutes, pricePerZoneTraveledKM);

        Station stationD = new Station("Unsupported station", Zone.B_SUBURBS);

        // Act
        SortedSet<Station> result = Trolleybus.getDestinationStations(stationD);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    public void getDestinationStations_SupportedStartingStation_ReturnsTwoStations()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB, stationC };

        Trolleybus Trolleybus = new Trolleybus(supportedStations, travelTimesMinutes, pricePerZoneTraveledKM);

        var EXPECTED_RESAULT = new SortedSet<Station>(new StationLexicographicComparer()) { stationB, stationC };

        // Act
        SortedSet<Station> result = Trolleybus.getDestinationStations(stationA);

        // Assert        
        CollectionAssert.AreEqual(EXPECTED_RESAULT, result);
    }

    [TestMethod]
    public void getDestinationStations_OnlyOneStationSupported_ReturnsEmptyCollection()
    {
        // Arrange
        List<Station> supportedStations = new List<Station> { stationA };

        travelTimesMinutes = new int[,] { { 0 } };

        Trolleybus Trolleybus = new Trolleybus(supportedStations, travelTimesMinutes, pricePerZoneTraveledKM);

        // Act
        SortedSet<Station> result = Trolleybus.getDestinationStations(stationA);

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

        Trolleybus Trolleybus = new Trolleybus(supportedStations, travelTimesMinutes, pricePerZoneTraveledKM);

        Station stationD = new Station("Unsupported station", Zone.B_SUBURBS);

        // Act
        double result = Trolleybus.getPriceKM(stationD, stationA);

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

        Trolleybus Trolleybus = new Trolleybus(supportedStations, travelTimesMinutes, pricePerZoneTraveledKM);

        Station stationD = new Station("Unsupported station", Zone.B_SUBURBS);

        // Act
        double result = Trolleybus.getPriceKM(stationA, stationD);

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

        Trolleybus Trolleybus = new Trolleybus(supportedStations, travelTimesMinutes, pricePerZoneTraveledKM);

        // Act
        double result = Trolleybus.getPriceKM(stationA, stationA);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [DynamicData(nameof(ValidTrolleybusTravelData))]
    public void getPriceKM_ValidStationsDynamicData_ReturnsExpectedValues(Station startingStation, Station destinationStation, double expectedTravelTime, double expectedTravelPrice)
    {
        // Arrange

        Trolleybus Trolleybus = new Trolleybus(StaticTestData.orderedTrolleybusStations, StaticTestData.trolleybusTravelTimesMinutes, pricePerZoneTraveledKM);

        // Act
        double resultTravelPrice = Trolleybus.getPriceKM(startingStation, destinationStation);

        // Assert

        Assert.AreEqual(expectedTravelPrice, resultTravelPrice);

    }

    [TestMethod]
    public void getStartingStations_ThreeStations_ContainsThreeStations()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB, stationC };

        Trolleybus Trolleybus = new Trolleybus(supportedStations, travelTimesMinutes, pricePerZoneTraveledKM);

        var EXPECTED_RESAULT = new SortedSet<Station>(new StationLexicographicComparer()) { stationA, stationB, stationC };

        // Act
        SortedSet<Station> result = Trolleybus.getStartingStations();

        // Assert        
        CollectionAssert.AreEqual(EXPECTED_RESAULT, result);
    }

    [TestMethod]
    public void getStartingStations_NotOrderedStations_LexicographicallySortedStations()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationC, stationA, stationB };

        Trolleybus Trolleybus = new Trolleybus(supportedStations, travelTimesMinutes, pricePerZoneTraveledKM);

        var EXPECTED_RESAULT = new SortedSet<Station>(new StationLexicographicComparer()) { stationA, stationB, stationC };

        // Act
        SortedSet<Station> result = Trolleybus.getStartingStations();

        // Assert
        CollectionAssert.AreEqual(EXPECTED_RESAULT, result);
    }
}
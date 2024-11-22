﻿using System.Runtime.CompilerServices;
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
    public void Bus_MoreTravelTimesThanStations_ThrowsArgumentException()
    {
        // Arrange

        Station stationA = new Station("First Station", Zone.A_CITY_CENTER);

        List<Station> supportedStations = new List<Station> { stationA };

        int[,] travelTimesMinutes = new int[,] { { 0, 5, 7 }, { 4, 0, 3 }, { 6, 4, 0 } };
        double[,] travelPricesKM = new double[,] { { 0, 1.2, 1.3 }, { 1.2, 0, 1.1 }, { 1.2, 1.1, 0 } };

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

        Station stationA = new Station("First Station", Zone.A_CITY_CENTER);
        Station stationB = new Station("Second Station", Zone.A_CITY_CENTER);

        List<Station> supportedStations = new List<Station> { stationA, stationB, stationA };

        int[,] travelTimesMinutes = new int[,] { { 0, 5 }, { 4, 0 } };
        double[,] travelPricesKM = new double[,] { { 0, 1.2 }, { 1.3, 0 } };

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

    public static IEnumerable<object[]> ValidBusTravelData
    {
        get
        {
            return new[]
            {
                new object[] {StaticTestData.vogosca, StaticTestData.ilijas, 6, 2.5},
                new object[] {StaticTestData.vogosca, StaticTestData.vogosca, 0, 0},
                new object[] {StaticTestData.sutjeska, StaticTestData.bare, 12, 1.2},
                new object[] {StaticTestData.bare, StaticTestData.ilijas, 12, 2.5},
            };
        }
    }

    [TestMethod]
    [DynamicData(nameof(ValidBusTravelData))]
    public void getCommuteDurationAndPrice_ValidStationsDynamicData_ReturnsExpectedValues(Station startingStation, Station destinationStation, double expectedTravelTime, double expectedTravelPrice)
    {
        // Arrange

        Bus bus = new Bus(StaticTestData.orderedBusStations, StaticTestData.busTravelTimesMinutes, StaticTestData.busTravelPricingKM);

        // Act

        double resultTravelTime = bus.getCommuteDurationMinutes(startingStation, destinationStation);
        double resultTravelPrice = bus.getPriceKM(startingStation, destinationStation);

        // Assert

        Assert.AreEqual(expectedTravelTime, resultTravelTime);
        Assert.AreEqual(expectedTravelPrice, resultTravelPrice);

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

        Station stationA = new Station("First Station", Zone.A_CITY_CENTER);

        List<Station> supportedStations = new List<Station> { stationA };

        int[,] travelTimesMinutes = new int[,] { { 0 } };
        double[,] travelPricesKM = new double[,] { { 0 } };

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

        Station stationA = new Station("First Station", Zone.A_CITY_CENTER);
        Station stationB = new Station("Second Station", Zone.B_SUBURBS);

        List<Station> supportedStations = new List<Station> { stationA, stationB };

        int[,] travelTimesMinutes = new int[,] { { 0, 5 }, { 4, 0 } };
        double[,] travelPricesKM = new double[,] { { 0, 1.2 }, { 1.3, 0 } };

        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        Station stationC = new Station("Third station", Zone.B_SUBURBS);

        // Act
        double result = bus.getPriceKM(stationC, stationA);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void getPriceKM_UnsupportedDestinationStation_ThrowsArgumentException()
    {
        // Arrange

        Station stationA = new Station("First Station", Zone.A_CITY_CENTER);
        Station stationB = new Station("Second Station", Zone.B_SUBURBS);

        List<Station> supportedStations = new List<Station> { stationA, stationB };

        int[,] travelTimesMinutes = new int[,] { { 0, 5 }, { 4, 0 } };
        double[,] travelPricesKM = new double[,] { { 0, 1.2 }, { 1.3, 0 } };

        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        Station stationC = new Station("Third station", Zone.B_SUBURBS);

        // Act
        double result = bus.getPriceKM(stationA, stationC);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void getPriceKM_SameStartingAndDestination_ThrowsArgumentException()
    {
        // Arrange

        Station stationA = new Station("First Station", Zone.A_CITY_CENTER);
        Station stationB = new Station("Second Station", Zone.B_SUBURBS);

        List<Station> supportedStations = new List<Station> { stationA, stationB };

        int[,] travelTimesMinutes = new int[,] { { 0, 5 }, { 4, 0 } };
        double[,] travelPricesKM = new double[,] { { 0, 1.2 }, { 1.3, 0 } };

        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        // Act
        double result = bus.getPriceKM(stationA, stationA);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    public void getPriceKM_Stations5MinutesAway_Returns1_2()
    {
        // Arrange

        const double EXPECTED_PRICE = 1.2;

        Station stationA = new Station("First Station", Zone.A_CITY_CENTER);
        Station stationB = new Station("Second Station", Zone.B_SUBURBS);

        List<Station> supportedStations = new List<Station> { stationA, stationB };

        int[,] travelTimesMinutes = new int[,] { { 0, 5 }, { 4, 0 } };
        double[,] travelPricesKM = new double[,] { { 0, 1.2 }, { 1.3, 0 } };

        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        // Act
        double result = bus.getPriceKM(stationA, stationB);

        // Assert
        Assert.AreEqual(result, EXPECTED_PRICE);
    }

    [TestMethod]
    public void getStartingStations_ThreeStations_ContainsThreeStations()
    {
        // Arrange

        Station stationA = new Station("First Station", Zone.A_CITY_CENTER);
        Station stationB = new Station("Second Station", Zone.B_SUBURBS);
        Station stationC = new Station("Third Station", Zone.C_OUTSKIRT);

        List<Station> supportedStations = new List<Station> { stationA, stationB, stationC };

        int[,] travelTimesMinutes = new int[,] { { 0, 5, 7 }, { 4, 0, 3 }, { 6, 4, 0 } };
        double[,] travelPricesKM = new double[,] { { 0, 1.2, 1.3 }, { 1.2, 0, 1.1 }, { 1.2, 1.1, 0 } };

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

        Station station1 = new Station("A station", Zone.A_CITY_CENTER);
        Station station2 = new Station("B station", Zone.B_SUBURBS);
        Station station3 = new Station("B Xtation", Zone.C_OUTSKIRT);

        List<Station> supportedStations = new List<Station> { station3, station1, station2 };

        int[,] travelTimesMinutes = new int[,] { { 0, 5, 7 }, { 4, 0, 3 }, { 6, 4, 0 } };
        double[,] travelPricesKM = new double[,] { { 0, 1.2, 1.3 }, { 1.2, 0, 1.1 }, { 1.2, 1.1, 0 } };

        Bus bus = new Bus(supportedStations, travelTimesMinutes, travelPricesKM);

        var EXPECTED_RESAULT = new SortedSet<Station>(new StationLexicographicComparer()) { station1, station2, station3 };

        // Act
        SortedSet<Station> result = bus.getStartingStations();

        // Assert
        CollectionAssert.AreEqual(EXPECTED_RESAULT, result);
    }
}
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using OptiRoute;

namespace TestProject;

[TestClass]
[ExcludeFromCodeCoverage]
public sealed class TramTest
{

    public static IEnumerable<object[]> ValidTramTravelData
    {
        get
        {
            return new[]
            {
                new object[] {StaticTestData.predsjednistvo, StaticTestData.tehnickaSkola, 1, 1, 1, 1},
                new object[] {StaticTestData.predsjednistvo, StaticTestData.kampus, 2, 3, 4, 3},
                new object[] {StaticTestData.tehnickaSkola, StaticTestData.ilidza, 3, 4, 9, 12},
            };
        }
    }
    private Station stationA = new Station("A", Zone.A_CITY_CENTER);
    private Station stationB = new Station("B", Zone.A_CITY_CENTER);
    private Station stationC = new Station("C", Zone.A_CITY_CENTER);


    [TestInitialize]
    public void TestInitialize()
    {
        stationA = new Station("A", Zone.A_CITY_CENTER);
        stationB = new Station("B", Zone.A_CITY_CENTER);
        stationC = new Station("C", Zone.A_CITY_CENTER);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Tram_DuplicatesInSupportedStations_ThrowsArgumentException()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB, stationA, stationC };

        // Act
        Tram tram = new Tram(supportedStations);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Tram_NegativeTimeBetweenStations_ThrowsArgumentException()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB, stationC };

        // Act
        Tram tram = new Tram(supportedStations, -1);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Tram_NegativePricePerStationKM_ThrowsArgumentException()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB, stationC };

        // Act
        Tram tram = new Tram(supportedStations, 5, -1);

        // Assert
        // No assert needed, the exception is expected
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void getCommuteDurationMinutes_UnsupportedStartingStation_ThrowsArgumentException()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationC, stationA, stationB };

        Tram tram = new Tram(supportedStations, 5, 0.5);

        // Act
        double commuteDurationMinutes = tram.getCommuteDurationMinutes(new Station("D", Zone.A_CITY_CENTER), stationB);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void getCommuteDurationMinutes_UnsupportedDestinationStation_ThrowsArgumentException()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationC, stationA, stationB };

        Tram tram = new Tram(supportedStations, 5, 0.5);

        // Act
        double commuteDurationMinutes = tram.getCommuteDurationMinutes(stationA, new Station("D", Zone.A_CITY_CENTER));

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void getCommuteDurationMinutes_SameStartingAndDestinationStation_ThrowsArgumentException()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationC, stationA, stationB };

        Tram tram = new Tram(supportedStations, 5, 0.5);

        // Act
        double commuteDurationMinutes = tram.getCommuteDurationMinutes(stationA, stationA);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [DataRow(5, 1, 0.5, 4.0)]
    [DataRow(2, 2, 0.5, 2.0)]
    [DataRow(3, 3, 1.0, 6.0)]
    [DataRow(100, 4, 0.7, 396.0)]
    [DataRow(1000, 5, 2.1, 4995.00)]
    [DataRow(1234, 6, 3.5, 7398.0)]
    public void getCommuteDurationMinutes_ValidStationsDataRow_ReturnsExpectedValues(int numberOfStations, int timeBetweenStationsMinutes, double pricePerStationKM, double expectedCommuteDurationMinutes)
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA};

        for (int i = 0; i < numberOfStations-2; i++)
        {
            supportedStations.Add(new Station($"Station{i}", Zone.A_CITY_CENTER));
        }

        supportedStations.Add(stationC);


        Tram tram = new Tram(supportedStations, timeBetweenStationsMinutes, pricePerStationKM);

        // Act
        double commuteDurationMinutes = tram.getCommuteDurationMinutes(stationA, stationC);

        // Assert
        Assert.AreEqual(expectedCommuteDurationMinutes, commuteDurationMinutes);
    }



    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void getDestinationStations_UnsupportedStartingStation_ThrowsArgumentExcpetion()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB, stationC };

        Tram tram = new Tram(supportedStations);

        Station stationD = new Station("Unsupported station", Zone.B_SUBURBS);

        // Act
        SortedSet<Station> result = tram.getDestinationStations(stationD);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    public void getDestinationStations_SupportedStartingStation_ReturnsTwoStations()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB, stationC };

        Tram tram = new Tram(supportedStations);

        var EXPECTED_RESAULT = new SortedSet<Station>(new StationLexicographicComparer()) { stationB, stationC };

        // Act
        SortedSet<Station> result = tram.getDestinationStations(stationA);

        // Assert        
        CollectionAssert.AreEqual(EXPECTED_RESAULT, result);
    }

    [TestMethod]
    public void getDestinationStations_OnlyOneStationSupported_ReturnsEmptyCollection()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA };

        Tram tram = new Tram(supportedStations);

        // Act
        SortedSet<Station> result = tram.getDestinationStations(stationA);

        // Assert
        CollectionAssert.AreEqual(new List<Station>(), result.ToList());
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void getPriceKM_UnsupportedStartingStation_ThrowsArgumentException()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB };

        Tram tram = new Tram(supportedStations);

        Station stationD = new Station("Unsupported station", Zone.B_SUBURBS);

        // Act
        double result = tram.getPriceKM(stationD, stationA);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void getPriceKM_UnsupportedDestinationStation_ThrowsArgumentException()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB };

        Tram tram = new Tram(supportedStations);

        Station stationD = new Station("Unsupported station", Zone.B_SUBURBS);

        // Act
        double result = tram.getPriceKM(stationA, stationD);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void getPriceKM_SameStartingAndDestination_ThrowsArgumentException()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationA, stationB };

        Tram tram = new Tram(supportedStations);

        // Act
        double result = tram.getPriceKM(stationA, stationA);

        // Assert
        // No assert needed, the exception is expected
    }

    [TestMethod]
    [DynamicData(nameof(ValidTramTravelData))]
    public void getPriceKM_ValidStationsDynamicData_ReturnsExpectedValues(Station startingStation, Station destinationStation, double pricePerStationKM, int timeBetweenStationsMinutes, double expectedTravelPriceKM, double expectedTravelTimeMinutes)
    {
        // Arrange

        Tram tram = new Tram(StaticTestData.orderedTramStations, timeBetweenStationsMinutes, pricePerStationKM);

        // Act
        double resultTravelPrice = tram.getPriceKM(startingStation, destinationStation);

        // Assert

        Assert.AreEqual(expectedTravelPriceKM, resultTravelPrice);

    }


    [TestMethod]
    public void GetStartingStations_ReturnsStationsInLexicographicOrder()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationC, stationA, stationB };

        Tram tram = new Tram(supportedStations);

        // Act
        SortedSet<Station> startingStations = tram.getStartingStations();

        // Assert
        CollectionAssert.AreEqual(new List<Station> { stationA, stationB, stationC }, startingStations.ToList());
    }

    [TestMethod]
    public void GetStartingStations_NotOrderedStations_LexicographicallySortedStations(){
        // Arrange

        List<Station> supportedStations = new List<Station> { stationB, stationC, stationA };

        Tram tram = new Tram(supportedStations);

        // Act
        SortedSet<Station> startingStations = tram.getStartingStations();

        // Assert
        CollectionAssert.AreEqual(new List<Station> { stationA, stationB, stationC }, startingStations.ToList());
    }

    [TestMethod]
    public void PricePerStationKM_ReturnsCorrectValue()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationC, stationA, stationB };

        Tram tram = new Tram(supportedStations, 5, 0.5);

        // Act
        double pricePerStationKM = tram.PricePerStationKM;

        // Assert
        Assert.AreEqual(0.5, pricePerStationKM);
    }

    [TestMethod]
    public void PricePerStationKM_SetterWorksCorrectly()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationC, stationA, stationB };

        Tram tram = new Tram(supportedStations, 5, 0.5);

        // Act
        tram.PricePerStationKM = 1.0;
        double updatedPricePerStationKM = tram.PricePerStationKM;

        // Assert
        Assert.AreEqual(1.0, updatedPricePerStationKM);
    }

    [TestMethod]
    public void TimeBetweenStationsMinutes_ReturnsCorrectValue()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationC, stationA, stationB };

        Tram tram = new Tram(supportedStations, 5, 0.5);

        // Act
        int timeBetweenStationsMinutes = tram.TimeBetweenStationsMinutes;

        // Assert
        Assert.AreEqual(5, timeBetweenStationsMinutes);
    }

    [TestMethod]
    public void TimeBetweenStationsMinutes_SetterWorksCorrectly()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationC, stationA, stationB };

        Tram tram = new Tram(supportedStations, 5, 0.5);

        // Act
        tram.TimeBetweenStationsMinutes = 10;
        int updatedTimeBetweenStationsMinutes = tram.TimeBetweenStationsMinutes;

        // Assert
        Assert.AreEqual(10, updatedTimeBetweenStationsMinutes);
    }

    [TestMethod]
    public void SupportedStations_ReturnsCorrectValue()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationC, stationA, stationB };

        Tram tram = new Tram(supportedStations, 5, 0.5);

        // Act
        List<Station> actualSupportedStations = tram.SupportedStations;

        // Assert
        CollectionAssert.AreEqual(supportedStations, actualSupportedStations);
    }

    [TestMethod]
    public void SupportedStations_SetterWorksCorrectly()
    {
        // Arrange

        List<Station> supportedStations = new List<Station> { stationC, stationA, stationB };

        Tram tram = new Tram(supportedStations, 5, 0.5);

        // Act
        List<Station> newSupportedStations = new List<Station> { stationA, stationB, stationC };
        tram.SupportedStations = newSupportedStations;
        List<Station> updatedSupportedStations = tram.SupportedStations;

        // Assert
        CollectionAssert.AreEqual(newSupportedStations, updatedSupportedStations);
    }



}
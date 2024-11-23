using Moq;
using OptiRoute;

namespace TestProject;

[TestClass]
public class ITransportationDataSourceTest
{
    [TestMethod]
    public void TestGetBusData()
    {
        var mockDataSource = new Mock<ITransportationDataSource>();

        var stationA = new Station("A", Zone.A_CITY_CENTER);
        var stationB = new Station("B", Zone.B_SUBURBS);

        var stations = new List<Station> { stationA, stationB };
        var intMatrix = new int[2, 2];
        var doubleMatrix = new double[2, 2];
        mockDataSource.Setup(ds => ds.getBusData()).Returns((stations, intMatrix, doubleMatrix));

        var result = mockDataSource.Object.getBusData();

        var bus = new Bus(result.Item1, result.Item2, result.Item3);
        Assert.IsNotNull(bus);

        CollectionAssert.AreEqual(stations, bus.getStartingStations().ToList());

    }

    [TestMethod]
    public void TestGetTramData()
    {
        var mockDataSource = new Mock<ITransportationDataSource>();
        var stationA = new Station("A", Zone.A_CITY_CENTER);
        var stationB = new Station("B", Zone.B_SUBURBS);

        var stations = new List<Station> { stationA, stationB };
        var intValue = 5;
        var doubleValue = 10.5;
        mockDataSource.Setup(ds => ds.getTramData()).Returns((stations, intValue, doubleValue));

        var result = mockDataSource.Object.getTramData();

        var tram = new Tram(result.Item1, result.Item2, result.Item3);
        Assert.IsNotNull(tram);

        CollectionAssert.AreEqual(stations, tram.getStartingStations().ToList());
    }

    [TestMethod]
    public void TestGetTrolleybusData()
    {
        var mockDataSource = new Mock<ITransportationDataSource>();
        var stationA = new Station("A", Zone.A_CITY_CENTER);
        var stationB = new Station("B", Zone.B_SUBURBS);

        var stations = new List<Station> { stationA, stationB };
        var intMatrix = new int[2, 2];
        var doubleValue = 15.5;
        mockDataSource.Setup(ds => ds.getTrolleybusData()).Returns((stations, intMatrix, doubleValue));

        var result = mockDataSource.Object.getTrolleybusData();

        var trolleybus = new Trolleybus(result.Item1, result.Item2, result.Item3);
        Assert.IsNotNull(trolleybus);

        CollectionAssert.AreEqual(stations, trolleybus.getStartingStations().ToList());
    }
}

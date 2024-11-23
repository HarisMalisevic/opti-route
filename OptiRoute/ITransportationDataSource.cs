using System;

namespace OptiRoute;

public interface ITransportationDataSource
{

    public (List<Station>, int[,], double[,]) getBusData();
    public (List<Station>, int, double) getTramData();
    public (List<Station>, int[,], double) getTrolleybusData();

}

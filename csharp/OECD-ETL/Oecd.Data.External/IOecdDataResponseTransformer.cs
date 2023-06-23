using Oecd.Common;
using Oecd.Data.External.Dto;

namespace Oecd.Data.External;

public interface IOecdDataResponseTransformer
{
    GenderEnt1 Transform(OecdDataResponse response, Dictionary<int, OecdDataResponseStructureDimensionSeries> seriesStructure, int[] dimensions, OecdDataResponseDataSetSeriesValue seriesValue, int observation, double? observationData);
}

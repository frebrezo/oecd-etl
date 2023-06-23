using Oecd.Common;
using Oecd.Data.External.Dto;

namespace Oecd.Data.External;

public interface IOecdDataStructureTransformer
{
    GenderEnt1Dimension TransformDimension(OecdDataStructure genderEnt1Structure, KeyValuePair<string, OecdDataResponseDataSetSeriesValue> seriesKeyValue);
    GenderEnt1Attribute TransformAttribute(OecdDataStructure genderEnt1Structure, KeyValuePair<string, OecdDataResponseDataSetSeriesValue> seriesKeyValue);
    GenderEnt1Observation TransformObservation(OecdDataStructure genderEnt1Structure, KeyValuePair<int, List<double?>> observationKeyValue);
}

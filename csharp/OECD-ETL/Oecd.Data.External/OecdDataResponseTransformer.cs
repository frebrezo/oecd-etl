using Oecd.Common;
using Oecd.Data.External.Dto;

namespace Oecd.Data.External;

public class OecdDataResponseTransformer : IOecdDataResponseTransformer
{
    public GenderEnt1 Transform(OecdDataResponse response, Dictionary<int, OecdDataResponseStructureDimensionSeries> seriesStructure, int[] dimensions, OecdDataResponseDataSetSeriesValue seriesValue, int observation, double? observationData)
    {
        GenderEnt1 data = new GenderEnt1();

        for (int i = 0; i < dimensions.Length; ++i)
        {
            int dimension = dimensions[i];

            string dimensionValueId = seriesStructure[i].Values[dimension].Id;
            string dimensionValueName = seriesStructure[i].Values[dimension].Name;

            switch (seriesStructure[i].Id)
            {
                case "LOCATION":
                    data.CountryId = dimensionValueId;
                    data.CountryName = dimensionValueName;
                    break;
                case "INDICATOR":
                    data.IndicatorId = dimensionValueId;
                    data.IndicatorName = dimensionValueName;
                    break;
                case "SEX":
                    data.SexId = dimensionValueId;
                    data.SexName = dimensionValueName;
                    break;
                case "AGE":
                    data.AgeId = dimensionValueId;
                    data.AgeName = dimensionValueName;
                    break;
                default:
                    Console.WriteLine($"Unsupported dimension [{seriesStructure[i].Values[dimension].Name}].");
                    break;
            }
        }

        for (int i = 0; i < seriesValue.Attributes.Count; ++i)
        {
            if (seriesValue.Attributes[i] == null) continue;

            string attributeId = response.Structure.Attributes.Series[i].Values[seriesValue.Attributes[i].Value].Id;
            string attributeName = response.Structure.Attributes.Series[i].Values[seriesValue.Attributes[i].Value].Name;

            switch (response.Structure.Attributes.Series[i].Id)
            {
                case "TIME_FORMAT":
                    data.TimeFormatId = attributeId;
                    data.TimeFormatName = attributeName;
                    break;
                case "UNIT":
                    data.UnitId = attributeId;
                    data.UnitName = attributeName;
                    break;
                case "POWERCODE":
                    data.UnitMultiplierId = attributeId;
                    data.UnitMultiplierName = attributeName;
                    break;
                case "REFERENCEPERIOD":
                    data.ReferencePeriodId = attributeId;
                    data.ReferencePeriodName = attributeName;
                    break;
            }
        }

        OecdDataResponseValue observationValue = response.Structure.Dimensions.Observation[0].Values[observation];

        data.Year = Convert.ToInt32(observationValue.Id);
        data.Value = observationData;

        return data;
    }
}

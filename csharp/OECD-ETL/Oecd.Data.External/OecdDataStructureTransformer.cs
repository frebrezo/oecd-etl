using Oecd.Common;
using Oecd.Data.External.Dto;

namespace Oecd.Data.External;

public class OecdDataStructureTransformer : IOecdDataStructureTransformer
{
    public GenderEnt1Dimension TransformDimension(OecdDataStructure genderEnt1Structure, KeyValuePair<string, OecdDataResponseDataSetSeriesValue> seriesKeyValue)
    {
        GenderEnt1Dimension dimension = new GenderEnt1Dimension();

        string dimenstionKeyString = seriesKeyValue.Key;
        List<int> dimensionKeyIndexes = dimenstionKeyString.Split(':').Select(x => int.Parse(x)).ToList();
        for (int i = 0; i < dimensionKeyIndexes.Count; ++i)
        {
            string dimensionSeriesId = genderEnt1Structure.DimensionSeries[i].Id;
            OecdDataValue dimensionSeriesValue = genderEnt1Structure.DimensionSeriesValues[dimensionSeriesId][dimensionKeyIndexes[i]];
            switch (dimensionSeriesId)
            {
                case "LOCATION":
                    dimension.CountryId = dimensionSeriesValue.Id;
                    dimension.CountryName = dimensionSeriesValue.Name;
                    break;
                case "INDICATOR":
                    dimension.IndicatorId = dimensionSeriesValue.Id;
                    dimension.IndicatorName = dimensionSeriesValue.Name;
                    break;
                case "SEX":
                    dimension.SexId = dimensionSeriesValue.Id;
                    dimension.SexName = dimensionSeriesValue.Name;
                    break;
                case "AGE":
                    dimension.AgeId = dimensionSeriesValue.Id;
                    dimension.AgeName = dimensionSeriesValue.Name;
                    break;
                default:
                    break;
            }
        }

        return dimension;
    }

    public GenderEnt1Attribute TransformAttribute(OecdDataStructure genderEnt1Structure, KeyValuePair<string, OecdDataResponseDataSetSeriesValue> seriesKeyValue)
    {
        GenderEnt1Attribute attribute = new GenderEnt1Attribute();

        for (int i = 0; i < seriesKeyValue.Value.Attributes.Count; ++i)
        {
            string attributeSeriesId = genderEnt1Structure.AttributeSeries[i].Id;
            if (seriesKeyValue.Value.Attributes[i] != null)
            {
                OecdDataValue attributeSeriesValue = genderEnt1Structure.AttributeSeriesValues[attributeSeriesId][seriesKeyValue.Value.Attributes[i].Value];
                switch (attributeSeriesId)
                {
                    case "TIME_FORMAT":
                        attribute.TimeFormatId = attributeSeriesValue.Id;
                        attribute.TimeFormatName = attributeSeriesValue.Name;
                        break;
                    case "UNIT":
                        attribute.UnitId = attributeSeriesValue.Id;
                        attribute.UnitName = attributeSeriesValue.Name;
                        break;
                    case "POWERCODE":
                        attribute.UnitMultiplierId = attributeSeriesValue.Id;
                        attribute.UnitMultiplierName = attributeSeriesValue.Name;
                        break;
                    case "REFERENCEPERIOD":
                        attribute.ReferencePeriodId = attributeSeriesValue.Id;
                        attribute.ReferencePeriodName = attributeSeriesValue.Name;
                        break;
                    default:
                        break;
                }
            }
        }

        return attribute;
    }

    public GenderEnt1Observation TransformObservation(OecdDataStructure genderEnt1Structure, KeyValuePair<int, List<double?>> observationKeyValue)
    {
        string dimensionObservationId = genderEnt1Structure.DimensionObservationValues[genderEnt1Structure.DimensionObservations[0].Id][observationKeyValue.Key].Id;

        string? attributeObservationName = null;
        if (observationKeyValue.Value[1] != null)
        {
            attributeObservationName = genderEnt1Structure.AttributeObservationValues[genderEnt1Structure.AttributeObservations[0].Id][(int)observationKeyValue.Value[1].Value].Name;
        }

        GenderEnt1Observation observation = new GenderEnt1Observation
        {
            Year = int.Parse(dimensionObservationId),
            Value = observationKeyValue.Value[0].Value,
            Status = attributeObservationName
        };

        return observation;
    }
}

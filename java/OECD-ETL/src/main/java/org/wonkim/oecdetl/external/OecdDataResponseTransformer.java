package org.wonkim.oecdetl.external;

import org.wonkim.oecdetl.external.dto.OecdDataResponse;
import org.wonkim.oecdetl.external.dto.OecdDataResponseDataSetSeriesValue;
import org.wonkim.oecdetl.external.dto.OecdDataResponseStructureDimensionSeries;
import org.wonkim.oecdetl.external.dto.OecdDataResponseValue;
import org.wonkim.oecdetl.model.GenderEnt1;

import java.util.List;
import java.util.Map;

public class OecdDataResponseTransformer {
    public GenderEnt1 Transform(OecdDataResponse response, Map<Integer, OecdDataResponseStructureDimensionSeries> seriesStructure, List<Integer> dimensions, OecdDataResponseDataSetSeriesValue seriesValue, Integer observation, Double observationData)
    {
        GenderEnt1 data = new GenderEnt1();

        for (int i = 0; i < dimensions.size(); ++i)
        {
            int dimension = dimensions.get(i);

            String dimensionValueId = seriesStructure.get(i).getValues().get(dimension).getId();
            String dimensionValueName = seriesStructure.get(i).getValues().get(dimension).getName();

            switch (seriesStructure.get(i).getId())
            {
                case "LOCATION":
                    data.setCountryId(dimensionValueId);
                    data.setCountryName(dimensionValueName);
                    break;
                case "INDICATOR":
                    data.setIndicatorId(dimensionValueId);
                    data.setIndicatorName(dimensionValueName);
                    break;
                case "SEX":
                    data.setSexId(dimensionValueId);
                    data.setSexName(dimensionValueName);
                    break;
                case "AGE":
                    data.setAgeId(dimensionValueId);
                    data.setAgeName(dimensionValueName);
                    break;
                default:
                    System.out.println(String.format("Unsupported dimension [%s].", seriesStructure.get(i).getValues().get(dimension).getName()));
                    break;
            }
        }

        for (int i = 0; i < seriesValue.getAttributes().size(); ++i)
        {
            if (seriesValue.getAttributes().get(i) == null) continue;

            String attributeId = response.getStructure().getAttributes().getSeries().get(i).getValues().get(seriesValue.getAttributes().get(i)).getId();
            String attributeName = response.getStructure().getAttributes().getSeries().get(i).getValues().get(seriesValue.getAttributes().get(i)).getName();

            switch (response.getStructure().getAttributes().getSeries().get(i).getId())
            {
                case "TIME_FORMAT":
                    data.setTimeFormatId(attributeId);
                    data.setTimeFormatName(attributeName);
                    break;
                case "UNIT":
                    data.setUnitId(attributeId);
                    data.setUnitName(attributeName);
                    break;
                case "POWERCODE":
                    data.setUnitMultiplierId(attributeId);
                    data.setUnitMultiplierName(attributeName);
                    break;
                case "REFERENCEPERIOD":
                    data.setReferencePeriodId(attributeId);
                    data.setReferencePeriodName(attributeName);
                    break;
            }
        }

        OecdDataResponseValue observationValue = response.getStructure().getDimensions().getObservation().get(0).getValues().get(observation);

        data.setYear(Integer.parseInt(observationValue.getId()));
        data.setValue(observationData);

        return data;
    }
}

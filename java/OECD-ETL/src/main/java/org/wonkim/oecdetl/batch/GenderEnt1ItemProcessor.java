package org.wonkim.oecdetl.batch;

import org.springframework.batch.item.ItemProcessor;
import org.wonkim.oecdetl.external.OecdDataResponseTransformer;
import org.wonkim.oecdetl.external.dto.OecdDataResponse;
import org.wonkim.oecdetl.external.dto.OecdDataResponseDataSet;
import org.wonkim.oecdetl.external.dto.OecdDataResponseDataSetSeriesValue;
import org.wonkim.oecdetl.external.dto.OecdDataResponseStructureDimensionSeries;
import org.wonkim.oecdetl.model.GenderEnt1;

import java.time.Duration;
import java.time.Instant;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Map;
import java.util.function.Function;
import java.util.stream.Collectors;

// https://docs.spring.io/spring-batch/docs/current/reference/html/processor.html
public class GenderEnt1ItemProcessor implements ItemProcessor<OecdDataResponse, List<GenderEnt1>> {
    private OecdDataResponseTransformer _transformer;

    public GenderEnt1ItemProcessor(OecdDataResponseTransformer transformer) {
        _transformer = transformer;
    }

    @Override
    public List<GenderEnt1> process(OecdDataResponse item) {
        List<GenderEnt1> result = new ArrayList<GenderEnt1>();

        // https://www.baeldung.com/java-measure-elapsed-time
        Instant startTime = Instant.now();
        // https://www.baeldung.com/java-list-to-map
        Map<Integer, OecdDataResponseStructureDimensionSeries> seriesStructure = item.getStructure().getDimensions().getSeries().stream().collect(
                Collectors.toMap(OecdDataResponseStructureDimensionSeries::getKeyPosition, Function.identity()));
        for (OecdDataResponseDataSet dataSet : item.getDataSets()) {
            for (String dimensionString : dataSet.getSeries().keySet()) {
                List<Integer> dimensions = Arrays.stream(dimensionString.split(":")).map(Integer::parseInt).collect(Collectors.toList());

                OecdDataResponseDataSetSeriesValue seriesValue = dataSet.getSeries().get(dimensionString);

                for (Integer observation : seriesValue.getObservations().keySet()) {
                    for (Double observationData : seriesValue.getObservations().get(observation)) {
                        if (observationData == null) continue;

                        GenderEnt1 data = _transformer.Transform(item, seriesStructure, dimensions, seriesValue, observation, observationData);

                        result.add(data);
                    }
                }
            }
        }
        Instant endTime = Instant.now();
        double totalSeconds = Duration.between(startTime, endTime).toMillis() / 1000;

        System.out.println(String.format("TransformOecdDataResponseToGenderEnt1 complete [%f] s.", totalSeconds));
        return result;
    }
}

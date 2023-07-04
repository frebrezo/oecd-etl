package org.wonkim.oecdetl.batch;

import org.apache.commons.lang3.time.StopWatch;
import org.springframework.batch.item.Chunk;
import org.springframework.batch.item.ItemWriter;
import org.springframework.jdbc.core.namedparam.NamedParameterJdbcTemplate;
import org.springframework.jdbc.core.namedparam.SqlParameterSource;
import org.springframework.jdbc.core.namedparam.SqlParameterSourceUtils;
import org.wonkim.oecdetl.model.GenderEnt1;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.List;
import java.util.stream.Collectors;

public class GenderEnt1ItemWriter implements ItemWriter<List<GenderEnt1>> {
    // https://stackoverflow.com/questions/3034186/in-java-is-there-a-way-to-write-a-string-literal-without-having-to-escape-quote
    private final String INSERT_SQL = """
INSERT INTO [dbo].[GenderEnt1Land]
([CountryId],[CountryName],[IndicatorId],[IndicatorName],[SexId],[SexName],[AgeId],[AgeName],[TimeFormatId],[TimeFormatName],[UnitId],[UnitName],[UnitMultiplierId],[UnitMultiplierName],[ReferencePeriodId],[ReferencePeriodName],[Year],[Value],[Status])
VALUES
(:countryId,:countryName,:indicatorId,:indicatorName,:sexId,:sexName,:ageId,:ageName,:timeFormatId,:timeFormatName,:unitId,:unitName,:unitMultiplierId,:unitMultiplierName,:referencePeriodId,:referencePeriodName,:year,:value,:status)
""";
    private final int COMMIT_SIZE = 100;

    private NamedParameterJdbcTemplate _jdbcTemplate;

    public GenderEnt1ItemWriter(NamedParameterJdbcTemplate jdbcTemplate) {
        _jdbcTemplate = jdbcTemplate;
    }

    @Override
    public void write(Chunk<? extends List<GenderEnt1>> chunk) {
        List<GenderEnt1> items = chunk.getItems().stream().flatMap(Collection::stream).collect(Collectors.toList());
        int totalRecords = items.size();

        List<GenderEnt1> commitItems = new ArrayList<>();
        totalRecords = 0;

        StopWatch sw = new StopWatch();
        sw.start();
        StopWatch commitSw = StopWatch.createStarted();
        for (int i = 0; i < items.size(); ++i) {
            if (commitItems.size() > 0 && commitItems.size() % COMMIT_SIZE == 0) {
                SqlParameterSource[] params = SqlParameterSourceUtils.createBatch(commitItems);
                int[] updateCounts = _jdbcTemplate.batchUpdate(INSERT_SQL, params);
                int updateCount = Arrays.stream(updateCounts).sum();
                totalRecords += updateCount;
                commitSw.stop();
                System.out.println(String.format("Committed records [%d]/[%d] in [%f] s.", updateCount, totalRecords, (commitSw.getTime() / 1000.0)));
                commitItems.clear();
                commitSw.reset();
                commitSw.start();
            }
            commitItems.add(items.get(i));
        }
        if (commitItems.size() > 0) {
            SqlParameterSource[] params = SqlParameterSourceUtils.createBatch(commitItems);
            int[] updateCounts = _jdbcTemplate.batchUpdate(INSERT_SQL, params);
            int updateCount = Arrays.stream(updateCounts).sum();
            totalRecords += updateCount;
            commitSw.stop();
            System.out.println(String.format("Committed records [%d]/[%d] in [%f] s.", updateCount, totalRecords, (commitSw.getTime() / 1000.0)));
        }
        sw.stop();
        System.out.println(String.format("WriteOecdData complete [%f] s.", (sw.getTime() / 1000.0)));
    }
}

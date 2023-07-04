package org.wonkim.oecdetl.batch;

import org.springframework.batch.item.ItemReader;
import org.wonkim.oecdetl.external.OecdDataServiceAgent;
import org.wonkim.oecdetl.external.dto.OecdDataResponse;

import java.time.Duration;
import java.time.Instant;
import java.util.concurrent.ExecutionException;

// https://docs.spring.io/spring-batch/docs/current/reference/html/readersAndWriters.html
public class OecdDataResponseItemReader implements ItemReader<OecdDataResponse> {
    private OecdDataServiceAgent _serviceAgent;
    private int page = 0;

    public OecdDataResponseItemReader(OecdDataServiceAgent serviceAgent) {
        _serviceAgent = serviceAgent;
    }

    @Override
    public OecdDataResponse read() throws ExecutionException, InterruptedException {
        // https://stackoverflow.com/questions/42834725/spring-batch-job-running-in-infinite-loop
        if (page > 0) return null;
        Instant startTime = Instant.now();
        OecdDataResponse data = _serviceAgent.Get();
        Instant endTime = Instant.now();
        double totalSeconds = Duration.between(startTime, endTime).toMillis() / 1000;
        System.out.println(String.format("ReadOecdData complete [%f] s.", totalSeconds));
        ++page;
        return data;
    }
}

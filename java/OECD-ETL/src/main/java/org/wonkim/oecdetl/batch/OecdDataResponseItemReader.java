package org.wonkim.oecdetl.batch;

import org.apache.commons.lang3.time.StopWatch;
import org.springframework.batch.item.ItemReader;
import org.wonkim.oecdetl.external.OecdDataServiceAgent;
import org.wonkim.oecdetl.external.dto.OecdDataResponse;

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
        StopWatch sw = StopWatch.createStarted();
        OecdDataResponse data = _serviceAgent.Get();
        sw.stop();
        System.out.println(String.format("ReadOecdData complete [%f] s.", (sw.getTime() / 1000.0)));
        ++page;
        return data;
    }
}

package org.wonkim.oecdetl.external;

import com.google.gson.Gson;
import org.wonkim.oecdetl.external.dto.OecdDataResponse;

import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.util.concurrent.CompletableFuture;
import java.util.concurrent.ExecutionException;

public class OecdDataServiceAgent {
    private String baseUrl = "https://stats.oecd.org/SDMX-JSON/data/";
    private Gson gson = new Gson();
    private HttpClient httpClient;
    private String dataSetId;

    public OecdDataServiceAgent(String dataSetId) {
        httpClient = HttpClient.newHttpClient();
        this.dataSetId = dataSetId;
    }

    public OecdDataResponse Get() throws ExecutionException, InterruptedException {
        OecdDataResponse data = null;

        HttpRequest httpRequest = HttpRequest.newBuilder(URI.create(baseUrl + dataSetId)).GET().build();
        CompletableFuture<HttpResponse<String>> futureHttpResponse = httpClient.sendAsync(httpRequest, HttpResponse.BodyHandlers.ofString());
        HttpResponse<String> httpResponse = futureHttpResponse.get();

        data = gson.fromJson(httpResponse.body(), OecdDataResponse.class);

        return data;
    }
}

package org.wonkim.oecdetl.external.dto;

import java.util.Map;

public class OecdDataResponseDataSet {
    private String action;
    private Map<String, OecdDataResponseDataSetSeriesValue> series;

    public String getAction() {
        return action;
    }

    public void setAction(String action) {
        this.action = action;
    }

    public Map<String, OecdDataResponseDataSetSeriesValue> getSeries() {
        return series;
    }

    public void setSeries(Map<String, OecdDataResponseDataSetSeriesValue> series) {
        this.series = series;
    }
}

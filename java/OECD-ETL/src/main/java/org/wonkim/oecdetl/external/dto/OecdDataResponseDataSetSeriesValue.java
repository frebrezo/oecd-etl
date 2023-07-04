package org.wonkim.oecdetl.external.dto;

import java.util.List;
import java.util.Map;

public class OecdDataResponseDataSetSeriesValue {
    private List<Integer> attributes;
    private Map<Integer, List<Double>> observations;

    public List<Integer> getAttributes() {
        return attributes;
    }

    public void setAttributes(List<Integer> attributes) {
        this.attributes = attributes;
    }

    public Map<Integer, List<Double>> getObservations() {
        return observations;
    }

    public void setObservations(Map<Integer, List<Double>> observations) {
        this.observations = observations;
    }
}

package org.wonkim.oecdetl.external.dto;

import java.util.List;

public class OecdDataResponseStructureAttribute {
    // "dataSet": []
    private List<OecdDataResponseStructureAttributeSeries> series;
    private List<OecdDataResponseStructureAttributeObservation> observation;

    public List<OecdDataResponseStructureAttributeSeries> getSeries() {
        return series;
    }

    public void setSeries(List<OecdDataResponseStructureAttributeSeries> series) {
        this.series = series;
    }

    public List<OecdDataResponseStructureAttributeObservation> getObservation() {
        return observation;
    }

    public void setObservation(List<OecdDataResponseStructureAttributeObservation> observation) {
        this.observation = observation;
    }
}

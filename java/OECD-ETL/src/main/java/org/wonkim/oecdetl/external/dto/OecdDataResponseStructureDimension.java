package org.wonkim.oecdetl.external.dto;

import java.util.List;

public class OecdDataResponseStructureDimension {
    private List<OecdDataResponseStructureDimensionSeries> series;
    private List<OecdDataResponseStructureDimensionObservation> observation;

    public List<OecdDataResponseStructureDimensionSeries> getSeries() {
        return series;
    }

    public void setSeries(List<OecdDataResponseStructureDimensionSeries> series) {
        this.series = series;
    }

    public List<OecdDataResponseStructureDimensionObservation> getObservation() {
        return observation;
    }

    public void setObservation(List<OecdDataResponseStructureDimensionObservation> observation) {
        this.observation = observation;
    }
}

package org.wonkim.oecdetl.external.dto;

import java.util.List;

public class OecdDataResponse {
    private OecdDataResponseHeader header;
    private List<OecdDataResponseDataSet> dataSets;
    private OecdDataResponseStructure structure;

    public OecdDataResponseHeader getHeader() {
        return header;
    }

    public void setHeader(OecdDataResponseHeader header) {
        this.header = header;
    }

    public List<OecdDataResponseDataSet> getDataSets() {
        return dataSets;
    }

    public void setDataSets(List<OecdDataResponseDataSet> dataSets) {
        this.dataSets = dataSets;
    }

    public OecdDataResponseStructure getStructure() {
        return structure;
    }

    public void setStructure(OecdDataResponseStructure structure) {
        this.structure = structure;
    }
}

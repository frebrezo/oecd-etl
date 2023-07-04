package org.wonkim.oecdetl.external.dto;

import java.util.List;

public class OecdDataResponseStructureAttributeObservation {
    private String id;
    private String name;
    private List<OecdDataResponseValue> values;

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public List<OecdDataResponseValue> getValues() {
        return values;
    }

    public void setValues(List<OecdDataResponseValue> values) {
        this.values = values;
    }
}

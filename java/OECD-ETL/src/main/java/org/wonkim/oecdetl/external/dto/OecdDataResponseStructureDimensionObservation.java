package org.wonkim.oecdetl.external.dto;

import java.util.List;

public class OecdDataResponseStructureDimensionObservation {
    private String id;
    private String name;
    private List<OecdDataResponseValue> values;
    private String role;

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

    public String getRole() {
        return role;
    }

    public void setRole(String role) {
        this.role = role;
    }
}

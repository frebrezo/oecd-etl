package org.wonkim.oecdetl.external.dto;

import java.util.List;

public class OecdDataResponseStructure {
    private List<OecdDataResponseLink> links;
    private String name;
    private String description;
    private OecdDataResponseStructureDimension dimensions;
    private OecdDataResponseStructureAttribute attributes;
    private List<OecdDataResponseStructureAnnotation> annotations;

    public List<OecdDataResponseLink> getLinks() {
        return links;
    }

    public void setLinks(List<OecdDataResponseLink> links) {
        this.links = links;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public OecdDataResponseStructureDimension getDimensions() {
        return dimensions;
    }

    public void setDimensions(OecdDataResponseStructureDimension dimensions) {
        this.dimensions = dimensions;
    }

    public OecdDataResponseStructureAttribute getAttributes() {
        return attributes;
    }

    public void setAttributes(OecdDataResponseStructureAttribute attributes) {
        this.attributes = attributes;
    }

    public List<OecdDataResponseStructureAnnotation> getAnnotations() {
        return annotations;
    }

    public void setAnnotations(List<OecdDataResponseStructureAnnotation> annotations) {
        this.annotations = annotations;
    }
}

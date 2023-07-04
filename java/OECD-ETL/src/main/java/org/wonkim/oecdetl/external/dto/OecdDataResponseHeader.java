package org.wonkim.oecdetl.external.dto;

import java.util.Date;
import java.util.List;
import java.util.UUID;

public class OecdDataResponseHeader {
    private UUID id;
    private Boolean test;
    private Date prepared;
    private OecdDataResponseHeaderSender sender;
    private List<OecdDataResponseLink> links;
}

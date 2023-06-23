namespace Oecd.Data.External.Dto;
public class OecdDataResponse
{
    public OecdDataResponseHeader Header { get; set; } = null!;
    public List<OecdDataResponseDataSet> DataSets { get; set; } = null!;
    public OecdDataResponseStructure Structure { get; set; } = null!;
}

public class OecdDataResponseHeader
{
    public Guid Id { get; set; }
    public bool Test { get; set; }
    public DateTime Prepared { get; set; }
    public OecdDataResponseHeaderSender Sender { get; set; } = null!;
    public List<OecdDataResponseLink> Links { get; set; } = new List<OecdDataResponseLink>();
}

public class OecdDataResponseValue
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
}

public class OecdDataResponseDataSet
{
    public string Action { get; set; } = null!;
    public Dictionary<string, OecdDataResponseDataSetSeriesValue> Series { get; set; } = new Dictionary<string, OecdDataResponseDataSetSeriesValue>();
}

public class OecdDataResponseStructure
{
    public List<OecdDataResponseLink> Links { get; set; } = new List<OecdDataResponseLink>();
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public OecdDataResponseStructureDimension Dimensions { get; set; } = null!;
    public OecdDataResponseStructureAttribute Attributes { get; set; } = null!;
    public List<OecdDataResponseStructureAnnotation> Annotations { get; set; } = new List<OecdDataResponseStructureAnnotation>();
}

public class OecdDataResponseLink
{
    public string Href { get; set; } = null!;
    public string Rel { get; set; } = null!;
}

public class OecdDataResponseHeaderSender
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
}

public class OecdDataResponseDataSetSeriesValue
{
    public List<int?> Attributes { get; set; } = new List<int?>();
    public Dictionary<int, List<double?>> Observations { get; set; } = new Dictionary<int, List<double?>>();
}

public class OecdDataResponseStructureDimension
{
    public List<OecdDataResponseStructureDimensionSeries> Series { get; set; } = new List<OecdDataResponseStructureDimensionSeries>();
    public List<OecdDataResponseStructureDimensionObservation> Observation { get; set; } = new List<OecdDataResponseStructureDimensionObservation>();
}

public class OecdDataResponseStructureDimensionSeries
{
    public int KeyPosition { get; set; }
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public List<OecdDataResponseValue> Values { get; set; } = new List<OecdDataResponseValue>();
    public string Role { get; set; } = null!;
}

public class OecdDataResponseStructureDimensionObservation
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public List<OecdDataResponseValue> Values { get; set; } = new List<OecdDataResponseValue>();
    public string Role { get; set; } = null!;
}

public class OecdDataResponseStructureAttribute
{
    // "dataSet": []
    public List<OecdDataResponseStructureAttributeSeries> Series { get; set; } = new List<OecdDataResponseStructureAttributeSeries>();
    public List<OecdDataResponseStructureAttributeObservation> Observation { get; set; } = new List<OecdDataResponseStructureAttributeObservation>();
}

public class OecdDataResponseStructureAttributeSeries
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public List<OecdDataResponseValue> Values { get; set; } = new List<OecdDataResponseValue>();
}

public class OecdDataResponseStructureAttributeObservation
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public List<OecdDataResponseValue> Values { get; set; } = new List<OecdDataResponseValue>();
}

public class OecdDataResponseStructureAnnotation
{
    public string Title { get; set; } = null!;
    public string Uri { get; set; } = null!;
    public string Text { get; set; } = null!;
}

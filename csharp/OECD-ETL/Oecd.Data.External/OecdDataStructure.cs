using Oecd.Data.External.Dto;

namespace Oecd.Data.External;

public class OecdDataStructure
{
    public Dictionary<int, OecdDataDimensionSeries> DimensionSeries { get; set; } = new Dictionary<int, OecdDataDimensionSeries>();
    public Dictionary<string, List<OecdDataValue>> DimensionSeriesValues { get; set; } = new Dictionary<string, List<OecdDataValue>>();
    public Dictionary<int, OecdDataDimensionObservation> DimensionObservations { get; set; } = new Dictionary<int, OecdDataDimensionObservation>();
    public Dictionary<string, List<OecdDataValue>> DimensionObservationValues { get; set; } = new Dictionary<string, List<OecdDataValue>>();
    public Dictionary<int, OecdDataAttributeSeries> AttributeSeries { get; set; } = new Dictionary<int, OecdDataAttributeSeries>();
    public Dictionary<string, List<OecdDataValue>> AttributeSeriesValues { get; set; } = new Dictionary<string, List<OecdDataValue>>();
    public Dictionary<int, OecdDataAttributeObservation> AttributeObservations { get; set; } = new Dictionary<int, OecdDataAttributeObservation>();
    public Dictionary<string, List<OecdDataValue>> AttributeObservationValues { get; set; } = new Dictionary<string, List<OecdDataValue>>();

    public OecdDataStructure(OecdDataResponseStructure structure)
    {
        foreach (OecdDataResponseStructureDimensionSeries series in structure.Dimensions.Series)
        {
            DimensionSeries.Add(series.KeyPosition, new OecdDataDimensionSeries(series.Id, series.Name));
            DimensionSeriesValues.Add(series.Id, series.Values.Select(x => new OecdDataValue(x.Id, x.Name)).ToList());
        }

        for (int i = 0; i < structure.Dimensions.Observation.Count; ++i)
        {
            OecdDataResponseStructureDimensionObservation observation = structure.Dimensions.Observation[i];
            DimensionObservations.Add(i, new OecdDataDimensionObservation(observation.Id, observation.Name));
            DimensionObservationValues.Add(observation.Id, observation.Values.Select(x => new OecdDataValue(x.Id, x.Name)).ToList());
        }

        for (int i = 0; i < structure.Attributes.Series.Count; ++i)
        {
            OecdDataResponseStructureAttributeSeries series = structure.Attributes.Series[i];
            AttributeSeries.Add(i, new OecdDataAttributeSeries(series.Id, series.Name));
            AttributeSeriesValues.Add(series.Id, series.Values.Select(x => new OecdDataValue(x.Id, x.Name)).ToList());
        }

        for (int i = 0; i < structure.Attributes.Observation.Count; ++i)
        {
            OecdDataResponseStructureAttributeObservation observation = structure.Attributes.Observation[i];
            AttributeObservations.Add(i, new OecdDataAttributeObservation(observation.Id, observation.Name));
            AttributeObservationValues.Add(observation.Id, observation.Values.Select(x => new OecdDataValue(x.Id, x.Name)).ToList());
        }
    }
}

public class OecdDataDimensionSeries
{
    public OecdDataDimensionSeries() { }
    public OecdDataDimensionSeries(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
}

public class OecdDataDimensionObservation
{
    public OecdDataDimensionObservation() { }
    public OecdDataDimensionObservation(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
}

public class OecdDataAttributeSeries
{
    public OecdDataAttributeSeries() { }
    public OecdDataAttributeSeries(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
}

public class OecdDataAttributeObservation
{
    public OecdDataAttributeObservation() { }
    public OecdDataAttributeObservation(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
}

public class OecdDataValue
{
    public OecdDataValue() { }
    public OecdDataValue(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
}

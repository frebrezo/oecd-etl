namespace Oecd.Common;

public class GenderEnt1 : IGenderEnt1Dimension, IGenderEnt1Attribute, IGenderEnt1Observation
{
    public GenderEnt1() { }

    public GenderEnt1(IGenderEnt1Dimension dimension, IGenderEnt1Attribute attribute, IGenderEnt1Observation observation)
    {
        SetDimensionProperties(dimension);
        SetAttributeProperties(attribute);
        SetObservationProperties(observation);
    }

    public int Id { get; set; }

    #region IGenderEnt1Dimension
    public string CountryId { get; set; } = null!;
    public string CountryName { get; set; } = null!;
    public string IndicatorId { get; set; } = null!;
    public string IndicatorName { get; set; } = null!;
    public string SexId { get; set; } = null!;
    public string SexName { get; set; } = null!;
    public string AgeId { get; set; } = null!;
    public string AgeName { get; set; } = null!;
    #endregion

    #region IGenderEnt1Attribute
    public string TimeFormatId { get; set; } = null!;
    public string TimeFormatName { get; set; } = null!;
    public string UnitId { get; set; } = null!;
    public string UnitName { get; set; } = null!;
    public string UnitMultiplierId { get; set; } = null!;
    public string UnitMultiplierName { get; set; } = null!;
    public string? ReferencePeriodId { get; set; }
    public string? ReferencePeriodName { get; set; }
    #endregion

    #region IGenderEnt1Observation
    public int Year { get; set; }
    public double? Value { get; set; }
    public string? Status { get; set; }
    #endregion

    public void SetDimensionProperties(IGenderEnt1Dimension dimension)
    {
        CountryId = dimension.CountryId;
        CountryName = dimension.CountryName;
        IndicatorId = dimension.IndicatorId;
        IndicatorName = dimension.IndicatorName;
        SexId = dimension.SexId;
        SexName = dimension.SexName;
        AgeId = dimension.AgeId;
        AgeName = dimension.AgeName;
    }

    public void SetAttributeProperties(IGenderEnt1Attribute attribute)
    {
        TimeFormatId = attribute.TimeFormatId;
        TimeFormatName = attribute.TimeFormatName;
        UnitId = attribute.UnitId;
        UnitName = attribute.UnitName;
        UnitMultiplierId = attribute.UnitMultiplierId;
        UnitMultiplierName = attribute.UnitMultiplierName;
        ReferencePeriodId = attribute.ReferencePeriodId;
        ReferencePeriodName = attribute.ReferencePeriodName;
    }

    public void SetObservationProperties(IGenderEnt1Observation observation)
    {
        Year = observation.Year;
        Value = observation.Value;
        Status = observation.Status;
    }
}

public interface IGenderEnt1Dimension
{
    public string CountryId { get; set; }
    public string CountryName { get; set; }
    public string IndicatorId { get; set; }
    public string IndicatorName { get; set; }
    public string SexId { get; set; }
    public string SexName { get; set; }
    public string AgeId { get; set; }
    public string AgeName { get; set; }
}

public interface IGenderEnt1Attribute
{
    public string TimeFormatId { get; set; }
    public string TimeFormatName { get; set; }
    public string UnitId { get; set; }
    public string UnitName { get; set; }
    public string UnitMultiplierId { get; set; }
    public string UnitMultiplierName { get; set; }
    public string? ReferencePeriodId { get; set; }
    public string? ReferencePeriodName { get; set; }
}

public interface IGenderEnt1Observation
{
    public int Year { get; set; }
    public double? Value { get; set; }
    public string? Status { get; set; }
}

public class GenderEnt1Dimension : IGenderEnt1Dimension
{
    public string CountryId { get; set; } = null!;
    public string CountryName { get; set; } = null!;
    public string IndicatorId { get; set; } = null!;
    public string IndicatorName { get; set; } = null!;
    public string SexId { get; set; } = null!;
    public string SexName { get; set; } = null!;
    public string AgeId { get; set; } = null!;
    public string AgeName { get; set; } = null!;
}

public class GenderEnt1Attribute : IGenderEnt1Attribute
{
    public string TimeFormatId { get; set; } = null!;
    public string TimeFormatName { get; set; } = null!;
    public string UnitId { get; set; } = null!;
    public string UnitName { get; set; } = null!;
    public string UnitMultiplierId { get; set; } = null!;
    public string UnitMultiplierName { get; set; } = null!;
    public string? ReferencePeriodId { get; set; }
    public string? ReferencePeriodName { get; set; }
}

public class GenderEnt1Observation : IGenderEnt1Observation
{
    public int Year { get; set; }
    public double? Value { get; set; }
    public string? Status { get; set; }
}

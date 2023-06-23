class GenderEnt1:
    Id: int

    CountryId: str
    CountryName: str
    IndicatorId: str
    IndicatorName: str
    SexId: str
    SexName: str
    AgeId: str
    AgeName: str

    TimeFormatId: str
    TimeFormatName: str
    UnitId: str
    UnitName: str
    UnitMultiplierId: str
    UnitMultiplierName: str
    ReferencePeriodId: str
    ReferencePeriodName: str

    Year: int
    Value: float
    Status: str

    # https://stackoverflow.com/questions/35282222/in-python-how-do-i-cast-a-class-object-to-a-dict
    def asdict(self):
        obj_dict = {}
        if hasattr(self, "Id"):
            obj_dict["Id"] = self.Id
        obj_dict.update({
            "CountryId": self.CountryId,
            "CountryName": self.CountryName,
            "IndicatorId": self.IndicatorId,
            "IndicatorName": self.IndicatorName,
            "SexId": self.SexId,
            "SexName": self.SexName,
            "AgeId": self.AgeId,
            "AgeName": self.AgeName,

            "TimeFormatId": self.TimeFormatId,
            "TimeFormatName": self.TimeFormatName,
            "UnitId": self.UnitId,
            "UnitName": self.UnitName,
            "UnitMultiplierId": self.UnitMultiplierId,
            "UnitMultiplierName": self.UnitMultiplierName,
            "ReferencePeriodId": self.ReferencePeriodId if hasattr(self, "ReferencePeriodId") else None,
            "ReferencePeriodName": self.ReferencePeriodName if hasattr(self, "ReferencePeriodName") else None,

            "Year": self.Year,
            "Value": self.Value,
            "Status": self.Status if hasattr(self, "Status") else None
        })
        return obj_dict


class GenderEnt1Dimension:
    CountryId: str
    CountryName: str
    IndicatorId: str
    IndicatorName: str
    SexId: str
    SexName: str
    AgeId: str
    AgeName: str


class GenderEnt1Attribute:
    TimeFormatId: str
    TimeFormatName: str
    UnitId: str
    UnitName: str
    UnitMultiplierId: str
    UnitMultiplierName: str
    ReferencePeriodId: str
    ReferencePeriodName: str


class GenderEnt1Observation:
    Year: int
    Value: float
    Status: str

    def __init__(self, year, value, status):
        self.Year = year
        self.Value = value
        self.Status = status

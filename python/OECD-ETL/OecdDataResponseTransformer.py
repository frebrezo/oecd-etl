import sys

sys.path.append('../OECD-Common')
from OecdDataResponse import *
from GenderEnt1 import GenderEnt1


class OecdDataResponseTransformer:
    def transform(self, response: OecdDataResponse, series_structure: Dict[int, OecdDataResponseStructureDimensionSeries], dimensions: List[int], series_value: OecdDataResponseDataSetSeriesValue, observation: int, observation_data: float) -> GenderEnt1:
        data = GenderEnt1()

        for i in range(0, len(dimensions)):
            dimension = dimensions[i]

            dimension_value_id = series_structure[i]["values"][dimension]["id"]
            dimension_value_name = series_structure[i]["values"][dimension]["name"]

            if series_structure[i]["id"] == "LOCATION":
                data.CountryId = dimension_value_id
                data.CountryName = dimension_value_name
            elif series_structure[i]["id"] == "INDICATOR":
                data.IndicatorId = dimension_value_id
                data.IndicatorName = dimension_value_name
            elif series_structure[i]["id"] == "SEX":
                data.SexId = dimension_value_id
                data.SexName = dimension_value_name
            elif series_structure[i]["id"] == "AGE":
                data.AgeId = dimension_value_id
                data.AgeName = dimension_value_name
            else:
                print("Unsupported dimension [%s]." % (series_structure[i]["values"][dimension]["name"]))

        for i in range(0, len(series_value["attributes"])):
            if series_value["attributes"][i] is None:
                continue

            attribute_id = response["structure"]["attributes"]["series"][i]["values"][series_value["attributes"][i]]["id"]
            attribute_name = response["structure"]["attributes"]["series"][i]["values"][series_value["attributes"][i]]["name"]

            if response["structure"]["attributes"]["series"][i]["id"] == "TIME_FORMAT":
                data.TimeFormatId = attribute_id
                data.TimeFormatName = attribute_name
            elif response["structure"]["attributes"]["series"][i]["id"] == "UNIT":
                data.UnitId = attribute_id
                data.UnitName = attribute_name
            elif response["structure"]["attributes"]["series"][i]["id"] == "POWERCODE":
                data.UnitMultiplierId = attribute_id
                data.UnitMultiplierName = attribute_name
            elif response["structure"]["attributes"]["series"][i]["id"] == "REFERENCEPERIOD":
                data.ReferencePeriodId = attribute_id
                data.ReferencePeriodName = attribute_name

        observation_value = response["structure"]["dimensions"]["observation"][0]["values"][observation]

        data.Year = int(observation_value["id"])
        data.Value = observation_data

        return data

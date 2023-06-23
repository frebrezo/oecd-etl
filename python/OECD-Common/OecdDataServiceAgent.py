import json
import requests


class OecdDataServiceAgent:
    BaseUrl: str = "https://stats.oecd.org/SDMX-JSON/data/"
    DataSetId: str


    def __init__(self, data_set_id):
        self.DataSetId = data_set_id


    def get(self) -> str:
        response = requests.get(self.BaseUrl + self.DataSetId)
        response.raise_for_status()
        return response.json()

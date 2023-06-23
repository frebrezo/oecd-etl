import datetime
import uuid
from typing import List, Dict, Any


# https://stackoverflow.com/questions/53905951/how-do-i-serialise-a-nested-dictionary-with-marshmallow
class OecdDataResponseValue:
    id: str
    name: str

    def __init__(self, id, name):
        self.id = id
        self.name = name


class OecdDataResponseStructureAnnotation:
    title: str
    uri: str
    text: str

    def __init__(self, title, uri, text):
        self.title = title
        self.uri = uri
        self.text = text


class OecdDataResponseStructureAttributeObservation:
    id: str
    name: str
    values: List[OecdDataResponseValue]

    def __init__(self, id, name, values):
        self.id = id
        self.name = name
        self.values = values


class OecdDataResponseStructureAttributeSeries:
    id: str
    name: str
    values: List[OecdDataResponseValue]
    role: str
    default: str

    def __init__(self, id, name, values, role = None, default = None):
        self.id = id
        self.name = name
        self.values = values
        self.role = role
        self.default = default


class OecdDataResponseStructureAttributeDataSet:
    id: str
    name: str

    def __init__(self, id, name):
        self.id = id
        self.name = name


class OecdDataResponseStructureAttribute:
    dataSet: List[OecdDataResponseStructureAttributeDataSet]
    series: List[OecdDataResponseStructureAttributeSeries]
    observation: List[OecdDataResponseStructureAttributeObservation]

    def __init__(self, dataSet, series, observation):
        self.dataSet = dataSet
        self.series = series
        self.observation = observation


class OecdDataResponseStructureDimensionObservation:
    id: str
    name: str
    values: List[OecdDataResponseValue]
    role: str

    def __init__(self, id, name, values, role):
        self.id = id
        self.name = name
        self.values = values
        self.role = role


class OecdDataResponseStructureDimensionSeries:
    keyPosition: int
    id: str
    name: str
    values: List[OecdDataResponseValue]
    role: str

    def __init__(self, keyPosition, id, name, values, role = None):
        self.keyPosition = keyPosition
        self.id = id
        self.name = name
        self.values = values
        self.role = role


class OecdDataResponseStructureDimension:
    series: List[OecdDataResponseStructureDimensionSeries]
    observation: List[OecdDataResponseStructureDimensionObservation]

    def __init__(self, series, observation):
        self.series = series
        self.observation = observation


class OecdDataResponseDataSetSeriesValue:
    attributes: List[Any] # List[int or None]
    observations: Dict[int, List[Any]] # List[float or None]

    def __init__(self, attributes, observations):
        self.attributes = attributes
        self.observations = observations


class OecdDataResponseHeaderSender:
    id: str
    name: str

    def __init__(self, id, name):
        self.id = id
        self.name = name


class OecdDataResponseLink:
    href: str
    rel: str

    def __init__(self, href, rel):
        self.href = href
        self.rel = rel


class OecdDataResponseStructure:
    links: List[OecdDataResponseLink]
    name: str
    description: str
    dimensions: OecdDataResponseStructureDimension
    attributes: OecdDataResponseStructureAttribute
    annotations: List[OecdDataResponseStructureAnnotation]

    def __init__(self, links, name, description, dimensions, attributes, annotations):
        self.links = links
        self.name = name
        self.description = description
        self.dimensions = dimensions
        self.attributes = attributes
        self.annotations = annotations


# https://stackoverflow.com/questions/53905951/how-do-i-serialise-a-nested-dictionary-with-marshmallow
class OecdDataResponseDataSet:
    action: str
    series: Dict[str, OecdDataResponseDataSetSeriesValue]

    def __init__(self, action, series):
        self.action = action
        self.series = series


class OecdDataResponseHeader:
    id: str # uuid
    test: bool
    prepared: datetime
    sender: OecdDataResponseHeaderSender
    links: List[OecdDataResponseLink]

    def __init__(self, id, test, prepared, sender, links):
        self.id = id
        self.test = test
        self.prepared = prepared
        self.sender = sender
        self.links = links


class OecdDataResponse:
    header: OecdDataResponseHeader
    dataSets: List[OecdDataResponseDataSet]
    structure: OecdDataResponseStructure

    def __init__(self, header, dataSets, structure):
        self.header = header
        self.dataSets = dataSets
        self.structure = structure

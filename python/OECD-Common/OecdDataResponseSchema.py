from marshmallow import Schema, fields, post_load
from OecdDataResponse import *


# https://marshmallow.readthedocs.io/en/stable/quickstart.html#declaring-schemas
# https://stackoverflow.com/questions/53905951/how-do-i-serialise-a-nested-dictionary-with-marshmallow
class OecdDataResponseValueSchema(Schema):
    id = fields.String()
    name = fields.String()

    @post_load
    def make_object(self, data, **kwargs):
        return OecdDataResponseValue(**data)


class OecdDataResponseStructureAnnotationSchema(Schema):
    title = fields.String()
    uri = fields.String()
    text = fields.String()

    @post_load
    def make_object(self, data, **kwargs):
        return OecdDataResponseStructureAnnotation(**data)


class OecdDataResponseStructureAttributeObservationSchema(Schema):
    id = fields.String()
    name = fields.String()
    values = fields.List(fields.Nested(OecdDataResponseValueSchema))

    @post_load
    def make_object(self, data, **kwargs):
        return OecdDataResponseStructureAttributeObservation(**data)


class OecdDataResponseStructureAttributeSeriesSchema(Schema):
    id = fields.String()
    name = fields.String()
    values = fields.List(fields.Nested(OecdDataResponseValueSchema))
    role = fields.String(required=False)
    default = fields.String(required=False)

    @post_load
    def make_object(self, data, **kwargs):
        return OecdDataResponseStructureAttributeSeries(**data)


class OecdDataResponseStructureAttributeDataSetSchema(Schema):
    id = fields.String()
    name = fields.String()

    @post_load
    def make_object(self, data, **kwargs):
        return OecdDataResponseStructureAttributeDataSet(**data)


class OecdDataResponseStructureAttributeSchema(Schema):
    dataSet = fields.List(fields.Nested(OecdDataResponseStructureAttributeDataSetSchema))
    series = fields.List(fields.Nested(OecdDataResponseStructureAttributeSeriesSchema))
    observation = fields.List(fields.Nested(OecdDataResponseStructureAttributeObservationSchema))

    @post_load
    def make_object(self, data, **kwargs):
        return OecdDataResponseStructureAttribute(**data)


class OecdDataResponseStructureDimensionObservationSchema(Schema):
    id = fields.String()
    name = fields.String()
    values = fields.List(fields.Nested(OecdDataResponseValueSchema))
    role = fields.String()

    @post_load
    def make_object(self, data, **kwargs):
        return OecdDataResponseStructureDimensionObservation(**data)


class OecdDataResponseStructureDimensionSeriesSchema(Schema):
    keyPosition: int = fields.Int()
    id = fields.String()
    name = fields.String()
    values = fields.List(fields.Nested(OecdDataResponseValueSchema))
    role = fields.String()

    @post_load
    def make_object(self, data, **kwargs):
        return OecdDataResponseStructureDimensionSeries(**data)


class OecdDataResponseStructureDimensionSchema(Schema):
    series = fields.List(fields.Nested(OecdDataResponseStructureDimensionSeriesSchema))
    observation = fields.List(fields.Nested(OecdDataResponseStructureDimensionObservationSchema))

    @post_load
    def make_object(self, data, **kwargs):
        return OecdDataResponseStructureDimension(**data)


class OecdDataResponseDataSetSeriesValueSchema(Schema):
    attributes = fields.List(fields.Raw(allow_none=True, missing=True))
    observations = fields.Dict(fields.Int(), fields.List(fields.Raw(allow_none=True, missing=True)))

    @post_load
    def make_object(self, data, **kwargs):
        return OecdDataResponseDataSetSeriesValue(**data)


class OecdDataResponseHeaderSenderSchema(Schema):
    id = fields.String()
    name = fields.String()

    @post_load
    def make_object(self, data, **kwargs):
        return OecdDataResponseHeaderSender(**data)


class OecdDataResponseLinkSchema(Schema):
    href = fields.String()
    rel = fields.String()

    @post_load
    def make_object(self, data, **kwargs):
        return OecdDataResponseLink(**data)


class OecdDataResponseStructureSchema(Schema):
    links = fields.List(fields.Nested(OecdDataResponseLinkSchema))
    name = fields.String()
    description = fields.String()
    dimensions = fields.Nested(OecdDataResponseStructureDimensionSchema)
    attributes = fields.Nested(OecdDataResponseStructureAttributeSchema)
    annotations = fields.List(fields.Nested(OecdDataResponseStructureAnnotationSchema))

    @post_load
    def make_object(self, data, **kwargs):
        return OecdDataResponseStructure(**data)


# https://stackoverflow.com/questions/53905951/how-do-i-serialise-a-nested-dictionary-with-marshmallow
class OecdDataResponseDataSetSchema(Schema):
    action = fields.String()
    series = fields.Dict(fields.String(), fields.Nested(OecdDataResponseDataSetSeriesValueSchema))

    @post_load
    def make_object(self, data, **kwargs):
        return OecdDataResponseDataSet(**data)


class OecdDataResponseHeaderSchema(Schema):
    id = fields.String() # uuid
    test = fields.Bool()
    prepared = fields.DateTime()
    sender = fields.Nested(OecdDataResponseHeaderSenderSchema)
    links = fields.List(fields.Nested(OecdDataResponseLinkSchema))

    @post_load
    def make_object(self, data, **kwargs):
        return OecdDataResponseHeader(**data)

class OecdDataResponseSchema(Schema):
    header = fields.Nested(OecdDataResponseHeaderSchema)
    dataSets = fields.List(fields.Nested(OecdDataResponseDataSetSchema))
    structure = fields.Nested(OecdDataResponseStructureSchema)

    @post_load
    def make_object(self, data, **kwargs):
        return OecdDataResponse(**data)
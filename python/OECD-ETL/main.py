# bonobo requires Python 3.9 or lower because the Iterable abstract class was removed from collections in Python 3.10
# ImportError: cannot import name 'Iterable' from 'collections'
# https://stackoverflow.com/questions/72032032/importerror-cannot-import-name-iterable-from-collections-in-python
import bonobo
import json
import sqlalchemy as db
import sys
from bonobo.config import use_context_processor
from sqlalchemy import Engine, MetaData, Connection, Table, MetaData, insert, text
from sqlalchemy.orm import Session
from timeit import default_timer
from typing import List
from OecdDataResponseTransformer import OecdDataResponseTransformer

# https://stackoverflow.com/questions/49039436/how-to-import-a-module-from-a-different-folder
sys.path.append('../OECD-Common')
from OecdDataServiceAgent import OecdDataServiceAgent
from GenderEnt1 import GenderEnt1


oecd_dataset: str = "GENDER_ENT1"
conn_str: str = "mssql+pymssql://java-app:Passw0rd1@localhost/Oecd"
commit_size: int = 100
total_records: int = 0
db_engine: Engine
db_metadata: MetaData
db_connection: Connection
db_session: Session


def with_opened_db_session(self, context):
    yield db_engine, db_metadata, db_conn, db_session


def with_opened_db_conn(self, context):
    yield db_engine, db_metadata, db_conn


@use_context_processor(with_opened_db_session)
def cleanup(db_engine, db_metadata, db_conn, db_session):
    db_session.execute(text("truncate table GenderEnt1Land"))
    db_session.commit()
    yield True # Chain nodes MUST return a value to continue chain.


def with_oecd_dataset(self, context):
    yield oecd_dataset


@use_context_processor(with_oecd_dataset)
def read_oecd_data(data_set_id: str, is_success: bool):
    agent = OecdDataServiceAgent(data_set_id)
    data = agent.get()
    json.dumps(data, indent=2)
    yield data


def transform_OecdDataResponse_to_GenderEnt1(response) -> List[GenderEnt1]:
    transformer = OecdDataResponseTransformer()

    series_structure = dict(map(lambda x: [int(x["keyPosition"]), x], response["structure"]["dimensions"]["series"]))

    records: List[GenderEnt1] = []
    for data_set in response["dataSets"]:
        for dimension_string in data_set["series"].keys():
            dimensions = list(map(int, dimension_string.split(':')))

            series_value = data_set["series"][dimension_string]

            for observation in series_value["observations"].keys():
                for observation_data in series_value["observations"][observation]:
                    if observation_data is None:
                        continue

                    data = transformer.transform(response, series_structure, dimensions, series_value, int(observation), observation_data)

                    records.append(data)

                    if len(records) > 0 and len(records) % commit_size == 0:
                        yield records
                        records: List[GenderEnt1] = []

    if len(records) > 0:
        yield records


@use_context_processor(with_opened_db_conn)
def insert_oecd_data(db_engine, db_metadata, db_conn, records: List[GenderEnt1]):
    start_time = default_timer()
    genderent1land_table = Table("GenderEnt1Land", db_metadata, autoload_with=db_engine)
    # https://docs.sqlalchemy.org/en/20/tutorial/data_insert.html
    insert_values = list(map(lambda x: x.asdict(), records))
    result = db_conn.execute(insert(genderent1land_table), insert_values)
    # Insert record by record is slow.
    # for record in records:
    #     insert_sql = insert(genderent1land_table).values(
    #         CountryId = record.CountryId
    #         ,CountryName = record.CountryName
    #         ,IndicatorId = record.IndicatorId
    #         ,IndicatorName = record.IndicatorName
    #         ,SexId = record.SexId
    #         ,SexName = record.SexName
    #         ,AgeId = record.AgeId
    #         ,AgeName = record.AgeName
    #         ,TimeFormatId = record.TimeFormatId
    #         ,TimeFormatName = record.TimeFormatName
    #         ,UnitId = record.UnitId
    #         ,UnitName = record.UnitName
    #         ,UnitMultiplierId = record.UnitMultiplierId
    #         ,UnitMultiplierName = record.UnitMultiplierName
    #         ,ReferencePeriodId = record.ReferencePeriodId if hasattr(record, "ReferencePeriodId") else None
    #         ,ReferencePeriodName = record.ReferencePeriodName if hasattr(record, "ReferencePeriodName") else None
    #         ,Year = record.Year
    #         ,Value = record.Value
    #         ,Status = record.Status if hasattr(record, "Status") else None)
    #     db_conn.execute(insert_sql)
    db_conn.commit()
    duration_time = default_timer() - start_time
    global total_records
    total_records += len(records)
    print(f'Committed records {len(records)}/{total_records} in {duration_time}')


def get_graph(**options):
    graph = bonobo.Graph()
    graph.add_chain(
        cleanup,
        read_oecd_data,
        transform_OecdDataResponse_to_GenderEnt1,
        insert_oecd_data
    )
    return graph


def get_services(**options):
    return {}


if __name__ == '__main__':
    db_engine = db.create_engine(conn_str)
    db_metadata = MetaData()
    with db_engine.connect() as db_conn:
        with Session(db_engine) as db_session:
            start_time = default_timer()
            parser = bonobo.get_argument_parser()
            with bonobo.parse_args(parser) as options:
                bonobo.run(get_graph(**options), services=get_services(**options))
            duration_time = default_timer() - start_time
            print(f'Total run time {duration_time}')

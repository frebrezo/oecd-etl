using Autofac.Features.AttributeFilters;
using Oecd.Common;
using Oecd.Data.External;
using Oecd.Data.External.Dto;

namespace Oecd.Data.Service;

/// <summary>
/// https://www.oecd-ilibrary.org/social-issues-migration-health/data/gender-equality/gender-equality-in-entrepreneurship_data-00723-en
/// https://stats.oecd.org/viewhtml.aspx?datasetcode=GENDER_ENT1&lang=en
/// https://stats.oecd.org/SDMX-JSON/data/GENDER_ENT1
/// </summary>
public class GenderEnt1Service : IGenderEnt1Service
{
    private readonly IOecdDataServiceAgent _genderEnt1ServiceAgent;
    private readonly IOecdDataStructureTransformer _transformer;

    public GenderEnt1Service([KeyFilter("GENDER_ENT1")] IOecdDataServiceAgent agent, IOecdDataStructureTransformer transformer)
    {
        _genderEnt1ServiceAgent = agent;
        _transformer = transformer;
    }

    public async Task<List<GenderEnt1>> GetGenderEnt1()
    {
        List<GenderEnt1> dataList = new List<GenderEnt1>();
        OecdDataStructure genderEnt1Structure = null!;

        OecdDataResponse? genderEnt1Response = await _genderEnt1ServiceAgent.Get();
        if (genderEnt1Response == null)
        {
            return dataList;
        }

        genderEnt1Structure = new OecdDataStructure(genderEnt1Response.Structure);

        foreach (OecdDataResponseDataSet dataSet in genderEnt1Response.DataSets)
        {
            foreach (KeyValuePair<string, OecdDataResponseDataSetSeriesValue> seriesKeyValue in dataSet.Series)
            {
                GenderEnt1Dimension dimension = _transformer.TransformDimension(genderEnt1Structure, seriesKeyValue);
                GenderEnt1Attribute attribute = _transformer.TransformAttribute(genderEnt1Structure, seriesKeyValue);

                foreach (KeyValuePair<int, List<double?>> observationKeyValue in seriesKeyValue.Value.Observations)
                {
                    GenderEnt1Observation observation = _transformer.TransformObservation(genderEnt1Structure, observationKeyValue);

                    dataList.Add(new GenderEnt1(dimension, attribute, observation)); ;
                }
            }
        }

        return dataList;
    }
}
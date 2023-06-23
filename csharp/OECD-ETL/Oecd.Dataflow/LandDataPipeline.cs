namespace Oecd.Dataflow;

using Autofac.Features.AttributeFilters;
using Oecd.Common;
using Oecd.Data.Common;
using Oecd.Data.External;
using Oecd.Data.External.Dto;
using Oecd.Dataflow.Data;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks.Dataflow;

public class LandDataPipeline : IDataPipeline
{
    private readonly IOecdDataServiceAgent _genderEnt1ServiceAgent;
    private readonly IOecdDataResponseTransformer _transformer;
    private readonly IOecdRepository _repo;

    private readonly BufferBlock<OecdDataResponse> readOecdBlock;
    private readonly TransformManyBlock<OecdDataResponse, GenderEnt1> transformOecdResponseBlock;
    private readonly BatchBlock<GenderEnt1> writeBlock;

    private int _totalRecords = 0;

    public LandDataPipeline([KeyFilter("GENDER_ENT1")] IOecdDataServiceAgent agent, IOecdDataResponseTransformer transformer, IOecdRepository repo)
    {
        _genderEnt1ServiceAgent = agent;
        _transformer = transformer;
        _repo = repo;

        readOecdBlock = new BufferBlock<OecdDataResponse>();
        transformOecdResponseBlock = new TransformManyBlock<OecdDataResponse, GenderEnt1>(TransformOecdDataResponseToGenderEnt1);
        writeBlock = new BatchBlock<GenderEnt1>(100);

        DataflowLinkOptions linkOptions = new DataflowLinkOptions { PropagateCompletion = true };
        readOecdBlock.LinkTo(transformOecdResponseBlock, linkOptions);
        transformOecdResponseBlock.LinkTo(writeBlock, linkOptions);
    }

    public async Task Run()
    {
        // read OECD data -> transform -> write

        // read stage -> transform 1 -> write to stage -> transform 2 -> write to stage

        // read from land -> transform -> write to stage 1 -> transform 2 -> write to stage 2 -> final calculation -> write final

        await CleanupLand();
        await ReadOecdData(readOecdBlock);
        await WriteOecdData(writeBlock);

        writeBlock.Completion.Wait();
    }

    private async Task CleanupLand()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        await _repo.TruncateGenderEnt1();
        sw.Stop();
        Console.WriteLine($"CleanupLand complete [{sw.Elapsed.TotalSeconds}] s.");
    }

    private async Task ReadOecdData(BufferBlock<OecdDataResponse> readOecdBlock)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        OecdDataResponse? response = await _genderEnt1ServiceAgent.Get();
        if (response != null)
        {
            readOecdBlock.Post(response);
        }
        readOecdBlock.Complete();
        sw.Stop();
        Console.WriteLine($"ReadOecdData complete [{sw.Elapsed.TotalSeconds}] s.");
    }

    private IEnumerable<GenderEnt1> TransformOecdDataResponseToGenderEnt1(OecdDataResponse response)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        Dictionary<int, OecdDataResponseStructureDimensionSeries> seriesStructure = response.Structure.Dimensions.Series.ToDictionary(k => k.KeyPosition, v => v);
        foreach (OecdDataResponseDataSet dataSet in response.DataSets)
        {
            foreach (string dimensionString in dataSet.Series.Keys)
            {
                int[] dimensions = dimensionString.Split(':').Select(x => Convert.ToInt32(x)).ToArray();

                OecdDataResponseDataSetSeriesValue seriesValue = dataSet.Series[dimensionString];

                foreach (int observation in seriesValue.Observations.Keys)
                {
                    foreach (double? observationData in seriesValue.Observations[observation])
                    {
                        if (observationData == null) continue;

                        GenderEnt1 data = _transformer.Transform(response, seriesStructure, dimensions, seriesValue, observation, observationData);

                        yield return data;
                    }
                }
            }
        }
        sw.Stop();
        Console.WriteLine($"TransformOecdDataResponseToGenderEnt1 complete [{sw.Elapsed.TotalSeconds}] s.");
    }

    private async Task WriteOecdData(BatchBlock<GenderEnt1> writeBlock)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        while (await writeBlock.OutputAvailableAsync())
        {
            Stopwatch commitSw = Stopwatch.StartNew();
            GenderEnt1[] dataBlock = await writeBlock.ReceiveAsync();
            //Console.WriteLine($"Received [{data.Length}] records.");
            await _repo.AddGenderEnt1(dataBlock);
            commitSw.Stop();
            _totalRecords += dataBlock.Length;
            Console.WriteLine($"Committed records [{dataBlock.Length}]/[{_totalRecords}] in [{commitSw.Elapsed.TotalSeconds}] s.");
        }
        sw.Stop();
        Console.WriteLine($"WriteOecdData complete [{sw.Elapsed.TotalSeconds}] s.");
    }
}

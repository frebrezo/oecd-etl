// See https://aka.ms/new-console-template for more information

using Autofac;
using Oecd.Data.Common;
using Oecd.Dataflow;

using ILifetimeScope scope = DiConfig.Container.BeginLifetimeScope();

IEnumerable<IDataPipeline> dataPipelines = scope.Resolve<IEnumerable<IDataPipeline>>();
foreach (IDataPipeline dataPipeline in dataPipelines)
{
    await dataPipeline.Run();
}

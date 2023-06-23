using Autofac;
using Autofac.Core;
using Autofac.Features.AttributeFilters;
using Oecd.Data.Common;
using Oecd.Data.External;
using Oecd.Dataflow.Data;

namespace Oecd.Dataflow;

public class DiConfig
{
    public static ContainerBuilder Builder { get; private set; } = new ContainerBuilder();
    public static IContainer Container { get; private set; } = Configure();

    private static IContainer Configure()
    {
        Builder.Register(c => new OecdDataServiceAgent("GENDER_ENT1")).Named<IOecdDataServiceAgent>("GENDER_ENT1").SingleInstance();
        Builder.RegisterType<OecdDataResponseTransformer>().As<IOecdDataResponseTransformer>().SingleInstance();
        Builder.RegisterType<OecdRepository>().As<IOecdRepository>().SingleInstance();
        Builder.RegisterType<LandDataPipeline>().As<IDataPipeline>().WithAttributeFiltering().SingleInstance();

        return Builder.Build();
    }
}

using Oecd.Common;

namespace Oecd.Dataflow.Data;

public interface IOecdRepository
{
    Task TruncateGenderEnt1();
    Task AddGenderEnt1(IEnumerable<GenderEnt1> dataBlock);
}

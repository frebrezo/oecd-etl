using Oecd.Common;

namespace Oecd.Data.Service;

public interface IGenderEnt1Service
{
    Task<List<GenderEnt1>> GetGenderEnt1();
}

using Oecd.Data.External.Dto;

namespace Oecd.Data.External;

public  interface IOecdDataServiceAgent
{
    Task<OecdDataResponse?> Get();
}

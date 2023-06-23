using Dapper;
using Oecd.Common;
using System.Data;
using System.Data.SqlClient;

namespace Oecd.Dataflow.Data;

public class OecdRepository : IOecdRepository
{
    const string GenderEnt1Land_Insert = @"
INSERT INTO [dbo].[GenderEnt1Land]
           ([CountryId]
           ,[CountryName]
           ,[IndicatorId]
           ,[IndicatorName]
           ,[SexId]
           ,[SexName]
           ,[AgeId]
           ,[AgeName]
           ,[TimeFormatId]
           ,[TimeFormatName]
           ,[UnitId]
           ,[UnitName]
           ,[UnitMultiplierId]
           ,[UnitMultiplierName]
           ,[ReferencePeriodId]
           ,[ReferencePeriodName]
           ,[Year]
           ,[Value]
           ,[Status])
     VALUES
           (@CountryId
           ,@CountryName
           ,@IndicatorId
           ,@IndicatorName
           ,@SexId
           ,@SexName
           ,@AgeId
           ,@AgeName
           ,@TimeFormatId
           ,@TimeFormatName
           ,@UnitId
           ,@UnitName
           ,@UnitMultiplierId
           ,@UnitMultiplierName
           ,@ReferencePeriodId
           ,@ReferencePeriodName
           ,@Year
           ,@Value
           ,@Status)";

    public async Task TruncateGenderEnt1()
    {
        using IDbConnection db = new SqlConnection("Server=.;Database=Oecd;Trusted_Connection=True;");
        db.Open();
        await db.ExecuteScalarAsync("truncate table GenderEnt1Land", null);
        db.Close();
    }

    public async Task AddGenderEnt1(IEnumerable<GenderEnt1> dataBlock)
    {
        using IDbConnection db = new SqlConnection("Server=.;Database=Oecd;Trusted_Connection=True;");
        db.Open();
        using IDbTransaction dbTx = db.BeginTransaction();
        foreach (GenderEnt1 data in dataBlock)
        {
            await db.ExecuteScalarAsync(GenderEnt1Land_Insert, data, dbTx);
        }
        dbTx.Commit();
        db.Close();
    }
}

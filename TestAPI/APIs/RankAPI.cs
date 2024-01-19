using DataAccess.Data;
using Model;
using AutoMapper;
using DTO;

namespace TESTAPI.APIs;

public static class RankAPI
{
    public static void ConfigureRankAPI(this WebApplication app)
    {
        app.MapGet("/Ranks", GetRanks);
        app.MapGet("/Ranks/{id}", GetRank);
        app.MapPost("/Ranks", InsertRank);
        app.MapPut("/Ranks", UpdateRank);
        app.MapDelete("/Ranks", DeleteRank);
    }

    private static async Task<IResult> GetRanks(IRankData RankData)
    {
        try
        {
            return Results.Ok(await RankData.GetRanks());
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }

    }

    private static async Task<IResult> GetRank(int id, IRankData RankData)
    {
        try
        {
            var Rank = await RankData.GetRank(id);
            if (Rank == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(Rank);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> InsertRank(IMapper mapper,RankModel _model, IRankData RankData)
    {
        try
        {
            RankModel model = mapper.Map<RankModel>(_model);
            await RankData.InsertRank(model);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> UpdateRank(IMapper mapper, RankModel _model, IRankData RankData)
    {
        try
        {
            RankModel model = mapper.Map<RankModel>(_model);
            await RankData.UpdateRank(model);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> DeleteRank(int id, IRankData RankData)
    {
        try
        {
            var Rank = RankData.GetRank(id);
            if (Rank == null)
            {
                return Results.NotFound();
            }
            await RankData.DeleteRank(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}


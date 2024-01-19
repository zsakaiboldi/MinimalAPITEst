using DataAccess.Data;
using Modell.Model;
using AutoMapper;
using DTO;

namespace TESTAPI.APIs;

public static class StatusAPI
{
    public static void ConfigureStatusAPI(this WebApplication app)
    {
        app.MapGet("/Statuses", GetStatuses);
        app.MapGet("/Statuses/{id}", GetStatus);
        app.MapPost("/Statuses", InsertStatus);
        app.MapPut("/Statuses", UpdateStatus);
        app.MapDelete("/Statuses", DeleteStatus);
    }

    private static async Task<IResult> GetStatuses(IStatusData StatusData)
    {
        try
        {
            return Results.Ok(await StatusData.GetStatuses());
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }

    }

    private static async Task<IResult> GetStatus(int id, IStatusData StatusData)
    {
        try
        {
            var Status = await StatusData.GetStatus(id);
            if (Status == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(Status);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> InsertStatus(IMapper mapper,StatusModelDTO _model, IStatusData StatusData)
    {
        try
        {
            StatusModel model = mapper.Map<StatusModel>(_model);
            model.Changed = DateTime.Now;
            await StatusData.InsertStatus(model);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> UpdateStatus(IMapper mapper, StatusModelDTO _model, IStatusData StatusData)
    {
        try
        {
            StatusModel model = mapper.Map<StatusModel>(_model);
            model.Changed = DateTime.Now;
            await StatusData.UpdateStatus(model);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> DeleteStatus(int id, IStatusData StatusData)
    {
        try
        {
            var Status = StatusData.GetStatus(id);
            if (Status == null)
            {
                return Results.NotFound();
            }
            await StatusData.DeleteStatus(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}



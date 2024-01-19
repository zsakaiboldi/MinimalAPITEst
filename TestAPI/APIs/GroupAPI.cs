using DataAccess.Data;
using Modell.Model;
using AutoMapper;
using DTO;

namespace TESTAPI.APIs;

public static class GroupAPI
{
    public static void ConfigureGroupAPI(this WebApplication app) 
    {
        app.MapGet("/Groups", GetGroups);
        app.MapGet("/Groups/{id}", GetGroup);
        app.MapPost("/Groups", InsertGroup);
        app.MapPut("/Groups", UpdateGroup);
        app.MapDelete("/Groups", DeleteGroup);
    }

    private static async Task<IResult> GetGroups(IGroupData groupData)
    {
        try
        {
            return Results.Ok(await groupData.GetGroups());
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }

    }

    private static async Task<IResult> GetGroup(int id, IGroupData groupData)
    {
        try
        {
            var group = await groupData.GetGroup(id);
            if (group == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(group);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> InsertGroup(IMapper mapper,GroupModelDTO _model, IGroupData groupData)
    {
        try
        {
            GroupModel model = mapper.Map<GroupModel>(_model);
            model.Date = DateTime.Now;
            await groupData.InsertGroup(model);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> UpdateGroup(IMapper mapper, GroupModelDTO _model, IGroupData groupData)
    {
        try
        {
            GroupModel model = mapper.Map<GroupModel>(_model);
            await groupData.UpdateGroup(model);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> DeleteGroup(int id, IGroupData groupData)
    {
        try
        {
            var group = groupData.GetGroup(id);
            if (group == null)
            {
                return Results.NotFound();
            }
            await groupData.DeleteGroup(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}


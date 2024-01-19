using DataAccess.Data;
using Modell.Model;
using AutoMapper;
using DTO;

namespace TESTAPI.APIs;

public static class RoleAPI
{
    public static void ConfigureRoleAPI(this WebApplication app)
    {
        app.MapGet("/Roles", GetRoles);
        app.MapGet("/Roles/{id}", GetRole);
        app.MapPost("/Roles", InsertRole);
        app.MapPut("/Roles", UpdateRole);
        app.MapDelete("/Roles", DeleteRole);
    }

    private static async Task<IResult> GetRoles(IRoleData RoleData)
    {
        try
        {
            return Results.Ok(await RoleData.GetRoles());
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }

    }

    private static async Task<IResult> GetRole(int id, IRoleData RoleData)
    {
        try
        {
            var Role = await RoleData.GetRole(id);
            if (Role == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(Role);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> InsertRole(IMapper mapper, RoleModelDTO _model, IRoleData RoleData)
    {
        try
        {
            RoleModel model = mapper.Map<RoleModel>(_model);
            model.Date = DateTime.Now;
            await RoleData.InsertRole(model);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> UpdateRole(IMapper mapper, RoleModelDTO _model, IRoleData RoleData)
    {
        try
        {
            RoleModel model = mapper.Map<RoleModel>(_model);
            model.Date = DateTime.Now;
            await RoleData.UpdateRole(model);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> DeleteRole(int id, IRoleData RoleData)
    {
        try
        {
            var Role = RoleData.GetRole(id);
            if (Role == null)
            {
                return Results.NotFound();
            }
            await RoleData.DeleteRole(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}


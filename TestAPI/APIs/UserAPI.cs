using Model;
using DataAccess.Data;
using AutoMapper;
using DTO;
using ServiceStack;
using TESTAPI.Authentication;

namespace TESTAPI.APIs;


public static class UserAPI
{

    

    public static void ConfigureUserAPI(this WebApplication app)
    {

        app.MapGet("/Users", GetUsers).AddEndpointFilter<ApiKeyEndpointFilter>();
        app.MapGet("/Users/{id}", GetUser).AddEndpointFilter<ApiKeyEndpointFilter>();
        app.MapPost("/Users", InsertUser).AddEndpointFilter<ApiKeyEndpointFilter>();
        app.MapPut("/Users", UpdateUser).AddEndpointFilter<ApiKeyEndpointFilter>();
        app.MapDelete("/Users", DeleteUser).AddEndpointFilter<ApiKeyEndpointFilter>();
    }
    
    private static async Task<IResult> GetUsers(IUserData userData)
    {
        try
        {            
            return Results.Ok(await userData.GetUsers());
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }

    }

    private static async Task<IResult> GetUser(int id, IUserData userData)
    {
        try
        {
            var user = await userData.GetUser(id);
            if (user == null)
            {
                return Results.NotFound("User not found");
            }
            return Results.Ok(user);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> InsertUser(IMapper mapper,UserModelDTO _model, IUserData userData)
    {
        try
        {
            UserModel model = mapper.Map<UserModel>(_model);
            model.Registration_Date = DateTime.Now;
            await userData.InsertUser(model);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> UpdateUser(IMapper mapper, UserModelDTO _model, IUserData userData)
    {
        try
        {

            UserModel model = mapper.Map<UserModel>(_model);
            await userData.UpdateUser(model);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> DeleteUser(int id, IUserData userData)
    {
        try
        {
            var user = userData.GetUser(id);
            if (user == null)
            {
                return Results.NotFound();
            }
            await userData.DeleteUser(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

}

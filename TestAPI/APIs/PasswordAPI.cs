using Modell.Model;
using DATA.Data;
using AutoMapper;
using DTO;

namespace TESTAPI.APIs;

public static class PasswordAPI
{
    public static void ConfigurePasswordAPI(this WebApplication app)
    {
        app.MapGet("/Passwords", GetPasswords);
        app.MapGet("/Passwords/{id}", GetPassword);
        app.MapGet("/UserPasswords/{id}", GetUserPassword);
        app.MapPost("/Passwords", InsertPassword);
        app.MapPut("/Passwords", UpdatePassword);
        app.MapDelete("/Passwords", DeletePassword);
    }

    private static async Task<IResult> GetPasswords(IPasswordData PasswordData)
    {
        try
        {
            return Results.Ok(await PasswordData.GetPasswords());
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }

    }

    private static async Task<IResult> GetPassword(int id, IPasswordData PasswordData)
    {
        try
        {
            var Password = await PasswordData.GetPassword(id);
            if (Password == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(Password);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> GetUserPassword(int id, IPasswordData PasswordData)
    {
        try
        {
            var Password = await PasswordData.GetUserPassword(id);
            if (Password == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(Password);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> InsertPassword(IMapper mapper,PasswordModelDTO _model, IPasswordData PasswordData)
    {
        try
        {
            PasswordModel model = mapper.Map<PasswordModel>(_model);
            var _pm = new PasswordManager();
            var list = new List<string>();
            list = _pm.HashPasword(model.Raw_Password, out var salt);
            model.Hashed_Password = list[0];
            model.Salt = list[1];
            model.Created = DateTime.Now;
            await PasswordData.InsertPassword(model);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> UpdatePassword(IMapper mapper, PasswordModelDTO _model, IPasswordData PasswordData)
    {
        try
        {
            PasswordModel model = mapper.Map<PasswordModel>(_model);
            await PasswordData.UpdatePassword(model);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> DeletePassword(int id, IPasswordData PasswordData)
    {
        try
        {
            var Password = PasswordData.GetPassword(id);
            if (Password == null)
            {
                return Results.NotFound();
            }
            await PasswordData.DeletePassword(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}



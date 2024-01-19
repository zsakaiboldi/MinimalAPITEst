using Modell.Model;
using AutoMapper;
using DTO;

namespace TESTAPI.APIs;

public static class ContactAPI
{
    public static void ConfigureContactAPI(this WebApplication app)
    {
        app.MapGet("/Contacts", GetContacts);
        app.MapGet("/Contacts/{id}", GetContact);
        app.MapPost("/Contacts", InsertContact);
        app.MapPut("/Contacts", UpdateContact);
        app.MapDelete("/Contacts", DeleteContact);
    }

    private static async Task<IResult> GetContacts(IContactData ContactData)
    {
        try
        {
            return Results.Ok(await ContactData.GetContacts());
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }

    }

    private static async Task<IResult> GetContact(int id, IContactData ContactData)
    {
        try
        {
            var Contact = await ContactData.GetContact(id);
            if (Contact == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(Contact);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> InsertContact(IMapper mapper,ContactModelDTO _model, IContactData ContactData)
    {
        try
        {
            ContactModel model = mapper.Map<ContactModel>(_model);
            await ContactData.InsertContact(model);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> UpdateContact(IMapper mapper, ContactModelDTO _model, IContactData ContactData)
    {
        try
        {
            ContactModel model = mapper.Map<ContactModel>(_model);
            await ContactData.UpdateContact(model);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> DeleteContact(int id, IContactData ContactData)
    {
        try
        {
            var Contact = ContactData.GetContact(id);
            if (Contact == null)
            {
                return Results.NotFound();
            }
            await ContactData.DeleteContact(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}


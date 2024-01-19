using DATA.Data;
using DataAccess;
using DataAccess.Data;
using DTO;
using Microsoft.OpenApi.Models;
using TESTAPI.APIs;
using TESTAPI.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication();
//builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(typeof(MapperConfig).Assembly);
builder.Services.AddSingleton<ISQLDataAccess, SQLDataAccess>();
builder.Services.AddSingleton<IUserData, UserData>();
builder.Services.AddSingleton<IGroupData, GroupData>();
builder.Services.AddSingleton<IContactData, ContactData>();
builder.Services.AddSingleton<IRankData, RankData>();
builder.Services.AddSingleton<IRoleData, RoleData>();
builder.Services.AddSingleton<IStatusData, StatusData>();
builder.Services.AddSingleton<IPasswordData, PasswordData>();
builder.Services.AddSwaggerGen(x => {
    x.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "The API Key to access the API",
        Type = SecuritySchemeType.ApiKey,
        Name = "Api-Key",
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });
    var scheme = new OpenApiSecurityScheme {
        Reference = new OpenApiReference {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header,
    };
    var requirement = new OpenApiSecurityRequirement
    {
        {scheme, new List<string> {} }
    };
    x.AddSecurityRequirement(requirement);
});

//builder.Services.AddScoped<ApiKeyAuthFilter>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthentication();
//app.UseAuthorization();

app.ConfigureUserAPI();
app.ConfigureGroupAPI();
app.ConfigureContactAPI();
app.ConfigureRankAPI();
app.ConfigureRoleAPI();
app.ConfigureStatusAPI();
app.ConfigurePasswordAPI();


app.Run();

namespace TESTAPI.Authentication;

public class ApiKeyEndpointFilter : IEndpointFilter
{


    private readonly IConfiguration _configuration;

    public ApiKeyEndpointFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstant.ApiKeyHeaderName, out var extractedApiKey)) 
        {
            return new UnAthorizedHttpObjectResult("API key missing");
        }
        var ApiKey = _configuration.GetValue<string>(AuthConstant.ApiKeySectionName);
        if (!ApiKey.Equals(extractedApiKey))
        {
            return new UnAthorizedHttpObjectResult("API key invalid");
        }

        return await next(context);
    }
}


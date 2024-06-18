using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims; 

using System.Threading.Tasks;

public class CustomAuthenticationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Authentication logic example (replace with your actual authentication mechanism)
        if (!IsAuthenticated(context.HttpContext))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // Continue to the action
        await next();
    }

    private bool IsAuthenticated(HttpContext context)
    {
        // Check if there's a valid JWT token in the request headers
        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        if (authHeader != null && authHeader.StartsWith("Bearer "))
        {
            var token = authHeader.Substring("Bearer ".Length).Trim();
            // Example: Validate and decode the JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("WTERJCBGWUIDB234NHBCJBJJB1QCBZOMMG")),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            try
            {
                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken) as ClaimsPrincipal;

                // Check if principal is authenticated
                return principal?.Identity?.IsAuthenticated ?? false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        return false;
    }

}

public class CustomResultFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var resultContext = await next();
        Console.WriteLine("Before executing");

        // Access the result
        var result = resultContext.Result;

        if (result is ObjectResult objectResult)
        {
            Console.WriteLine($"Result: {objectResult.Value}");
        }
        else if (result is ContentResult contentResult)
        {
            Console.WriteLine($"Result: {contentResult.Content}");
        }
        else if (result is StatusCodeResult statusCodeResult)
        {
            Console.WriteLine($"Result: {statusCodeResult.StatusCode}");
        }
        else
        {
            Console.WriteLine("Result: Unknown type");
        }

        Console.WriteLine("After executing");

    }
}

public class CustomExceptionFilter : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        var exception = context.Exception;  
        if(exception.InnerException != null )
        {
            Console.WriteLine(exception.Message);
        }
        else
        {
            Console.WriteLine(exception.Message);
        }

        return Task.CompletedTask;
    }
}


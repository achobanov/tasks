namespace VL.Challenge.API.Filters;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate _next)
    {
        this._next = _next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ApplicationException ex)
        {
            context.Response.StatusCode = (int)StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(ex.Message);
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{DateTime.Now}: {ex.Message}" + Environment.NewLine + ex.StackTrace);
            context.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("Please try again later"!);
            return;
        }
    }
}

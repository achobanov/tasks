using Challenge.Common;
using Challenge.Domain.Core;
using Challenge.Web.Common.Contracts;

namespace Challenge.API.Filters;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly INotifier _notifier;

    public ErrorHandlerMiddleware(RequestDelegate _next, INotifier notifier)
    {
        this._next = _next;
        _notifier = notifier;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException validation)
        {
            context.Response.StatusCode = (int)StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(validation.Message);
            return;
        }
        catch (DomainAggregateException validations)
        {
            context.Response.StatusCode = (int)StatusCodes.Status400BadRequest;
            var contract = new AggregateValidationContract(validations);
            await context.Response.WriteAsJsonAsync(contract);
            return;
        }
        catch (Exception ex)
        {
            await _notifier.Error(ex.Message, ex.StackTrace);
            context.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("Please try again later"!);
            return;
        }
    }
}

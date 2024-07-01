using Challenge.Common.Injection;
using Challenge.Domain.Core;
using Challenge.Web.Client.Toasts;

namespace Challenge.Web.Client.Shared;

public class Safe : ISafe
{
    private readonly IToaster _toaster;

    public Safe(IToaster toaster)
    {
        _toaster = toaster;
    }

    public async Task Execute(Func<Task> func)
    {
        try
        {
            await func();
        }
        catch (DomainException validation)
        {
            HandleValidation(validation);
        }
        catch (DomainAggregateException aggregate)
        {
            await _toaster.Validation(aggregate.Message, $"Validation errors: {aggregate.Validations.Count()}");
            foreach (var validation in aggregate.Validations)
            {
                HandleValidation(validation, 20);
            }
        }
        catch (Exception ex)
        {
            await _toaster.Error(ex.Message, ex.StackTrace);
        }
    }

    private void HandleValidation(DomainException validation, int time = 10)
    {
        _toaster.Validation(validation.Message, validation?.StackTrace);
    }
}

public interface ISafe : ITransient
{
    Task Execute(Func<Task> func);
}

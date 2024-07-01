using Challenge.Common;
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
            await _toaster.Validation(validation.Message, validation.StackTrace);
        }
        catch (Exception ex)
        {
            await _toaster.Error(ex.Message, ex.StackTrace);
        }
    }
}

public interface ISafe : ITransient
{
    Task Execute(Func<Task> func);
}

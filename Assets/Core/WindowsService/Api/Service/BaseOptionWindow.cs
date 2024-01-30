using Core.WindowsService.Data;

namespace Core.WindowsService.Api.Service
{
    public abstract class BaseOptionWindow<TOption> : BaseWindow where TOption : WindowsOption
    {
        protected TOption CastedOption => (TOption)_option;
    }
}
using System;

namespace NuevoInterView.CMS.Repository.Interface
{
    public interface IServiceProviderAccessor
    {
        IServiceProvider ServiceProvider { get; set; }
    }
}

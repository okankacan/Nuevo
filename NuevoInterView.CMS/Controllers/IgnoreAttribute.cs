using System;

namespace NuevoInterView.CMS.Controllers
{
    /// <summary>
    ///  IgnoreAttribute ile daha önceden belirtiğiniz flitrelerden kaçabilirsiniz. Örneğin loginFilter dan
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class IgnoreAttribute : Attribute
    {
    }
}
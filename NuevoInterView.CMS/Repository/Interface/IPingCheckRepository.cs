using NuevoInterView.CMS.Models;
using System.Threading.Tasks;

namespace NuevoInterView.CMS.Repository.Interface
{
    public interface IPingCheckRepository
    {
        Task<PingResponse> Check(string url);
    }
}

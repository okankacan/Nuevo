using System.Threading.Tasks;

namespace NuevoInterView.CMS.Repository.Interface
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string to, string body, string subject, byte[][] attachmentDatas = null, string[] ccList = null, (byte[] data, string fileNameWithExtension)[] fileAttachments = null);
    }
}

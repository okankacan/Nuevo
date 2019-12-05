using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NuevoInterView.CMS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace NuevoInterView.CMS.Repository

{
    public class EmailService : IEmailService
    {
         private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
   
        }

        public async Task<bool> SendEmailAsync(string to, string body, string subject, byte[][] attachmentDatas = null, string[] ccList = null, (byte[] data, string fileNameWithExtension)[] fileAttachments = null)
        {
            var streamList = new List<Stream>();
            try
            {

                    var mail = new MailMessage();
                    var fromAddress = _configuration.GetSection("EmailSettings:RegisterFromAddress").Value;
                
                var displayName = _configuration.GetSection("EmailSettings:RegisterDisplayAddress").Value;

                var htmlView = AlternateView.CreateAlternateViewFromString(
                                        body,
                                        Encoding.UTF8,
                                        MediaTypeNames.Text.Html);

                    if (attachmentDatas == null || attachmentDatas.Length == 0)
                    {
                        mail.Body = body;
                    }
                    else
                    {
                        string mediaType = MediaTypeNames.Image.Jpeg;
                        for (int i = 0; i < attachmentDatas.Length; i++)
                        {
                            var ms = new MemoryStream(attachmentDatas[i]);
                            streamList.Add(ms);
                            var img = new LinkedResource(ms, mediaType)
                            {
                                ContentId = "@@Embedded" + i,
                                TransferEncoding = TransferEncoding.Base64
                            };
                            img.ContentType.MediaType = mediaType;
                            img.ContentType.Name = img.ContentId;
                            img.ContentLink = new Uri("cid:" + img.ContentId);
                            htmlView.LinkedResources.Add(img);

                        }
                        mail.AlternateViews.Add(htmlView);
                    }

                    if (fileAttachments != null && fileAttachments.Any())
                        foreach (var file in fileAttachments)
                            mail.Attachments.Add(new Attachment(new MemoryStream(file.data), file.fileNameWithExtension));

              

                    mail.From = new MailAddress(fromAddress, displayName);
                    mail.To.Add(to);
                    mail.Subject = subject;
                    mail.IsBodyHtml = true;

                    try
                    {
                    
                    var smtpMailClient = new SmtpClient(_configuration.GetSection("EmailSettings:SmtpHost").Value, int.Parse(_configuration.GetSection("EmailSettings:SmtpPort").Value))
                        {
                            UseDefaultCredentials = true,
                            Credentials =
                                new NetworkCredential(_configuration.GetSection("EmailSettings:RegisterSendAddress").Value,
                                _configuration.GetSection("EmailSettings:RegisterSendAddress").Value),
                            EnableSsl = true,
                            Timeout = 50000
                        };
                        await smtpMailClient.SendMailAsync(mail);
                        Console.WriteLine($"mail ok : {to}");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        
                        return false;
                    }
               
            }
            catch (Exception ex)
            {
               
                return false;
            }
            finally
            {
                streamList.ForEach(c => c?.Dispose());
            }
        }
 
    }
}
using NuevoInterView.CMS.Models;
using NuevoInterView.CMS.Repository.Interface;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NuevoInterView.CMS.Repository
{
    public class PingCheckRepository : IPingCheckRepository
    {

        private readonly HttpClient _httpClient;

        public PingCheckRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PingResponse> Check(string url)
        {
            try
            {
                var checkPing = await _httpClient.GetAsync(url);
                var pingControl = new PingResponse()
                {
                    Status = checkPing.StatusCode,

                };
                return pingControl;

            }
            catch (Exception ex)
            {
                var pingControl = new PingResponse()
                {
                    Status = HttpStatusCode.BadRequest
                };
                return pingControl;
            }
        }
    }
}

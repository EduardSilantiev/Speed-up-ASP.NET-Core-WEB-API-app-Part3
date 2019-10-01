using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SpeedUpCoreAPIExample.Interfaces;
using SpeedUpCoreAPIExample.Settings;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpeedUpCoreAPIExample.Helpers
{
    // HttpClient for application's own controllers access 
    public class SelfHttpClient : ISelfHttpClient
    {
        private readonly HttpClient _client;

        public SelfHttpClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IOptions<ApiSettings> settings)
        {
            string baseAddress = string.Format("{0}://{1}/api/v{2}/",
                                                httpContextAccessor.HttpContext.Request.Scheme,
                                                httpContextAccessor.HttpContext.Request.Host,
                                                settings.Value.Version);

            _client = httpClient;
            _client.BaseAddress = new Uri(baseAddress);
        }

        /// <summary>
        /// Call any controller's action with HttpPost method and Id parameter.
        /// </summary>
        /// <param name="apiRoute">Relative API route.</param>
        /// <param name="id">The parameter.</param>
        public async Task PostIdAsync(string apiRoute, string id)
        {
            try
            {
                var result = await _client.PostAsync(string.Format("{0}/{1}", apiRoute, id), null).ConfigureAwait(false);
            }
            catch
            {
                //ignore errors
            }
        }
    }
}

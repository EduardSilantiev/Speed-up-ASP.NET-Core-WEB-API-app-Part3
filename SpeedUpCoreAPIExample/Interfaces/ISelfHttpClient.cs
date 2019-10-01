using System.Threading.Tasks;

namespace SpeedUpCoreAPIExample.Interfaces
{
    /// <summary>
    /// HttpClient for application's own controllers access 
    /// </summary>
    public interface ISelfHttpClient
    {
        /// <summary>
        /// Call any controller's action with HttpPost method and Id parameter.
        /// </summary>
        /// <param name="apiRoute">Relative API route.</param>
        /// <param name="id">The parameter.</param>
        Task PostIdAsync(string apiRoute, string id);
    }
}

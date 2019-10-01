using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpeedUpCoreAPIExample.Filters;
using SpeedUpCoreAPIExample.Interfaces;
using SpeedUpCoreAPIExample.Swagger;
using SpeedUpCoreAPIExample.ViewModels;
using Swashbuckle.AspNetCore.Examples;

namespace SpeedUpCoreAPIExample.Controllers
{
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]/")]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
    [ApiController]
    [ValidateId]
    public class PricesController : ControllerBase
    {
        private readonly IPricesService _pricesService;

        public PricesController(IPricesService pricesService)
        {
            _pricesService = pricesService;
        }

        /// <summary>
        /// Gets Prices by product Id.
        /// </summary>
        /// <remarks>GET /api/v1/prices/1</remarks>
        /// <param name="id">Product's Id.</param>
        /// <param name="pageIndex">Index of page to display (if not set, defauld value = 1 - first page is used).</param>
        /// <param name="pageSize">Size of page (if not set, defauld value is used).</param>
        /// <returns>List of prices swith pagination state</returns>
        /// <response code="200">Prices found and returned successfully.</response>
        [ProducesResponseType(typeof(PricesPageViewModel), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(PricesExamples))]
        [HttpGet("{id}")]
        [ValidatePaging]
        public async Task<IActionResult> GetPricesAsync(int id, int pageIndex, int pageSize)
        {
            PricesPageViewModel pricesPageViewModel = await _pricesService.GetPricesAsync(id, pageIndex, pageSize);

            return new OkObjectResult(pricesPageViewModel);
        }

        /// <summary>
        /// Prepare Prices by product Id.
        /// </summary>
        /// <remarks>POST api/v1/prices/prepare/5</remarks>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("prepare/{id}")]
        public async Task<IActionResult> PreparePricesAsync(int id)
        {
            await _pricesService.PreparePricesAsync(id);

            return Ok();
        }
    }
}
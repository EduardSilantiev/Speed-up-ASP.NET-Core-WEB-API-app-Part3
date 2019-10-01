using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpeedUpCoreAPIExample.Swagger;
using SpeedUpCoreAPIExample.Filters;
using SpeedUpCoreAPIExample.Interfaces;
using SpeedUpCoreAPIExample.ViewModels;
using Swashbuckle.AspNetCore.Examples;
using System.Threading.Tasks;

namespace SpeedUpCoreAPIExample.Controllers
{
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]/")]
    [ApiController]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        /// <summary>
        /// Gets all Products with pagination.
        /// </summary>
        /// <remarks>GET /api/v1/products/?pageIndex=1&amp;pageSize=20</remarks>
        /// <param name="pageIndex">Index of page to display (if not set, defauld value = 1 - first page is used).</param>
        /// <param name="pageSize">Size of page (if not set, defauld value is used).</param>
        /// <returns>List of product swith pagination state</returns>
        /// <response code="200">Products found and returned successfully.</response>
        [ProducesResponseType(typeof(ProductsPageViewModel), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ProductsExample))]
        [HttpGet]
        [ValidatePaging]
        public async Task<IActionResult> GetAllProductsAsync(int pageIndex, int pageSize)
        {
            ProductsPageViewModel productsPageViewModel = await _productsService.GetAllProductsAsync(pageIndex, pageSize);

            return new OkObjectResult(productsPageViewModel);
        }

        /// <summary>
        /// Gets a Product by Id.
        /// </summary>
        /// <remarks>GET /api/v1/products/1</remarks>
        /// <param name="id">Product's Id.</param>
        /// <returns>A Product information</returns>
        /// <response code="200">Product found and returned successfully.</response>
        /// <response code="404">Product was not found.</response>
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ProductExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProductNotFoundExample))]
        [HttpGet("{id}")]
        [ValidateId]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            ProductViewModel productViewModel = await _productsService.GetProductAsync(id);

            return new OkObjectResult(productViewModel);
        }

        /// <summary>
        /// Find Products by SKU with pagination
        /// </summary>
        /// <remarks>GET /api/v1/products/find/aaa/?pageIndex=1&amp;pageSize=20</remarks>
        /// <param name="sku">Search pattern.</param>
        /// <param name="pageIndex">Index of page to display (if not set, defauld value = 1 - first page is used).</param>
        /// <param name="pageSize">Size of page (if not set, defauld value is used).</param>
        /// <returns>List of product swith pagination state</returns>
        /// <response code="200">Products found and returned successfully.</response>
        [ProducesResponseType(typeof(ProductsPageViewModel), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ProductsExample))]
        [HttpGet("find/{sku}")]
        [ValidatePaging]
        public async Task<IActionResult> FindProductsAsync(string sku, int pageIndex, int pageSize)
        {
            ProductsPageViewModel productsPageViewModel = await _productsService.FindProductsAsync(sku, pageIndex, pageSize);

            return new OkObjectResult(productsPageViewModel);
        }

        /// <summary>
        /// Delete a Product by Id.
        /// </summary>
        /// <remarks>DELETE /api/v1/products/1</remarks>
        /// <param name="id">Product's Id.</param>
        /// <returns>A Product information</returns>
        /// <response code="200">Product found and deleted successfully.</response>
        /// <response code="404">Product was not found.</response>
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ProductExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProductNotFoundExample))]
        [HttpDelete("{id}")]
        [ValidateId]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            ProductViewModel productViewModel = await _productsService.DeleteProductAsync(id);

            return new OkObjectResult(productViewModel);
        }
    }
}
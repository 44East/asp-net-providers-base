using Microsoft.AspNetCore.Mvc;
using ProvidersBase.Models.DTO;
using ProvidersBase.Models.Entities;
using ProvidersBase.Services.DataAccessLayer;

namespace ProvidersBase.Controllers
{
    [ApiController]
    [Route("/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IEntityTransactor<ProviderProductDTO> _entityTransactor;

        public ProductsController(IEntityTransactor<ProviderProductDTO> entityTransactor)
        {
            _entityTransactor = entityTransactor;
        }
        /// <summary>
        /// Get all <see cref="ProviderProduct"/> from DB in DTO form
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _entityTransactor.GetAllEntitiesAsync());
        }
        /// <summary>
        /// Get the <see cref="ProviderProduct"/> by Id from DB
        /// </summary>
        /// <param name="id">The product id</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                return Ok(await _entityTransactor.GetEntityByIdAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Get the <see cref="ProviderProduct"/> by company inn from DB
        /// </summary>
        /// <param name="inn">The tax number of the company</param>
        [HttpGet("inn/{inn}")]
        public async Task<IActionResult> GetAllProductsByProviderINN(string inn)
        {
            try
            {
                return Ok(await _entityTransactor.GetAllEntityByINNAsync(inn));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Create the new <see cref="ProviderProduct"/> object in DB
        /// </summary>
        /// <param name="providerProductDTO">DTO object for mapping</param>
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProviderProductDTO providerProductDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The some fields don't have valid values");
            }
            try
            {
                return Ok($"Is the operation completed - {await _entityTransactor.CreateEntityAsync(providerProductDTO)}?");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Edit a current <see cref="ProviderProduct"/> object in DB
        /// </summary>
        /// <param name="id">Id for searching in DB</param>
        /// <param name="providerProductDTO">DTO object for mapping</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProviderProductDTO providerProductDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The some fields don't have valid values");
            }
            try
            {
                return Ok($"Is the operation completed - {await _entityTransactor.UpdateEntityAsync(id, providerProductDTO)}?");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Delete the current <see cref="ProviderProduct"/> object in DB
        /// </summary>
        /// <param name="id">searching parametr</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                return Ok($"Is the operation completed - {await _entityTransactor.DeleteEntityAsync(id)}?");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

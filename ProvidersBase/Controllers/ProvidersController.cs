using Microsoft.AspNetCore.Mvc;
using ProvidersBase.Models.DTO;
using ProvidersBase.Models.Entities;
using ProvidersBase.Services.DataAccessLayer;

namespace ProvidersBase.Controllers
{
    [ApiController]
    [Route("api/providers")]
    public class ProvidersController : ControllerBase
    {
        private readonly IEntityTransactor<ProviderCompanyDTO> _entityTransactor;

        public ProvidersController(IEntityTransactor<ProviderCompanyDTO> entityTransactor)
        {
            _entityTransactor = entityTransactor;
        }
        /// <summary>
        /// Get all <see cref="ProviderCompany"/> from DB
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllProviders()
        {
            return Ok(await _entityTransactor.GetAllEntitiesAsync());
        }
        /// <summary>
        /// Get the <see cref="ProviderCompany"/> by Id from DB
        /// </summary>
        /// <param name="id">The company id</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProviderById(int id)
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
        /// Get the <see cref="ProviderCompany"/> by company inn from DB
        /// </summary>
        /// <param name="inn">The tax number of the company</param>
        [HttpGet("inn/{inn}")]
        public async Task<IActionResult> GetAllProvidersByINN(string inn)
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
        /// Create the new <see cref="ProviderCompany"/> object in DB
        /// </summary>
        /// <param name="providerCompanyDTO">DTO object for mapping</param>
        [HttpPost]
        public async Task<IActionResult> CreateProvider(ProviderCompanyDTO providerCompanyDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The some fields don't have valid values");
            }
            try
            {
                return Ok($"Is the operation completed - {await _entityTransactor.CreateEntityAsync(providerCompanyDTO)}?");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// Edit a current <see cref="ProviderCompany"/> object in DB
        /// </summary>
        /// <param name="id">Id for searching in DB</param>
        /// <param name="providerCompanyDTO">DTO object for mapping</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProvider(int id, ProviderCompanyDTO providerCompanyDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The some fields don't have valid values");
            }
            try
            {
                return Ok($"Is the operation completed - {await _entityTransactor.UpdateEntityAsync(id, providerCompanyDTO)}?");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Delete the current <see cref="ProviderCompany"/> object in DB
        /// </summary>
        /// <param name="id">searching parametr</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvider(int? id)
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

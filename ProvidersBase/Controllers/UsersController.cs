using Microsoft.AspNetCore.Mvc;
using ProvidersBase.Models.DTO;
using ProvidersBase.Models.Entities;
using ProvidersBase.Services.DataAccessLayer;

namespace ProvidersBase.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IEntityTransactor<ProviderUserDTO> _entityTransactor;

        public UsersController(IEntityTransactor<ProviderUserDTO> entityTransactor)
        {
            _entityTransactor = entityTransactor;
        }
        /// <summary>
        /// Get all <see cref="ProviderUser"/> from DB
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _entityTransactor.GetAllEntitiesAsync());
        }

        /// <summary>
        /// Get the <see cref="ProviderUser"/> by Id from DB
        /// </summary>
        /// <param name="id">The user id</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
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
        /// Get the <see cref="ProviderUser"/> by company inn from DB
        /// </summary>
        /// <param name="inn">The tax number of the company</param>
        [HttpGet("inn/{inn}")]
        public async Task<IActionResult> GetAllUsersByProviderINN(string inn)
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
        /// Create the new <see cref="ProviderUser"/> object in DB
        /// </summary>
        /// <param name="providerUserDTO">DTO object for mapping</param>
        [HttpPost]
        public async Task<IActionResult> CreateUser(ProviderUserDTO providerUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The some fields don't have valid values");
            }
            try
            {
                return Ok($"Is the operation completed - {await _entityTransactor.CreateEntityAsync(providerUserDTO)}?");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Edit a current <see cref="ProviderUser"/> object in DB
        /// </summary>
        /// <param name="id">Id for searching in DB</param>
        /// <param name="providerUserDTO">DTO object for mapping</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, ProviderUserDTO providerUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The some fields don't have valid values");
            }
            try
            {
                return Ok($"Is the operation completed - {await _entityTransactor.UpdateEntityAsync(id, providerUserDTO)}?");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Delete the current <see cref="ProviderUser"/> object in DB
        /// </summary>
        /// <param name="id">searching parametr</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int? id)
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

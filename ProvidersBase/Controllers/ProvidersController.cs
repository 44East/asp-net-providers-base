using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvidersBase.Model.DataAccessLayer;
using ProvidersBase.Model.DTO;
using ProvidersBase.Model.Mappers;
using ProvidersBase.Model.Models;

namespace ProvidersBase.Controllers
{
    [ApiController]
    [Route("/providers")]
    public class ProvidersController : ControllerBase
    {
        private readonly IMapper<ProviderCompany, ProviderCompanyDTO> _mapper;
        private readonly ProvidersContext _context;

        public ProvidersController(IMapper<ProviderCompany, ProviderCompanyDTO> mapper, ProvidersContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        /// <summary>
        /// Get all <see cref="ProviderCompany"/> from DB
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllProviders()
        {
            //Mapping all the objects by IMapper
            return Ok(_mapper.Map(await _context.Providers.ToListAsync()));
        }
        /// <summary>
        /// Get the <see cref="ProviderCompany"/> by Id from DB
        /// </summary>
        /// <param name="id">The company id</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProviderById(int id)
        {
            var provider = await _context.Providers.FirstOrDefaultAsync(p => p.Id == id);

            if (provider == null)
            {
                return NotFound($"Not found any {nameof(ProviderCompany)}");
            }
            return Ok(_mapper.Map(provider));
        }
        /// <summary>
        /// Get the <see cref="ProviderCompany"/> by company inn from DB
        /// </summary>
        /// <param name="inn">The tax number of the company</param>
        [HttpGet("inn/{inn}")]
        public async Task<IActionResult> GetAllProvidersByINN(string inn)
        {
            if (string.IsNullOrEmpty(inn))
            {
                return NotFound("Inn not found");
            }
            var providers = await _context.Providers
                .Where(p => p.INN.Equals(inn))
                .ToListAsync();

            if (providers.Count() > 0)
            {
                return Ok(_mapper.Map(providers));
            }
            return NotFound($"Not found any {nameof(ProviderCompany)}");

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
            //Create a new instance and add it into collection 
            var entry = _context.Add(new ProviderCompany());
            //Then mapping by EF Core
            var provider = _mapper.ReverseMap(providerCompanyDTO);
            entry.CurrentValues.SetValues(provider);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
            return Ok();

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
            //Try to find a company object
            var provider = await _context.Providers.FirstOrDefaultAsync(p => p.Id == id);

            if (provider != null)
            {
                var entry = _context.Update(provider);
                //Mapping by EF Core
                entry.CurrentValues.SetValues(providerCompanyDTO);
                try
                {
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch
                {
                    throw;
                }
            }
            return NotFound($"{nameof(ProviderCompany)} not found");
        }
        /// <summary>
        /// Delete the current <see cref="ProviderCompany"/> object in DB
        /// </summary>
        /// <param name="id">searching parametr</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvider(int? id)
        {
            if (id == null || _context.Providers == null)
            {
                return BadRequest();
            }
            var provider = await _context.Providers.FindAsync(id);

            if (provider != null)
            {
                _context.Providers.Remove(provider);
                try
                {
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch
                {
                    throw;
                }
            }
            return NotFound($"The {nameof(ProviderCompany)} cann't be find");
        }
    }
}

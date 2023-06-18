using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvidersBase.Model.DataAccessLayer;
using ProvidersBase.Model.DTO;
using ProvidersBase.Model.Mappers;
using ProvidersBase.Model.Models;

namespace ProvidersBase.Controllers
{
    [ApiController]
    [Route("/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper<ProviderProduct, ProviderProductDTO> _mapper;
        private readonly ProvidersContext _context;

        public ProductsController(IMapper<ProviderProduct, ProviderProductDTO> mapper, ProvidersContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        /// <summary>
        /// Get all <see cref="ProviderProduct"/> from DB
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            //Mapping all the objects by IMapper
            return Ok(_mapper.Map(await _context.Products.ToListAsync()));
        }
        /// <summary>
        /// Get the <see cref="ProviderProduct"/> by Id from DB
        /// </summary>
        /// <param name="id">The product id</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound($"Not found any {nameof(ProviderProduct)}");
            }
            return Ok(_mapper.Map(product));
        }
        /// <summary>
        /// Get the <see cref="ProviderProduct"/> by company inn from DB
        /// </summary>
        /// <param name="inn">The tax number of the company</param>
        [HttpGet("inn/{inn}")]
        public async Task<IActionResult> GetAllProductsByProviderINN(string inn)
        {
            if (string.IsNullOrEmpty(inn))
            {
                 return NotFound("Inn not found");
            }
            //Get collection all the products with all the providers objects
            var products = await _context.Products
                .Include(p => p.Provider)
                .Where(p=>p.Provider.INN.Equals(inn))
                .ToListAsync();

            if (products.Count() > 0) 
            { 
                return Ok( _mapper.Map(products));
            }
            return NotFound($"Not found any {nameof(ProviderProduct)}");

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

            var entry = _context.Add(new ProviderProduct());
            var product = _mapper.ReverseMap(providerProductDTO);
            entry.CurrentValues.SetValues(product);

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
            //Check the provider field for existing it in DB
            if (!await _context.Providers.AnyAsync(p => p.Id == providerProductDTO.ProviderId))
            {
                return BadRequest($"The {nameof(ProviderCompany)} is not exist!");
            }
            //Try to find a user object
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product != null)
            {                
                var entry = _context.Update(product);
                //Mapping by EF Core
                entry.CurrentValues.SetValues(providerProductDTO);
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
            return NotFound($"{nameof(ProviderProduct)} not found");
        }
        /// <summary>
        /// Delete the current <see cref="ProviderProduct"/> object in DB
        /// </summary>
        /// <param name="id">searching parametr</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return BadRequest();
            }
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                _context.Products.Remove(product);
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
            return NotFound($"The {nameof(ProviderProduct)} cann't be find");
        }

    }
}

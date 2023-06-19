using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvidersBase.Model.DataAccessLayer;
using ProvidersBase.Model.DTO;
using ProvidersBase.Model.Mappers;
using ProvidersBase.Model.Models;

namespace ProvidersBase.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper<ProviderUser, ProviderUserDTO> _mapper;
        private readonly ProvidersContext _context;

        public UsersController(IMapper<ProviderUser, ProviderUserDTO> mapper, ProvidersContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        /// <summary>
        /// Get all <see cref="ProviderUser"/> from DB
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            //Mapping all the objects by IMapper
            return Ok(_mapper.Map(await _context.Users.ToListAsync()));
        }

        /// <summary>
        /// Get the <see cref="ProviderUser"/> by Id from DB
        /// </summary>
        /// <param name="id">The user id</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound($"Not found any {nameof(ProviderUser)}");
            }
            return Ok(_mapper.Map(user));
        }
        /// <summary>
        /// Get the <see cref="ProviderUser"/> by company inn from DB
        /// </summary>
        /// <param name="inn">The tax number of the company</param>
        [HttpGet("inn/{inn}")]
        public async Task<IActionResult> GetAllUsersByProviderINN(string inn)
        {
            if (string.IsNullOrEmpty(inn))
            {
                return NotFound("Inn not found");
            }
            //Get collection all the users with all the providers objects
            var users = await _context.Users
                .Include(u => u.Provider)
                .Where(u => u.Provider.INN.Equals(inn))
                .ToListAsync();

            if (users.Count() > 0)
            {
                return Ok(_mapper.Map(users));
            }
            return NotFound($"Not found any {nameof(ProviderUser)}");

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
            //Create a new instance and add it into collection 
            var entry = _context.Add(new ProviderUser());
            //If there are problems during a transaction, all changes will be rolled back.
            var transaction = _context.Database.BeginTransaction();

            //Then mapping data from DTO to a new instance
            var user = _mapper.ReverseMap(providerUserDTO);

            //And final mapping by EF Core
            entry.CurrentValues.SetValues(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                //Try to roll back all changes
                await transaction.RollbackAsync();
                throw;
            }
            //If the transaction is successful, all changes will be committed to the database.
            await transaction.CommitAsync();
            return Ok();

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
            //Check the provider field for existing it in DB
            if (!await _context.Providers.AnyAsync(p => p.Id == providerUserDTO.ProviderId))
            {
                return BadRequest($"The {nameof(ProviderUser)} is not exist!");
            }
            //Try to find a user object
            var user = await _context.Products.FirstOrDefaultAsync(u => u.Id == id);

            if (user != null)
            {
                //If there are problems during a transaction, all changes will be rolled back.
                var transaction = _context.Database.BeginTransaction();
                //Set the upadating object
                var entry = _context.Update(user);
                //Mapping by EF Core
                entry.CurrentValues.SetValues(providerUserDTO);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    //Try to roll back all changes
                    await transaction.RollbackAsync();
                    throw;
                }
                //If the transaction is successful, all changes will be committed to the database.
                await transaction.CommitAsync();
                return Ok();
            }
            return NotFound($"{nameof(ProviderUser)} not found");
        }
        /// <summary>
        /// Delete the current <see cref="ProviderUser"/> object in DB
        /// </summary>
        /// <param name="id">searching parametr</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return BadRequest();
            }
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                //If there are problems during a transaction, all changes will be rolled back.
                var transaction = _context.Database.BeginTransaction();
                _context.Users.Remove(user);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    //Try to roll back all changes
                    await transaction.RollbackAsync();
                    throw;
                }
                //If the transaction is successful, all changes will be committed to the database.
                await transaction.CommitAsync();
                return Ok();
            }
            return NotFound($"The {nameof(ProviderUser)} cann't be find");
        }
    }
}

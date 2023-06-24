using Microsoft.EntityFrameworkCore;
using ProvidersBase.Models.DTO;
using ProvidersBase.Models.Entities;
using ProvidersBase.Services.Mappers;

namespace ProvidersBase.Services.DataAccessLayer
{
    public class ProviderUserTransactor : IEntityTransactor<ProviderUserDTO>
    {
        private readonly ProvidersContext _context;
        private readonly IMapper<ProviderUser, ProviderUserDTO> _mapper;
        public ProviderUserTransactor(ProvidersContext context, IMapper<ProviderUser, ProviderUserDTO> mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateEntityAsync(ProviderUserDTO dto)
        {
            //Check the provider field for existing it in DB
            if (!await _context.Providers.AnyAsync(p => p.Id == dto.ProviderId))
            {
                throw new ArgumentException($"The {nameof(ProviderCompany)} is not exist!");
            }
            //Create a new instance and add it into collection
            var entry = _context.Add(new ProviderUser());
            //If there are problems during a transaction, all changes will be rolled back.
            using var transaction = _context.Database.BeginTransaction();

            //Then mapping data from DTO to a new instance
            var user = _mapper.ReverseMap(dto);

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
            return true;
        }

        public async Task<bool> DeleteEntityAsync(int? id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new ArgumentException($"The {nameof(ProviderUser)} cann't be find");
            }
            //If there are problems during a transaction, all changes will be rolled back.
            using var transaction = _context.Database.BeginTransaction();
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
            return true;
        }

        public async Task<IEnumerable<ProviderUserDTO>> GetAllEntitiesAsync()
        {
            //Mapping all the objects by IMapper
            return _mapper.Map(await _context.Users.ToListAsync());
        }

        public async Task<IEnumerable<ProviderUserDTO>> GetAllEntityByINNAsync(string inn)
        {
            //Get collection all the users with all the providers objects
            var users = await _context.Users
                .Include(u => u.Provider)
                .Where(u => u.Provider.INN.Equals(inn))
                .ToListAsync();

            if (users.Count() == 0)
            {
                throw new NullReferenceException($"Not found any {nameof(ProviderUser)}");
            }
            return _mapper.Map(users);
        }

        public async Task<ProviderUserDTO> GetEntityByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == id);

            if (user == null)
            {
                throw new NullReferenceException($"Not found any {nameof(ProviderUser)}");
            }
            return _mapper.Map(user);
        }

        public async Task<bool> UpdateEntityAsync(int id, ProviderUserDTO dto)
        {
            if (!await _context.Providers.AnyAsync(p => p.Id == dto.ProviderId))
            {
                throw new ArgumentException($"The {nameof(ProviderCompany)} is not exist!");
            }
            //Try to find a user object
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == id);

            if (user == null)
            {
                throw new ArgumentException($"The {nameof(ProviderUser)} cann't be find");
            }
            //If there are problems during a transaction, all changes will be rolled back.
            using var transaction = _context.Database.BeginTransaction();
            //Set the upadating object
            var entry = _context.Update(user);
            //Mapping by EF Core on updating object a new data
            entry.CurrentValues.SetValues(dto);
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
            return true;
        }
    }
}

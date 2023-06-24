using Microsoft.EntityFrameworkCore;
using ProvidersBase.Models.DTO;
using ProvidersBase.Models.Entities;
using ProvidersBase.Services.Mappers;

namespace ProvidersBase.Services.DataAccessLayer
{
    public class ProviderCompanyTransactor : IEntityTransactor<ProviderCompanyDTO>
    {
        private readonly ProvidersContext _context;
        private readonly IMapper<ProviderCompany, ProviderCompanyDTO> _mapper;
        public ProviderCompanyTransactor(ProvidersContext context, IMapper<ProviderCompany, ProviderCompanyDTO> mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateEntityAsync(ProviderCompanyDTO dto)
        {
            //Create a new instance and add it into collection
            var entry = _context.Add(new ProviderProduct());
            //If there are problems during a transaction, all changes will be rolled back.
            using var transaction = _context.Database.BeginTransaction();

            //Then mapping data from DTO to a new instance
            var company = _mapper.ReverseMap(dto);

            //And final mapping by EF Core
            entry.CurrentValues.SetValues(company);

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
            var company = await _context.Providers.FindAsync(id);

            if (company == null)
            {
                throw new ArgumentException($"The {nameof(ProviderCompany)} cann't be find");
            }
            //If there are problems during a transaction, all changes will be rolled back.
            using var transaction = _context.Database.BeginTransaction();
            _context.Providers.Remove(company);
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

        public async Task<IEnumerable<ProviderCompanyDTO>> GetAllEntitiesAsync()
        {
            //Mapping all the objects by IMapper
            return _mapper.Map(await _context.Providers.ToListAsync());
        }

        public async Task<IEnumerable<ProviderCompanyDTO>> GetAllEntityByINNAsync(string inn)
        {
            //Get collection all the companies with all the providers objects
            var companies = await _context.Providers
                .Where(p => p.INN.Equals(inn))
                .ToListAsync();

            if (companies.Count() == 0)
            {
                throw new NullReferenceException($"Not found any {nameof(ProviderCompany)}");
            }
            return _mapper.Map(companies);
        }

        public async Task<ProviderCompanyDTO> GetEntityByIdAsync(int id)
        {
            var company = await _context.Providers.FirstOrDefaultAsync(p => p.Id == id);

            if (company == null)
            {
                throw new NullReferenceException($"Not found any {nameof(ProviderCompany)}");
            }
            return _mapper.Map(company);
        }

        public async Task<bool> UpdateEntityAsync(int id, ProviderCompanyDTO dto)
        {
            //Try to find a user object
            var company = await _context.Providers.FirstOrDefaultAsync(p => p.Id == id);

            if (company == null)
            {
                throw new ArgumentException($"The {nameof(ProviderCompany)} cann't be find");
            }
            //If there are problems during a transaction, all changes will be rolled back.
            using var transaction = _context.Database.BeginTransaction();
            //Set the upadating object
            var entry = _context.Update(company);
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

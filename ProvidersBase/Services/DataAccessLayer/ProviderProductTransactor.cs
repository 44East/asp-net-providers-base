using Microsoft.EntityFrameworkCore;
using ProvidersBase.Models.DTO;
using ProvidersBase.Models.Entities;
using ProvidersBase.Services.Mappers;

namespace ProvidersBase.Services.DataAccessLayer
{
    public class ProviderProductTransactor : IEntityTransactor<ProviderProductDTO>
    {
        private readonly ProvidersContext _context;
        private readonly IMapper<ProviderProduct, ProviderProductDTO> _mapper;
        public ProviderProductTransactor(ProvidersContext context, IMapper<ProviderProduct, ProviderProductDTO> mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateEntityAsync(ProviderProductDTO dto)
        {
            //Check the provider field for existing it in DB
            if (!await _context.Providers.AnyAsync(p => p.Id == dto.ProviderId))
            {
                throw new ArgumentException($"The {nameof(ProviderCompany)} is not exist!");
            }
            //Create a new instance and add it into collection
            var entry = _context.Add(new ProviderProduct());
            //If there are problems during a transaction, all changes will be rolled back.
            using var transaction = _context.Database.BeginTransaction();

            //Then mapping data from DTO to a new instance
            var product = _mapper.ReverseMap(dto);

            //And final mapping by EF Core
            entry.CurrentValues.SetValues(product);

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
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                throw new ArgumentException($"The {nameof(ProviderProduct)} cann't be find");
            }
            //If there are problems during a transaction, all changes will be rolled back.
            using var transaction = _context.Database.BeginTransaction();
            _context.Products.Remove(product);
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

        public async Task<IEnumerable<ProviderProductDTO>> GetAllEntitiesAsync()
        {
            //Mapping all the objects by IMapper
            return _mapper.Map(await _context.Products.ToListAsync());
        }

        public async Task<IEnumerable<ProviderProductDTO>> GetAllEntityByINNAsync(string inn)
        {
            //Get collection all the products with all the providers objects
            var products = await _context.Products
                .Include(p => p.Provider)
                .Where(p => p.Provider.INN.Equals(inn))
                .ToListAsync();

            if (products.Count() == 0)
            {
                throw new NullReferenceException($"Not found any {nameof(ProviderProduct)}");
            }
            return _mapper.Map(products);
        }

        public async Task<ProviderProductDTO> GetEntityByIdAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                throw new NullReferenceException($"Not found any {nameof(ProviderProduct)}");
            }
            return _mapper.Map(product);
        }

        public async Task<bool> UpdateEntityAsync(int id, ProviderProductDTO dto)
        {
            if (!await _context.Providers.AnyAsync(p => p.Id == dto.ProviderId))
            {
                throw new ArgumentException($"The {nameof(ProviderCompany)} is not exist!");
            }
            //Try to find a user object
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                throw new ArgumentException($"The {nameof(ProviderProduct)} cann't be find");
            }
            //If there are problems during a transaction, all changes will be rolled back.
            using var transaction = _context.Database.BeginTransaction();
            //Set the upadating object
            var entry = _context.Update(product);
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

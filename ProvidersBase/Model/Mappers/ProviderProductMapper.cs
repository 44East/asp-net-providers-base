using ProvidersBase.Model.DTO;
using ProvidersBase.Model.Models;

namespace ProvidersBase.Model.Mappers
{
    public class ProviderProductMapper : IMapper<ProviderProduct, ProviderProductDTO>
    {
        public ProviderProductDTO Map(ProviderProduct source)
        {
            return new ProviderProductDTO()
            {
                Id = source.Id,
                Description = source.Description,
                InPacking = source.InPacking,
                Price = source.Price,
                ProviderId = source.ProviderId,
                Title = source.Title
            };
        }

        public IEnumerable<ProviderProductDTO> Map(IEnumerable<ProviderProduct> source)
        {
            return source.Select(Map);
        }

        public ProviderProduct ReverseMap(ProviderProductDTO destination)
        {
            return new ProviderProduct()
            {
                Description = destination.Description,
                InPacking = destination.InPacking,
                Price = destination.Price,
                ProviderId = destination.ProviderId,
                Title = destination.Title
            };
        }

        public IEnumerable<ProviderProduct> ReverseMap(IEnumerable<ProviderProductDTO> destination)
        {
            return destination.Select(ReverseMap);
        }
    }
}

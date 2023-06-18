using Microsoft.EntityFrameworkCore;
using ProvidersBase.Model.DataAccessLayer;
using ProvidersBase.Model.DTO;
using ProvidersBase.Model.Models;

namespace ProvidersBase.Model.Mappers
{
    public class ProviderCompanyMapper : IMapper<ProviderCompany, ProviderCompanyDTO>
    {
        public ProviderCompanyDTO Map(ProviderCompany source)
        {
            return new ProviderCompanyDTO()
            {
                Address = source.Address,
                CompanyTitle = source.CompanyTitle,
                Email = source.Email,
                INN = source.INN,
                Id = source.Id
            };
        }

        public IEnumerable<ProviderCompanyDTO> Map(IEnumerable<ProviderCompany> source)
        {
            return source.Select(Map);
        }

        public ProviderCompany ReverseMap(ProviderCompanyDTO destination)
        {
            return new ProviderCompany()
            {
                Address = destination.Address,
                CompanyTitle = destination.CompanyTitle,
                Email = destination.Email,
                INN = destination.INN
            };
        }

        public IEnumerable<ProviderCompany> ReverseMap(IEnumerable<ProviderCompanyDTO> destination)
        {
            return destination.Select(ReverseMap);
        }
    }
}

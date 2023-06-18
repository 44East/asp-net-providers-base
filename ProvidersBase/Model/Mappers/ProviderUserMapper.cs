using ProvidersBase.Model.DTO;
using ProvidersBase.Model.Models;

namespace ProvidersBase.Model.Mappers
{
    public class ProviderUserMapper : IMapper<ProviderUser, ProviderUserDTO>
    {
        public ProviderUserDTO Map(ProviderUser source)
        {
            return new ProviderUserDTO()
            {
                Id = source.Id,
                Name = source.Name,
                Email = source.Email,
                PhoneNumber = source.PhoneNumber,
                ProviderId = source.ProviderId,
                Username = source.Username
            };
        }

        public IEnumerable<ProviderUserDTO> Map(IEnumerable<ProviderUser> source)
        {
            return source.Select(Map);
        }

        public ProviderUser ReverseMap(ProviderUserDTO destination)
        {
            return new ProviderUser()
            {
                Name = destination.Name,
                Email = destination.Email,
                PhoneNumber = destination.PhoneNumber,
                ProviderId = destination.ProviderId,
                Username = destination.Username
            };
        }

        public IEnumerable<ProviderUser> ReverseMap(IEnumerable<ProviderUserDTO> destination)
        {
            return destination.Select(ReverseMap);
        }
    }
}

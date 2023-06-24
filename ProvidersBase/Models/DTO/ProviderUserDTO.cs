
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProvidersBase.Models.Entities;

namespace ProvidersBase.Models.DTO
{
    /// <summary>
    /// View model of <see cref="ProviderUser"/> uses for creation a new object into DB, for safety it contains only primitive types
    /// </summary>
    public class ProviderUserDTO
    {
        public int Id { get; set; }
        [BindRequired, MaxLength(50)]
        public int ProviderId { get; set; }
        [BindRequired]
        public string Name { get; set; }
        [BindRequired, MaxLength(50)]
        public string Username { get; set; }
        [EmailAddress, MaxLength(254)]
        public string Email { get; set; }
        [Phone, MinLength(10), MaxLength(10)]
        public string PhoneNumber { get; set; }
    }
}

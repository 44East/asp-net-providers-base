
using System.ComponentModel.DataAnnotations;
using ProvidersBase.Model.Models;
namespace ProvidersBase.Model.ViewModels
{
    /// <summary>
    /// View model of <see cref="ProviderUser"/> uses for creation a new object into DB, for safety it contains only primitive types
    /// </summary>
    public class ProviderUserVM
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone, MinLength(10), MaxLength(10)]
        public string PhoneNumber { get; set; }
    }
}

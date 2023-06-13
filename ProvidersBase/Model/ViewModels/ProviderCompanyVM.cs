using ProvidersBase.Model.Models;
using System.ComponentModel.DataAnnotations;

namespace ProvidersBase.Model.ViewModels
{
    /// <summary>
    /// View model of <see cref="ProviderCompany"/> uses for creation a new object into DB, for safety it contains only primitive types
    /// </summary>
    public class ProviderCompanyVM
    {
        public int Id { get; set; }
        public string CompanyTitle { get; set; }
        [MinLength(10), MaxLength(10)]
        public string INN { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
    }
}

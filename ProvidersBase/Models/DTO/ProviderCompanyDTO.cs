using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProvidersBase.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProvidersBase.Models.DTO
{
    /// <summary>
    /// View model of <see cref="ProviderCompany"/> uses for creation a new object into DB, for safety it contains only primitive types
    /// </summary>
    public class ProviderCompanyDTO
    {
        public int Id { get; set; }

        [BindRequired, MaxLength(50)]
        public string CompanyTitle { get; set; }

        [BindRequired ,MinLength(10), MaxLength(10)]
        public string INN { get; set; }

        [BindRequired ,EmailAddress, MaxLength(254)]
        public string Email { get; set; }
        [BindRequired, MaxLength(200)]
        public string Address { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ProvidersBase.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProvidersBase.Models.DTO
{
    /// <summary>
    /// View model of <see cref="ProviderProduct"/> uses for creation a new object into DB, for safety it contains only primitive types
    /// </summary>
    public class ProviderProductDTO
    {
        public int Id { get; set; }
        [BindRequired]
        public int ProviderId { get; set; }
        [BindRequired, MaxLength(50)]
        public string Title { get; set; }
        [BindRequired, MaxLength(200)]
        public string Description { get; set; }
        [BindRequired, Precision(18, 2)]
        public decimal Price { get; set; }
        [BindRequired]
        public int InPacking { get; set; }
    }
}

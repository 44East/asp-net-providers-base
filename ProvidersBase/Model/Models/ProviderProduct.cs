using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProvidersBase.Model.Models
{
    [Table("Products")]
    public class ProviderProduct
    {
        [Key, Column("product_id")]
        public int Id { get; set; }

        [Column("provider_id")]
        public int ProviderId { get; set; }

        [ForeignKey(nameof(ProviderId))]
        public ProviderCompany Provider { get; set; }

        [Column("title"), Required, MaxLength(50)]
        public string Title { get; set; }

        [Column("description"),  Required, MaxLength(200)]
        public string Description { get; set; }

        [Column("price_per_unit"), Precision(18,2), Required]
        public decimal Price { get; set; }

        [Column("unit"), Required]
        public int InPacking { get; set; }
    }
}

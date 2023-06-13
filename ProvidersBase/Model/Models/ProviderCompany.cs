using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProvidersBase.Model.Models
{
    [Table("Providers")]
    public class ProviderCompany
    {
        [Key, Column("company_id")]
        public int Id { get; set; }

        [Column("company_title"), Required, MaxLength(50)]
        public string CompanyTitle { get; set; }

        [Column("INN"), Required, MinLength(10), MaxLength(10)] //The company tax number is mut be 10 digits
        public string INN { get; set; }

        [Column("email"), Required, EmailAddress, MaxLength(254)]
        public string Email { get; set; }

        [Column("address"), Required, MaxLength(200)]
        public string Address { get; set; }

        [NotMapped]
        public ICollection<ProviderUser>? Users { get; set; }
        [NotMapped]
        public ICollection<ProviderProduct>? Products { get; set; }
    }
}

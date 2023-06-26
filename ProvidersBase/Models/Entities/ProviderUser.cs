using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProvidersBase.Models.Entities
{
    [Table("Users")]
    public class ProviderUser
    {
        [Key, Column("user_id")]
        public int Id { get; set; }

        [Column("provider_id")]
        public int ProviderId { get; set; }

        [ForeignKey(nameof(ProviderId))]
        public ProviderCompany? Provider { get; set; }

        [Column("name"), Required, MaxLength(50)]
        public string Name { get; set; }

        [Column("username"),  Required, MaxLength(50)]
        public string Username { get; set; }

        [Column("email"), Required, EmailAddress, MaxLength(254)]
        public string Email { get; set; }

        [Column("phone_number"),  Required, Phone, MinLength(10), MaxLength(10)]
        public string PhoneNumber { get; set; }
    }
}

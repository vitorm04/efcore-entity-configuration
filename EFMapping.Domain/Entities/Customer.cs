using System;
using System.ComponentModel.DataAnnotations;

namespace EFMapping.Domain.Entities
{
    public sealed class Customer
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(80)]
        public string Name { get; set; }

        [MaxLength(50), Required,]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public bool Deleted { get; set; }

        [MaxLength(11)]
        public string NationalId { get; set; }
    }
}

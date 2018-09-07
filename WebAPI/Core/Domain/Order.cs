using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Core.Domain
{
    public class Order
    {
        public int OrderId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        [Required]
        public DateTimeOffset DateCreated { get; set; }

        public Party Party { get; set; }

        [Required]
        public int PartyId { get; set; }
    }
}

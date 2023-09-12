using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopping.Models
{
   public class ApplicationUser:IdentityUser
    {
        [Required]
        public string name { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]

        public Company Company { get; set; }

        [NotMapped]
        public string Role { get; set; }
    }
}

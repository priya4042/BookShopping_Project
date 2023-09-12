using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopping_Models
{
   public class Company
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string StreetAddresss { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Postalcode { get; set; }

        public string Phonenumber { get; set; }

        public bool IsAuthorized { get; set; }
    }
}

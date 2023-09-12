using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShoppingProject.Models
{
   public class Product
    {
        public int Id { get; set; }
        [Required]

        public string title { get; set; }
        [Required]

        public string Discription { get; set; }
        [Required]

        public string ISBN { get; set; }

        public string Author { get; set; }

        [Required]
        [Range(1,10000)]
        public double ListPrice { get; set; }

        [Required]
        [Range(1,10000)]
        public double Price50 { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price100 { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopping.Models
{
    
   public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }

        
        [Required]
        public string Author { get; set; }

        [Range(1, 10000)]
        [Required]
        public double ListPrice { get; set; }

        [Range(1, 10000)]
        [Required]
        public double Price50 { get; set; }

        [Range(1, 10000)]
        [Required]
        public double Price100 { get; set; }

        [Range(1, 10000)]
        [Required]
        public double Price { get; set; }

        public string ImageUrl { get; set; }

        [Display(Name ="Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]

        public Category Category { get; set; }

        [Display(Name = "CoverType")]
        public int CoverTypeId { get; set; }
        [ForeignKey("CoverTypeId")]

        public CoverType CoverType { get; set; }
    }
}

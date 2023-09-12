﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShoppingProject.Models
{
   public class ShoppingCart
    {
        public ShoppingCart()
        {
            Count = 1;
        }
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]

        public ApplicatiionUser ApplicationUser { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]

        public Product Product { get; set; }

        [Range(1, 1000, ErrorMessage = "plz enter count 1 to 1000")]
        public int Count { get; set; }

        public double Price { get; set; }
    }
}

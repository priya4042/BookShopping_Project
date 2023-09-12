using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshopping.Models
{
   public class OrderDetail
    {
        public int Id { get; set; }

        public int OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]

        public OrderHeader OrderHeader { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]

        public Product Product { get; set; }

        [Range(1,1000,ErrorMessage ="Plz enter 1,1000 count")]
        public int Count { get; set; }

        public double Price { get; set; }

    }
}

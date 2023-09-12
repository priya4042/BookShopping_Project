using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopping.Models
{
  public class OrderHeader
    {
        public int Id { get; set; }

        public string AppliationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]

        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        public DateTime Orderdate { get; set; }


        public string OrderStatus { get; set; }

        [Required]
        public double OrderTotal { get; set; }

        [Required]
        public DateTime ShippingDate { get; set; }

        public string PaymentStatus { get; set; }

        public DateTime PaymentDueDate { get; set; }

        public DateTime PaymentDate { get; set; }

        public string TransactionId { get; set; }

        public string TrackingNumber { get; set; }

        public string Carrier { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string StreetAddress { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

    }
}


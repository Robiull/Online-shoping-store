using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Models
{
    public class ProductDetails
    {
        public int ProductId { get; set; }
        [Required]
        [Range(1,50)]
        public Nullable<int> CategoryId { get; set; }

        [Required(ErrorMessage = "Product Name is Required")]
        [StringLength(100, ErrorMessage = "Minimum 3 and Maximum 100", MinimumLength = 3)]
        public string ProductName { get; set; }
        [Required(ErrorMessage ="Description Is required")]
        public string Descriptions { get; set; }
        [Range(typeof(decimal),"1","2000000000",ErrorMessage ="Invalid Price")]
        public Nullable<int> ProductPrice { get; set; }
        public string ProductImage { get; set; }
        [Range(typeof(int),"1","500",ErrorMessage ="Invalid Quantity")]
        public Nullable<int> ProductQuantity { get; set; }

        public SelectList Categories { get; set; }


    }
}
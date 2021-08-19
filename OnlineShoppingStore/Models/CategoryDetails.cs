using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShoppingStore.Models
{
    public class CategoryDetails
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="CategoryName is Required")]
        [StringLength (100, ErrorMessage ="Minimum 5 and maximum 50 Character",MinimumLength =3)]
        public string CategoryName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }


    }
}
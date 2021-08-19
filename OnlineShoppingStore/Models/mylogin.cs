using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Models
{
    public class mylogin
    {
        public int userid { get; set; }
        [Display(Name = "User Name"), Required]
        public string username { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least  {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string userpassword { get; set; }
        [DataType(DataType.Password)] 
        [Display(Name = "ConfirmPassword")]
        [System.ComponentModel.DataAnnotations.Compare("userpassword", ErrorMessage ="Password Do not match")]
        public string confirmpassword { get; set; }
        [Required]
        [EmailAddress]
        [Display (Name = "Email")]
        public string usermail { get; set; }
        public string usercontact { get; set; }
        public string userlocation { get; set; }
        public string userfirstname { get; set; }
        public string userlastname { get; set; }
        public string usergender { get; set; }

    }
}
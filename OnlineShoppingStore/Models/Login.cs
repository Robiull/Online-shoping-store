using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingStore.Models
{
    public class Login
    {
     
        public string userFirstName { get; set; }
        public string userlastname { get; set; }
        public string userName { get; set; }
        public string userpassword { get; set; }
        public string userMail { get; set; }
        public string Password { get; set; }
        public string Password { get; set; }
        public string Password { get; set; }
        public string Password { get; set; }
        public string Password { get; set; }
        public string Password { get; set; }
        public string Password { get; set; }
   userMail varchar(50)  UNIQUE,
   userContact varchar(50) NOT NULL,
  usergender varchar(10) NOT NULL,
  IsEmailValid bit NOT NULL,
   ActivationCode uniqueIdentifier NOT NULL
    }
}
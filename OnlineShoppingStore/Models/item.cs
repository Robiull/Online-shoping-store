using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace OnlineShoppingStore.Models
{
    public class item
    {

          public Adminproductlist  Product { get; set; }
          public int Quantity { get; set; }

        public DataTable ProductCartTable { get; set; }

        public int carttracnumber { get; set; }



    }
}
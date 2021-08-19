using OnlineShoppingStore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class MyFeaturesController : Controller
    {
        // GET: MyFeatures
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Wishlist()
        {
            return View();
        }

        string connectionString = @"Data Source = DESKTOP-2V8523P\SQLEXPRESS;  Initial Catalog =OnlineShoppingStore;  Integrated Security  = true";


        public ActionResult MyWishlist(int id) {
            bool check = true;
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from Product where ProductId = @search", sqlcon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@search", id);
                sqlDa.Fill(dtblProduct);

            }
            if (Session["wishlist"] == null)
            {
                List<wishlist> wishlist = new List<wishlist>();
                wishlist.Add(new wishlist()
                {
                    ProductWishlistTable = dtblProduct
                });
                Session["wishlist"] = wishlist;
            }
            else
            {

                List<wishlist> wishlist = (List<wishlist>)Session["wishlist"];
                DataRow dtrow = dtblProduct.Rows[0];
                foreach (var item in wishlist.ToList())
                {
                    int Pid = Convert.ToInt32(item.ProductWishlistTable.Rows[0][0]);
                    if (Pid == id)
                    {
                        check = false;
                        wishlist.Remove(item);
                        break;
                    }
                   
                }

                
                    wishlist.Add(new wishlist()
                    {

                        ProductWishlistTable = dtblProduct
                    });

              

                Session["wishlist"] = wishlist;



            }
            return Redirect("../Home/Index");

           
        }


    }
}
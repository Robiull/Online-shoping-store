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
    public class HomeController : Controller
    {

        string connectionString = @"Data Source = DESKTOP-3IDLQ84;  Initial Catalog =OnlineShoppingStore;  Integrated Security  = true";
      


        [HttpGet]
        public ActionResult IndexSearch(string search)
        {
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from Product where ProductName = @search", sqlcon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@search", search);
                sqlDa.Fill(dtblProduct);

            }

            return View(dtblProduct);
        }
        [HttpGet]
        public ActionResult Index()
        { 
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from Product", sqlcon);
                sqlDa.Fill(dtblProduct);

            }

            //    return View(dtblProduct);
            //   List<combine> comb = new List<combine>();
            combine comb = new combine();
            comb.ProductTable = dtblProduct;
            comb.name = "test";
            return View(comb);
            
        }
        public int IncCartNumber()
        {
            int carttracid = 1;
            int cartnumber = 0;
            int prevQuantity = 1;
            //     int ProductId = Convert.ToInt32(Request.QueryString["id"]);
            DataTable dtblProduct = new DataTable();
            DataTable dtblProductcart = new DataTable();

            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {

                sqlcon.Open();
                // SqlCommand sqlCmdgetcart = new SqlCommand(query, sqlcon);
                string query = $"Select  * from Carttrac  where carttracid = @carttracid";
                SqlDataAdapter sqlDacart = new SqlDataAdapter(query, sqlcon);
                sqlDacart.SelectCommand.Parameters.AddWithValue("@carttracid", carttracid);
                sqlDacart.Fill(dtblProductcart);
                if (dtblProductcart.Rows.Count == 1)
                {
                    cartnumber = Convert.ToInt32(dtblProductcart.Rows[0][1]);
                  //  cartnumber = cartnumber + 1;
                }

                string cartupdatequery = "UPDATE Carttrac SET  cartnumber= @cartnumber where carttracid = @carttracid ";
                SqlCommand sqlCmdupdatecart = new SqlCommand(cartupdatequery, sqlcon);
                sqlCmdupdatecart.Parameters.AddWithValue("@carttracid", carttracid);
                sqlCmdupdatecart.Parameters.AddWithValue("@cartnumber", cartnumber);
                sqlCmdupdatecart.ExecuteNonQuery();

            }
          
            return cartnumber;

        }

        public void UpdateCartnum(int cartnumber)
        {
            cartnumber = cartnumber + 1;
            int carttracid = 1;
          // int cartnumber = 0;

            //     int ProductId = Convert.ToInt32(Request.QueryString["id"]);
            DataTable dtblProduct = new DataTable();
            DataTable dtblProductcart = new DataTable();

            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {

                sqlcon.Open();
                //SqlCommand sqlCmdupdatecart= new SqlCommand(query, sqlcon);
                string cartupdatequery = "UPDATE Carttrac SET  cartnumber= @cartnumber where carttracid = @carttracid ";
                SqlCommand sqlCmdupdatecart = new SqlCommand(cartupdatequery, sqlcon);
                sqlCmdupdatecart.Parameters.AddWithValue("@carttracid", carttracid);
                sqlCmdupdatecart.Parameters.AddWithValue("@cartnumber", cartnumber);
                sqlCmdupdatecart.ExecuteNonQuery();

            }

        }
        public void InsertIntoCart(int Quantity, DataTable dtblProduct,int num) {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "Insert into Cart  Values(@CartNumber,@ProductName,@ProductId,@Quantity) ";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Parameters.AddWithValue("@CartNumber",num );
                sqlCmd.Parameters.AddWithValue("@ProductName",dtblProduct.Rows[0][2]);

                sqlCmd.Parameters.AddWithValue("@ProductId", dtblProduct.Rows[0][0]);
                sqlCmd.Parameters.AddWithValue("@Quantity", Quantity);

                sqlCmd.ExecuteNonQuery();
            }


        }



         
        public ActionResult AddtoCart(int id) {
            int carttracid = 1;
            int cartnumber = 1;
            int prevQuantity = 1;
       //     int ProductId = Convert.ToInt32(Request.QueryString["id"]);
            DataTable dtblProduct = new DataTable();
            DataTable dtblProductcart = new DataTable();

            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from Product where ProductId = @ProductId", sqlcon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@ProductId", id);
                sqlDa.Fill(dtblProduct);

            }
            if (Session["cart"] == null)
            {
                 cartnumber = IncCartNumber();

                MyCartnumber nm = new MyCartnumber();
                Session["cartNumber"]  = cartnumber.ToString();

                UpdateCartnum(cartnumber);
                List<item> cart = new List<item>();
                cart.Add(new item()
                {
                    Quantity = 1,
                    ProductCartTable = dtblProduct,
                    carttracnumber = cartnumber

                });
                InsertIntoCart(1, dtblProduct, cartnumber);
                Session["cart"] = cart;
              
            }
            else {

                List<item> cart = (List<item>)Session["cart"];
                DataRow dtrow = dtblProduct.Rows[0];
                foreach (var item in cart.ToList())
                {
                    int Pid = Convert.ToInt32(item.ProductCartTable.Rows[0][0]);
                    if (Pid == id)
                    {
                         prevQuantity = item.Quantity +1;
                        cart.Remove(item);
                         break;
                    }
                    
                }
                cart.Add(new item()
                {
                    Quantity = prevQuantity,
                    ProductCartTable = dtblProduct,
                    carttracnumber = IncCartNumber() - 1
                });

                int sendCardnumber = IncCartNumber() - 1;
                InsertIntoCart(prevQuantity, dtblProduct, sendCardnumber);
                Session["cart"] = cart;

            }
            return Redirect("Index");

        }

        public ActionResult AddtoCartview() {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult userlogin()
        {  

            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        public ActionResult regform(string firstname,string lastname,string username,string password, string gender, string email, string phone)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "Insert into Users  Values(@userfirstname,@userlastname,@username,@userpassword,@usermail,@usercontact,@usergender) ";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
               

                sqlCmd.Parameters.AddWithValue("@userfirstname", firstname);

                sqlCmd.Parameters.AddWithValue("@userlastname", lastname);

                sqlCmd.Parameters.AddWithValue("@username", username);

                sqlCmd.Parameters.AddWithValue("@userpassword", password);

                sqlCmd.Parameters.AddWithValue("@usermail", email);

                sqlCmd.Parameters.AddWithValue("@usercontact", phone);

                sqlCmd.Parameters.AddWithValue("@usergender", gender);

                sqlCmd.ExecuteNonQuery();
            }


            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int id) {
          
            List<item> cart = (List<item>)Session["cart"];

            foreach (var item in cart) {
                int Pid=Convert.ToInt32(item.ProductCartTable.Rows[0][0]);
                if (Pid == id) {
                    cart.Remove(item);
                    break;
                }
            }

            Session["cart"] = cart;
            return Redirect("Index");

        }
        public ActionResult Addtocartedit() {
            return View();
        }
        public ActionResult Login() {

            return View();

        }
        public ActionResult AddToCartinc(int id) {

            int prevQuantity = 1;
            //     int ProductId = Convert.ToInt32(Request.QueryString["id"]);
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from Product where ProductId = @search", sqlcon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@search", id);
                sqlDa.Fill(dtblProduct);

            }
            if (Session["cart"] == null)
            {
                List<item> cart = new List<item>();
                cart.Add(new item()
                {


                    Quantity = 1,
                    ProductCartTable = dtblProduct
                });
                Session["cart"] = cart;
            }
            else
            {

                List<item> cart = (List<item>)Session["cart"];
                DataRow dtrow = dtblProduct.Rows[0];
                foreach (var item in cart.ToList())
                {
                    int Pid = Convert.ToInt32(item.ProductCartTable.Rows[0][0]);
                    if (Pid == id)
                    {
                        prevQuantity = item.Quantity + 1;
                        cart.Remove(item);
                        
                        break;
                    }

                }
                cart.Add(new item()
                {
                    Quantity = prevQuantity,
                    ProductCartTable = dtblProduct
                });

                Session["cart"] = cart;
            }
            return Redirect("Addtocartedit");

        }


        public ActionResult DecreaseQty(int id)
        {

             int ProductId = Convert.ToInt32(Request.QueryString["id"]);
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from Product where ProductId = @search", sqlcon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@search", id);
                sqlDa.Fill(dtblProduct);

            }

            if (Session["cart"] != null)
            {
           //     int pid = Convert.ToInt32((item.ProductCartTable.Rows[0][0]).ToString());

                List<item> cart = (List<item>)Session["cart"];
            //    var product = ctx.Tbl_Product.Find(productId);
                foreach (var item in cart)
                {
                    int pid = Convert.ToInt32(item.ProductCartTable.Rows[0][0]);

                    if (pid== id)
                    {
                        int prevQty = item.Quantity;
                        if (prevQty > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new item()
                            {
                                ProductCartTable = dtblProduct,
                                Quantity = prevQty - 1
                            });
                        }
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("Addtocartedit");
        }


        [HttpGet]
        public ActionResult log(string username, string password) {

            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from Users where username = @username  and userpassword=@userpassword", sqlcon);
               
                sqlDa.SelectCommand.Parameters.AddWithValue("@username", username);
                sqlDa.SelectCommand.Parameters.AddWithValue("@userpassword", password);

                sqlDa.Fill(dtblProduct);
            }

            foreach (DataRow item in dtblProduct.Rows)
            {
                if (item["username"].ToString()==username && item["userpassword"].ToString()==password) {

                    Session["username"] = username;
                    Session["firstname"] = item["userfirstname"];
                    break;
                }
            }

              if (Session["username"] == null)
            {

                return Content("Incorrect");
            }
            else {


                return Redirect("Index");

            }

        }







        public ActionResult OrderDetails()
        {


            return View();
        }

        public ActionResult MyOrder(string FirsrName, string LastName, string Mail, string OrderAddress, string City, string Country, int ZipCode, string PhoneNumber, string PaymentMethod, string ShippingType)
        {
            int carttracid = 1;
            int cartnumber, userid;
            DataTable dtblProduct = new DataTable();
            string currentusername;
            int lineTotal = 0;
            DataTable dtblProductusers = new DataTable();

            if (Session["username"] == null)
            {
                return RedirectToAction("../Home/Login");
            }
            else
            {
                currentusername = Session["username"].ToString();

                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();
                    SqlDataAdapter sqlDauser = new SqlDataAdapter("Select * from Users where username = @username", sqlcon);
                    sqlDauser.SelectCommand.Parameters.AddWithValue("@username", currentusername);

                    sqlDauser.Fill(dtblProductusers);
                  
                }

                foreach(item item in (List<item>)Session["cart"])
                {
                    // productPrice = Convert.ToInt32(item.ProductCartTable.Rows[0][5]);
                    int productPrice = Convert.ToInt32(item.ProductCartTable.Rows[0][5]);
                     lineTotal = lineTotal+Convert.ToInt32(item.Quantity * productPrice);

                } 


                    using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();
                    string query = "Insert into Orders  Values(@FirsrName,@LastName,@Mail,@OrderAddress,@City,@Country,@ZipCode," +
                                                              "@PhoneNumber,@PaymentMethod,@ShippingType,@cartnumber,@userid,@Totalbill) ";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlcon);


                    sqlCmd.Parameters.AddWithValue("@FirsrName", FirsrName);

                    sqlCmd.Parameters.AddWithValue("@LastName", LastName);

                    sqlCmd.Parameters.AddWithValue("@Mail", Mail);

                    sqlCmd.Parameters.AddWithValue("@OrderAddress", OrderAddress);

                    sqlCmd.Parameters.AddWithValue("@City", City);

                    sqlCmd.Parameters.AddWithValue("@Country", Country);

                    sqlCmd.Parameters.AddWithValue("@ZipCode", ZipCode);

                    sqlCmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);

                    sqlCmd.Parameters.AddWithValue("@PaymentMethod", PaymentMethod);

                    sqlCmd.Parameters.AddWithValue("@ShippingType", ShippingType);

                    sqlCmd.Parameters.AddWithValue("@cartnumber", Convert.ToInt32(Convert.ToInt32(Session["cartNumber"].ToString())));

                    sqlCmd.Parameters.AddWithValue("@userid", Convert.ToInt32(dtblProductusers.Rows[0][0]));
                    sqlCmd.Parameters.AddWithValue("@Totalbill", lineTotal);

                    sqlCmd.ExecuteNonQuery();
                }

                Session["cartNumber"] = null;
                Session["cart"] = null;
                return RedirectToAction("Index");
            }
        }



        public ActionResult UserProfile() {


            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from Orders", sqlcon);
                sqlDa.Fill(dtblProduct);

            }
            usermodel user = new usermodel();
            user.Order = dtblProduct;
            return View(user);



        }






    }
}
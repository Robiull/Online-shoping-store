using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using OnlineShoppingStore.Models;

namespace OnlineShoppingStore.Controllers
{
    public class MyAdminController : Controller
    {
        string connectionString = @"Data Source = DESKTOP-2V8523P\SQLEXPRESS;  Initial Catalog =OnlineShoppingStore;  Integrated Security  = true";
          
        // GET: MyAdmin
        public ActionResult Index()
        {
            return View();
        }

        public List<SelectListItem> GetCategory() {
            List<SelectListItem> list = new List<SelectListItem>();

            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from Category", sqlcon);
                sqlDa.Fill(dtblProduct);

                foreach (DataRow item in dtblProduct.Rows)
                {
                    list.Add(new SelectListItem { Value = item["CategoryId"].ToString(), Text = item["CategoryName"].ToString() });

                }
            }
            return list;
        }

        public ActionResult MyDashbord()
        {
            return View();
        }
        // GET: MyAdmin
        [HttpGet]
        public ActionResult MyProducts()
        {
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString)) {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from Product", sqlcon);
                sqlDa.Fill(dtblProduct);

            }

                return View(dtblProduct);
        }
        [HttpGet]
        public ActionResult Create() {
            ViewBag.CategoryList = GetCategory();

            return View(new Adminproductlist());
        }
        [HttpPost]
        public ActionResult Create(Adminproductlist productlist, HttpPostedFileBase file) {

           
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), pic);
                file.SaveAs(path);

            }

            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "Insert into Product  Values(@CategoryId,@ProductName,@ProductDescription,@ProductQuantity,@ProductPrice,@ProductImage,@IsAvilable,@ProductColor,@DiscountAvailable,@Taglistid,@ProductSize) ";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Parameters.AddWithValue("@CategoryId", productlist.CategoryId);

                sqlCmd.Parameters.AddWithValue("@ProductName", productlist.ProductName);

                sqlCmd.Parameters.AddWithValue("@ProductDescription", productlist.Descriptions);

                sqlCmd.Parameters.AddWithValue("@ProductQuantity", productlist.ProductQuantity);

                sqlCmd.Parameters.AddWithValue("@ProductPrice", productlist.ProductPrice);

                sqlCmd.Parameters.AddWithValue("@ProductImage", pic);

                sqlCmd.Parameters.AddWithValue("@IsAvilable", productlist.Isavaiable);

                sqlCmd.Parameters.AddWithValue("@ProductColor", productlist.ProductColor);

                sqlCmd.Parameters.AddWithValue("@DiscountAvailable", productlist.Isdiscount);

                sqlCmd.Parameters.AddWithValue("@Taglistid", productlist.Taglist);

                sqlCmd.Parameters.AddWithValue("@ProductSize", productlist.ProductSize);
                sqlCmd.ExecuteNonQuery();
            }
           

                return RedirectToAction("MyProducts");
        }

        [HttpGet]
        [Route("MyAdmin/Edit/{ProductId}")]
        public ActionResult Edit(int ProductId)
        {
            Adminproductlist productlist = new Adminproductlist();
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {

                sqlcon.Open();
                string query = $"Select  * from Product  where ProductId = @ProductId";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query,sqlcon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@ProductId", ProductId);
                sqlDa.Fill(dtblProduct);
            }
            if (dtblProduct.Rows.Count == 1)
            {
                productlist.ProductId = Convert.ToInt32(dtblProduct.Rows[0][0].ToString());
                productlist.CategoryId = Convert.ToInt32(dtblProduct.Rows[0][1].ToString());
                productlist.ProductName = dtblProduct.Rows[0][2].ToString();
                productlist.Descriptions = dtblProduct.Rows[0][3].ToString();

                productlist.ProductQuantity = Convert.ToInt32(dtblProduct.Rows[0][4].ToString());

                productlist.ProductPrice = Convert.ToInt32(dtblProduct.Rows[0][5].ToString());

                productlist.ProductImage = dtblProduct.Rows[0][6].ToString();

                productlist.Isavaiable = bool.Parse(dtblProduct.Rows[0][7].ToString());

                productlist.ProductColor = dtblProduct.Rows[0][8].ToString();


                productlist.Isdiscount = bool.Parse(dtblProduct.Rows[0][9].ToString());

                
                productlist.Taglist = Convert.ToInt32(dtblProduct.Rows[0][10].ToString());


                productlist.ProductSize = dtblProduct.Rows[0][11].ToString();
                return View(productlist);
            }
            else
            {
                return RedirectToAction("MyProducts");

            }

        }

        [HttpPost]

        public ActionResult EditA(Adminproductlist productlist, HttpPostedFileBase  file) {


            string pic=null;
            if (file != null)
            {


                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), pic);
                file.SaveAs(path);
                productlist.ProductImage = pic;
            }
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "UPDATE Product SET  CategoryId= @CategoryId,ProductName=@ProductName,ProductDescription=@ProductDescription,ProductQuantity=@ProductQuantity,ProductPrice=@ProductPrice,ProductImage=@ProductImage,IsAvilable=@IsAvilable,ProductColor=@ProductColor,DiscountAvailable=@DiscountAvailable,Taglistid=@Taglistid,ProductSize=@ProductSize  where ProductId = @ProductId ";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Parameters.AddWithValue("@ProductId", productlist.ProductId.ToString());

                sqlCmd.Parameters.AddWithValue("@CategoryId", productlist.CategoryId.ToString());

                sqlCmd.Parameters.AddWithValue("@ProductName", productlist.ProductName.ToString());

                sqlCmd.Parameters.AddWithValue("@ProductDescription", productlist.Descriptions.ToString());

                sqlCmd.Parameters.AddWithValue("@ProductQuantity", productlist.ProductQuantity.ToString());

                sqlCmd.Parameters.AddWithValue("@ProductPrice", productlist.ProductPrice.ToString());

                sqlCmd.Parameters.AddWithValue("@ProductImage", pic);

                sqlCmd.Parameters.AddWithValue("@IsAvilable", productlist.Isavaiable.ToString());

                sqlCmd.Parameters.AddWithValue("@ProductColor", productlist.ProductColor.ToString());

                sqlCmd.Parameters.AddWithValue("@DiscountAvailable", productlist.Isdiscount.ToString());

                sqlCmd.Parameters.AddWithValue("@Taglistid", productlist.Taglist.ToString());

                sqlCmd.Parameters.AddWithValue("@ProductSize", productlist.ProductSize.ToString());
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("MyProducts");

        }

        [HttpGet]
        [Route("MyAdmin/OrderviewDetails/{OrderId}")]
        public ActionResult OrderviewDetails(int OrderId) {

            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {

                sqlcon.Open();
                string query = $"Select  * from Orders  where OrderId = @OrderId";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlcon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@OrderId", OrderId);
                sqlDa.Fill(dtblProduct);
            }

            return View(dtblProduct);
        }


        public ActionResult Orderview() {


            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from Orders", sqlcon);
                sqlDa.Fill(dtblProduct);

            }

            return View(dtblProduct);

        }
        int cid;
        
        public ActionResult ConfirmOrder(int id)
        {
            cid = id;
            Session["Orderid"] = id;


            return View();

        }
        public ActionResult InsertConfirm(string OrderStatus, String date) {
            cid = Convert.ToInt32(Session["Orderid"].ToString());
            Session["Orderid"] = null;

            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "Insert into adminOrder  Values(@OrderId,@DeliveryDate,@OrderStatus) ";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Parameters.AddWithValue("@OrderId", cid);

                sqlCmd.Parameters.AddWithValue("@DeliveryDate", date.ToString());

                sqlCmd.Parameters.AddWithValue("@OrderStatus", OrderStatus.ToString());
                sqlCmd.ExecuteNonQuery();
            }






                return Content(OrderStatus+ date+"  "+cid);
        }

        
    }


    }

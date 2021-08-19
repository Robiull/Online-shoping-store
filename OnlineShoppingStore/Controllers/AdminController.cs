using Newtonsoft.Json;
using OnlineShoppingStore.Database;
using OnlineShoppingStore.Models;
using OnlineShoppingStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.AdminPanelStyle
{
    public class AdminController : Controller
    {
        // GET: Admin
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();



        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult productslist()
        {
            return View();
        }

        public ActionResult Categories() {
            List<Category> allCategories = _unitOfWork.GetRepositoryInstance<Category>().GetAllRecordsIQuerable().Where(i => i.IsDelete == false).ToList();
            return View(allCategories);

        }

        public ActionResult AddCategory() {
            return UpdateCategory(0);

        }




        public ActionResult UpdateCategory(int categoryId) {
           // return  Content(categoryId.ToString());
            CategoryDetails cd;
            if (categoryId != 0)
            {
               cd = JsonConvert.DeserializeObject<CategoryDetails>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Category>().GetFirstOrDefault(categoryId)));

            }
            else {
                cd = new CategoryDetails();
            }

            return View("~/Views/Admin/UpdateCategory.cshtml",cd);
        }

        public ActionResult Product()
        {

            return View(_unitOfWork.GetRepositoryInstance<Product>().GetProducts());

        }

        public ActionResult ProductEdit(int productId) {

            return View(_unitOfWork.GetRepositoryInstance<Product>().GetFirstOrDefault(productId));

        } 


        [HttpPost]

        public ActionResult ProductEdit(Product product_Table)
        {
            _unitOfWork.GetRepositoryInstance<Product>().Update(product_Table);
            return RedirectToAction("Product");

        }


        public ActionResult ProductAdd( )
        {

            return View("~/Views/Admin/ProductAdd.cshtml");

        }


        [HttpPost]

        public ActionResult ProductAdd(Product product_Table)
        {
            _unitOfWork.GetRepositoryInstance<Product>().Update(product_Table);
            return RedirectToAction("Product");

        }
    }

  
}
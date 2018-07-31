using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCProject.Models;

namespace MVCProject.Controllers
{
    public class RegisterShoppingCRUDController : Controller
    {
        TrainingDBEntities db = new TrainingDBEntities();
        // GET: RegisterShoppingCRUD
        [HttpGet]
        public ActionResult InsertRegisterShopping()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertRegisterShopping(RegisterShopping rs)
        {
            rs.Name = Request.Form["txtname"].ToString();
            rs.Gender = Request.Form["gender"].ToString();
            rs.Membership = Request.Form["ddlmember"].ToString();
            rs.ShoppingPreference = Request.Form["shop"].ToString();
            var res = Request.Form["cbcod"].ToString();
            if (res=="false")
            {
                rs.COD = "no";
            }
            else
            {
                rs.COD = "yes";
            }
            rs.Password = Request.Form["txtpass"].ToString();
            db.RegisterShoppings.Add(rs);
            var r = db.SaveChanges();
            if (r > 0)
                ModelState.AddModelError("", "Data Inserted");
            return View();

        }
        public ActionResult SelectAllRegisterShopping()
        {
            var data = db.RegisterShoppings.ToList();
            return View(data);
        }
        [HttpGet]
        public ActionResult SelectById()
        {
            var data = new SelectList(db.RegisterShoppings, "Id", "Name");
            Session["rsdata"] = data;
            return View();
        }
        [HttpPost]
        public ActionResult SelectById(string command)
        {
           int  id = int.Parse(Request.Form["ddlid"].ToString());
            if (command == "Search by Id")
            {
                var data = db.RegisterShoppings.Where(X => X.Id == id).SingleOrDefault();
                ViewData["userdata"] = data;
                return View();
            }
            if(command== "update")
            {
                int oldid = int.Parse(Request.Form["txtid"].ToString());
                var olddata = db.RegisterShoppings.Where(x => x.Id == oldid).SingleOrDefault();
                olddata.Membership = Request.Form["txtmem"].ToString();
                olddata.ShoppingPreference = Request.Form["txtshop"].ToString();
                olddata.COD = Request.Form["txtcod"].ToString();
                olddata.Password = Request.Form["txtpass"].ToString();
                var res = db.SaveChanges();
                if (res > 0)
                    ModelState.AddModelError("", "Data update");
                return View();
            }
            if (command == "Delete")
            {
                int oldid = int.Parse(Request.Form["txtid"].ToString());
                var olddata = db.RegisterShoppings.Where(x => x.Id == oldid).SingleOrDefault();
                db.RegisterShoppings.Remove(olddata);
                var res = db.SaveChanges();
                if (res > 0)
                    ModelState.AddModelError("", "Data Deleted");
                return View();
            }
            return View();

        }
    }
}
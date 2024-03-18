using Hunger.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hunger.Controllers
{
    public class AdminController : Controller
    {
        ZeroEntities db = new ZeroEntities();
        // GET: Admin
        public ActionResult RequestList()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("LogIn", "Home");
            }
            return View(db.tbl_restaurant.ToList());
        }


        [HttpGet]
        public ActionResult AssignEmployee(int id)
        {
            var req = db.tbl_request.Find(id);
            ViewBag.EmployeeID = db.tbl_employee.ToList();
            return View(req);

            //return RedirectToAction("RequestList");
        }

        [HttpPost]
        public ActionResult AssignEmployee(tbl_request request)
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("LogIn", "Home");
            }
       
            var req = db.tbl_request.Find(request.id);
            req.employee_id = request.employee_id;
            req.status = "Picking Up";

            db.SaveChanges();
            return RedirectToAction("RequestList");
        }


        //------------------------------------  Log Out -----------------------------------------------
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("LogIn", "Home");
        }
    }




}

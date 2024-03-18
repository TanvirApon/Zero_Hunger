using Hunger.EF;
using Hunger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hunger.Controllers
{
    public class EmployeeController : Controller
    {
        ZeroEntities db = new ZeroEntities();
        // GET: Employee
        public ActionResult MyRequestList()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("LogIn", "Home");
            }
  
            int userID = (int)Session["userID"];
            var list = db.tbl_request.Where(x => x.employee_id == userID).ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult UpdateRequest(int id)
        {
            if (Session["userID"] == null) { return RedirectToAction("LogIn", "Home"); }

            var req = db.tbl_request.Find(id);
            req.col_time = DateTime.Now;
            req.status = "Collection Done";

            db.SaveChanges();

            return RedirectToAction("MyRequestList");
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("LogIn", "Home");
        }
    }
}
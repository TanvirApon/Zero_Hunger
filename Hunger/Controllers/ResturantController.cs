using Hunger.EF;
using Hunger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hunger.Controllers
{
    public class ResturantController : Controller
    {
        ZeroEntities db = new ZeroEntities();
        // GET: Resturant
        public ActionResult MyRequestList()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("LogIn", "Home");
            }
            
            var list = db.tbl_request.Where(x => x.restaurent_id == (int)Session["userID"]);
            return View(db.tbl_request.ToList());
        }

        public ActionResult AddRequest()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("LogIn", "Home");
            }
       

            tbl_request request = new tbl_request()
            {
                req_time = DateTime.Now,
                status = "Pending",
                restaurent_id = (int)Session["userID"]
            };
            db.tbl_request.Add(request);
            db.SaveChanges();

            var checkRequest = db.tbl_request.FirstOrDefault(x => x.req_time == request.req_time);

            if (checkRequest != null)
            {
                return RedirectToAction("ItemsInRequest", new { id = checkRequest.id });
            }

            return View();
        }
        public ActionResult ItemsInRequest(int id)
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("LogIn", "Home");
            }
         
            var items = db.tbl_item.Where(x => x.request_id == id).ToList();
            ViewBag.reqID = id;
            return View(items);
        }

        [HttpGet]
        public ActionResult AddItem(int id)
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("LogIn", "Home");
            }
            ViewBag.id = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddItem(ItemDTO tempItem)
        {
           
            tbl_item item = new tbl_item()
            {
                name = tempItem.name,
                expire_date = tempItem.ExpireDate,
                quantity = (int)tempItem.quantity,
                RequestID = tempItem.RequestID,
            };
            db.tbl_item.Add(item);
            db.SaveChanges();

            return RedirectToAction("ItemsInRequest", new { id = item.request_id });
        }
        public ActionResult DeleteItem(int id)
        {
            
            var item = db.tbl_item.Find(id);
            int reqID = (int)item.request_id;
            db.tbl_item.Remove(item);
            db.SaveChanges();
            return RedirectToAction("ItemsInRequest", new { id = reqID });
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("LogIn", "Home");
        }
    }
}
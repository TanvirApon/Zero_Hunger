using Hunger.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.ModelConfiguration.Configuration;
using Hunger.Models;
using AutoMapper;


namespace Hunger.Controllers
{
    public class HomeController : Controller
    {

        ZeroEntities db = new ZeroEntities();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Login(LoginValidation user)
        {
            if (user.UserType == "Admin")
            {
                var dbAdmin = db.tbl_admin.FirstOrDefault(x => x.email.Equals(user.email));
                if (dbAdmin != null && dbAdmin.password.Equals(user.password))
                {
                    Session["userID"] = dbAdmin.id;
                    return RedirectToAction("RequestList", "Admin");
                }
            }
            else if (user.UserType == "Resturant")
            {
                var dbResturant = db.tbl_restaurant.FirstOrDefault(x => x.email.Equals(user.email));
                if (dbResturant != null && dbResturant.password.Equals(user.password))
                {
                    Session["userID"] = dbResturant.id;
                    return RedirectToAction("MyRequestList", "Resturant");
                }
            }
            else
            {
                var dbEmployee = db.tbl_employee.FirstOrDefault(x => x.email.Equals(user.email));
                if (dbEmployee != null && dbEmployee.password.Equals(user.password))
                {
                    Session["userID"] = dbEmployee.id;
                    return RedirectToAction("MyRequestList", "Employee");
                }
            }
            return View(user);
        }


        //-------------------------------------- Employee Registration ----------------------------------
        [HttpGet]
        public ActionResult EmployeeRegistration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EmployeeRegistration(EmployeeDTO tempEmployee)
        {
  
            #region
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeDTO, tbl_employee>();
            });
            var mapper = new Mapper(config);
            var data = mapper.Map<tbl_employee>(tempEmployee);
            #endregion

            db.tbl_employee.Add(data);
            db.SaveChanges();

            return RedirectToAction("Login");
        }

        //------------------------------------- Resturant Registration --------------------------------
        [HttpGet]
        public ActionResult ResturantRegistration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResturantRegistration(ResturantDTO tempResturant)
        {
           
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ResturantDTO, tbl_restaurant>();
            });
            var mapper = new Mapper(config);
            var data = mapper.Map<tbl_restaurant>(tempResturant);

            db.tbl_restaurant.Add(data);
            return RedirectToAction("Login");
        }
    }

}


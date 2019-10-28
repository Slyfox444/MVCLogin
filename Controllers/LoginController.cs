using MVCLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLogin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(MVCLogin.Models.User userModel)
        {
            using (LoginDatabaseEntities db = new LoginDatabaseEntities())
            {
                var userDetails = db.Users.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();

                if(userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong Username or Password. ";
                    return View("Index", userModel);
                }
                else
                {
                    Session["UserID"] = userDetails.UserID;
                    Session["userName"] = userDetails.UserID;
                    return RedirectToAction("Index", "Home");
                }
            }
                
        }

        public ActionResult LogOut()
        {
            int userId = (int)Session["UserID"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        

    }
}
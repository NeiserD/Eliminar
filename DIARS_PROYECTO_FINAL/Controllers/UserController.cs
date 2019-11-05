using DIARS_PROYECTO_FINAL.BD;
using DIARS_PROYECTO_FINAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DIARS_PROYECTO_FINAL.Controllers
{
    public class UserController : Controller
    {
        
        //public ActionResult Index()
        //{
        //    var usuarios = db.Usuarios.ToList();
        //    return View(usuarios);
        //}
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario objUser)
        {
            if (ModelState.IsValid)
            {
                using (StoreContext db = new StoreContext())
                {
                    var obj = db.Usuarios.Where(a => a.username.Equals(objUser.username) && a.password.Equals(objUser.password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["Id"] = obj.Id.ToString();
                        Session["username"] = obj.username.ToString();
                        return RedirectToAction("UserDashBoard");
                    }
                }
            }
            return View(objUser);
        }

        public ActionResult UserDashBoard()
        {
            if (Session["Id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Salir()
        {
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            return RedirectToAction("Login");
            //ModelState.AddModelError("", "Sesión Cerrada");
        }
    }
} 

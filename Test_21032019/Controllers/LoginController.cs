using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Test_21032019.Models;
using System.Xml;


namespace Test_21032019.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // Register
        [HttpGet]
        public ActionResult Register()
        {
            Login user = new Login();
            return View(user);
        }
        [HttpPost]
        public ActionResult Register(Login userNew)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userNew.aktywny = false;
                    Guid guidPotwierdzenie = Guid.NewGuid();
                    userNew.guid = guidPotwierdzenie.ToString();
                    userNew.SaveToDatabase(); // -> F11

                    string url = System.Web.HttpRuntime.AppDomainAppVirtualPath + Url.Action("Aktywacja")+$"?kod={guidPotwierdzenie.ToString()}";
             
                    string subject = "Link aktywacyjny";

                    emailClass.mailSend(userNew.email, url, subject);
                    return View("Zarejestrowano");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(userNew);
        }
        public ActionResult Aktywacja(string kod)
        {
            testowaEntities ent = new testowaEntities();
            var user = ent.loginies.Where(x => x.guid == kod).FirstOrDefault();
            if (user != null)
            {
                user.aktywny = true;
                return View("Aktywacja");
            }
            else
            {
                return View("Nieudanaaktywacja");
            }
        }



        // Logowanie
        //???

        [HttpGet]
        public ActionResult Logowanie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logowanie(string login, string password)
        {
            Login newLog = new Login();
            password = Cryptop.Hash(password);
            bool check = newLog.CheckIfExist(login, password);
            if (check == true)
            {
                //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(login, false, 30);
                FormsAuthentication.SetAuthCookie(login, false);
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View();
            }
        }
        //Wylogowanie
        public ActionResult Wylogowanie()
        {
            //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(login, false, 30);
            FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Home");
            
           
        }

        //Resetowanie hasla
        public ActionResult passwordReset()
        {
            passwordResetModel model = new passwordResetModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerateGuidForEmail(passwordResetModel model)
        {
            Guid nowyGuid = Guid.NewGuid();
            passwordResetModel.setResetCode(model.adresEmail, nowyGuid.ToString());
            // przeslanie mailem

            string subjectMail = "Twoj kod resetujacy haslo to:";
            string path = HttpContext.Server.MapPath("~/Content/template/guidMailBody.html");
            string body = System.IO.File.ReadAllText(path);
            body = body.Replace("{guid}", nowyGuid.ToString());
            emailClass.mailSend(model.adresEmail, body, subjectMail);

            return View("PodanieGuidu", model);
        }

        [HttpPost]
        public ActionResult AkceptacjaGuidu(passwordResetModel model)
        {
            
            bool check = passwordResetModel.checkGuid(model.guid, model.adresEmail);
            if (check == true)
            {
                return View("SetNewPassword", model);
            }
            return View("PodanieGuidu", model);
        }

        [HttpPost]
        public ActionResult SetNewPassword(passwordResetModel model, string newPassword, string newPasswordConfirm)
        {
            if(newPassword == newPasswordConfirm)
            {
                passwordResetModel.resetPassword(model.adresEmail, model.guid, newPasswordConfirm);
                return RedirectToAction("Logowanie");
           }
            return View("SetNewPassword", model);
        }

    }
}


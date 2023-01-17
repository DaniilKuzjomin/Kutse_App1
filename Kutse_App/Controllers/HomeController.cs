using Kutse_App.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Kutse_App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string pidu = "";
            if (DateTime.Now.Month == 1) { pidu = "Jõulud."; } 
            
            else if (DateTime.Now.Month == 2) { pidu = "iseseisvuspäev."; }

            else if (DateTime.Now.Month == 3) { pidu = "Naistepäev."; }

            else if (DateTime.Now.Month == 4) { pidu = "Aprillinali."; }

            else if (DateTime.Now.Month == 5) { pidu = "Võidupüha pidu."; }

            else if (DateTime.Now.Month == 6) { pidu = "Lastekaitsepäev."; }

            else if (DateTime.Now.Month == 7) { pidu = "Ülemaailmne UFO päev."; }

            else if (DateTime.Now.Month == 8) { pidu = "Nostalgiline päev."; }

            else if (DateTime.Now.Month == 9) { pidu = "Teadmiste päev."; }

            else if (DateTime.Now.Month == 10) { pidu = "Loomade päev."; }

            else if (DateTime.Now.Month == 11) { pidu = "Veegani päev."; }

            else if (DateTime.Now.Month == 12) { pidu = "Vanaasta õhtu."; }


            ViewBag.Message = "Ootan sind meie peole! " + pidu + " Palun tule kindlasti!";

            int hour = DateTime.Now.Hour;
            if (hour <= 16)
            {
                ViewBag.Greeting = hour < 10 ? "Tere hommikust" : "Tere päevast";
            }
            else if (hour > 16)
            {
                ViewBag.Greeting = hour < 20 ? "Tere õhtu" : "Tere päevast";
            }
            return View();
        }
        [HttpGet]
        public ViewResult Ankeet()
        {
            return View();
        }
        public ViewResult About()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Ankeet(Guest guest)
        {
            E_mail(guest);
            ViewBag.Mail = guest.Email;
            if (ModelState.IsValid)
            {
                return View("Thanks", guest);
            }
            else
            { return View(); }
        }
        public void E_mail(Guest guest)
        {
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "yeezyresell350@gmail.com";
                WebMail.Password = "okoladdbpgzkambl";
                WebMail.From = "yeezyresell350@gmail.com";
                WebMail.Send("daniil.kuzjomin@gmail.com", "Vastus kutsele", guest.Name + " vastas " + ((guest.WillAttend ?? false) ? "tuleb peole " : "ei tule peole"));
                ViewBag.Message = "Kiri on saatnud!";
            }
            catch (Exception)
            {
                ViewBag.Message = "Mul on kahju! Ei saa kirja saada!!!";
            }
        }
        public void Meeldetuletus(Guest guest)
        {
            try
            {

            
            WebMail.SmtpServer = "smtp.gmail.com";
            WebMail.SmtpPort = 587;
            WebMail.EnableSsl = true;
            WebMail.UserName = "yeezyresell350@gmail.com";
            WebMail.Password = "okoladdbpgzkambl";
            WebMail.Send(guest.Email, "Meeldetuletus", guest.Name + ", ara unusta et meie peo on super! Sind ootavad väga!",
                    null, "yeezyresell350@gmail.com",
                    filesToAttach: new String[] { Path.Combine(Server.MapPath("~/Images/"), Path.GetFileName("vecherinki-pati.jpg")) });
                ViewBag.Message = "Kiri on saatnud!";
            }
            catch (Exception)
            {
                ViewBag.Message = "Kahju! Ei saa kirja saada";
            }
        }

        GuestContext db = new GuestContext();

        public ActionResult Guests()
        {
            IEnumerable<Guest> guests = db.Guests;
            return View(guests);
        }

    }
}
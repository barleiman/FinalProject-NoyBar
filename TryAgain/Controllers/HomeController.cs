using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TryAgain.DAL;
using TryAgain.Models;

namespace TryAgain.Controllers
{
    public class HomeController : Controller
    {
        private DBContext db = new DBContext();


        // GET: Home
        public ActionResult Index()
        {

            // TODO: getting the data for the current user


            return View();
        }


        public ActionResult Statistics()
        {

            // calaculating the num of posts in every rate 
            CalcRatesGraphData();

            // calculating the num of posts in the last  month
            CalcLineGraphData();

            return View();
        }




        public ActionResult Contact()
        {
            string strIP = db.getIP();

            // TODO: getting the data for the current user

            // getting the ip data of the current user 
            ViewData["geoIP"] = strIP;

            return View();
        }


        [AllowAnonymous]
        public ActionResult Register()
        {
            User model = new User();

            ViewData["Error"] = "";

            return View(model);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                List<User> fans = db._users.Where(fan => fan.Email.Equals(model.Email)).ToList();
                if (fans.Count() == 0)
                {
                    model.RegistrationDate = DateTime.Now;
                    model.FanAuthority = Models.User.Authority.Fan;
                    model.UserName = model.FirstName + "  " + model.LastName;
                    db._users.Add(model);
                    db.SaveChanges();

                    ViewModelBase.logedonUser = model;

                    return RedirectToAction("Index", "Managment");
                }
                else { ViewData["Error"] = "The email already exist"; ; }
            }


            // If we got this far, something failed, redisplay form
            return View(model);
        }


        public ActionResult Login()
        {
            LoginViewModel lvmLogin = new LoginViewModel();

            ViewData["Error"] = "";


            return View(lvmLogin);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        // public ActionResult Login(Fan model)
        {
            List<User> fans = db._users.Where(fan => fan.Email.Equals(model.Email) && (fan.Password.Equals(model.Password))).ToList();

            if (ModelState.IsValid)
            {

                if (fans != null && fans.Count > 0)
                {
                    ViewModelBase.logedonUser = fans.First();
                }
                else
                {
                    ViewData["loginError"] = "Wrong user or password";
                    return RedirectToAction("Login", "Home");
                }

                return RedirectToAction("Index", "Managment");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        // POST: /Home/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            // AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            ViewModelBase.logedonUser = null;

            return RedirectToAction("Index", "Managment");
        }

        //

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        /// <summary>
        /// Calc the post rating data to the bar graph
        /// </summary>
        public void CalcRatesGraphData()
        {
            List<Post> lstFans = db._posts.ToList();
            int maxRate = 5;
            int[] data = new int[maxRate + 1];

            for (int i = 0; i < maxRate; i++)
            {
                data[i] = 0;
            }

            int RounRate = 0;

            foreach (Post item in lstFans)
            {
                if (item.postRate <= maxRate && item.postRate >= 0)
                {
                    RounRate = (int)Math.Round(item.postRate);
                    data[RounRate]++;
                }
            }

            ViewData["RateData"] = data;
        }

        /// <summary>
        /// Calc the post rating data to the bar graph
        /// </summary>
        public void CalcLineGraphData()
        {
            List<Post> lstFans = db._posts.ToList();

            int monthCount = 0;
            int maxDate = 6;
            int currMonth;
            int currYear;
            string[] data = new string[maxDate];


            for (int i = 0; i < maxDate; i++)
            {
                currMonth = DateTime.Today.AddMonths(-(i + 1)).Month;
                currYear = DateTime.Today.AddMonths(-i).Year;
                monthCount = lstFans.Where(ps => ps.PostDate.Month.Equals(currMonth) && ps.PostDate.Year.Equals(currYear)).Count();

                data[i] = DateTime.Today.AddMonths(-i).GetDateTimeFormats('u')[0].Substring(0, 10) + "," + monthCount;//.ToString("yyyy-MM-dd") + "," + monthCount ;
            }


            string json = JsonConvert.SerializeObject(data);

            ViewData["LineData"] = json;
        }



    }
    public static class JavaScript
    {
        public static string Serialize(object o)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(o);
        }
    }

}

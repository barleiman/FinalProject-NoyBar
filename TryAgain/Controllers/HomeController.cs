using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            string strIP = db.getIP();

            // TODO: getting the data fo the current user

            // getting the ip data of the current user 
            ViewData["geoIP"] = strIP;

            CalcRatesGraphData();

            CalcLineGraphData();
            return View();
        }

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
            string str = "[ { year : 2007, value: -10},{ year : 2008, value: 34}, { year : 2009, value: 10},{ year : 2010, value: -60},  { year : 2011, value: -40}, { year : 2012, value: 20},{ year : 2013, value: 45}, { year : 2014, value: 50},{ year : 2015, value: 60},  { year : 2016, value: 40} ]";

            var json = new JavaScriptSerializer().Serialize(str);


            ViewData["LineData"] = json;
        }


        /// <summary>
        /// Calc the post rating data to the bar graph
        /// </summary>
        public void CalcRatesGraphData2()
        {
            List<Post> lstFans = db._posts.ToList();
            int maxRate = 5;
            int[] data = new int[maxRate + 1];
            string strdata = "[";
            Dictionary<string, int> dddd = new Dictionary<string, int>();

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

            string[] final = new string[maxRate];
            for (int i = 0; i < maxRate; i++)
            {
                dddd.Add(i.ToString(), data[i]);
                final[i] = @"{ 'key':" +i.ToString() + ", 'value': " + data[i] + " }";
                strdata += @"{ key:" + i.ToString() + ", value: " + data[i] + " }";
                if (i!= maxRate-1)
                {
                    strdata += ",";
                }
            }

            strdata += "]";

            ViewData["RateData"] = dddd;
        }
}
}

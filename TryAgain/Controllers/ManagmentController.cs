using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TryAgain.DAL;
using TryAgain.Models;

namespace TryAgain.Controllers
{
    public class ManagmentController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Managment
        public ActionResult Index()
        {
            string strIP = db.getIP();
           
            // TODO: getting the data fo the current user

            // getting the ip data of the current user 
            ViewData["geoIP"] = strIP;

            return View(db._posts.ToList());
        }

        // GET: Managment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db._posts.Find(id);
            IEnumerable<Comment> lsComment = post.Comments;
            if (post == null)
            {
                return HttpNotFound();
            }
           
            return View(lsComment);
        }

        // GET: Managment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Managment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostID,Title,Author,AuthorSiteAddr,PostDate,PostText,postRate")] Post post)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors })
                    .ToArray();
            }
            if (ModelState.IsValid)
            {
                post.PostDate = DateTime.Now.Date;
                post.postRate = 0;
                db._posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Managment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db._posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Managment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostID,Title,Author,AuthorSiteAddr,PostDate,PostText,postRate")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.PostDate = DateTime.Now.Date;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Managment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db._posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Managment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db._posts.Find(id);
            List<Comment> comm = db._comments.Where((item) =>(item.PostID == id)).ToList();

            db._comments.RemoveRange(comm);
      
            db._posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}

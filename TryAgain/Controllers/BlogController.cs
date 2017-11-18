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
using TryAgain.GeoIPWebService;
using System.ServiceModel;
using Newtonsoft.Json.Linq;

namespace TryAgain.Controllers
{
    public class BlogController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Blog
        public ActionResult Index(int? id)
        {
            int maxDaysBack = 30;
            List<Post> lsPosts = db._posts.ToList();
            List<Post> topPosts;
            string str = db.getIP();


            // The default page of the blog is the most rated post
            if (id == null)
            {
                topPosts = db.TopPosts(maxDaysBack);
                if (topPosts != null && topPosts.Count() > 0)
                {
                    return View(topPosts.ElementAt(0));
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.PartialContent);
                }
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // getting the specific post if specified
            Post post = db._posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db._comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Blog/Create
        public ActionResult Create(int ID)
        {
            Comment comm = new Comment();
            comm.PostID = ID;
            comm.CommentDate = DateTime.Now.Date;
            comm.commentUser = ViewModelBase.logedonUser;

            return View(comm);
        }

        // POST: Blog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentID,PostID,Title,Text,CommentDate,givenRate,commentUser")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                Post post = db._posts.Find(comment.PostID);
                comment.commentUser = ViewModelBase.logedonUser;
                post.Comments.Add(comment);

                db._comments.Add(comment);
                db.Entry(comment.commentUser).State = EntityState.Unchanged;
                db.SaveChanges();

                double calcRate = 0;
                foreach (Comment item in post.Comments)
                {
                    calcRate += item.givenRate;
                }
                post.postRate = ((double)calcRate / (post.Comments.Count())) ;

                return RedirectToAction("Index", "Blog", new { id = comment.PostID });
            }

            return View(comment);
        }

        // GET: Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db._comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            db.Entry(comment).State = EntityState.Modified;
            return View(comment);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentID,PostID,Title,Text,CommentDate,givenRate,commentUser")] Comment comment)
        {
            if (ModelState.IsValid)
            {

                db.Entry(comment).State = EntityState.Modified;
                db.Entry(comment.commentUser).State = EntityState.Unchanged;


                Post post = db._posts.Find(comment.PostID);
                double calcRate = 0;

                foreach (Comment item in post.Comments)
                {
                    calcRate += item.givenRate;
                }

                post.postRate = ((double)calcRate / (post.Comments.Count()));

                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Details", "Blog", new { id = comment.CommentID });
            }
            return View(comment);
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db._comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db._comments.Find(id);
            Post post = db._posts.Find(comment.PostID);
            post.Comments.Remove(comment);

            double calcRate = 0;

            foreach (Comment item in post.Comments)
            {
                calcRate += item.givenRate;
            }

            post.postRate = ((double)calcRate / (post.Comments.Count()));

            db._comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index", "Blog", new { id = comment.PostID });
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

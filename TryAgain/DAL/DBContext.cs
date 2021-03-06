﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using System.Web;
using TryAgain.GeoIPWebService;
using TryAgain.Models;
using TryAgain.SunsetWebService;

namespace TryAgain.DAL
{
    public class DBContext : DbContext
    {
        public DbSet<Post> _posts { get; set; }
        public DbSet<Comment> _comments { get; set; }
        public DbSet<User> _users { get; set; }

        /// <summary>
        /// Query for the top posts on the page
        /// </summary>
        /// <returns></returns>
        public List<Post> TopPosts(int days)
        {
            DateTime minDate = DateTime.Now.Date.AddDays(-days);
            List<Post> lstPosts = _posts.Where(post => (post.PostDate.CompareTo(minDate) >= 0)).ToList();
            double avgPostRate = 0;

            foreach (Post item in lstPosts)
            {
                avgPostRate += item.postRate;
            }

            avgPostRate = ((double)avgPostRate / lstPosts.Count());


            // השאילתה - צריך לבחור את הטבלה ואז לעשות לה WHERE
            // אחרי זה עושים בסוגריים את הביטוי כאשר חייב לציין קודם ערך שייצג את השורה: לדוגמא 
            //(x => ())
            //X מייצג את השורות ואז ניתן לבדוק לפי הערכים של אותה שורה כמו תאריך הפוסט/דירוג הפוסט וכו
            List<Post> topPosts = lstPosts.Where(ps => (ps.postRate > avgPostRate)).ToList();


            return topPosts;
        }

        /// <summary>
        /// WEB-SERVICE get the current IP of the user
        /// </summary>
        /// <returns></returns>
        public string getIP()
        {

            string results = "";

            try
            {
                BasicHttpBinding newbinding = new BasicHttpBinding();
                newbinding.MaxReceivedMessageSize = 2000000;

                EndpointAddress epaAddGeo = new EndpointAddress("http://www.webservicex.net/geoipservice.asmx");

                //SunSetRiseServiceSoapClient sunriseClient = new SunSetRiseServiceSoapClient(newbinding, epaSunrise);

                GeoIPServiceSoapClient geip = new GeoIPServiceSoapClient(newbinding, epaAddGeo);

                var geoipcnt = geip.GetGeoIPContext();

                results = " current country : " + geoipcnt.CountryName + " and current IP: " + geoipcnt.IP;

            }
            catch (Exception ex)
            {
                results = ex.Message + " web service failed";
            }

            return results;

        }

        public List<Post> FindPost(DateTime postDate, string TitleWord, string author)
        {
            List<Post> lstFoundPost = new List<Post>();
            lstFoundPost = _posts.Where(ps => (ps.PostDate.CompareTo(postDate) >= 0) && (ps.Title.Contains(TitleWord)) &&
                                              (ps.postUser.UserName.Contains(author))).ToList();

            return lstFoundPost;
        }

        public List<Post> FindPost(DateTime postDate, string TitleWord)
        {
            List<Post> lstFoundPost = new List<Post>();
            lstFoundPost = _posts.Where(ps => (ps.PostDate.CompareTo(postDate) >= 0) && (ps.Title.Contains(TitleWord))).ToList();

            return lstFoundPost;
        }

        public List<Post> FindPost(string author, DateTime postDate)
        {
            List<Post> lstFoundPost = new List<Post>();
            lstFoundPost = _posts.Where(ps => (ps.PostDate.CompareTo(postDate) >= 0) && ((ps.postUser.UserName.Contains(author)))).ToList();

            return lstFoundPost;
        }

        /// <summary>
        /// WEB-SERVICE get the current IP of the user
        /// </summary>
        /// <returns></returns>
        public List<Comment> FindComments(DateTime commentDate, string searchWord, string advertiser)
        {
            List<Comment> lstFoundComments = new List<Comment>();

            lstFoundComments = _comments.Where(cm => (cm.CommentDate.CompareTo(commentDate) >= 0) && (cm.commentUser.UserName.Equals(advertiser))
                                                      && (cm.Text.Contains(searchWord))).ToList();

            return lstFoundComments;
        }

        public List<Comment> FindComments(string advertiser, DateTime commentDate)
        { 
        
            List<Comment> lstFoundComments = new List<Comment>();
            lstFoundComments = _comments.Where(cm => (cm.CommentDate.CompareTo(commentDate) >= 0) && (cm.commentUser.UserName.Equals(advertiser))).ToList();

            return lstFoundComments;
        }

        public List<Comment> FindComments(DateTime commentDate, string searchWord)
        {
            List<Comment> lstFoundComments = new List<Comment>();

            lstFoundComments = _comments.Where(cm => (cm.CommentDate.CompareTo(commentDate) >= 0) && (cm.Text.Contains(searchWord))).ToList();

            return lstFoundComments;
        }

        public List<Post> popularPosts()
        {
            // getting the postid of all the posts the fan ever commented on 
            var postsForFans =
                 (from comment in _comments
                 group comment by comment.PostID into PostComments
                 join post in _posts on PostComments.Key equals post.PostID
                 let commCount = PostComments.Count()
                 orderby commCount descending
                 select post).Take(10).ToList();
                
            return postsForFans;
        }

        //TODO: שיניתי את זה  
        public List<Post> recommendedPosts (string FanID)
        {
            // Find Recommended authors for the fan
            var RecommendedAuthors =
            (from comment in _comments
             join post in _posts on comment.PostID equals post.PostID
             where comment.commentUser.Email == FanID
             select new { comment.PostID, post.postUser.Email } into TBL
             group TBL by TBL.Email into favAuthors
             select new { favAuthors.Key }).Take(3).ToList();

            List<Post> recPosts = new List<Post>();
            if (RecommendedAuthors.Count == 3)
            {
                string strAuth1 = RecommendedAuthors[0].Key.ToString();
                string strAuth2 = RecommendedAuthors[1].Key.ToString();
                string strAuth3 = RecommendedAuthors[2].Key.ToString();
                // Find the recommended posts according to top 3 authors
                recPosts = _posts.Where(pst => ((pst.postUser.Email.Equals(strAuth1)) ||
                                                           (pst.postUser.Email.Equals(strAuth2)) ||
                                                           (pst.postUser.Email.Equals(strAuth3)) &&
                                                           (pst.postRate.Equals(5.0)))).ToList();
            }
            else if(RecommendedAuthors.Count > 0)
            {
                string strAuth = RecommendedAuthors[0].Key.ToString();
                // Find the recommended posts according to  best authors
                 recPosts = _posts.Where(pst => (pst.postUser.Email.ToString().Equals(strAuth))&&
                                                           (pst.postRate.Equals(5.0))).ToList();
            }
            
            if(recPosts.Count==0)
            {
                // if not found the top author for the user take the most rated authors in the blog
                 recPosts = TopPosts(30);
            }
            
            return recPosts;
        }

    }
}
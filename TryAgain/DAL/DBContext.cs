using System;
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
        public DbSet<Fan> _Fans { get; set; }
        public DbSet<Comment> _comments { get; set; }

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
            List<Post> topPosts = lstPosts.Where(ps => (ps.postRate > avgPostRate )).ToList();

            
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
    }
}
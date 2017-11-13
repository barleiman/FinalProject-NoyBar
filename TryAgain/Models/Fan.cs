using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TryAgain.Models
{

    public class Fan : ViewModelBase
    {
        static int countIDs = 0;

        public enum Authority { Admin, Writer, Fan };

        public Fan()
        {
            this.ID = countIDs;
            countIDs++;
        }

        [Key]
        public int ID { get; set; }

        [Display(Name = "Username")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Text)]
        public string Password { get; set; }

        [Display(Name = "Fan firs-tname")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Display(Name = "Fan last-name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Display(Name = "Fan gender")]
        public string Gender { get; set; }


        [Display(Name = "Fan birthdate")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Registration date")]
        public DateTime RegistrationDate { get; set; }

        public Authority FanAuthority { get; set; }
      }
}
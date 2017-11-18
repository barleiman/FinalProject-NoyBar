using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TryAgain.Models
{
    public class User
    {
        static int countIDs = 0;

        public User()
        {
           // this.ID = countIDs;
            countIDs++;
        }

        /*[Key]
        [Column(Order = 2)]
        public int ID { get; set; }
        */
        [Key]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public enum Authority { Admin, Fan };


        [Display(Name = "Fan first-name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Display(Name = "Fan last-name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Display(Name = "Fan last-name")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Display(Name = "Fan gender")]
        public string Gender { get; set; }


        [Display(Name = "Fan birthdate")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Registration date")]
        public DateTime RegistrationDate { get; set; }

        public Authority FanAuthority { get; set; }

        [Display(Name = "User website address")]
        [DataType(DataType.Url)]
        public string SiteAddr { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}
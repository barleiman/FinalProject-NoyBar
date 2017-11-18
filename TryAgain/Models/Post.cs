using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TryAgain.Models
{
    public class Post : ViewModelBase
    {
        static int countIDs = 0;

        public Post()
        {
            this.PostID = countIDs;
            countIDs++;

            this.Comments = new List<Comment>();
        }


        [Key]
        [ConcurrencyCheck]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //  [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostID { get; set; }

        [Display(Name = "Post title")]
        [DataType(DataType.Text)]
        public string Title { get; set; }
        /*
        [Display(Name = "Author name")]
        [DataType(DataType.Text)]
        [StringLength(80, ErrorMessage = "First name cannot be longer than 80 characters.")]
        public string Author { get; set; }

        [Display(Name = "Author website address")]
        [DataType(DataType.Url)]
        public string AuthorSiteAddr { get; set; }
        */

        [Display(Name = "Post writing Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PostDate { get; set; }

        [Display(Name = "Post text")]
        [DataType(DataType.MultilineText)]
        public string PostText { get; set; }


        [Display(Name = "Post rate")]
        [Range(0, 5)]
        public double postRate { get; set; }

        public virtual User postUser { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

    }
}
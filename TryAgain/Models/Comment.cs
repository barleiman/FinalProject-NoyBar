using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TryAgain.Models
{
    public class Comment : ViewModelBase
    {
        static int countIDs = 0;

        public Comment()
        {
            this.CommentID = countIDs;
            countIDs++;
        }


        [ConcurrencyCheck]
        [Key]
        // [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentID { get; set; }

        //[ForeignKey("Post.PostID")]
        public int PostID { get; set; }

        [Display(Name = "Comment title")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        /*
        [Display(Name = "Commenter name")]
        [DataType(DataType.Text)]
        [StringLength(80, ErrorMessage = "Full name cannot be longer than 80 characters.")]
        public string Commenter { get; set; }

        [Display(Name = "Commenter site address")]
        [DataType(DataType.Url)]
        public string CommenterSiteAddr { get; set; }
        */

        [Display(Name = "Comment text")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [Display(Name = "Comment writing Date")]
        [DataType(DataType.DateTime)]
        [ConcurrencyCheck]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CommentDate { get; set; }


        [Display(Name = "Given rate")]
        [Range(0, 5)]
        public double givenRate { set; get; }

        public virtual User commentUser { get; set; }

    }

    public class searchComment
    {
        public string commentor;
        public string Text;
        public DateTime date;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OffRosterManager.Models
{
    public class OffRosterRequestComment
    {
        public int CommentId { get; set; }
        public int OffRosterRequestId { get; set; }
        public string CommentedBy { get; set; }
        public DateTime CommentedAt { get; set; }

        [Display(Name = "Add a new comment:")]
        [Required(ErrorMessage = "Please enter a comment")]
        public string Comment { get; set; }
    }
}

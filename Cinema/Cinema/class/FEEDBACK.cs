namespace CinemaAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("FEEDBACK")]
    public partial class FEEDBACK
    {
        [StringLength(10)]
        public string FeedbackID { get; set; }

        [StringLength(10)]
        public string UserID { get; set; }

        public string FeedbackContent { get; set; }

        public virtual USER_ACCOUNT USER_ACCOUNT { get; set; }
    }
}

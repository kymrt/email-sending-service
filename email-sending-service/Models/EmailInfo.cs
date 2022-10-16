using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace email_sending_service.Models
{
    public class EmailInfo
    {
        [Column("Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Email")]
        [Required]
        public string EmailAddress { get; set; }

        [Column("State")]
        public int State { get; set; }

        [Column("RegisterDate")]
        public DateTime RegisterDate { get; set; }

        [Column("SentDate")]
        public DateTime? SentDate { get; set; }
    }
}

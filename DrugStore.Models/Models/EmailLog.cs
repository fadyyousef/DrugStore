using DrugStore.Model.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugStore.Model.Models
{
    public class EmailLog : IAuditBase
    {
        [Key]
        public int EmailLogID { get; set; }

        [Column(TypeName = "NVARCHAR"), Required, StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string EmailTo { get; set; }

        [Column(TypeName = "NVARCHAR"), Required, StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string EmailFrom { get; set; }

        [Column(TypeName = "NVARCHAR"), StringLength(250)]
        public string EmailCC { get; set; }

        [Column(TypeName = "NVARCHAR"), Required, StringLength(250)]
        public string EmailSubject { get; set; }

        [Column(TypeName = "NVARCHAR(max)"), Required]
        public string EmailBody { get; set; }

        [Required]
        public bool isHTML { get; set; }

    }
}
using DrugStore.ViewModels.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugStore.ViewModels
{
    public class DrugCompanyVM : IAuditBase
    {
        [Key]
        public int CompanyID { get; set; }

        [Column(TypeName = "NVARCHAR"), Required,
            StringLength(250, ErrorMessage = "Company Name cannot be longer than 250 characters.")]
        public string Name { get; set; }

        [Column(TypeName = "NVARCHAR"), Required,
            StringLength(250, ErrorMessage = "Address cannot be longer than 250 characters.")]
        public string Address { get; set; }

        [Column(TypeName = "NVARCHAR"),
            StringLength(25, ErrorMessage = "Phone cannot be longer than 25 characters.")]
        public string Phone { get; set; }

        [Required]
        public bool isLocal { get; set; }

        public virtual ICollection<DrugVM> Drugs { get; set; }
    }
}
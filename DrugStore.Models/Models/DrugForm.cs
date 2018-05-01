using DrugStore.Model.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugStore.Model.Models
{
    public class DrugForm : IAuditBase
    {
        [Key]
        public int FormID { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [Required]
        [StringLength(250, ErrorMessage = "Form Name cannot be longer than 250 characters.")]
        public string Name { get; set; }

        public virtual ICollection<Drug> Drugs { get; set; }
    }
}
using DrugStore.ViewModels.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugStore.ViewModels
{
    public class DrugFormVM : IAuditBase
    {
        [Key]
        public int FormID { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [Required]
        [StringLength(250, ErrorMessage = "Form Name cannot be longer than 250 characters.")]
        public string Name { get; set; }

        public virtual ICollection<DrugVM> Drugs { get; set; }
    }
}
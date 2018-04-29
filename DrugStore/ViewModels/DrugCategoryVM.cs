using System.Collections.Generic;
using DrugStore.ViewModels.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugStore.ViewModels
{
    public class DrugCategoryVM : IAuditBase
    {
        [Key]
        public int CategoryID { get; set; }

        [Column(TypeName = "NVARCHAR"), Required,
            StringLength(250, ErrorMessage = "Category Name cannot be longer than 250 characters.")]
        public string Name { get; set; }

        public virtual ICollection<DrugVM> Drugs { get; set; }
    }
}
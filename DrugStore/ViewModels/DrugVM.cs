using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using DrugStore.ViewModels.Interfaces;

namespace DrugStore.ViewModels
{
    public class DrugVM: IAuditBase
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public int CategoryID { get; set; }
        public int FormID { get; set; }

        public SelectList DrugCompanies { get; set; }
        public SelectList DrugCategories { get; set; }
        public SelectList DrugForms { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(250, ErrorMessage = "Drug Name cannot be longer than 250 characters.")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(250, ErrorMessage = "Market Name cannot be longer than 250 characters.")]
        public string MarketName { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(1000, ErrorMessage = "Active Ingredients cannot be longer than 1000 characters.")]
        public string ActiveIngredients { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(100, ErrorMessage = "Dose Amount cannot be longer than 100 characters.")]
        public string DoseAmount { get; set; }

        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Invalid Target Price; Maximum Two Decimal Points.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Target Price; Max 18 digits")]
        public decimal Price { get; set; }

        [Required]
        public int AvailableAmount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Production Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ProductionDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Expiry Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpiryDate { get; set; }
    }
}
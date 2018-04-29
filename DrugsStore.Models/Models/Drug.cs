using DrugStore.Model.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugStore.Model.Models
{
    public class Drug : IAuditBase
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("DrugCompany")]
        public int CompanyID { get; set; }

        [ForeignKey("DrugCategory")]
        public int CategoryID { get; set; }

        [ForeignKey("DrugForm")]
        public int FormID { get; set; }

        [Column(TypeName = "NVARCHAR"), StringLength(250, ErrorMessage = "Drug Name cannot be longer than 250 characters.")]
        public string Name { get; set; }

        [Column(TypeName = "NVARCHAR"), Required, StringLength(250, ErrorMessage = "Market Name cannot be longer than 250 characters.")]
        public string MarketName { get; set; }

        [Column(TypeName = "NVARCHAR"), Required,
            StringLength(1000, ErrorMessage = "Active Ingredients cannot be longer than 1000 characters.")]
        public string ActiveIngredients { get; set; }

        [Column(TypeName = "NVARCHAR"), Required,
            StringLength(100, ErrorMessage = "Dose Amount cannot be longer than 100 characters.")]
        public string DoseAmount { get; set; }

        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Invalid Target Price; Maximum Two Decimal Points.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Target Price; Max 18 digits")]
        public decimal Price { get; set; }

        [Required]
        public int AvailableAmount { get; set; }

        [DataType(DataType.Date), Display(Name = "Production Date"), Required,
            DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ProductionDate { get; set; }

        [DataType(DataType.Date), Display(Name = "Expiry Date"), Required,
            DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpiryDate { get; set; }

        public virtual DrugForm DrugForm { get; set; }
        public virtual DrugCategory DrugCategory { get; set; }
        public virtual DrugCompany DrugCompany { get; set; }
    }
}
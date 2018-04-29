using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DrugStore.ViewModels.Interfaces;

namespace DrugStore.ViewModels
{
    public class RegisterVM
    {
        public int ID { get; set; }

        [ForeignKey("UserRole")]
        public int UserRoleID { get; set; }

        [Column("FirstName"), Display(Name = "First Name"), Required,
         StringLength(50, ErrorMessage = "First Name cannot be longer than 25 characters.", MinimumLength = 1)]
        public string FirstName { get; set; }

        [Column("MiddleName"), Display(Name = "Middle Name"), Required,
         StringLength(50, ErrorMessage = "Middle Name cannot be longer than 25 characters.", MinimumLength = 1)]
        public string MiddleName { get; set; }

        [Column("LastName"), Display(Name = "Last Name"), Required,
         StringLength(50, ErrorMessage = "Last Name cannot be longer than 25 characters.", MinimumLength = 1)]
        public string LastName { get; set; }

        public string FullName
        {
            get { return FirstName + " " + MiddleName + " " + LastName; }
        }

        [Column(TypeName = "NVARCHAR"), Required, StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [Required]
        [StringLength(25, ErrorMessage = "Phone cannot be longer than 25 characters.")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Column(TypeName = "NVARCHAR"), Required, StringLength(100, ErrorMessage = "User Name cannot be longer than 100 characters.")]
        public string UserName { get; set; }

        [Column(TypeName = "NVARCHAR(max)"), Required]
        public string Password { get; set; }

        [Column(TypeName = "NVARCHAR(max)"), Required]
        public string ConfirmPassword { get; set; }

        [Required]
        public bool IsActive { get; set; }

    }
}
using DrugStore.ViewModels.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugStore.ViewModels
{
    public class UserRoleVM : IAuditBase
    {
        [Key]
        public int UserRoleID { get; set; }

        [Column(TypeName = "NVARCHAR"), Required,
            StringLength(100, ErrorMessage = "User Role Name cannot be longer than 100 characters.", MinimumLength = 1)]
        public string UserRoleName { get; set; }
    }
}
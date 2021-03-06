﻿using DrugStore.Model.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugStore.Model.Models
{
    public class UserRole : IAuditBase
    {
        public int UserRoleID { get; set; }

        [Column(TypeName = "NVARCHAR"), Required,
            StringLength(100, ErrorMessage = "User Role Name cannot be longer than 100 characters.", MinimumLength = 1)]
        public string UserRoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
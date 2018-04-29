﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DrugStore.ViewModels.Interfaces
{
    public class IAuditBase
    {
        [Column(TypeName = "NVARCHAR"), StringLength(100)]
        public string UpdatedBy { get; set; }

        [DataType(DataType.Date), Display(Name = "Updated Date"),
            DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedDate { get; set; }

        [Column(TypeName = "NVARCHAR"), StringLength(100)]
        public string CreatedBy { get; set; }

        [DataType(DataType.Date), Display(Name = "Created Date"),
            DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
    }
}
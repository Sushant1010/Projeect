﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Models
{
    [Table("NCIT_MIS.A_Category")]
    public class A_Category
    {
        [Key]
        [Column(Order = 0)]

        public int CategoryId { get; set; }


        [Column(Order = 1)]

        public int DepartmentId { get; set; }


        [Column(Order = 2)]
        [StringLength(50)]
        public string CategoryName { get; set; }


        [StringLength(50)]
        public string CategoryCode { get; set; }

        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Models
{

    [Table("NCIT_MIS.Sys_Department")]
    public class Sys_Department
    {
        [Key]

        public int DepartmentId { get; set; }

        [StringLength(100)]
        public string DepartmentName { get; set; }


        [StringLength(50)]
        public int PhoneNo { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public string DepartmentCode { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int EnteredBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public int LastUpdatedBy { get; set; }

        public bool IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
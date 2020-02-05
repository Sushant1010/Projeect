using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Models
{
    [Table("NCIT_MIS.A_Location")]
    public class A_Location
    {
        [Key]

        public int LocationId { get; set; }

        [StringLength(100)]
        public string LocationName { get; set; }

        public string LocationCode { get; set; }

        public int DepartmentId { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int EnteredBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public int LastUpdatedBy { get; set; }

        public bool IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
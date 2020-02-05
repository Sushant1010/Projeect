

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Models
{
    [Table("NCIT_MIS.INV_Unit")]
    public class INV_Unit
    {
        [Key]
        [Column(Order = 0)]

        public int UnitId { get; set; }


        [Column(Order = 1)]

        public int DepartmentId { get; set; }


        [Column(Order = 2)]
        [StringLength(50)]
        public string UnitName { get; set; }


        [StringLength(50)]
        public string UnitCode { get; set; }

        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }
    }
}
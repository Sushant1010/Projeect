

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Models
{

    public class INV_Vendor
    {
        [Key]
        [Column(Order = 0)]

        public int VendorId { get; set; }


        [Column(Order = 1)]

        public int DepartmentId { get; set; }




        [Column(Order = 2)]
        [StringLength(50)]
        public string VendorName { get; set; }


        [StringLength(50)]
        public string VendorCode { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string PanNo { get; set; }




        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }
    }
}
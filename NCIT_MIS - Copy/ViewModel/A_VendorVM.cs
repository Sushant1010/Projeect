using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCIT_MIS.ViewModel
{
    public class A_VendorVM
    {
        public int VendorId { get; set; }

        public string VendorName { get; set; }
        public string VendorAddress { get; set; }

        public string VendorPhone { get; set; }
        public string VendorMobile { get; set; }

        public string PanNo { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int EnteredBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public int LastUpdatedBy { get; set; }

        public bool IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
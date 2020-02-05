using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Models
{
    [Table("NCIT_MIS.A_Asset")]
    public class A_Asset
    {
        [Key]

        public int AssetId { get; set; }


        public int AssetItemId { get; set; }


        public int DepartmentId { get; set; }


        public int LocationId { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public string AssetDate { get; set; }
        public string AssetDateBS { get; set; }
        public int UsefullLife { get; set; }
        public bool IsAccessory { get; set; }
        public int AccessoryId { get; set; }
        public bool IsDepreciationApplicable { get; set; }
        public string DepreciationStartDate { get; set; }
        public string DepreciationStartDateBS { get; set; }
        public string DepreciationRemarks { get; set; }
        public bool IsScrap { get; set; }
        public string ScrapDate { get; set; }
        public string ScrapDateBS { get; set; }
        public decimal ScrapRealizedValue { get; set; }
        public string AssetUniqueCode { get; set; }
        public bool IsTransfered { get; set; }
        public int AssetTransferedId { get; set; }

        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }
    }
}
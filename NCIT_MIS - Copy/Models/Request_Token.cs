using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Models
{
    [Table("NCIT_MIS.Request_Token")]
    public class Request_Token
    {
        [Key]
        public int TokenId { get; set; }
        public string RequestCategory { get; set; }
        public string ApprovedByAdmin { get; set; }
        public DateTime ApprovedByAdminDate { get; set; }
        public string RequestStatus { get; set; }
        public DateTime RequestStatusDate { get; set; }
        public int RequestToken { get; set; }
        public int DepartmentId { get; set; }
        public int LocationId { get; set; }
        public int EnteredBy { get; set; }
        public DateTime EnteredDate { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int DeletedBy { get; set; }
        public DateTime DeletedDate { get; set; }
        public int UnitId { get; set; }
    }
}
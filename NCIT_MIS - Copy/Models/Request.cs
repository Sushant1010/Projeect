using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Models
{
    [Table("NCIT_MIS.Request")]
    public class Request
    {
        [Key]
        public int RequestId { get; set; }
        public int RequestItemId { get; set; }
        public int RequestQuantity { get; set; }
        public int RequestedBy { get; set; }
        public DateTime RequestedDate { get; set; }
        public int TokenId { get; set; }
        public int EnteredBy { get; set; }
        public DateTime EnteredDate { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int UnitId { get; set; }
    }
}
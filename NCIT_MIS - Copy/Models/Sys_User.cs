

namespace NCIT_MIS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("NCIT_MIS.Sys_User")]
    public class Sys_User
    {
        [Key]
        [Column(Order = 0)]

        public int UserId { get; set; }


        [Column(Order = 1)]
        [StringLength(100)]
        public string FullName { get; set; }


        [Column(Order = 2)]
        [StringLength(50)]
        public string Email { get; set; }


        [Column(Order = 3)]
        [StringLength(600)]
        public string Password { get; set; }

        [StringLength(50)]
        public string ActiveCode { get; set; }

        public bool? IsActive { get; set; }


        [Column(Order = 4)]
        public bool IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? EnteredBy { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public int? DepartmentId { get; set; }

        public bool IsAdmin { get; set; }
        public int UserType { get; set; }

        public string PasswordSalt { get; set; }

        public bool AssetModuleAllow { get; set; }
        public bool InventoryModuleAllow { get; set; }
        public bool RequestModuleAllow { get; set; }
    }
}
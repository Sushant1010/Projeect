using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NCIT_MIS.ViewModel
{
    public class SystemUserVM
    {
        public int UserId { get; set; }

        public string FullName { get; set; }
        public string UserTypeName { get; set; }
        public string Email { get; set; }
        public string UEmail { get; set; }

        public string Password { get; set; }

        public string ActiveCode { get; set; }

        public bool? IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? EnteredBy { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public int? DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public bool IsAdmin { get; set; }

        public int UserType { get; set; }

        public bool AssetModuleAllow { get; set; }
        public bool InventoryModuleAllow { get; set; }
        public bool RequestModuleAllow { get; set; }
    }
}
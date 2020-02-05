

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.ViewModel
{
    public class Inv_RequestVM
    {
        public int TokenId { get; set; }
        public string RequestCategory { get; set; }
        public string ApprovedByAdmin { get; set; }
        public DateTime ApprovedByAdminDate { get; set; }
        public string RequestStatus { get; set; }
        public DateTime RequestStatusDate { get; set; }
        public int RequestToken { get; set; }
        public int DepartmentId { get; set; }
        public int LocationId { get; set; }
        public string DepartmentName { get; set; }
        public int EnteredBy { get; set; }
        public DateTime EnteredDate { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int DeletedBy { get; set; }
        public DateTime DeletedDate { get; set; }

        public List<RequestItemByRequestTokenId> RequestItemByRequestTokenIdList { get; set; }
    }

    public class RequestItemByRequestTokenId
    {
        public int RequestId { get; set; }
        public string RequestCategory { get; set; }
        public int LocationId { get; set; }
        public int RequestItemId { get; set; }
        public string ItemName { get; set; }
        public int RequestQuantity { get; set; }
        public int RequestToken { get; set; }
    }

    public class RequestItem
    {
        public int RequestId { get; set; }
        public string RequestCategory { get; set; }
        public int RequestItemId { get; set; }
        public int CategoryId { get; set; }
        public int LocationId { get; set; }
        public string ItemName { get; set; }
        public string CategoryName { get; set; }
        public int RequestQuantity { get; set; }
        public int RequestToken { get; set; }
    }

    public class RequestDetail
    {
        public Inv_RequestVM Request { get; set; }
        public List<Inv_RequestVM> RequestList { get; set; }
        public List<RequestItem> RequestItemList { get; set; }
    }
}
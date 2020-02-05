using Dapper;
using NCIT_MIS.Models;
using NCIT_MIS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Repository
{
    public class RequestRepo
    {
        //For Request Create
        public Request_Token TokenCount()
        {
            string sql = " select top 1 RequestToken from Request_Token " +
                "where IsDeleted=0" +
                " order by RequestToken desc";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Request_Token>(sql).FirstOrDefault();
                db.Close();
                return lst;
            }
        }

        public int AddToken(Request_Token rt)
        {
            string sql = " insert into Request_Token(" +
                         "RequestCategory, ApprovedByAdmin," +
                         " ApprovedByAdminDate, RequestStatus, RequestStatusDate, RequestToken, DepartmentId,LocationId," +
                         " EnteredBy, EnteredDate, LastUpdatedBy, LastUpdatedDate, IsDeleted, DeletedDate, DeletedBy" +
                         ")" +
                         " values(" +
                         "@RequestCategory, @ApprovedByAdmin," +
                         " null, @RequestStatus, null, @RequestToken, @DepartmentId,@LocationId," +
                         " @EnteredBy, @EnteredDate, 0, null, @IsDeleted, null, 0" +
                         ")  SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = DbHelper.GetDBConnection())
            {
                int a = db.Query<int>(sql, rt).SingleOrDefault();
                db.Close();
                return a;

            }
        }

        public int AddRequestItem(Request requestitem)
        {
            string sql = " insert into Request(" +
                         " RequestItemId, RequestQuantity, RequestedBy, RequestedDate," +
                         " TokenId," +
                         " EnteredBy, EnteredDate, LastUpdatedBy, LastUpdatedDate, IsDeleted, DeletedDate, DeletedBy,UnitId" +
                         ")" +
                         " values(" +
                         "@RequestItemId, @RequestQuantity, @RequestedBy, @RequestedDate," +
                         " @TokenId," +
                         " @EnteredBy, @EnteredDate, 0, null, @IsDeleted, null, 0,@UnitId" +
                         ")  SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = DbHelper.GetDBConnection())
            {
                int a = db.Query<int>(sql, requestitem).SingleOrDefault();
                db.Close();
                return a;

            }
        }

        //For Request Listing
        public RequestDetail GetRequestListDetail(int depid)
        {
            RequestDetail detail = new RequestDetail();
            detail.RequestList = GetTokenList(depid);
            for (int i = 0; i < detail.RequestList.Count; i++)
            {
                detail.RequestList[i].RequestItemByRequestTokenIdList = GetRequestItemListByRequestTokenId(depid, detail.RequestList[i].TokenId, detail.RequestList[i].RequestCategory);
            }
            return detail;
        }

        public List<RequestVM> GetTokenList(int departmentId)
        {
            string sql = "select r.TokenId,r.RequestToken, r.RequestCategory, d.DepartmentName,r.ApprovedByAdmin, r.RequestStatus" +
                "  from Request_Token r" +
                " left join Sys_Department d on d.DepartmentId = r.DepartmentId" +
                " where 1 = 1 and r.IsDeleted = 0 and r.DepartmentId ="+departmentId;
            sql += " order by r.RequestToken  desc";
            using (var db = DbHelper.GetDBConnection())
            {
                db.Close();
                return db.Query<RequestVM>(sql).ToList();
            }
        }

        public RequestDetail GetRequestListDetailAdmin(int depid)
        {
            RequestDetail detail = new RequestDetail();
            detail.RequestList = GetTokenList1(depid);
            for (int i = 0; i < detail.RequestList.Count; i++)
            {
                detail.RequestList[i].RequestItemByRequestTokenIdList = GetRequestItemListByRequestTokenId(depid, detail.RequestList[i].TokenId,detail.RequestList[i].RequestCategory);
            }
            return detail;
        }

        public List<RequestVM> GetTokenList1(int departmentId)
        {
            string sql = "select r.TokenId,r.RequestToken, r.RequestCategory, d.DepartmentName,r.ApprovedByAdmin, r.RequestStatus" +
                "  from Request_Token r" +
                " left join Sys_Department d on d.DepartmentId = r.DepartmentId" +
                " where 1 = 1 and r.IsDeleted = 0 ";
            sql += " order by r.RequestToken  desc";
            using (var db = DbHelper.GetDBConnection())
            {
                db.Close();
                return db.Query<RequestVM>(sql).ToList();
            }
        }
        public List<RequestItemByRequestTokenId> GetRequestItemListByRequestTokenId(int depid, int tokenId, string requestCategory)
        {
            string sql1 = "";
            if (requestCategory == "Asset")
            {
                string sql = "select r.RequestId, r.RequestItemId, ai.AssetItemName, r.RequestQuantity" +
                    " from Request r" +
                    " left join A_Item ai on ai.AssetItemId = r.RequestItemId" +
                    " where 1=1 and r.IsDeleted=0 and  r.TokenId=" + tokenId;

                using (var db = DbHelper.GetDBConnection())
                {
                    db.Close();
                    return db.Query<RequestItemByRequestTokenId>(sql).ToList();
                }
            }
            if (requestCategory == "Inventory")
            {
                string sql = "select r.RequestId, r.RequestItemId, ai.ItemName, r.RequestQuantity" +
                    " from Request r" +
                    " left join Inv_Item ai on ai.ItemId = r.RequestItemId" +
                    " where 1=1 and r.IsDeleted=0 and  r.TokenId=" + tokenId;

                using (var db = DbHelper.GetDBConnection())
                {
                    db.Close();
                    return db.Query<RequestItemByRequestTokenId>(sql).ToList();
                }
            }
            using (var db = DbHelper.GetDBConnection())
            {
                db.Close();
                return db.Query<RequestItemByRequestTokenId>(sql1).ToList();
            }
        }

        //For RequestItem Details
        public RequestDetail GetRequestDetail(int depId, int tokenid)
        {
            RequestDetail detail = new RequestDetail();
            detail.Request = GetRequestToken(depId, tokenid);
            detail.RequestItemList = GetRequestItemList(depId, tokenid);
            return detail;
        }

        public RequestVM GetRequestToken(int depId, int tokenid)
        {
            string sql = "select TokenId,RequestToken,ApprovedByAdmin,RequestStatus, LocationId" +
                " from Request_Token rt" +
                " where 1 = 1 and IsDeleted = 0 and RequestCategory='Asset' and TokenId = " + tokenid + " and DepartmentId = " + depId;

            using (var db = DbHelper.GetDBConnection())
            {
                db.Close();
                return db.Query<RequestVM>(sql).SingleOrDefault();
            }
        }

        public List<RequestItem> GetRequestItemList(int depid, int tokenid)
        {
            string sql = "select " +
                " r.RequestId, ai.AssetCategoryId, ac.CategoryName," +
                " r.TokenId, r.RequestItemId, ai.AssetItemName, r.RequestQuantity, rt.LocationId" +
                " from Request r" +
                " left join A_Item ai on ai.AssetItemId = r.RequestItemId" +
                " left join Request_Token rt on rt.TokenId = r.TokenId" +
                " left join A_Category ac on ac.CategoryId = ai.AssetCategoryId" +
                " where 1=1 and r.IsDeleted=0 and r.TokenId=" + tokenid;
            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<RequestItem>(sql).ToList();
            }
        }

        //For RequestItem Edit
        public void EditRequestToken(int tokenId, int locationId)
        {
            string sql = " Update Request_Token set LocationId=" + locationId +
            " where TokenId=" + tokenId;

            using (var db = DbHelper.GetDBConnection())
            {
                db.Query(sql);
                db.Close();
            }
        }
        public void EditRequestItemDelete(DateTime dateTime, int uid, int tokenId)
        {
            string sql = " Update Request set IsDeleted=1,DeletedBy=" + uid + ",DeletedDate= '" + dateTime + "'" +
            " where TokenId=" + tokenId;

            using (var db = DbHelper.GetDBConnection())
            {
                db.Query(sql);
                db.Close();
            }
        }
        public void EditRequestItem(Request requestitem)
        {
            string sql = " Update Request set" +
                         " RequestItemId=@RequestItemId, RequestQuantity=@RequestQuantity," +
                         " LastUpdatedBy=@LastUpdatedBy, LastUpdatedDate=@LastUpdatedDate, IsDeleted=@IsDeleted, DeletedBy=@DeletedBy, DeletedDate=@DeletedDate" +
                         " where RequestId=@RequestId";

            using (var db = DbHelper.GetDBConnection())
            {
                db.Query(sql, requestitem);
                db.Close();
            }
        }

        public void DeleteRequestToken(int id, int deletedby, DateTime deleteddate)
        {
            string sql = " Update Request_Token set IsDeleted=1,DeletedBy=" + deletedby + ",DeletedDate= '" + deleteddate + "'" +
            " where TokenId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                db.Query(sql);
                db.Close();
            }
        }

        public void ApprovedRequestToken(int id, int deletedby, DateTime deleteddate)
        {
            string sql = " Update Request_Token set ApprovedByAdmin='Approved',ApprovedByAdminDate= '" + deleteddate + "'" +
            " where TokenId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                db.Query(sql);
                db.Close();
            }
        }

        public void RejectRequestToken(int id, int deletedby, DateTime deleteddate)
        {
            string sql = " Update Request_Token set ApprovedByAdmin='Reject',ApprovedByAdminDate= '" + deleteddate + "'" +
            " where TokenId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                db.Query(sql);
                db.Close();
            }
        }


        public RequestDetail GetRequestListDetailStore(int depid)
        {
            RequestDetail detail = new RequestDetail();
            detail.RequestList = GetTokenListStore(depid);
            for (int i = 0; i < detail.RequestList.Count; i++)
            {
                detail.RequestList[i].RequestItemByRequestTokenIdList = GetRequestItemListByRequestTokenId(depid, detail.RequestList[i].TokenId, detail.RequestList[i].RequestCategory);
            }
            return detail;
        }

        public List<RequestVM> GetTokenListStore(int departmentId)
        {
            string sql = "select r.TokenId,r.RequestToken, r.RequestCategory, d.DepartmentName,r.ApprovedByAdmin, r.RequestStatus" +
                "  from Request_Token r" +
                " left join Sys_Department d on d.DepartmentId = r.DepartmentId" +
                " where 1 = 1 and r.IsDeleted = 0 and r.ApprovedByAdmin='Approved' ";
            sql += " order by r.RequestToken  desc";
            using (var db = DbHelper.GetDBConnection())
            {
                db.Close();
                return db.Query<RequestVM>(sql).ToList();
            }
        }

        //public void ApprovedRequestTokenStore(int id, int deletedby, DateTime deleteddate)
        //{
        //    string sql = " Update Request_Token set RequestStatus='Approved',RequestStatusDate= '" + deleteddate + "'" +
        //    " where TokenId=" + id;

        //    using (var db = DbHelper.GetDBConnection())
        //    {
        //        db.Query(sql);
        //        db.Close();
        //    }
        //}

        public void RejectRequestTokenStore(int id, int deletedby, DateTime deleteddate)
        {
            string sql = " Update Request_Token set RequestStatus='Reject',RequestStatusDate= '" + deleteddate + "'" +
            " where TokenId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                db.Query(sql);
                db.Close();
            }
        }
    }
}
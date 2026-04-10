
﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DataLayer
{
    public class DocumentUpload
    {
        public int ID { get; set; }
        public int DocumentType { get; set; }
        public string ApplicationNo { get; set; }
        public string session { get; set; }
        public string FileName { get; set; }
        public string IPaddress { get; set; }
        public DateTime CreateDate { get; set; }
        public string Msg { get; set; }
        public string DocumentTypeName { get; set; }
        public bool Status { get; set; }
        public string file { get; set; }
        public string hfile { get; set; }
        public int SID { get; set; }
        public DocumentUpload SaveDocument(DocumentUpload ob)
        {
            var IP = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<DocumentUpload>("[sp_DocumentUpload]", new
                {
                    @Action = "Insert",
                    @ID = ob.ID,
                    @DocumentType = ob.DocumentType,
                    @IPaddress = IP,
                    @ApplicationNo = ob.ApplicationNo,
                    @session = ob.session,
                    @FileName = ob.FileName,
                    @SID = ob.SID
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public List<Commn_master> GetCommMaster()
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Commn_master>("select * from TBL_COMMON where Titlecode='AdmissionDoc'").ToList();
                return obj;
            }

        }
       
        public DocumentUploadList DocumentdetailList(int pageIndex1 = 1, int pageSize1 = 25)
        { var app= ClsLanguage.GetCookies("NBApplicationNo");
            DocumentUploadList list = new DocumentUploadList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_DocumentUpload]", new { Action = "view", PageIndex = pageIndex1, pageSize = pageSize1 , @ApplicationNo =app}, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<DocumentUpload>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public DocumentUpload GetDocumentByID(int id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<DocumentUpload>("sp_DocumentUpload", new { Action = "viewbyid", ID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public DocumentUpload DeleteDocumentByID(int id = 0)
        {
            DocumentUpload ob = new DocumentUpload();
           
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<DocumentUpload>("sp_DocumentUpload", new { Action = "delete", ID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
    }
    public class DocumentUploadList
    {
        public List<DocumentUpload> qlist { get; set; }
        public string totalCount { get; set; }
    }
}


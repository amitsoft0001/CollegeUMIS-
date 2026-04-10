using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Student_Admission_Choicesubject
    {
        public string ID { get; set; }
        public int SID { get; set; }
        public string collegeidlist { get; set; }
        public string hounors_subjectidlist { get; set; }
        public string Subsidiary1_subjectidlist { get; set; }
        public string Subsidiary2_subjectidlist { get; set; }
        public string Compulsory1_subjectidlist { get; set; }
        public string Compulsory2_subjectidlist { get; set; }

        public int CollegeID { get; set; }
        public int hounors_subjectid { get; set; }
        [Required(ErrorMessage ="Please Select Subsidiary Subject first")]
        public int Subsidiary1_subjectid { get; set; }
        [Required(ErrorMessage = "Please Select Subsidiary Subject Second")]
        public int Subsidiary2_subjectid { get; set; }
        [Required(ErrorMessage = "Please Select Composition1  Subject")]
        public int Compulsory1_subjectid { get; set; }
       
        public int Compulsory2_subjectid { get; set; }

        public string adddate { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }
        public int sessionid { get; set; }
        public int ModifyBy { get; set; }
        public string IPAddress { get; set; }
        public string modify_date { get; set; }

        public int SubjectID { get; set; }
        public string CollegeName { get; set; }
        public string HonSubName { get; set; }
        public string SubjectIds { get; set; }
        public int Nationality { get; set; }
        public string hid { get; set; }
        public int MinorId { get; set; }
        public int MDCId { get; set; }
        public int AECId { get; set; }
        public int SECId { get; set; }
        public int VACId { get; set; }
        public Student_Admission_Choicesubject savest_choicesubject(Student_Admission_Choicesubject obj1)
        {
          
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Student_Admission_Choicesubject>("[sp_Student_choice_subject]", new
                {
                    @Action = "insert",
                    @SID = obj1.SID,
                    @collegeidlist = obj1.collegeidlist,
                    @hounors_subjectidlist = obj1.hounors_subjectidlist,
                    @Subsidiary1_subjectidlist = obj1.Subsidiary1_subjectidlist,
                    @Subsidiary2_subjectidlist = obj1.Subsidiary2_subjectidlist,
                    @Compulsory1_subjectidlist = obj1.Compulsory1_subjectidlist,
                    @Compulsory2_subjectidlist = obj1.Compulsory2_subjectidlist,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public List<Student_Admission_Choicesubject> viewst_choicesubject(int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Student_Admission_Choicesubject>("[sp_Student_choice_subject]", new
                {
                    @Action = "viewbysid",
                    @SID = SID,
                }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }

        }
        public class ChooseSubjectCopy
        {
            public string SID { get; set; } = string.Empty;
            public int StudentId { get; set; } = 0;
            public int ID { get; set; } = 0;
            public int MajorID { get; set; } = 0;
            public string SubjectIds { get; set; } = string.Empty;
        }
        public Student_Admission_Choicesubject updateSubjectChoiceDetailsCopy(ChooseSubjectCopy ob)
        {
            var IP = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Student_Admission_Choicesubject>("[sp_College_StudentDetailsUpdate]", new
                {
                    @Action = "UpdateSubject",
                    @SID = ob.StudentId,
                    @Id = ob.ID,
                    @SubjectIds = ob.SubjectIds,
                    @MajorId = ob.MajorID,
                    @IPAddress = IP,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public Student_Admission_Choicesubject againfillform(int SID, int sessionid)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Student_Admission_Choicesubject>("[sp_Student_choice_subject]", new
                {
                    @Action = "againfillform",
                    @SID = SID,
                    @sessionid = sessionid
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public Student_Admission_Choicesubject getSubjectChoiceDetail(int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Student_Admission_Choicesubject>("[sp_College_StudentDetailsUpdate]", new
                {
                    @Action = "SubjectDetails",
                    @Id = SID,
                   
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        //updateSubjectChoiceDetails

        public Student_Admission_Choicesubject updateSubjectChoiceDetails(Student_Admission_Choicesubject ob)
        {
            var IP = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Student_Admission_Choicesubject>("[sp_College_StudentDetailsUpdate]", new
                {
                    @Action = "UpdateSubject",
                    @SID = ob.SID,
                    @Id = ob.hid,              
                    @Subsidiary1_subjectid = ob.Subsidiary1_subjectid,
                    @Subsidiary2_subjectid = ob.Subsidiary2_subjectid,
                    @Compulsory1_subjectid = ob.Compulsory1_subjectid,
                    @Compulsory2_subjectid = ob.Compulsory2_subjectid,
                    @ModifyBy = ob.ModifyBy,
                    @IPAddress = IP,
                    @session=ob.sessionid

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }

        public Student_Admission_Choicesubject insertSubjectChooseManualAD(Student_Admission_Choicesubject ob)
        {
            var IP = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Student_Admission_Choicesubject>("[sp_SubjectChooseManualAdmission]", new
                {
                    @Action = "insertSubject",
                    @SID = ob.SID,
                    @Id = (ob.hid==null?"0":ob.hid),
                    @hounors_subjectid = ob.hounors_subjectid,
                    @Subsidiary1_subjectid = ob.Subsidiary1_subjectid,
                    @Subsidiary2_subjectid = ob.Subsidiary2_subjectid,
                    @Compulsory1_subjectid = ob.Compulsory1_subjectid,
                    @Compulsory2_subjectid = ob.Compulsory2_subjectid,
                    @ModifyBy = ob.ModifyBy,
                    @IPAddress = IP,
                    @sessionid = ob.sessionid,
                    @collegeid = ob.CollegeID,

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }

        public Student_Admission_Choicesubject getSubjectChoiceDetailformanual(int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Student_Admission_Choicesubject>("[sp_SubjectChooseManualAdmission]", new
                {
                    @Action = "SubjectDetails",
                    @Id = SID,

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using static System.Net.WebRequestMethods;
/// <summary>
/// </summary>
public class SMSFUN
{

   
    static DataTable dtSMS = new DataTable();
    public SMSFUN()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static void sms_StudentDocumentVerification( string mobileno) // template id 1 for student Registration
    {
        //SMS.Sendget(mobileno, "Dear Student, you have to inform that your documents have been verified.You need to login at student portal with your User Id and password to submit college admission fees. Regard,  Munger University  ");
        // tempalet id : '1207161838712930854
        // template name : doc verfied college
        // Content: Dear {#var#}, you have to inform that your {#var#} have been verified. You need to login at student portal with your User Id and password to submit {#var#} Regards, Munger University 
        string doctype = "documents";
        string doctype1 = "college admission fees";
        SMS.SendgetDLT(mobileno, "Dear Student, you have to inform that your "+ doctype + " have been verified. You need to login at student portal with your User Id and password to submit "+doctype1+" Regards, Munger University ", "1207161838712930854");
    }
    public static void sms_StudentDocumentReject(string mobileno) // template id 1 for student Registration
    {
        // SMS.Sendget(mobileno, "Dear Student, you have to inform that your documents have been Rejected.You Can login at student portal with your User Id and password to find the reason. Regard,  Munger University  ");
        // tempalet id : 1207161857498782555
        // template name : student doc rejected college
        // Content: Dear {#var#}, you have to inform that your {#var#} have been Rejected. for more information login to student portal Regards, Munger University 
        string doctype = "documents";
        string doctype1 = "college admission fees";
        SMS.SendgetDLT(mobileno, "Dear Student, you have to inform that your "+ doctype + " have been Rejected. for more information login to student portal Regards, Munger University ", "1207161857498782555");

    }
    public static void sms_loginotp(string mobileno,string otp) // template id 1 for student Registration
    {
        //string messgage = "Munger, OTP is " + otp + " .Use this to Login verify ";
        if (mobileno == null || mobileno == "")
        {
            return;
        }

        //mobileno = "8562889437";
        // tempalet id : '1207161857492492280
        // template name : one time password2
        // Content: OTP is {#var#}, Verify your Mobile No Regards, Munger University 
        SMS.SendgetDLT(mobileno, "Dear Customer, Your OTP is " + otp + " for Munger University, Munger.Please do not share this OTP.Regards", "1207161729866691748");
        
        // SMS.Sendget(mobileno, messgage);



    }
    public static void Send_OTPverifymobile(string mobileno, string otp) // template id 3 for student passsword send
    {
        // tempalet id : '1207161857492492280
        // template name : one time password2
        // Content: OTP is {#var#}, Verify your Mobile No Regards, Munger University 
        SMS.SendgetDLT(mobileno, "OTP is "+ otp + ", Verify your Mobile No Regards, Munger University", "1207161857492492280");

    }

}
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Website.Models
{
    public class ApiSecurity
    {
        public static bool VaidateUser(string username, string password)
        {

            BL_CollegeLogin obj = new BL_CollegeLogin();
            BL_CollegeLogin objlogin = new BL_CollegeLogin();
            objlogin.CollegeCode = username;
            objlogin.Password = password;
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            Byte[] hashedBytes;
            UTF8Encoding encoder = new UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(objlogin.Password));
            objlogin.U_password = hashedBytes;
                   
            obj = objlogin.Login(objlogin);
               
                    if (!obj.Isactive)
                    { return false;
                    }
            else
                    { return true;
                    }
                   
                    

                
            
           }
    }
}

using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Country
    {
        public Int32 ID { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Country  Code is required")]
        public string ShortName { get; set; }
        public bool Status { get; set; }
        public bool IsActive { get; set; }
        public string Msg { get; set; }

        public int PhoneCode { get; set; }
        public DateTime CreateDate { get; set; }
       
        public List<Country> GetAllCountries(int id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Country>("Select * from tbl_Country   where id=80").ToList();
                return obj;
            }

        }
    }
    public class countries
    {
        public List<Country> countrylist { get; set; }
        public string totalCount { get; set; }

    }
}

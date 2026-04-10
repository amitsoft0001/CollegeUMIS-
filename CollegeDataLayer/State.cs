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
    public class State
    {
        public Int32 stateID { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string StateName { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string CountryName { get; set; }

        public bool Status { get; set; }
        public bool IsActive { get; set; }
        public string Msg { get; set; }

        public static bool activedeactivedatastate(int id)
        {
            throw new NotImplementedException();
        }

        [Required(ErrorMessage = "Country is required")]

        public string CountryID { get; set; }
        public DateTime CreateDate { get; set; }
       
        public List<State> GetStateListByCountryId(string CountryId)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<State>("sp_getstate", new { @CountryId = CountryId }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
          
        }

        
    }
    public class States
    {
        public List<State> statelist { get; set; }
        public string totalCount { get; set; }

    }
}

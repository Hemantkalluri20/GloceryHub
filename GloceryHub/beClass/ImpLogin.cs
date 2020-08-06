using GloceryHub.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GloceryHub.beClass
{
    public class ImpLogin
    {
        public void beLogin(LoginDetailsModel opLoginDetailsModel)
        {
            Int64 resCode = 0;  
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand oCmd = conn.CreateCommand())
                    {
                        oCmd.CommandText = "";
                        oCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        oCmd.Parameters.AddWithValue("@emailID", opLoginDetailsModel.emailID);
                        oCmd.Parameters.AddWithValue("@password", opLoginDetailsModel.password);
                        var code = oCmd.Parameters.Add("@opcode", SqlDbType.BigInt);
                        code.Direction = ParameterDirection.Output;
                        oCmd.ExecuteNonQuery();
                        resCode = Convert.ToInt64(code.Value);
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

            

    }
}
using GloceryHub.Models;
using HireCraft.HM_APIService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GloceryHub.beClass
{
    public class ImpSignUp
    {
        public APIResponses beSignUp(SignUpDetails opSignUpDetails)
        {
            APIResponses response = new APIResponses();
            Int64 resCode = 0;
            try
            {
                // saving data
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand oCmd = conn.CreateCommand())
                    {
                        oCmd.CommandText = "usp_signup";
                        oCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        oCmd.Parameters.AddWithValue("@firstName", opSignUpDetails.firstName);
                        oCmd.Parameters.AddWithValue("@shopName", opSignUpDetails.shopName);
                        oCmd.Parameters.AddWithValue("@lastName", opSignUpDetails.lastName);
                        oCmd.Parameters.AddWithValue("@roleID", opSignUpDetails.roleID);
                        oCmd.Parameters.AddWithValue("@mobileNo", opSignUpDetails.mobileNo);
                        oCmd.Parameters.AddWithValue("@shopAddress", opSignUpDetails.shopAddress);
                        oCmd.Parameters.AddWithValue("@shopTelephone", opSignUpDetails.shopTelephone);
                        oCmd.Parameters.AddWithValue("@state", opSignUpDetails.state);
                        oCmd.Parameters.AddWithValue("@emailID", opSignUpDetails.emailID);
                        var code = oCmd.Parameters.Add("@opcode", SqlDbType.BigInt);
                        code.Direction = ParameterDirection.Output;
                        var msg = oCmd.Parameters.Add("@msg", SqlDbType.NVarChar,-1);
                        msg.Direction = ParameterDirection.Output;
                        oCmd.ExecuteNonQuery();
                        resCode = Convert.ToInt64(code.Value);
                        response.message = Convert.ToString(msg.Value);
                        response.statusCode = resCode;
                        //rescode and msg are returning from the sp where 201 stands for successfully registered and 200 stands for duplicate entry and 500 for internal server error
                    }
                }
                
            }
            catch(Exception ex)
            {
                response.message = "Something went wrong";
                response.statusCode = 500;
                Log.LogData("Error at beSignUp "+ex.ToString(), Log.Status.Error);
            }
            return response;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FS.Models {

    public class database {
        private SqlConnection con = new SqlConnection("Server =DESKTOP-MB4S2GC\\TOANDANG; Database = FeedbackSystem ; Trusted_Connection=True;MultipleActiveResultSets=True");

        public int logincheck_admin(AppUser p) {
            SqlCommand com = new SqlCommand("ad_login", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@username", p.UserName);
            com.Parameters.AddWithValue("@password", p.Password);
            SqlParameter oblogin = new SqlParameter();
            oblogin.ParameterName = "@isvalid";
            oblogin.SqlDbType = SqlDbType.Bit;
            oblogin.Direction = ParameterDirection.Output;
            com.Parameters.Add(oblogin);
            con.Open();
            com.ExecuteNonQuery();
            int res = Convert.ToInt32(oblogin.Value);
            con.Close();
            return res;
        }

        public int logincheck_trainer(AppUser p) {
            SqlCommand com = new SqlCommand("trainer_login", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@username", p.UserName);
            com.Parameters.AddWithValue("@password", p.Password);
            SqlParameter oblogin = new SqlParameter();
            oblogin.ParameterName = "@isvalid";
            oblogin.SqlDbType = SqlDbType.Bit;
            oblogin.Direction = ParameterDirection.Output;
            com.Parameters.Add(oblogin);
            con.Open();
            com.ExecuteNonQuery();
            int res = Convert.ToInt32(oblogin.Value);
            con.Close();
            return res;
        }

        public int logincheck_trainee(AppUser p) {
            SqlCommand com = new SqlCommand("trainee_login", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@username", p.UserName);
            com.Parameters.AddWithValue("@password", p.Password);
            SqlParameter oblogin = new SqlParameter();
            oblogin.ParameterName = "@isvalid";
            oblogin.SqlDbType = SqlDbType.Bit;
            oblogin.Direction = ParameterDirection.Output;
            com.Parameters.Add(oblogin);
            con.Open();
            com.ExecuteNonQuery();
            int res = Convert.ToInt32(oblogin.Value);
            con.Close();
            return res;
        }

        public int logincheck(AppUser p) {
            SqlCommand com = new SqlCommand("login", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@username", p.UserName);
            com.Parameters.AddWithValue("@role", p.Role);
            SqlParameter oblogin = new SqlParameter();
            oblogin.ParameterName = "@isvalid";
            oblogin.SqlDbType = SqlDbType.Bit;
            oblogin.Direction = ParameterDirection.Output;
            com.Parameters.Add(oblogin);
            con.Open();
            com.ExecuteNonQuery();
            int res = Convert.ToInt32(oblogin.Value);
            con.Close();
            return res;
        }
    }
}
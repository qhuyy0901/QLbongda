using DAL;
using System.Data;
using System.Data.SqlClient;

namespace BUS
{
    public class BUS_Login
    {
        private SqlConnection conn = new SqlConnection(
            @"Server=HUY\MSSQLSEVER;Initial Catalog=QuanLySanBong;Integrated Security=True");

        public bool CheckLogin(string username, string password)
        {
            string sql = "SELECT COUNT(*) FROM Users WHERE UserName = @user AND Password = @pass";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@user", username);
            cmd.Parameters.AddWithValue("@pass", password);

            conn.Open();
            int result = (int)cmd.ExecuteScalar();
            conn.Close();

            return result > 0;
        }
    }
}

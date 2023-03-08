using ClassLibrary1.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.BAL
{
    public class Customer_Register
    {
        public List<Customer_DAL> CustomerList = new List<Customer_DAL>();
        public string? errorMsg;
        public string? SuccessMsg;
        public void OnPost(string fname, string lname, string email, string password, DateTime bdate)
        {
            try
            {
                String ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_ShoeCorner;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    String Query = "insert into tb_Customer(FirstName,LastName,Email,Password,BirthDate,CreatedOn) values(@fname,@lname,@email,@password,@bdate,@createdon);";
                    using (SqlCommand cmd = new SqlCommand(Query, connection))
                    {
                        cmd.Parameters.AddWithValue("@fname", fname);
                        cmd.Parameters.AddWithValue("@lname", lname);
                        cmd.Parameters.AddWithValue("@email", email);

                        cmd.Parameters.AddWithValue("@password", EncodePasswordToBase64(password));
                        cmd.Parameters.AddWithValue("@bdate", bdate);
                        cmd.Parameters.AddWithValue("@createdon", DateTime.Now);
                        
                        cmd.ExecuteNonQuery();


                    }
                    connection.Close();
                    SuccessMsg = "User Addes Successfully";
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.ToString();
            }
        }

        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        
    }
}

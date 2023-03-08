using ClassLibrary1.DAL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.BAL
{
    public class Login_BAL
    {
        //private readonly IHttpContextAccessor httpContextAccessor;

        //public Login_BAL(IHttpContextAccessor httpContexts)
        //{
        //    httpContextAccessor = httpContexts;
        //}
        String successMsg;
        public List<Customer_DAL> customerData = new List<Customer_DAL>();


        public String OnGet(string email, string password)
        {
                
            try
            {
                string email2 = email;
                string password2 = password;
                //string myDb1ConnectionString = _configuration.GetConnectionString("DbString");
                String ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_ShoeCorner;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    //String Query = "select tb_Product.Product_id,tb_Product.Product_Name,tb_Product.Product_Price,tb_Product.Product_img1, tb_Cart.Quantity from tb_Product join tb_Cart on tb_Product.Product_id=tb_Cart.Product_id;";
                    String Query = "select * from tb_Customer where Email = '"+email+"';";
                    using (SqlCommand cmd = new SqlCommand(Query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductDAL classDAL = new ProductDAL();
                                Customer_DAL customer_DAL = new Customer_DAL();
                                customer_DAL.Customer_id = reader.GetInt32(0);
                                customer_DAL.FirstName = reader.GetString(1);
                                customer_DAL.LastName = reader.GetString(2);
                                customer_DAL.Email = reader.GetString(3);
                                string pass = reader.GetString(4);
                                customer_DAL.BirthDate = reader.GetDateTime(5);

                               //httpContextAccessor.HttpContext.Session.SetInt32("User_id",customer_DAL.Customer_id);
                               //httpContextAccessor.HttpContext.Session.SetString("User_name", customer_DAL.FirstName);

                                if (password2 == DecodeFrom64(pass))
                                {
                                    successMsg = "Log in successfully";
                                }
                                else
                                {
                                    successMsg = "Login Failed";
                                }
                            }
                        }
                    }

                    
                    connection.Close();
                    return successMsg;
                }
            }
            catch (Exception ex)
            {
                successMsg = ex.ToString();
                return successMsg;
            }
        }

        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

    }
}

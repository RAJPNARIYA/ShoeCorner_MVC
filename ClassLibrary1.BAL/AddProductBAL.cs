using ClassLibrary1.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeCorner.BAL
{
    public class AddProductBAL
    {
        public List<ProductDAL> CustomerList = new List<ProductDAL>();
        public string? SuccessMsg;
        public string? errorMsg;


        public void AddProduct(string pname, string price, string desc, string img1, string img2, string gender, string cat_id)
        {
            try
            {
                String ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_ShoeCorner;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                 
                    String Query = "insert into tb_Product(Product_Name,Product_Price,Product_Desc,Product_img1,Product_img2,Gender,Category_id) values(@p_name,@p_price,@p_desc,@p_img1,@p_img2,@gender,@cat_id);";
                    using (SqlCommand cmd = new SqlCommand(Query, connection))
                    {
                        cmd.Parameters.AddWithValue("@p_name", pname);
                        cmd.Parameters.AddWithValue("@p_price", price);
                        cmd.Parameters.AddWithValue("@desc", desc);

                        cmd.Parameters.AddWithValue("@p_img1", img1);
                        cmd.Parameters.AddWithValue("@p_img2", img2);
                        if(gender == "Men")
                        {
                            cmd.Parameters.AddWithValue("@gender", 1);
                        }
                        else if (gender == "Women")
                        {
                            cmd.Parameters.AddWithValue("@gender", 2);
                        }

                        if(cat_id == "Men Casual")
                        {
                            cmd.Parameters.AddWithValue("@cat_id", 3);
                        }
                        else if (cat_id == "Men Sport")
                        {
                            cmd.Parameters.AddWithValue("@cat_id", 4);
                        }
                        else if (cat_id == "Men Formal")
                        {
                            cmd.Parameters.AddWithValue("@cat_id", 5);
                        }
                        else if (cat_id == "Women Heels")
                        {
                            cmd.Parameters.AddWithValue("@cat_id", 6);
                        }
                        else if (cat_id == "Women Slider")
                        {
                            cmd.Parameters.AddWithValue("@cat_id", 7);
                        }
                        else if (cat_id == "Women Sport")
                        {
                            cmd.Parameters.AddWithValue("@cat_id", 8);
                        }
                        
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
    }
}

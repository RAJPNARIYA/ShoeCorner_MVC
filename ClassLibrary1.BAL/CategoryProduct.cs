using ClassLibrary1.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.BAL
{
	public class CategoryProduct
	{
        public List<ProductDAL> ProductList = new List<ProductDAL>();
        public string? errorMsg;

        public void OnGet(int id,string ConnectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string Query = "SELECT * FROM [db_ShoeCorner].[dbo].[tb_Product] where Category_id="+id+"or Gender="+id;
                    using (SqlCommand cmd = new SqlCommand(Query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductDAL product_List = new ProductDAL();
                                product_List.Product_id = "" + reader.GetInt32(0);
                                product_List.Product_Name = reader.GetString(1);
                                product_List.Product_Price = "" + reader.GetInt32(2);
                                product_List.Product_Desc = "" + reader.GetString(3);
                                product_List.Product_img1 = reader.GetString(4);
                                product_List.Product_img2 = reader.GetString(5);
                                product_List.Gender = reader.GetString(6);
                                product_List.Category_id = "" + reader.GetInt32(7);

                                ProductList.Add(product_List);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.ToString();
            }
        }
    }
}


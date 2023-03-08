using ClassLibrary1.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ClassLibrary1.BAL
{
	public class ProductDetails
	{
        public List<ProductDAL> productList = new List<ProductDAL>();
        public string? errorMsg;

        public void OnGet(int id,string ConnectionString)
        {
            try
            {   
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string Query = "SELECT * FROM [db_ShoeCorner].[dbo].[tb_Product] where Product_id= "+id;
                    using (SqlCommand cmd = new SqlCommand(Query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductDAL product_item = new ProductDAL();
                                product_item.Product_id = "" + reader.GetInt32(0);
                                product_item.Product_Name = reader.GetString(1);
                                product_item.Product_Price = "" + reader.GetInt32(2);
                                product_item.Product_Desc = "" + reader.GetString(3);
                                product_item.Product_img1 = reader.GetString(4);
                                product_item.Product_img2 = reader.GetString(5);
                                product_item.Gender = reader.GetString(6);
                                product_item.Category_id ="" + reader.GetInt32(7);

                                productList.Add(product_item);
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

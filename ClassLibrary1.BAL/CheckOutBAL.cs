using ClassLibrary1.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.BAL
{
    public class CheckOutBAL
    {
        public List<CartDAL> CheckOutList = new List<CartDAL>();
        public string? errorMsg;
        public void OnGet(string ConnectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    
                    string Query = "select Count(tb_Cart.Product_id) As Qua, Sum(tb_Cart.Quantity) as Total, tb_Product.Product_id,tb_Product.Product_Name,tb_Product.Product_Price,tb_Product.Product_img1 from tb_Product join tb_Cart on tb_Product.Product_id=tb_Cart.Product_id Group by tb_Cart.Product_id,tb_Product.Product_id,tb_Product.Product_Name,tb_Product.Product_Price,tb_Product.Product_img1;";
                    
                    using (SqlCommand cmd = new SqlCommand(Query, connection))
                    {

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CartDAL Cart_item = new CartDAL();
                                Cart_item.Quantity = "" + reader.GetInt32(0);
                                Cart_item.Total_Quantity = reader.GetInt32(1);
                                Cart_item.Product_id = "" + reader.GetInt32(2);
                                Cart_item.Product_Name = reader.GetString(3);
                                Cart_item.Product_Price = "" + reader.GetInt32(4);
                                Cart_item.Product_img = reader.GetString(5);
                                Cart_item.Total_Price = Convert.ToInt32(Cart_item.Total_Quantity) * Convert.ToInt32(Cart_item.Product_Price);

                                CheckOutList.Add(Cart_item);
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

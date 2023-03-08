using ClassLibrary1.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace ClassLibrary1.BAL
{
    public class Product
    {

        public List<ProductDAL> productList = new List<ProductDAL>();
        public string? errorMsg;
        public List<Slider_DAL> SliderList = new List<Slider_DAL>();

        public void OnGet(string ConnectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string Query = "SELECT TOP (8) * FROM [db_ShoeCorner].[dbo].[tb_Product]";
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

        public void GetSlider(string ConnectionString)
        {
            try
            {
                //string myDb1ConnectionString = _configuration.GetConnectionString("DbString");
                //String ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_ShoeCorner;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    String slider_query = "select * from [dbo].[tb_Slider]";
                    using (SqlCommand cmd = new SqlCommand(slider_query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Slider_DAL sliderDetails = new Slider_DAL();
                                sliderDetails.Slider_id = "" + reader.GetInt32(0);
                                sliderDetails.Slider_Name = reader.GetString(1);
                                sliderDetails.Slider_img = reader.GetString(2);
                                sliderDetails.Slider_line1 = reader.GetString(3);
                                sliderDetails.Slider_line2 = reader.GetString(4);

                                SliderList.Add(sliderDetails);
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


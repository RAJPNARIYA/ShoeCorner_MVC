using ClassLibrary1.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.BAL
{
    public class Brand
    {
        Brand_DAL brand_item= new Brand_DAL();
        public List<Brand_DAL> BrandList = new List<Brand_DAL>();
        public String errorMsg;

        public void OnGet()
        {
            try
            {

                String ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_ShoeCorner;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    String Query = "SELECT * FROM [db_ShoeCorner].[dbo].[tb_Brand]";
                    using (SqlCommand cmd = new SqlCommand(Query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                brand_item.Brand_id = "" + reader.GetInt32(0);
                                brand_item.Brand_Name = reader.GetString(1);
                                brand_item.Brand_img = "" + reader.GetString(2);


                                BrandList.Add(brand_item);
                            }
                        }
                    }
                    connection.Close();

                }
            }
            catch (Exception e)
            {
                errorMsg = e.ToString();

            }
        }
    }
}

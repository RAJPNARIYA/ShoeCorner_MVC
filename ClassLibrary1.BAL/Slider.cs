using ClassLibrary1.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.BAL
{
	public class Slider
	{
        public List<Slider_DAL> SliderList = new List<Slider_DAL>();
        public string? errorMsg;
        public void OnGet(string ConnectionString)
        {
            try
            {
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


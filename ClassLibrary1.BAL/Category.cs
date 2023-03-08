using ClassLibrary1.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.BAL
{
    public class Category
    {
        public List<Category_DAL> CategoryList = new List<Category_DAL>();
        public string? errorMsg;

        public void OnGet(string ConnectionString)
        {
            try {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string Query = "select tb_Category.Category_id, tb_Category.Category_Name,tb_Category.Category_img  from tb_Category join tb_CategoryMapping on tb_Category.Category_id=tb_CategoryMapping.Category_id;";
                    using (SqlCommand cmd = new SqlCommand(Query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Category_DAL category_item = new Category_DAL();
                                category_item.Category_id = "" + reader.GetInt32(0);
                                category_item.Category_Name = reader.GetString(1);
                                category_item.Category_img = "" + reader.GetString(2);
                                

                                CategoryList.Add(category_item);
                            }
                        }
                    }
                    connection.Close();
                    
                }
            }
            catch(Exception e) {
                errorMsg = e.ToString();
                
            }
        }
    }
}

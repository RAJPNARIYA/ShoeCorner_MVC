using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.DAL
{
    public class CartDAL
    {
        public String Cart_id { get; set; }
        public String Product_id { get; set; }
        public String Quantity { get; set; }

        public String Product_Name { get; set; }

        public String Product_img { get; set; }

        public String Product_Price { get; set; }

        public int Qua { get; set; }

        public int Total_Price { get; set; }

        public int Total_Quantity { get; set; }

    }
}

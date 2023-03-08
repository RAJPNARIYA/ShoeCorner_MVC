namespace ClassLibrary1.DAL
{
    public class ProductDAL
    {
        public String Product_id { get; set; }

        public String Product_Name { get; set; }

        public String Product_Desc { get; set; }

        public String Product_img1 { get; set; }

        public String Product_img2 { get; set; }

        public String Product_Price { get; set; }

        public String Category_id { get; set; }

        public String Gender { get; set; }


        public String FirstName { get; set; }
        public List<ProductDAL> productList { get; set; }
    }
}
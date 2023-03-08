using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication8.Models;
using ClassLibrary1.BAL;
using ClassLibrary1.DAL;
using Microsoft.Extensions.Configuration;
using WebApplication8.Controllers;
using System.Linq;
using System.IO.Pipelines;
using System.Net.NetworkInformation;
using System.Xml.Linq;
using System;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using ShoeCorner.BAL;
using System.Reflection;

namespace WebApplication8.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        Product Data = new Product();
        Men men=new Men();
        Female female=new Female(); 
        Cart cart=new Cart();
        ProductDetails productDetails=new ProductDetails();
        Slider slider = new Slider();
        Category category = new Category(); 
        CategoryProduct categoryProduct=new CategoryProduct();
        CartDAL cartDAL=new CartDAL();  
        Customer_Register customerRegister=new Customer_Register();
        Customer_Register customer_Register = new Customer_Register();
        Login_BAL login_BAL = new Login_BAL();

        public ActionResult Index()
        {
            string ConnectionString = _configuration.GetConnectionString("DbString");
            Data.OnGet(ConnectionString);
            Data.GetSlider(ConnectionString);
            slider.OnGet(ConnectionString);
            return View(Data.productList);
        }

        

        
        public ActionResult Men()
        {
            string ConnectionString = _configuration.GetConnectionString("DbString");
            men.OnGet(ConnectionString);
            return View(men.MenProduct);
        }

        public ActionResult Female()
        {
            string ConnectionString = _configuration.GetConnectionString("DbString");
            female.OnGet(ConnectionString);
            return View(female.FemaleProduct);
        }

        public ActionResult Cart(int Qua, int id, int price,int size)
        {
            string ConnectionString = _configuration.GetConnectionString("DbString");
            cart.OnGet(ConnectionString);
            cart.OnPost(Qua, id, price,size, ConnectionString);
            return View(cart.CartList);
        }

        public ActionResult CartUpdate(int id, int qua)
        {
            string ConnectionString = _configuration.GetConnectionString("DbString");
            cart.OnPut(id, qua, ConnectionString);
            return Redirect("/Home/Cart");
        }

        public ActionResult CheckOut()
        {
            string ConnectionString = _configuration.GetConnectionString("DbString");
            CheckOutBAL checkOutBAL = new CheckOutBAL();
            checkOutBAL.OnGet(ConnectionString);
            return View(checkOutBAL.CheckOutList);
        }

        public ActionResult Category()
        {
            string ConnectionString = _configuration.GetConnectionString("DbString");
            category.OnGet(ConnectionString);
            return View(category.CategoryList);
        }

        public ActionResult CategoryProduct(int id)
        {
            string ConnectionString = _configuration.GetConnectionString("DbString");
            categoryProduct.OnGet(id,ConnectionString);
            return View(categoryProduct.ProductList);
        }

        public ActionResult ProductDetails(int id)
        {
            string ConnectionString = _configuration.GetConnectionString("DbString");
            productDetails.OnGet(id,ConnectionString);
            return View(productDetails.productList);
        }

        public ActionResult Login()
        {
            return View();
        }



        public ActionResult LoginCheck(string email, string password)
        {
            string successMsg = "";
            string password2 = password;
            string email2 = email;
            try
            {
                //string myDb1ConnectionString = _configuration.GetConnectionString("DbString");
                string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_ShoeCorner;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    //String Query = "select tb_Product.Product_id,tb_Product.Product_Name,tb_Product.Product_Price,tb_Product.Product_img1, tb_Cart.Quantity from tb_Product join tb_Cart on tb_Product.Product_id=tb_Cart.Product_id;";
                    string Query = "select * from tb_Customer where Email = '" + email + "';";
                    using (SqlCommand cmd = new SqlCommand(Query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductDAL classDAL = new ProductDAL();
                                Customer_DAL customer_DAL = new Customer_DAL();
                                customer_DAL.Customer_id = reader.GetInt32(0);
                                customer_DAL.FirstName = reader.GetString(1);
                                customer_DAL.LastName = reader.GetString(2);
                                customer_DAL.Email = reader.GetString(3);
                                string pass = reader.GetString(4);
                                customer_DAL.BirthDate = reader.GetDateTime(5);



                                if (password2 == DecodeFrom64(pass))
                                {
                                    _httpContextAccessor.HttpContext.Session.SetInt32("User_id", customer_DAL.Customer_id);
                                    _httpContextAccessor.HttpContext.Session.SetString("User_name", customer_DAL.FirstName);

                                    successMsg = "Log in successfully";
                                }
                                else
                                {
                                    successMsg = "Login Failed";
                                }


                            }
                        }
                    }


                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                successMsg = ex.ToString();
            }
            if (successMsg == "Log in successfully")
            {
                TempData["SuccessMsg"] = "Log in successfully";
                return Redirect("Index");
            }
            else
            {
                TempData["ErrorMsg"] = "User name or password incorrect";
                return Redirect("/Home/Login");
            }
        }


        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public ActionResult Register()
        {
            Customer_Register customer_Register = new Customer_Register();
            string? msg = customer_Register.SuccessMsg;
            ViewBag.Message = msg;
            return View();
        }


        public ActionResult LogOut()
        {
            //string? msg = "LogOut Done";
            _httpContextAccessor.HttpContext.Session.Clear();
            return Redirect("Index");
        }

        [HttpPost]
        public ActionResult AddData(string fname, string lname, string email, string password, DateTime bdate)
        {
            customerRegister.OnPost(fname, lname, email, password, bdate);
            string? msg = customer_Register.SuccessMsg;
            ViewBag.Message = msg;


            return Redirect("/Home/Register");
            
        }

        public ActionResult ProductAddPage(string pname, string price, string desc, string img1, string img2, string genders, string cat_id)
        {
            try
            {
                string ConnectionString = _configuration.GetConnectionString("DbString");
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string Query = "insert into tb_Product(Product_Name,Product_Price,Product_Desc,Product_img1,Product_img2,Gender,Category_id) values(@p_name,@p_price,@p_desc,@p_img1,@p_img2,@gender,@cat_id);";
                    using (SqlCommand cmd = new SqlCommand(Query, connection))
                    {
                        cmd.Parameters.AddWithValue("@p_name", pname);
                        cmd.Parameters.AddWithValue("@p_price", price);
                        cmd.Parameters.AddWithValue("@p_desc", desc);

                        cmd.Parameters.AddWithValue("@p_img1", img1);
                        cmd.Parameters.AddWithValue("@p_img2", img2);
                        if (genders == "Men")
                        {
                            cmd.Parameters.AddWithValue("@gender", "1");
                        }
                        else if (genders == "Women")
                        {
                            cmd.Parameters.AddWithValue("@gender", "2");
                        }

                        if (cat_id == "Men Casual")
                        {
                            cmd.Parameters.AddWithValue("@cat_id", "3");
                        }
                        else if (cat_id == "Men Sport")
                        {
                            cmd.Parameters.AddWithValue("@cat_id", "4");
                        }
                        else if (cat_id == "Men Formal")
                        {
                            cmd.Parameters.AddWithValue("@cat_id", "5");
                        }
                        else if (cat_id == "Women Heels")
                        {
                            cmd.Parameters.AddWithValue("@cat_id", "6");
                        }
                        else if (cat_id == "Women Slider")
                        {
                            cmd.Parameters.AddWithValue("@cat_id", "7");
                        }
                        else if (cat_id == "Women Sport")
                        {
                            cmd.Parameters.AddWithValue("@cat_id", "8");
                        }

                        cmd.ExecuteNonQuery();


                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                ex.ToString(); 
            }
            return Redirect("Index");
        }


        public ActionResult AddsProduct()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
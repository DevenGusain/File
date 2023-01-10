using FMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlTypes;
using System.Data;

namespace FMS.Controllers
{
    public class HomeController : Controller
    {       
        string _config;   
        private readonly ILogger<HomeController> _logger;
        
        public object Session { get; private set; }

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration.GetSection("ConnectionStrings").GetChildren().FirstOrDefault(config => config.Key=="DBConnection").Value;
            
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            using (SqlConnection conn = new SqlConnection(_config))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("select * from master_table;", conn);
                SqlDataAdapter da = new SqlDataAdapter(comm);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                DataSet ds = new DataSet("user");
                da.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                return View(dt);
            }          
        }




        [HttpGet]
        public IActionResult Login()
        {
            fms_users users = new fms_users();
            return View(users);
        }

        [HttpPost]
        public IActionResult Login(fms_users users) 
        {
            using (SqlConnection conn = new SqlConnection(_config))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("select * from fms_users;", conn);
                SqlDataAdapter da = new SqlDataAdapter(comm);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                DataSet ds = new DataSet("user");
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count >= 0)
                {
                    string db_pass = GetMD5(users.Password).ToString();
                    if (db_pass == ds.Tables[0].Rows[0]["password"].ToString())
                    {
                        ViewBag.status = 1;
                        ViewBag.msg = "your login is successful";                        
                        HttpContext.Session.SetString("username", users.Username.ToUpper().ToString());
                        TempData["username"] =users.Username;
                        ViewBag.username = users.Username;
                        return RedirectToAction("success", "File");
                    }
                    else
                    {
                        ViewBag.status = 0;
                        ViewBag.msg = "unsuccessful attempt";
                    }
                }
                conn.Close();
            }
            return View();
        }
                      

        


        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        public IActionResult Privacy()
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
using FMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlTypes;
using System.Data;
using MyTested.AspNetCore.Mvc.Utilities.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol.Plugins;
using Microsoft.AspNetCore.Authorization;



public class FileController : Controller
    {
        public string _config;

        private readonly ILogger<FileController> _logger;
     public FileController(ILogger<FileController> logger, IConfiguration configuration)
     {
        _logger = logger;  
        _config = configuration.GetSection("ConnectionStrings").GetChildren().FirstOrDefault(config => config.Key == "DBConnection").Value;
     }


    [HttpGet]
    
    public IActionResult success()
    {
        if (HttpContext.Session.GetString("username") != null)
        {
            //ViewBag.user = HttpContext.Session.GetString("username").ToUpper().ToString();

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
        else
            return RedirectToAction("Index", "Home");

    }


    [HttpGet]
    public IActionResult AddFile()
    {
       droplist dl = new droplist();

       try
        {
            using (SqlConnection conn = new SqlConnection(_config))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("select * from structure;", conn);
                SqlDataAdapter da = new SqlDataAdapter(comm);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                da.Fill(ds);

                using (SqlDataReader sdr = comm.ExecuteReader()) { 
                
                while (sdr.Read())
                    {
                        if (!sdr.IsDBNull("location")) 
                        dl.loc_list.Add(new location_list { Location = sdr["location"].ToString()});

                        if (!sdr.IsDBNull("section"))
                            dl.sec_list.Add(new section_list { Section = sdr["section"].ToString() });

                        if (!sdr.IsDBNull("almirah_No"))
                            dl.alm_list.Add(new almirah_list { Almirah_No = sdr["almirah_No"].ToString() });

                        if (!sdr.IsDBNull("part_No"))
                            dl.par_list.Add(new part_list { Part_No = sdr["part_No"].ToString() });

                        if (!sdr.IsDBNull("row_No"))
                            dl.rows_list.Add(new row_list { Row_No = sdr["row_No"].ToString() });

                        if (!sdr.IsDBNull("col"))
                            dl.cols_list.Add(new col_list { Col_No = sdr["col"].ToString() });


                        //sec_list.Add(new section_list { Section = sdr["section"].ToString() });
                        //alm_list.Add(new almirah_list { Almirah_No = sdr["almirah_No"].ToString() });
                        //par_list.Add(new part_list { Part_No = sdr["part_No"].ToString() });
                        //rows_list.Add(new row_list { Row_No = sdr["row_No"].ToString() });
                        //cols_list.Add(new col_list { Col_No = sdr["col"].ToString() });
                    }
                }                   
            }
        }
        catch(Exception ex)
        {
            
        }
        return View(dl);
    }

    

    [HttpPost]
    public IActionResult AddFile(masterTable mt) 
    { 
        return View(); 
    }

}



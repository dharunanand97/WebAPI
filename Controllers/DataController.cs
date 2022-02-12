using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI2;
using WebAPI2.Data;
using Newtonsoft.Json;

namespace WebAPI2.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class DataController : ControllerBase
        {           
            // GET api/<DataController>/5
            [HttpGet("{table_name}")]
            public string Getdata(string table_name)
            {
                //set the connection string
                string connString = @"Server=localhost;Database=ProdCustDB;Trusted_Connection=True;";
              
                try
                {
                    //sql connection object
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        Console.WriteLine("log1");

                        //retrieve the SQL Server instance version
                        string query = @"USE ProdCustDB;SELECT * FROM " + table_name + ";";
                        
                        //define the SqlCommand object
                        SqlCommand cmd1 = new SqlCommand(query, conn);
                        //cmd1.Parameters.AddWithValue("@param1", table_name1);
                        Console.WriteLine("log2");
                        //open connection
                        conn.Open();
                        Console.WriteLine("log3");
                        //execute the SQLCommand
                        SqlDataReader dr1 = cmd1.ExecuteReader();
                        Console.WriteLine("log4");
                        var details = new List<Dictionary<string, object>>();
                        Console.WriteLine(Environment.NewLine + "Retrieving data from database..." + Environment.NewLine);
                        Console.WriteLine("Retrieved records:");
                      
                        if (dr1.HasRows)
                        {
                            //return "Inside has rows";

                            while (dr1.Read())
                            {
                                
                                var dict = new Dictionary<string, object>();

                                for (int i = 0; i < dr1.FieldCount; i++)
                                {
                                dict.Add(dr1.GetName(i), dr1.IsDBNull(i) ? null : dr1.GetValue(i));
                                }

                                details.Add(dict);

                            }
                            dr1.Close();

                            //close connection
                            conn.Close();
                            string jss = JsonConvert.SerializeObject(details);
                            return jss;
                        }
                        else
                        {
                            Console.WriteLine("No table found.");
                            return "NUll1";
                        }

                        //close data reader

                    }
                }
                catch (Exception ex)
                {
                    //display error message
                    Console.WriteLine("Exception: " + ex.Message);
                    return (string)ex.Message;
                }

                return "NULL3";
            }

           
            // POST api/<DataController>
            [HttpPost]
            public void Post([FromBody] string value)
            {
            }

            // PUT api/<DataController>/5
            [HttpPut("{id}")]
            public void Put(int id, [FromBody] string value)
            {
            }

            // DELETE api/<DataController>/5
            [HttpDelete("{id}")]
            public void Delete(int id)
            {
            }
        }
}


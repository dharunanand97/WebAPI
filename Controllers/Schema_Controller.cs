#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI2;
using WebAPI2.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
namespace WebAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Schema_Controller : ControllerBase
    {
        // GET: api/<Schema_Controller>
        [HttpGet("{table_name}")]
        public string[] GetSchema(string table_name)
        {
            //set the connection string
            string connString = @"Server=localhost;Database=ProdCustDB;Trusted_Connection=True;";
            //string table_name1 = "ProductType";
            String[] str1 = { "NULL" };
            try
            {
                //sql connection object
                using (SqlConnection conn = new SqlConnection(connString))
                {

                    //retrieve the SQL Server instance version
                    string query = @"SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME= @table_name;";

                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@table_name", table_name);

                    //open connection
                    conn.Open();

                    //execute the SQLCommand
                    SqlDataReader dr = cmd.ExecuteReader();

                    Console.WriteLine(Environment.NewLine + "Schema Controller" + Environment.NewLine);
                    Console.WriteLine("Get Schema");
                    var obj1 = new List<Schema>{ };
                    
                    List<string> list = new List<string>();
                    //check if there are records
                    if (dr.HasRows)
                    {
                        //return "Inside has rows";

                        while (dr.Read())
                        {                               
                            list.Add((string)dr["COLUMN_NAME"]);
                                       
                        }
                        dr.Close();

                        //close connection
                        conn.Close();
                        string jsonData = JsonConvert.SerializeObject(obj1);
                        String[] str = list.ToArray();
                        
                        return str;
                    }
                    else
                    {
                        Console.WriteLine("No table found.");
                        return str1;
                    }

                    //close data reader

                }
            }
            catch (Exception ex)
            {
                //display error message
                Console.WriteLine("Exception: " + ex.Message);
                return str1;
            }
            return str1;
        }

        [HttpGet()]
        public string[] GetTables()
            {
            //set the connection string
            string connString = @"Server=localhost;Database=ProdCustDB;Trusted_Connection=True;";
            //string table_name1 = "ProductType";
            String[] str1 = { "NULL" };
            try
            {
                //sql connection object
                using (SqlConnection conn = new SqlConnection(connString))
                {

                    //retrieve the SQL Server instance version
                    string query = @"USE PRODCUSTDB;SELECT * FROM INFORMATION_SCHEMA.TABLES ;";

                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);
                    //cmd.Parameters.AddWithValue("@table_name", table_name);

                    //open connection
                    conn.Open();

                    //execute the SQLCommand
                    SqlDataReader dr = cmd.ExecuteReader();

                    Console.WriteLine(Environment.NewLine + "Schema Controller" + Environment.NewLine);
                    Console.WriteLine("Get tables");
                    var obj1 = new List<Schema> { };

                    List<string> list = new List<string>();
                    //check if there are records
                    if (dr.HasRows)
                    {
                        //return "Inside has rows";

                        while (dr.Read())
                        {
                            list.Add((string)dr["TABLE_NAME"]);

                        }
                        dr.Close();

                        //close connection
                        conn.Close();
                        string jsonData = JsonConvert.SerializeObject(obj1);
                        String[] str = list.ToArray();
                        return str;
                    }
                    else
                    {
                        Console.WriteLine("No table found.");
                        return str1;
                    }

                    //close data reader

                }
            }
            catch (Exception ex)
            {
                //display error message
                Console.WriteLine("Exception: " + ex.Message);
                return str1;
            }
            return str1;
        }




        // POST api/<Schema_Controller>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/<Schema_Controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Schema_Controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

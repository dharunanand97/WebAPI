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
using Newtonsoft.Json.Linq;
using System.Text.Json;
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
                        Console.WriteLine(Environment.NewLine + "Get Data" + Environment.NewLine);
                        Console.WriteLine("executing:");
                      
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
        public void Post([FromBody] object value)

        {
            string connString = @"Server=localhost;Database=ProdCustDB;Trusted_Connection=True;";
            string jo = value.ToString();
            JArray jsonArray = JArray.Parse(jo);
            int updated_row_length = jsonArray.Count;
            dynamic data = JObject.Parse(jsonArray[0].ToString());
            Console.WriteLine(data.table_name);
            string table_name = data.table_name;

            Console.WriteLine(updated_row_length);
            //Console.WriteLine(query);
            for (int i = 0; i < updated_row_length; i++)
            {
                Console.WriteLine(i);
                dynamic data1 = JObject.Parse(jsonArray[i].ToString());
                //Console.WriteLine(data1);
                Dictionary<string, string> dictObj = data1.ToObject<Dictionary<string, string>>();
                List<string> keyList = new List<string>(dictObj.Keys);
                List<string> valueList = new List<string>(dictObj.Values);
                string query1 = "UPDATE " + table_name + " SET ";
                string query2 = "";
                int limit = keyList.Count - 2;
                for (int j = 1; j < limit; j++)
                {
                    if (j == limit - 1)
                    {
                        query2 += keyList[j] + "=" + valueList[j] + " ";
                    }
                    else
                        query2 += keyList[j] + "=" + valueList[j] + ",";

                }
                string query3 = "WHERE " + keyList[0] + "=" + valueList[0] + ";";
                string query4 = query1 + query2 + query3;
                Console.WriteLine(query4);

                try
                {
                    //sql connection object
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        SqlCommand sqlcom = new SqlCommand(query4,conn);
                        conn.Open();
                        try
                        {
                            sqlcom.ExecuteNonQuery();
                            Console.WriteLine("Successful");
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        conn.Close(); 
                    }

                }
                catch (Exception ex)
                {
                    //display error message
                    Console.WriteLine("Exception: " + ex.Message);

                }
            }
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


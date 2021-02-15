using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPianoController : ControllerBase
    {
        TextWriterTraceListener writer = new TextWriterTraceListener(System.Console.Out);

        private readonly IConfiguration _configuration;
        private string TableJob = "Job_Piano";
        private string TableMacro = "Macro";
        public JobPianoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            DataTable jsonTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("JobPortalAppCon");
            
            using (var conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("getJobsList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PassedTableName", TableJob));

                SqlDataReader rdr = cmd.ExecuteReader();
                jsonTable.Load(rdr);
                conn.Close();
            }

            return new JsonResult(jsonTable);
        }

        [HttpPost]
        public JsonResult Post(JobPiano jp)
        {
            DataTable jsonTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("JobPortalAppCon");
            
            if (jp.Descr is null) {
                jp.Descr = "";
            }
            if (jp.Params is null)
            {
                jp.Params = "";
            }

            using (var conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insertJobPiano", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@JobName", jp.JobName));
                cmd.Parameters.Add(new SqlParameter("@Lib", jp.Lib));
                cmd.Parameters.Add(new SqlParameter("@Macro", jp.Macro));
                cmd.Parameters.Add(new SqlParameter("@Friday2X", jp.Friday2X));
                cmd.Parameters.Add(new SqlParameter("@Suspended", jp.Suspended));
                cmd.Parameters.Add(new SqlParameter("@Descr", jp.Descr));
                cmd.Parameters.Add(new SqlParameter("@Params", jp.Params));
                cmd.Parameters.Add(new SqlParameter("@JobPage", jp.JobPage));
                cmd.Parameters.Add(new SqlParameter("@Prty", jp.Prty));

                SqlDataReader rdr = cmd.ExecuteReader();
                jsonTable.Load(rdr);
                conn.Close();
            }

            return new JsonResult("Job inserito");
        }

        [HttpPut]
        public JsonResult Put(JobPiano jp)
        {
            DataTable jsonTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("JobPortalAppCon");
            
            using (var conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("updateJobPiano", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@JobID", jp.JobID));
                cmd.Parameters.Add(new SqlParameter("@JobName", jp.JobName));
                cmd.Parameters.Add(new SqlParameter("@Lib", jp.Lib));
                cmd.Parameters.Add(new SqlParameter("@Macro", jp.Macro));
                cmd.Parameters.Add(new SqlParameter("@Friday2X", jp.Friday2X));
                cmd.Parameters.Add(new SqlParameter("@Suspended", jp.Suspended));
                cmd.Parameters.Add(new SqlParameter("@Descr", jp.Descr));
                cmd.Parameters.Add(new SqlParameter("@Params", jp.Params));
                cmd.Parameters.Add(new SqlParameter("@JobPage", jp.JobPage));
                cmd.Parameters.Add(new SqlParameter("@Prty", jp.Prty));
                SqlDataReader rdr = cmd.ExecuteReader();
                jsonTable.Load(rdr);
                conn.Close();
            }

            return new JsonResult("Job aggiornato");
        }

        
        [Route("GetAllJobsPiano")]
        public JsonResult GetAllJobsPiano()
        {
            DataTable jsonTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("JobPortalAppCon");
            
            using (var conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("getAllJobs", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PassedTableName", TableJob));

                SqlDataReader rdr = cmd.ExecuteReader();
                jsonTable.Load(rdr);
                conn.Close();
            }

            return new JsonResult(jsonTable);
        }

        [Route("GetJobsPianoByPage")]
        public JsonResult GetJobsPianoByPage(string page)
        {
            DataTable jsonTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("JobPortalAppCon");

            using (var conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("getJobsByPage", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PassedTableName", TableJob));
                cmd.Parameters.Add(new SqlParameter("@JobPage", page));

                SqlDataReader rdr = cmd.ExecuteReader();
                jsonTable.Load(rdr);
                conn.Close();
            }

            return new JsonResult(jsonTable);
        }

        [Route("GetAllMacroPianoList")]
        public JsonResult GetAllMacroPianoList()
        {
            DataTable jsonTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("JobPortalAppCon");
            
            using (var conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("getAllMacroPianoList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                
                SqlDataReader rdr = cmd.ExecuteReader();
                jsonTable.Load(rdr);
                conn.Close();
            }

            return new JsonResult(jsonTable);
        }


        [Route("GetAllSuspendedPianoOpt")]
        public JsonResult GetAllSuspendedPianoOpt()
        {
            DataTable jsonTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("JobPortalAppCon");
            
            using (var conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("getAllSuspendedOpt", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PassedTableName", TableJob));

                SqlDataReader rdr = cmd.ExecuteReader();
                jsonTable.Load(rdr);
                conn.Close();
            }

            return new JsonResult(jsonTable);
        }

        [Route("GetAllFridayPianoJobs")]
        public JsonResult GetAllFridayJobs()
        {
            DataTable jsonTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("JobPortalAppCon");
            
            using (var conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("getAllFridayJobs", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PassedTableName", TableJob));

                SqlDataReader rdr = cmd.ExecuteReader();
                jsonTable.Load(rdr);
                conn.Close();
            }

            return new JsonResult(jsonTable);
        }

        [Route("GetAllPianoPages")]
        public JsonResult GetAllPianoPages()
        {
            DataTable jsonTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("JobPortalAppCon");
            
            using (var conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("getAllPages", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PassedTableName", TableJob));

                SqlDataReader rdr = cmd.ExecuteReader();
                jsonTable.Load(rdr);
                conn.Close();
            }

            return new JsonResult(jsonTable);
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            DataTable jsonTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("JobPortalAppCon");

            System.Diagnostics.Debug.WriteLine("id to delete: " + id.ToString());

            using (var conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("deleteJobPiano", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@JobID", id));

                SqlDataReader rdr = cmd.ExecuteReader();
                jsonTable.Load(rdr);
                conn.Close();
            }

            return new JsonResult("job cancellato");
        }

        [HttpPost("DeleteMulti")]
        public JsonResult DeleteMulti(Id2Delete[] id2delete)
        {
            DataTable jsonTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("JobPortalAppCon");


            foreach (Id2Delete item in id2delete)
            {
                if (!id2delete.Equals(0))
                {
                    Debug.WriteLine("items to delete: " + item.jobID.ToString() + "," + item.prty.ToString());
                }
                else
                {
                    Debug.WriteLine("no items to delete, array is empty");
                }
            }
            
            

            using (var conn = new SqlConnection(sqlDataSource))
            {
                
                for (int i = 0; i < id2delete.Length; i++)
                {
                    conn.Open();
                    Debug.WriteLine("deleting job with prty: " + id2delete[i].prty.ToString() + " and ID: " + id2delete[i].jobID.ToString());
                    SqlCommand cmd = new SqlCommand("deleteMultiJobPiano", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@JobID", id2delete[i].jobID.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@JobPrty", id2delete[i].prty.ToString()));
                    SqlDataReader rdr = cmd.ExecuteReader();
                    jsonTable.Load(rdr);
                    conn.Close();
                }
                
            }

            return new JsonResult("job cancellati");
        }
    }
}



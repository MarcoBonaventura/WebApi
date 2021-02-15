using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebApi.Models;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobItaliaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private string TableJob = "Job_Italia";
        private string TableMacro = "Macro";
        public JobItaliaController(IConfiguration configuration)
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
        public JsonResult Post(JobItalia ji)
        {
            DataTable jsonTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("JobPortalAppCon");

            if (ji.Descr is null)
            {
                ji.Descr = "";
            }
            if (ji.Params is null)
            {
                ji.Params = "";
            }

            using (var conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insertJobItalia", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@JobName", ji.JobName));
                cmd.Parameters.Add(new SqlParameter("@Lib", ji.Lib));
                cmd.Parameters.Add(new SqlParameter("@Macro", ji.Macro));
                cmd.Parameters.Add(new SqlParameter("@Friday2X", ji.Friday2X));
                cmd.Parameters.Add(new SqlParameter("@Suspended", ji.Suspended));
                cmd.Parameters.Add(new SqlParameter("@Descr", ji.Descr));
                cmd.Parameters.Add(new SqlParameter("@Params", ji.Params));
                cmd.Parameters.Add(new SqlParameter("@JobPage", ji.JobPage));
                cmd.Parameters.Add(new SqlParameter("@Prty", ji.Prty));

                SqlDataReader rdr = cmd.ExecuteReader();
                jsonTable.Load(rdr);
                conn.Close();
            }

            return new JsonResult("Job inserito");
        }

        [HttpPut]
        public JsonResult Put(JobItalia ji)
        {
            DataTable jsonTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("JobPortalAppCon");

            if (ji.Descr is null)
            {
                ji.Descr = "";
            }
            if (ji.Params is null)
            {
                ji.Params = "";
            }

            using (var conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("updateJobItalia", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@JobID", ji.JobID));
                cmd.Parameters.Add(new SqlParameter("@JobName", ji.JobName));
                cmd.Parameters.Add(new SqlParameter("@Lib", ji.Lib));
                cmd.Parameters.Add(new SqlParameter("@Macro", ji.Macro));
                cmd.Parameters.Add(new SqlParameter("@Friday2X", ji.Friday2X));
                cmd.Parameters.Add(new SqlParameter("@Suspended", ji.Suspended));
                cmd.Parameters.Add(new SqlParameter("@Descr", ji.Descr));
                cmd.Parameters.Add(new SqlParameter("@Params", ji.Params));
                cmd.Parameters.Add(new SqlParameter("@JobPage", ji.JobPage));
                cmd.Parameters.Add(new SqlParameter("@Prty", ji.Prty));
                SqlDataReader rdr = cmd.ExecuteReader();
                jsonTable.Load(rdr);
                conn.Close();
            }

            return new JsonResult("Job aggiornato");
        }


        [Route("GetAllJobsItalia")]
        public JsonResult GetAllJobsItalia()
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

        [Route("GetJobsItaliaByPage")]
        public JsonResult GetJobsItaliaByPage(string page)
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

        [Route("GetAllMacroItaliaList")]
        public JsonResult GetAllMacroItaliaList()
        {
            DataTable jsonTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("JobPortalAppCon");

            using (var conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("getAllMacroList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PassedTableName", TableMacro));

                SqlDataReader rdr = cmd.ExecuteReader();
                jsonTable.Load(rdr);
                conn.Close();
            }

            return new JsonResult(jsonTable);
        }


        [Route("GetAllSuspendedItaliaOpt")]
        public JsonResult GetAllSuspendedItaliaOpt()
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

        [Route("GetAllFridayItaliaJobs")]
        public JsonResult GetAllFridayItaliaJobs()
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

        [Route("GetAllItaliaPages")]
        public JsonResult GetAllItaliaPages()
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

            using (var conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("deleteJobItalia", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@JobID", id));

                SqlDataReader rdr = cmd.ExecuteReader();
                jsonTable.Load(rdr);
                conn.Close();
            }

            return new JsonResult(jsonTable);
        }


    }
}

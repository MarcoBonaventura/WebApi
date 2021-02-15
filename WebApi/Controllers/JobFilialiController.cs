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
    public class JobFilialiController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private string TableJob = "Job_Filiali";
        private string TableMacro = "Macro";
        public JobFilialiController(IConfiguration configuration)
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
        public JsonResult Post(JobFiliali jf)
        {
            DataTable jsonTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("JobPortalAppCon");

            if (jf.Descr is null)
            {
                jf.Descr = "";
            }
            if (jf.Params is null)
            {
                jf.Params = "";
            }

            using (var conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insertJobFiliali", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@JobName", jf.JobName));
                cmd.Parameters.Add(new SqlParameter("@Lib", jf.Lib));
                cmd.Parameters.Add(new SqlParameter("@Macro", jf.Macro));
                cmd.Parameters.Add(new SqlParameter("@Friday2X", jf.Friday2X));
                cmd.Parameters.Add(new SqlParameter("@Suspended", jf.Suspended));
                cmd.Parameters.Add(new SqlParameter("@Descr", jf.Descr));
                cmd.Parameters.Add(new SqlParameter("@Params", jf.Params));
                cmd.Parameters.Add(new SqlParameter("@JobPage", jf.JobPage));
                cmd.Parameters.Add(new SqlParameter("@Prty", jf.Prty));

                SqlDataReader rdr = cmd.ExecuteReader();
                jsonTable.Load(rdr);
                conn.Close();
            }

            return new JsonResult("Job inserito");
        }

        [HttpPut]
        public JsonResult Put(JobFiliali jf)
        {
            DataTable jsonTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("JobPortalAppCon");

            if (jf.Descr is null)
            {
                jf.Descr = "";
            }
            if (jf.Params is null)
            {
                jf.Params = "";
            }

            using (var conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("updateJobFiliali", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@JobID", jf.JobID));
                cmd.Parameters.Add(new SqlParameter("@JobName", jf.JobName));
                cmd.Parameters.Add(new SqlParameter("@Lib", jf.Lib));
                cmd.Parameters.Add(new SqlParameter("@Macro", jf.Macro));
                cmd.Parameters.Add(new SqlParameter("@Friday2X", jf.Friday2X));
                cmd.Parameters.Add(new SqlParameter("@Suspended", jf.Suspended));
                cmd.Parameters.Add(new SqlParameter("@Descr", jf.Descr));
                cmd.Parameters.Add(new SqlParameter("@Params", jf.Params));
                cmd.Parameters.Add(new SqlParameter("@JobPage", jf.JobPage));
                cmd.Parameters.Add(new SqlParameter("@Prty", jf.Prty));
                SqlDataReader rdr = cmd.ExecuteReader();
                jsonTable.Load(rdr);
                conn.Close();
            }

            return new JsonResult("Job aggiornato");
        }


        [Route("GetAllJobsFiliali")]
        public JsonResult GetAllJobsFiliali()
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

        [Route("GetJobsFilialiByPage")]
        public JsonResult GetJobsFilialiByPage(string page)
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

        [Route("GetAllMacroFilialiList")]
        public JsonResult GetAllMacroFilialiList()
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


        [Route("GetAllSuspendedFilialiOpt")]
        public JsonResult GetAllSuspendedFilialiOpt()
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

        [Route("GetAllFridayFilialiJobs")]
        public JsonResult GetAllFridayFilialiJobs()
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

        [Route("GetAllFilialiPages")]
        public JsonResult GetAllFilialiPages()
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
                SqlCommand cmd = new SqlCommand("deleteJobFiliali", conn);
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

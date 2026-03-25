using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaysisReconAPI.DatabaseContext;
using PaysisReconAPI.Model;
using PaysisReconAPI.Service;
using System.Data;

namespace PaysisReconAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {

        private readonly CommonServices commonService;
        public ReportsController(IDataDbContext db)
        {
            commonService = new CommonServices(db);
        }

        [HttpGet("GetNetworkList")]
        public IActionResult GetNetworkList(string Channel)
        {
            List<Network> result = new List<Network>();
            result = commonService.GetNetworkListServices(Channel);

            if (result != null && result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(200, "No Data found");
            }
        }

        [HttpGet("GetFileTypeList")]
        public IActionResult GetFileTypeList()
        {
            List<filetypeModel> result = new List<filetypeModel>();

            result = commonService.GetFileTypeList();


            if (result != null && result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(200, "No Data found");
            }
        }

        [HttpGet("GetReportData")]
        public IActionResult GetReportData(int id, DateTime BusinessDate, string network)
        {
            List<ReportsModel> result = new List<ReportsModel>();

            // Call the service method to get the report data based on the provided parameters
            result = commonService.GetReportsData(id, BusinessDate.ToString(), network);


            if (result != null && result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(200, "No Data found");
            }
        }


        [HttpPost("InsertReportRequest")]
        public IActionResult InsertReportRequest(ReportsModel reportRequestModel)
        {
            string result = string.Empty;
            // Call the service method to insert the report request based on the provided model
            result = commonService.InsertReportRequest(reportRequestModel);
            if (!string.IsNullOrEmpty(result))
            {
                return Ok(new { message = "Report request inserted successfully", responseCode = result });
            }
            else
            {
                return StatusCode(500, "Failed to insert report request");
            }
        }

        [HttpGet("GetReportListById")]
        public IActionResult GetReportListById(int id)
        {
            List<ReportsModel> result = new List<ReportsModel>();


            // Call the service method to get the report list based on the provided ID
            result = commonService.GetReportListById(id);
            if (result != null && result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(200, "No Data found");
            }
        }

        [HttpDelete("DeleteReportRequest")]
        public IActionResult DeleteReport(int id)
        {

            string result = string.Empty;
            // Call the service method to delete the report request based on the provided ID
            result = commonService.DeleteReportRequest(id.ToString());

            if (!string.IsNullOrEmpty(result))
            {
                return Ok(new { message = "Report request deleted successfully", responseCode = result });
            }
            else
            {
                return StatusCode(500, "Failed to delete report request");
            }
        }

    }
}

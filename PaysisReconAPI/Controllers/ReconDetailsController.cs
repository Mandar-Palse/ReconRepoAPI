using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaysisReconAPI.DatabaseContext;
using PaysisReconAPI.Model;
using PaysisReconAPI.Service;

namespace PaysisReconAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReconDetailsController : ControllerBase
    {
        private readonly ReconDetailsService mastersService;
        public ReconDetailsController(IDataDbContext db)
        {
            mastersService = new ReconDetailsService(db);
        }

        [HttpGet("GetReconConfirmedDetails")]
        public async Task<IActionResult> GetReconConfirmedDetails(DateTime businessDate, string channel)
        {

            bool result = mastersService.GetReconConfirmedDetailsServices(businessDate.ToString(), channel);

            return Ok(result);
        }

        [HttpGet("GetReconGroupList")]
        public async Task<IActionResult> GetReconGroupList(DateTime businessDate, string channel)
        {
            //List<RecongroupList> result = new List<RecongroupList>();

            //result = mastersService.GetReconGroupListServices(businessDate.ToString(), channel);

            var result = new List<RecongroupList>
            {
                new RecongroupList { id = 1, recongroupname = "Bank Reconciliation" },
                new RecongroupList { id = 2, recongroupname = "Payment Gateway Recon" },
                new RecongroupList { id = 3, recongroupname = "Vendor Settlement Recon" },
                new RecongroupList { id = 4, recongroupname = "Internal Ledger Recon" }
            };

            if (result != null && result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(200, "No Data found");
            }
        }

        [HttpGet("GetReconStatus")]
        public async Task<IActionResult> GetReconStatus(DateTime businessDate, string channel)
        {

            //List<ReconStatusMaster> result = new List<ReconStatusMaster>();

            //result = mastersService.GetReconStatusMasterDetailsServices(businessDate.ToString(), channel);

            var result = new List<ReconStatusMaster>
            {
                new ReconStatusMaster { id = 1, recondate = "2026-03-20", recongroupname = "Bank Reconciliation", function_name = "AutoMatch", statusname = "Completed", requestdatetime = "2026-03-20 10:00:00", requestclosedatetime = "2026-03-20 10:05:00", Action = "View" },
                new ReconStatusMaster { id = 2, recondate = "2026-03-20", recongroupname = "Payment Gateway Recon", function_name = "Validation", statusname = "In Progress", requestdatetime = "2026-03-20 10:10:00", requestclosedatetime = "", Action = "Rerun" },
                new ReconStatusMaster { id = 3, recondate = "2026-03-20", recongroupname = "Vendor Settlement Recon", function_name = "DataLoad", statusname = "Failed", requestdatetime = "2026-03-20 10:15:00", requestclosedatetime = "2026-03-20 10:17:00", Action = "Retry" },
                new ReconStatusMaster { id = 4, recondate = "2026-03-21", recongroupname = "Internal Ledger Recon", function_name = "AutoMatch", statusname = "Completed", requestdatetime = "2026-03-21 09:30:00", requestclosedatetime = "2026-03-21 09:35:00", Action = "View" },
                new ReconStatusMaster { id = 5, recondate = "2026-03-21", recongroupname = "Bank Reconciliation", function_name = "Validation", statusname = "Completed", requestdatetime = "2026-03-21 09:40:00", requestclosedatetime = "2026-03-21 09:45:00", Action = "View" },
                new ReconStatusMaster { id = 6, recondate = "2026-03-21", recongroupname = "Payment Gateway Recon", function_name = "DataLoad", statusname = "Completed", requestdatetime = "2026-03-21 10:00:00", requestclosedatetime = "2026-03-21 10:03:00", Action = "View" },
                new ReconStatusMaster { id = 7, recondate = "2026-03-21", recongroupname = "Vendor Settlement Recon", function_name = "AutoMatch", statusname = "In Progress", requestdatetime = "2026-03-21 10:10:00", requestclosedatetime = "", Action = "Rerun" },
                new ReconStatusMaster { id = 8, recondate = "2026-03-22", recongroupname = "Internal Ledger Recon", function_name = "Validation", statusname = "Failed", requestdatetime = "2026-03-22 08:50:00", requestclosedatetime = "2026-03-22 08:55:00", Action = "Retry" },
                new ReconStatusMaster { id = 9, recondate = "2026-03-22", recongroupname = "Bank Reconciliation", function_name = "DataLoad", statusname = "Completed", requestdatetime = "2026-03-22 09:00:00", requestclosedatetime = "2026-03-22 09:02:00", Action = "View" },
                new ReconStatusMaster { id = 10, recondate = "2026-03-22", recongroupname = "Payment Gateway Recon", function_name = "AutoMatch", statusname = "In Progress", requestdatetime = "2026-03-22 09:10:00", requestclosedatetime = "", Action = "Rerun" }
            };

            if (result != null && result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(200, "No Data found");
            }
        }


        [HttpPost("RaiseReconRequest")]
        public async Task<IActionResult> RaiseReconRequest(int recongroupid, DateTime businessDate, int requestedby)
        {
            string result = mastersService.Raise_request_reconServices(recongroupid, businessDate.ToString(), requestedby);

            return Ok(result);
        }

    }
}

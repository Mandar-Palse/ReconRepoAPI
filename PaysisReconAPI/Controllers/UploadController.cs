using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaysisReconAPI.DatabaseContext;
using PaysisReconAPI.Model;
using PaysisReconAPI.Service;

namespace PaysisReconAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly UploadFileServices mastersService;
        public UploadController(IDataDbContext db)
        {
            mastersService = new UploadFileServices(db);

        }

        [HttpPost]
        [Route("file")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            WatcherModel objWatcher = mastersService.GetFilewatcher_DetailsNew().Where(x => file.FileName.Contains(x.FilenameFormat) &&
                        file.FileName.StartsWith(x.FilenameFormat)).FirstOrDefault();

            if (objWatcher != null)
            {
                // Check file is exist or not in database
                string FileExistOrNot = mastersService.CheckFileExistornotServices(file.FileName);

                if (FileExistOrNot.ToString().Contains("01:"))
                {
                    return BadRequest(new { message = "File Already exists", filename = file.FileName });
                }
                else
                {
                    if (!Directory.Exists(objWatcher.FilePath + @"\"))
                        Directory.CreateDirectory(objWatcher.FilePath + @"\");

                    //string path = Server.MapPath("~/uploads/" + filename);

                    string destfilepath = objWatcher.FilePath + @"\" + file.FileName;
                    string archivefilepath = objWatcher.archivepath + @"\" + file.FileName;

                    using (var stream = new FileStream(destfilepath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Inserting file details into fileuploaddata table
                    string file_upload_status = mastersService.insert_into_fileuploaddataServices(objWatcher.FMID, file.FileName, destfilepath, archivefilepath);


                    return Ok(new { message = "File uploaded successfully", fileName = file.FileName });
                }
            }
            else
            {
                return BadRequest(new { message = "Invalid File name", filename = file.FileName });
            }
        }

        [HttpGet("GetUploadedFilesList")]
        public async Task<IActionResult> GetUploadedFilesList(DateTime businessDate)
        {
            List<Fileuploaddata> result = new List<Fileuploaddata>();

            result = mastersService.GetFileUploadDataListFromUploadServices(businessDate, "IMPS");

            //List<Fileuploaddata> result = new List<Fileuploaddata>
            //{
            //    new Fileuploaddata
            //    {
            //        id = 1,
            //        filedescription = "Sales Data",
            //        filename = "sales_20260320.csv",
            //        businessdate = "2026-03-20",
            //        temp_tbl_count = "1200",
            //        data_tbl_count = "1100",
            //        final_tbl_count = "1080",
            //        succrecord = "1075",
            //        totrecord = "1200",
            //        ReconConfirmation = true,
            //        error_message = "",
            //        Average7DayCount = "950"
            //    },
            //    new Fileuploaddata
            //    {
            //        id = 2,
            //        filedescription = "Recon File",
            //        filename = "recon_20260320.csv",
            //        businessdate = "2026-03-20",
            //        temp_tbl_count = "980",
            //        data_tbl_count = "970",
            //        final_tbl_count = "960",
            //        succrecord = "955",
            //        totrecord = "980",
            //        ReconConfirmation = false,
            //        error_message = "Mismatch found",
            //        Average7DayCount = "870"
            //    },
            //    new Fileuploaddata
            //    {
            //        id = 3,
            //        filedescription = "Inventory Upload",
            //        filename = "inventory_20260320.csv",
            //        businessdate = "2026-03-20",
            //        temp_tbl_count = "1500",
            //        data_tbl_count = "1450",
            //        final_tbl_count = "1400",
            //        succrecord = "1390",
            //        totrecord = "1500",
            //        ReconConfirmation = true,
            //        error_message = "",
            //        Average7DayCount = "1300"
            //    },
            //    new Fileuploaddata
            //    {
            //        id = 4,
            //        filedescription = "Customer Data",
            //        filename = "customers_20260320.csv",
            //        businessdate = "2026-03-20",
            //        temp_tbl_count = "2000",
            //        data_tbl_count = "1950",
            //        final_tbl_count = "1900",
            //        succrecord = "1890",
            //        totrecord = "2000",
            //        ReconConfirmation = true,
            //        error_message = "",
            //        Average7DayCount = "1800"
            //    }
            //};


            if (result != null && result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(200, "No Data found");
            }
        }

    }
}

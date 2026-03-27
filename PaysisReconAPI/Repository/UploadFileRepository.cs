using Dapper;
using PaysisReconAPI.DatabaseContext;
using PaysisReconAPI.Model;
using PaysisReconAPI.Service;
using System.Data;

namespace PaysisReconAPI.Repository
{
    public class UploadFileRepository
    {
        private readonly IDataDbContext _db;

        public UploadFileRepository(IDataDbContext db)
        {
            this._db = db;
            commonServices = new CommonServices(db);
        }

        CommonServices commonServices = null;   
        public List<WatcherModel> GetFilewatcherDetailsNew()
        {
            List<WatcherModel> watcher = null;
            try
            {
                using (var con = _db.GetOpenSqlConnection())
                {
                    using (var trans = con.BeginTransaction())
                    {
                        watcher = con.Query<WatcherModel>("Select * from recon.usp_GetFileWatcherDetails_new()", null).ToList();
                        trans.Commit();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                watcher = new List<WatcherModel>();
                ErrorMasterModel errormastermodel = new ErrorMasterModel();
                errormastermodel.function_name = "GetFilewatcherDetailsNew() | usp_GetFileWatcherDetails_new";
                errormastermodel.errorcode = "01";
                errormastermodel.error_message = ex.Message;
                errormastermodel.exception_context = "".ToString();
                errormastermodel.errordescription = "App_Error";
                commonServices.Insert_ErrorLog(errormastermodel);
            }
            return watcher;
            // throw new NotImplementedException();
        }

        public string CheckFileExistornot(string filename)
        {
            string result = "";
            try
            {
                using (var con = _db.GetOpenSqlConnection())
                {
                    var dp = new DynamicParameters();
                    dp.Add("@p_filename", filename, DbType.String);
                    con.BeginTransaction();
                    result = con.Query<string>("Select * from recon.usp_validate_FileExistornot(@p_filename)", dp).FirstOrDefault();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                result = "Error";
                ErrorMasterModel errormastermodel = new ErrorMasterModel();
                errormastermodel.function_name = "CheckFileExistornot() | usp_validate_FileExistornot";
                errormastermodel.errorcode = "01";
                errormastermodel.error_message = ex.Message;
                errormastermodel.exception_context = "".ToString();
                errormastermodel.errordescription = "App_Error";
                commonServices.Insert_ErrorLog(errormastermodel);
            }
            return result;
        }

        public string insert_into_fileuploaddata(int fileid, string filename, string filepath, string filepath_archive)
        {
            string result = "";
            try
            {
                using (var con = _db.GetOpenSqlConnection())
                {
                    var dp = new DynamicParameters();
                    dp.Add("@p_fileid", fileid, DbType.Int32);
                    dp.Add("@p_filename", filename, DbType.String);
                    dp.Add("@p_filepath", filepath, DbType.String);
                    dp.Add("@p_filepath_archive", filepath_archive, DbType.String);
                    var x = con.BeginTransaction();
                    result = con.Query<string>("Select * from recon.usp_insert_fileuploaddata(@p_fileid,@p_filename,@p_filepath,@p_filepath_archive)", dp).FirstOrDefault();
                    x.Commit();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                result = "Error";
                ErrorMasterModel errormastermodel = new ErrorMasterModel();
                errormastermodel.function_name = "CheckFileExistornot() | usp_validate_FileExistornot";
                errormastermodel.errorcode = "01";
                errormastermodel.error_message = ex.Message;
                errormastermodel.exception_context = "".ToString();
                errormastermodel.errordescription = "App_Error";
                commonServices.Insert_ErrorLog(errormastermodel);
            }
            return result;
        }

        public List<Fileuploaddata> GetFileUploadDataListFromUpload(DateTime uploaddate, string network)
        {
            List<Fileuploaddata> result = new List<Fileuploaddata>();
            try
            {
                using (var con = _db.GetOpenSqlConnection())
                {
                    var dp = new DynamicParameters();
                    dp.Add("@p_uploaddate", uploaddate, DbType.Date);
                    dp.Add("@p_network", network, DbType.String);
                    con.BeginTransaction();
                    result = con.Query<Fileuploaddata>("Select * from recon.usp_get_fileuploaddatafromUpload(@p_uploaddate,@p_network)", dp).ToList();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                result = new List<Fileuploaddata>();
                ErrorMasterModel errormastermodel = new ErrorMasterModel();
                errormastermodel.function_name = "GetFileUploadDataListFromUpload() | usp_get_fileuploaddatafromUpload";
                errormastermodel.errorcode = "01";
                errormastermodel.error_message = ex.Message;
                errormastermodel.exception_context = "".ToString();
                errormastermodel.errordescription = "App_Error";
                commonServices.Insert_ErrorLog(errormastermodel);
            }
            return result;
        }

    }
}

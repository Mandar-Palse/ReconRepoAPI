using Dapper;
using PaysisReconAPI.DatabaseContext;
using PaysisReconAPI.Model;
using PaysisReconAPI.Service;
using System.Data;

namespace PaysisReconAPI.Repository
{
    public class ReconDetailsRepository
    {
        private readonly IDataDbContext _db;
        CommonServices commonServices;
        ErrorMasterModel errormastermodel;
        public ReconDetailsRepository(IDataDbContext db)
        {
            this._db = db;
            commonServices = new CommonServices(db);
            errormastermodel = new ErrorMasterModel();
        }

        public bool GetReconConfirmedDetails(string businessdate, string network)
        {
            bool result = false;
            try
            {
                using (var con = _db.GetOpenSqlConnection())
                {
                    var dp = new DynamicParameters();
                    dp.Add("@p_businessdate", businessdate, DbType.String);
                    dp.Add("@p_network", network, DbType.String);
                    con.BeginTransaction();
                    result = con.Query<bool>("Select * From usp_get_recon_confirmation(@p_businessdate,@p_network)", dp).FirstOrDefault();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                result = false;
                errormastermodel.function_name = "GetReconConfirmedDetails() | usp_get_recon_confirmation";
                errormastermodel.errorcode = "01";
                errormastermodel.error_message = ex.Message;
                errormastermodel.exception_context = "".ToString();
                errormastermodel.errordescription = "App_Error";
                commonServices.Insert_ErrorLog(errormastermodel);
            }
            return result;
        }

        public string GetReconExistsConfirmation(string businessdate, string network)
        {
            string result = "";
            try
            {
                using (var con = _db.GetOpenSqlConnection())
                {
                    var dp = new DynamicParameters();
                    dp.Add("@p_businessdate", businessdate, DbType.String);
                    dp.Add("@p_network", network, DbType.String);
                    var x = con.BeginTransaction();
                    result = con.Query<string>("Select * usp_recon_exists_confirmation(@p_businessdate,@p_network)", dp).FirstOrDefault();
                    x.Commit();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                result = "Error at application repository!";
                errormastermodel.function_name = "GetReconExistsConfirmation() | usp_recon_exists_confirmation";
                errormastermodel.errorcode = "01";
                errormastermodel.error_message = ex.Message;
                errormastermodel.exception_context = "".ToString();
                errormastermodel.errordescription = "App_Error";
                commonServices.Insert_ErrorLog(errormastermodel);
            }
            return result;
        }

        public string PutReconConfirmationDetails(string businessdate, string userid, string network)
        {
            string result = "";
            try
            {
                using (var con = _db.GetOpenSqlConnection())
                {
                    var dp = new DynamicParameters();
                    dp.Add("@p_businessdate", businessdate, DbType.String);
                    dp.Add("@p_userid", userid, DbType.String);
                    dp.Add("@p_network", network, DbType.String);
                    var x = con.BeginTransaction();
                    result = con.Query<string>("Select * from usp_insert_recon_confirmationDetails(@p_businessdate,@p_userid,@p_network)", dp).FirstOrDefault();
                    x.Commit();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                result = "Error at application repository!";
                errormastermodel.function_name = "PutReconConfirmationDetails() | usp_insert_recon_confirmationDetails";
                errormastermodel.errorcode = "01";
                errormastermodel.error_message = ex.Message;
                errormastermodel.exception_context = "".ToString();
                errormastermodel.errordescription = "App_Error";
                commonServices.Insert_ErrorLog(errormastermodel);
            }
            return result;
        }

        public List<RecongroupList> GetReconGroupList(string businessdate, string network)
        {
            List<RecongroupList> result = new List<RecongroupList>();
            try
            {
                using (var con = _db.GetOpenSqlConnection())
                {
                    var dp = new DynamicParameters();
                    dp.Add("@p_businessdate", businessdate, DbType.String);
                    dp.Add("@p_network", network, DbType.String);
                    con.BeginTransaction();
                    result = con.Query<RecongroupList>("Select * From usp_ret_recongrouplist(@p_businessdate,@p_network)", dp).ToList();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                result = new List<RecongroupList>();
                errormastermodel.function_name = "GetReconGroupList() | usp_ret_recongrouplist";
                errormastermodel.errorcode = "01";
                errormastermodel.error_message = ex.Message;
                errormastermodel.exception_context = "".ToString();
                errormastermodel.errordescription = "App_Error";
                commonServices.Insert_ErrorLog(errormastermodel);
            }
            return result;
        }

        public List<ReconStatusMaster> GetReconStatusMasterDetails(string businessdate, string network)
        {
            List<ReconStatusMaster> result;
            try
            {
                result = new List<ReconStatusMaster>();

                using (var con = _db.GetOpenSqlConnection())
                {
                    var dp = new DynamicParameters();
                    dp.Add("@p_businessdate", businessdate, DbType.String);
                    dp.Add("@p_network", network, DbType.String);
                    var x = con.BeginTransaction();
                    result = con.Query<ReconStatusMaster>("Select * From usp_request_recon_statusdetails(@p_businessdate,@p_network)", dp).ToList();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                result = new List<ReconStatusMaster>();
                errormastermodel.function_name = "GetReconStatusMasterDetails() | usp_request_recon_statusdetails";
                errormastermodel.errorcode = "01";
                errormastermodel.error_message = ex.Message;
                errormastermodel.exception_context = "".ToString();
                errormastermodel.errordescription = "App_Error";
                commonServices.Insert_ErrorLog(errormastermodel);
            }
            return result;
        }

        public string Raise_request_recon(int recongroupid, string businessdate, int requestby)
        {
            string result = "";
            try
            {
                using (var con = _db.GetOpenSqlConnection())
                {
                    var dp = new DynamicParameters();
                    dp.Add("@p_recongroupid", recongroupid, DbType.Int32);
                    dp.Add("@p_businessdate", businessdate, DbType.String);
                    dp.Add("@p_requestby", requestby, DbType.Int32);
                    var x = con.BeginTransaction();
                    result = con.Query<string>("Select * From usp_request_recon(@p_recongroupid,@p_businessdate,@p_requestby)", dp).FirstOrDefault();
                    x.Commit();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                result = "Error at application repository!";
                errormastermodel.function_name = "Raise_request_recon() | usp_request_recon";
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

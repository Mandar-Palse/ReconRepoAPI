using Dapper;
using PaysisReconAPI.DatabaseContext;
using PaysisReconAPI.Model;
using System.Data;

namespace PaysisReconAPI.Repository
{
    public class CommonRepository
    {
        private readonly IDataDbContext _db;

        public CommonRepository(IDataDbContext db)
        {
            this._db = db;
        }

        public void Insert_ErrorLog(ErrorMasterModel ObjErrorMasterModel)
        {
            try
            {
                if (string.IsNullOrEmpty(ObjErrorMasterModel.errorcode.Trim()))
                {
                    ObjErrorMasterModel.errorcode = "0";
                }

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@p_function_name", ObjErrorMasterModel.function_name, DbType.String, ParameterDirection.Input, 0);
                dp.Add("@p_errorcode", Convert.ToInt32(ObjErrorMasterModel.errorcode), DbType.Int32, ParameterDirection.Input, 0);
                dp.Add("@p_error_message", ObjErrorMasterModel.error_message, DbType.String, ParameterDirection.Input, 0);
                dp.Add("@p_exception_context", ObjErrorMasterModel.exception_context, DbType.String, ParameterDirection.Input, 0);
                dp.Add("@p_errordescription", ObjErrorMasterModel.errordescription, DbType.String, ParameterDirection.Input, 0);

                using (var con = _db.GetOpenSqlConnection())
                {
                    var query = "SELECT * FROM usp_errorlog_insert(@p_function_name, @p_errorcode, @p_error_message, @p_exception_context,@p_errordescription)";

                    var conn = con.BeginTransaction();
                    con.Query(query, dp);
                    conn.Commit();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

        public List<Network> GetNetworkList(string network)
        {
            List<Network> retVal = new List<Network>();
            try
            {
                using (var con = _db.GetOpenSqlConnection())
                {
                    DynamicParameters dp = new DynamicParameters();
                    using (var trans = con.BeginTransaction())
                    {
                        dp.Add("@p_network", network, DbType.String, ParameterDirection.Input, 0);
                        retVal = con.Query<Network>("Select * from usp_getnetworkdropdownlist(@p_network)", dp).ToList();
                        trans.Commit();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMasterModel ObjErrorMasterModel = new ErrorMasterModel();
                ObjErrorMasterModel.function_name = "GetNetwork_dropdownList() :  usp_getnetworkdropdownlist";
                ObjErrorMasterModel.exception_context = ex.StackTrace;
                ObjErrorMasterModel.errordescription = "Application_error";
                ObjErrorMasterModel.error_message = ex.ToString();
                ObjErrorMasterModel.errorcode = "";

                Insert_ErrorLog(ObjErrorMasterModel);
                retVal = new List<Network>();
            }
            return retVal;
        }

        public List<filetypeModel> GetFiletypeList()
        {
            List<filetypeModel> retVal = null;
            try
            {
                using (var con = _db.GetOpenSqlConnection())
                {
                    DynamicParameters dp = new DynamicParameters();
                    using (var trans = con.BeginTransaction())
                    {
                        dp.Add("p_event", "@getfiletypeMasterlist", DbType.String, ParameterDirection.Input, 0);
                        retVal = con.Query<filetypeModel>("Select * from usp_getfilemasterdropdowndetails(@getfiletypeMasterlist)", dp, commandType: CommandType.StoredProcedure).ToList();
                        trans.Commit();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMasterModel ObjErrorMasterModel = new ErrorMasterModel();
                ObjErrorMasterModel.function_name = "FiletypeModels() :  usp_getfilemasterdropdowndetails";
                ObjErrorMasterModel.exception_context = ex.StackTrace;
                ObjErrorMasterModel.errordescription = "Application_error";
                ObjErrorMasterModel.error_message = ex.ToString();
                ObjErrorMasterModel.errorcode = "";

                Insert_ErrorLog(ObjErrorMasterModel);
                retVal = new List<filetypeModel>();
            }
            return retVal;
        }

        public List<ReportsModel> GetReportsData(int id, string BusinessDate, string network)
        {
            List<ReportsModel> retVal = null;
            try
            {
                var dp = new DynamicParameters();
                using (var con = _db.GetOpenSqlConnection())
                {
                    dp.Add("@p_reportstatusid", id, DbType.Int32);
                    dp.Add("@p_BusinessDate", BusinessDate, DbType.String);
                    dp.Add("@p_network", network, DbType.String);
                    retVal = con.Query<ReportsModel>("Select * from usp_getReportRequests(@p_reportstatusid,@p_BusinessDate,@p_network)", dp).ToList();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorMasterModel ObjErrorMasterModel = new ErrorMasterModel();
                ObjErrorMasterModel.function_name = "GetReportRequests() :  usp_getReportRequests";
                ObjErrorMasterModel.exception_context = ex.StackTrace;
                ObjErrorMasterModel.errordescription = "Application_error";
                ObjErrorMasterModel.error_message = ex.ToString();
                ObjErrorMasterModel.errorcode = "";

                Insert_ErrorLog(ObjErrorMasterModel);
                retVal = new List<ReportsModel>();
            }
            return retVal;
        }

        public string Insert_ReportRequest(ReportsModel ObjReportsModel)
        {
            string RspCode = string.Empty;
            try
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@p_rsid", ObjReportsModel.reportstatusid, DbType.Int32, ParameterDirection.Input, 0);
                dp.Add("@p_reportid", ObjReportsModel.reportid, DbType.Int32, ParameterDirection.Input, 0);
                dp.Add("@p_reportdate", Convert.ToDateTime(ObjReportsModel.reportdate), DbType.DateTime, ParameterDirection.Input, 0);
                dp.Add("@p_filetypeid", ObjReportsModel.ftmid, DbType.Int32, ParameterDirection.Input, 0);
                dp.Add("@p_actionflag", ObjReportsModel.ActionFlag, DbType.String, ParameterDirection.Input, 0);
                dp.Add("@p_rspcode", RspCode, DbType.String, ParameterDirection.Output, 0);

                if (!string.IsNullOrEmpty(ObjReportsModel.reporttodate))
                {
                    dp.Add("@p_reporttodate", Convert.ToDateTime(ObjReportsModel.reporttodate), DbType.DateTime, ParameterDirection.Input, 0);
                    using (var con = _db.GetOpenSqlConnection())
                    {
                        con.Query<VouchersModel>("Select * from usp_ReportRequestDateRangeReport_Insert(@p_rsid,@p_reportid,@p_reportdate,@p_filetypeid,@p_actionflag,@p_reporttodate,@p_rspcode)", dp, commandType: CommandType.StoredProcedure);
                        con.Close();
                        RspCode = dp.Get<String>("p_rspcode");
                    }
                }
                else
                {
                    using (var con = _db.GetOpenSqlConnection())
                    {
                        con.Query<ReportsModel>("Select * from usp_ReportRequest_Insert(@p_rsid,@p_reportid,@p_reportdate,@p_filetypeid,@p_actionflag,@p_rspcode)", dp, commandType: CommandType.StoredProcedure);
                        con.Close();
                        RspCode = dp.Get<String>("p_rspcode");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMasterModel ObjErrorMasterModel = new ErrorMasterModel();
                ObjErrorMasterModel.function_name = "Insert_ReportRequest() :  usp_ReportRequest_Insert";
                ObjErrorMasterModel.exception_context = ex.StackTrace;
                ObjErrorMasterModel.errordescription = "Application_error";
                ObjErrorMasterModel.error_message = ex.ToString();
                ObjErrorMasterModel.errorcode = "";

                Insert_ErrorLog(ObjErrorMasterModel);
            }
            return RspCode;
            //return true;
        }

        public List<ReportsModel> GetReportListByid(int id)
        {
            List<ReportsModel> retVal = null;
            try
            {
                using (var con = _db.GetOpenSqlConnection())
                {
                    DynamicParameters dp = new DynamicParameters();
                    using (var trans = con.BeginTransaction())
                    {
                        dp.Add("p_Event", "getReportMasterlist", DbType.String, ParameterDirection.Input, 0);
                        dp.Add("p_id", id, DbType.Int32, ParameterDirection.Input, 0);
                        retVal = con.Query<ReportsModel>("usp_getfilemasterdropdowndetails", dp, commandType: CommandType.StoredProcedure).ToList();
                        trans.Commit();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMasterModel ObjErrorMasterModel = new ErrorMasterModel();
                ObjErrorMasterModel.function_name = "GetReportListByid() :  usp_getfilemasterdropdowndetails";
                ObjErrorMasterModel.exception_context = ex.StackTrace;
                ObjErrorMasterModel.errordescription = "Application_error";
                ObjErrorMasterModel.error_message = ex.ToString();
                ObjErrorMasterModel.errorcode = "";

                Insert_ErrorLog(ObjErrorMasterModel);
                retVal = new List<ReportsModel>();
            }
            return retVal;
        }

        public string DeleteReportRequest(string reportstatusid)
        {
            string result = "";
            try
            {
                using (var con = _db.GetOpenSqlConnection())
                {
                    var dp = new DynamicParameters();
                    dp.Add("p_reportstatusid", reportstatusid, DbType.String);
                    var x = con.BeginTransaction();
                    result = con.Query<string>("usp_delete_reportrequest", dp, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    x.Commit();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                result = "03: Error at repository function!";
                ErrorMasterModel ObjErrorMasterModel = new ErrorMasterModel();
                ObjErrorMasterModel.function_name = "DeleteReportRequest() :  usp_delete_reportrequest";
                ObjErrorMasterModel.exception_context = ex.StackTrace;
                ObjErrorMasterModel.errordescription = "Application_error";
                ObjErrorMasterModel.error_message = ex.ToString();
                ObjErrorMasterModel.errorcode = "";
                Insert_ErrorLog(ObjErrorMasterModel);
            }
            return result;
        }
    }
}

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
                dp.Add("p_function_name", ObjErrorMasterModel.function_name, DbType.String, ParameterDirection.Input, 0);
                dp.Add("p_errorcode", Convert.ToInt16(ObjErrorMasterModel.errorcode), DbType.Int16, ParameterDirection.Input, 0);
                dp.Add("p_error_message", ObjErrorMasterModel.error_message, DbType.String, ParameterDirection.Input, 0);
                dp.Add("p_exception_context", ObjErrorMasterModel.exception_context, DbType.String, ParameterDirection.Input, 0);
                dp.Add("p_errordescription", ObjErrorMasterModel.errordescription, DbType.String, ParameterDirection.Input, 0);

                using (var con = _db.GetOpenSqlConnection())
                {
                    var conn = con.BeginTransaction();
                    con.Query("usp_errorlog_insert", dp, commandType: CommandType.StoredProcedure);
                    conn.Commit();
                    //con.Query<string>("usp_errorlog_insert", dp, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    con.Close();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }


    }
}

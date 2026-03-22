using Dapper;
using PaysisReconAPI.DatabaseContext;
using PaysisReconAPI.Model;
using System.Data;

namespace PaysisReconAPI.Repository
{
    public class AccountRepository
    {
       private readonly IDataDbContext _db;

        public AccountRepository(IDataDbContext db)
        {  
            this._db = db; 
        }

        public ResponseStatus AuthenticateUser(string UserName, string Password, string hostName, string myIP)
        {
            ResponseStatus retVal = new ResponseStatus();

            try
            {
                var dp = new DynamicParameters();
                dp.Add("@p_username", UserName, DbType.String);
                dp.Add("@p_password", Password, DbType.String);
                dp.Add("@p_hostname", hostName ?? "", DbType.String);
                dp.Add("@p_myip", myIP ?? "", DbType.String);

                using (var con = _db.GetOpenSqlConnection())
                {
                    var query = "SELECT * FROM public.usp_authenticatecustomer(@p_username, @p_password, @p_hostname, @p_myip)";

                    retVal = con.Query<ResponseStatus>(query, dp).FirstOrDefault();

                    if (retVal == null)
                    {
                        retVal = new ResponseStatus
                        {
                            strcode = "01",
                            strmessage = "Invalid credentials"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                retVal = new ResponseStatus()
                {
                    strcode = "02",
                    strmessage = ex.Message
                };
            }

            return retVal;
        }
    }
}

using PaysisReconAPI.DatabaseContext;
using PaysisReconAPI.Model;
using PaysisReconAPI.Repository;

namespace PaysisReconAPI.Service
{
    public class AccountService
    {
        private readonly AccountRepository _accRepo;

        public AccountService(IDataDbContext db) 
        {
            _accRepo = new AccountRepository(db);
        }

        public ResponseStatus AuthenticateUser(string UserName, string Password, string hostName, string myIP)
        {
            return _accRepo.AuthenticateUser(UserName, Password, hostName, myIP);
        }

    }
}

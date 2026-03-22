using PaysisReconAPI.DatabaseContext;
using PaysisReconAPI.Model;
using PaysisReconAPI.Repository;

namespace PaysisReconAPI.Service
{
    public class CommonServices
    {

       private readonly CommonRepository ObjCommonRepository = null;

        public CommonServices(IDataDbContext db)
        {
            ObjCommonRepository = new CommonRepository(db); 
        }


        public void Insert_ErrorLog(ErrorMasterModel ObjErrorMasterModel)
        {
            ObjCommonRepository.Insert_ErrorLog(ObjErrorMasterModel);
        }
    }
}

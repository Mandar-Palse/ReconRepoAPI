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


        public List<Network> GetNetworkListServices(string network)
        {
            return ObjCommonRepository.GetNetworkList(network);
        }

        public List<filetypeModel> GetFileTypeList()
        {
            return ObjCommonRepository.GetFiletypeList();

        }

        public List<ReportsModel> GetReportsData(int it, string businessdate, string network)
        {
            return ObjCommonRepository.GetReportsData(it, businessdate, network);
        }

        public string InsertReportRequest(ReportsModel reportRequestModel)
        {
            return ObjCommonRepository.Insert_ReportRequest(reportRequestModel);
        }


        public List<ReportsModel> GetReportListById(int id)
        {
            return ObjCommonRepository.GetReportListByid(id);
        }

        public string DeleteReportRequest(string id)
        {
            return ObjCommonRepository.DeleteReportRequest(id);
        }
    }
}

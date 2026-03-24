using PaysisReconAPI.DatabaseContext;
using PaysisReconAPI.Model;
using PaysisReconAPI.Repository;

namespace PaysisReconAPI.Service
{
    public class ReconDetailsService
    {
        ReconDetailsRepository ObjCommonRepository = null;
        public ReconDetailsService(IDataDbContext db)
        {
            ObjCommonRepository = new ReconDetailsRepository(db);
        }

        public bool GetReconConfirmedDetailsServices(string businessdate, string network)
        {
            return ObjCommonRepository.GetReconConfirmedDetails(businessdate, network);
        }

        public string ReconExistsConfirmationServices(string businessdate, string network)
        {
            return ObjCommonRepository.GetReconExistsConfirmation(businessdate, network);
        }

        public string PutReconConfirmationDetailsServices(string businessdate, string userid, string network)
        {
            return ObjCommonRepository.PutReconConfirmationDetails(businessdate, userid, network);
        }

        public List<RecongroupList> GetReconGroupListServices(string businessdate, string network)
        {
            return ObjCommonRepository.GetReconGroupList(businessdate, network);
        }

        public List<ReconStatusMaster> GetReconStatusMasterDetailsServices(string businessdate, string network)
        {
            return ObjCommonRepository.GetReconStatusMasterDetails(businessdate, network);
        }

        public string Raise_request_reconServices(int recongroupid, string businessdate, int requestby)
        {
            return ObjCommonRepository.Raise_request_recon(recongroupid, businessdate, requestby);
        }
    }
}

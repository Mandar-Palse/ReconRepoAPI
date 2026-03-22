using PaysisReconAPI.DatabaseContext;
using PaysisReconAPI.Model;
using PaysisReconAPI.Repository;

namespace PaysisReconAPI.Service
{
    public class UploadFileServices
    {
        private readonly UploadFileRepository ObjCommonRepository;


        public UploadFileServices(IDataDbContext db) 
        { 
            ObjCommonRepository = new UploadFileRepository(db);
        }

        public List<WatcherModel> GetFilewatcher_DetailsNew()
        {
            return ObjCommonRepository.GetFilewatcherDetailsNew();
        }

        public string CheckFileExistornotServices(string filename)
        {
            return ObjCommonRepository.CheckFileExistornot(filename);
        }
        public string insert_into_fileuploaddataServices(int fileid, string filename, string filepath, string filepath_archive)
        {
            return ObjCommonRepository.insert_into_fileuploaddata(fileid, filename, filepath, filepath_archive);
        }

        public List<Fileuploaddata> GetFileUploadDataListFromUploadServices(string uploaddate, string network)
        {
            return ObjCommonRepository.GetFileUploadDataListFromUpload(uploaddate, network);
        }

    }
}

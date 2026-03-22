namespace PaysisReconAPI.Model
{
    public class WatcherModel
    {

        public string FilePath { get; set; }
        public string FilenameFormat { get; set; }
        public int FileBankMappingID { get; set; }
        public int BankID { get; set; }
        public int FMID { get; set; }

        public string archivepath { get; set; }
    }
}

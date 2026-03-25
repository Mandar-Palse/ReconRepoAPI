namespace PaysisReconAPI.Model
{
    public class VouchersModel
    {
        public Int32 reportstatusid { get; set; }
        public Int32 reportid { get; set; }
        public string reportname { get; set; }
        public string reportstatus { get; set; }
        public string reportdate { get; set; }
        public string reportedon { get; set; }
        public string generatedOn { get; set; }
        public string reportfilepath { get; set; }
        public string ttumreportfilepath { get; set; }
        public Int32 ftmid { get; set; }
        public string ActionFlag { get; set; }
        public string FileName { get; set; }
    }
}

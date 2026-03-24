namespace PaysisReconAPI.Model
{
    public class Fileuploaddatalist
    {
        public string _businessdate { get; set; }
        public List<Fileuploaddata> fileuploaddatalist { get; set; }
        public string userid { get; set; }
        public bool ReconConfirmation { get; set; }
        public int fileuploaddatalistCount { get; set; }
    }

    public class Fileuploaddata
    {
        public int id { get; set; }
        public string filedescription { get; set; }
        public string filename { get; set; }
        public string businessdate { get; set; }
        public string temp_tbl_count { get; set; }
        public string data_tbl_count { get; set; }
        public string final_tbl_count { get; set; }
        public string succrecord { get; set; }
        public string totrecord { get; set; }
        public bool ReconConfirmation { get; set; }
        public string error_message { get; set; }
        public string Average7DayCount { get; set; }

    }
}

namespace PaysisReconAPI.Model
{
    public class ErrorMasterModel
    {
        public string function_name { get; set; }
        public string errorcode { get; set; }
        public string error_message { get; set; }
        public string exception_context { get; set; }
        public string errordescription { get; set; }
    }
}

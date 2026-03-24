namespace PaysisReconAPI.Model
{
    public class RaiseReconRequestDto
    {
        public int ReconGroupId { get; set; }
        public DateTime BusinessDate { get; set; }
        public int RequestedBy { get; set; }
    }
}

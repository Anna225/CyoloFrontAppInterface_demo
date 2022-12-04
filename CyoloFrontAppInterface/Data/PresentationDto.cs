namespace CyoloFrontAppInterface.Data
{
    public class PresentationDto
    {
        public int ID { get; set; }
        public int CourtCaseNo { get; set; }
        public int LawyerId { get; set; }
        public int Available { get; set; }
        public string? ReceiveRef { get; set; }
        public string? Comments { get; set; }
    }
}

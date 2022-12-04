namespace CyoloFrontAppInterface.Data
{
    public class CourtCaseAgendaDto
    {
        public int id { get; set; }
        public string? courtCaseNo { get; set; }
        public string? hearingGeneral { get; set; }
        public string? hearingDate { get; set; }
        public string? hearingTime { get; set; }
        public string? hearingType { get; set; }
        public string? chamberId { get; set; }
        public string? country { get; set; }
    }
}
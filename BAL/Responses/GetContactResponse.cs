namespace BAL.Responses
{
    public class GetContactResponse
    {
        public Guid Id { get; set; }
        public string? ContactValue { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? ContactType { get; set; }

    }
}

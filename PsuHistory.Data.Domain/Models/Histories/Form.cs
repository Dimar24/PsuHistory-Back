namespace PsuHistory.Data.Domain.Models.Histories
{
    public class Form : KeyGuidEntityBase
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
    }
}

namespace Domain
{
    public class ActionTask
    {
        public Guid Id { get; set; }
        public string ParentID { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompletedDate { get; set; }
    }
}
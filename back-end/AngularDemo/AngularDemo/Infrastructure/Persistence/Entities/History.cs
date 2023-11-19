namespace CEZ.AngularDemo.WebAPI.Infrastructure.Persistence.Entities
{
    public class History
    {
        public History() { }
        public int Id { get; set; }

        public int ChangeType { get; set; }
        public string? PrevValue { get; set; }
        public string? CurrValue { get; set; }

        public DateTime ChangeDate { get; set; }
        
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}

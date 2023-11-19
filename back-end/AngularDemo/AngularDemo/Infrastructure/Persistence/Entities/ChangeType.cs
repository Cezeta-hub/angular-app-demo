namespace CEZ.AngularDemo.WebAPI.Infrastructure.Persistence.Entities
{
    public class ChangeType
    {
        public ChangeType()
        {
            Histories = new HashSet<History>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<History> Histories { get; set; }
    }
}

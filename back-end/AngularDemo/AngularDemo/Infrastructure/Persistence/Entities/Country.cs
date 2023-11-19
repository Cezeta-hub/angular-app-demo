namespace CEZ.AngularDemo.WebAPI.Infrastructure.Persistence.Entities
{
    public class Country
    {
        public Country()
        {
            Users = new HashSet<User>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Value { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}

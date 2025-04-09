namespace MyServer.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public virtual ICollection<Module> Modules { get; set; } = new List<Module>();
    }
}

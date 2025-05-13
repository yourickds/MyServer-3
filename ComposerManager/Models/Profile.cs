namespace ComposerManager.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public virtual ICollection<Module> Modules { get; set; } = new List<Module>();
        public virtual ICollection<Domain> Domains { get; set; } = new List<Domain>();
    }
}

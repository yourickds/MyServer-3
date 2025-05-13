namespace PHPManager.Models
{
    public class Module
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Dir { get; set; }
        public string? Variable { get; set; }
        public virtual ICollection<Profile> Profiles { get; set; } = new List<Profile>();
    }
}

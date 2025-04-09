namespace MyServer.Models
{
    public class Domain
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? DocumentRoot { get; set; }
    }
}

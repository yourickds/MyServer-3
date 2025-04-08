namespace MyServer.Models
{
    public class Favourite
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string FilePath { get; set; }

        public string? Arguments { get; set; }

        public bool InBrowser { get; set; }
    }
}

namespace MyServer.Models
{
    class Service
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string FilePath { get; set; }

        public string? Arguments { get; set; }

        public bool Startup { get; set; }

        public bool Status { get; set; }

        public int? Pid { get; set; }
    }
}

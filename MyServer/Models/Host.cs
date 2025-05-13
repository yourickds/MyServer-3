
namespace MyServer.Models
{
    public class Host
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Ip { get; set; }
    }
}

namespace PHPManager.Models
{
    public class Domain
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? DocumentRoot { get; set; }
        public required int ProfileId { get; set; } // Внешний ключ

        public required virtual Profile Profile { get; set; } // Навигационное свойство
    }
}

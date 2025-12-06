// Models/Card.cs
namespace Kanban.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;       // antes era Titulo
        public string Description { get; set; } = string.Empty; // antes era Descricao
        public DateTime CreatedAt { get; set; }

        public int ColumnId { get; set; }
        public Column? Column { get; set; }
    }
}

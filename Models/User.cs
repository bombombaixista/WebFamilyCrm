namespace Kanban.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Nome { get; set; } = "";
        public string Login { get; set; } = "";
        public string PasswordHash { get; set; } = "";
    }
}

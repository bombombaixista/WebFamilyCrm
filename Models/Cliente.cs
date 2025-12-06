using System.Text.RegularExpressions;

namespace Kanban.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }

        public int? GrupoId { get; set; }
        public Grupo? Grupo { get; set; }
    }
}

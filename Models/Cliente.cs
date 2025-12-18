using System.Text.RegularExpressions;

namespace Kanban.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        // Dados principais
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;

        // Empresa
        public string Empresa { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;

        // Informações comerciais
        public string Origem { get; set; } = string.Empty; // Site, WhatsApp, Indicação
        public string Status { get; set; } = "Ativo";       // Ativo / Inativo
        public string Observacoes { get; set; } = string.Empty;

        // Controle
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}

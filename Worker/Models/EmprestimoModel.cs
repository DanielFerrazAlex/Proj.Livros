namespace Worker.Models
{
    public class EmprestimoModel
    {
        public string Aluno { get; set; } = string.Empty;
        public string Livro { get; set; } = string.Empty;
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
    }
}

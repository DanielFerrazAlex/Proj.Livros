namespace Backend.Models
{
    public class LivrosModel
    {
        public Guid Id { get; set; }
        public string NomeLivro { get; set; }
        public string NomeAutor { get; set; }
        public string Genero { get; set; }
        public bool Active { get; set; }
    }
}

namespace Backend.Models.DTO_s
{
    public class AutoresDTO
    {
        public string Autor { get; set; }
        public List<LivrosDTO>? Livros { get; set; }
    }
}

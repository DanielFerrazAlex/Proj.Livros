using Backend.Models;
using Backend.Models.DTO_s;

namespace Backend.Mappers
{
    public class LivrosMapper
    {
        public static LivrosDTO ToDTO(LivrosModel livro)
        {
            return new LivrosDTO
            {
                Id = livro.Id,
                Livro = livro.NomeLivro,
                Autor = livro.NomeAutor,
                Genero = livro.Genero
            };
        }
    }
}

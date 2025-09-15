using Backend.Models;
using Backend.Models.DTO_s;

namespace Backend.Repositories.Interfaces
{
    public interface ILivrosRepository
    {
        Task<ResponseModel<List<LivrosDTO>>> SelecionarLivrosGenero(string genero);
        Task<ResponseModel<List<LivrosDTO>>> SelecionarLivrosPorAutor(string autor);
        Task<object> CadastrarLivro(LivrosModel livro);
        Task<object> DeletarLivroPorNomeLivro(string nomeLivro);
    }
}

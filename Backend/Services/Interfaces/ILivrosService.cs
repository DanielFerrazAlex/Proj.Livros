using Backend.Models;
using Backend.Models.DTO_s;

namespace Backend.Services.Interfaces
{
    public interface ILivrosService
    {
        Task<ResponseModel<List<LivrosDTO>>> SelecionarLivrosPorGenero(string genero);
        Task<ResponseModel<List<LivrosDTO>>> SelecionarLivrosPorAutor(string autor);
        Task<ResponseModel<object>> CadastrarLivro(LivrosModel livro);
        Task<ResponseModel<object>> DeletarLivroPorNome(string nomeLivro);
    }
}

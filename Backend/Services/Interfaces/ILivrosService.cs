using Backend.Models;
using Backend.Models.DTO_s;

namespace Backend.Services.Interfaces
{
    public interface ILivrosService
    {
        Task<ResponseModel<List<LivrosDTO>>> SelecionarLivros();
        Task<ResponseModel<List<LivrosDTO>>> SelecionarLivrosPorTermo(string termo);
        Task<ResponseModel<LivrosDTO>> SelecionarLivrosPorId(Guid id);
        Task<ResponseModel<object>> CadastrarLivro(LivrosModel livro);
        Task<ResponseModel<object>> EditarLivro(Guid id, LivrosModel livro);
        Task<ResponseModel<object>> DeletarLivro(Guid id);
    }
}

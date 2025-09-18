using Backend.Models;

namespace Backend.Repositories.Interfaces
{
    public interface ILivrosRepository
    {
        Task<List<LivrosModel>> SelecionarLivros();
        Task<List<LivrosModel>> SelecionarLivrosPorTermo(string termo);
        Task<LivrosModel> SelecionarLivrosPorId(Guid id);
        Task<int> CadastrarLivro(LivrosModel livro);
        Task<int> EditarLivro(Guid id, LivrosModel livro);
        Task<int> DeletarLivro(Guid id);
    }
}

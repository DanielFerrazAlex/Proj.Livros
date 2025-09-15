using Backend.Models;

namespace Backend.Repositories.Interfaces
{
    public interface ILivrosRepository
    {
        Task<List<LivrosModel>> SelecionarLivrosPorGenero(string genero);
        Task<List<LivrosModel>> SelecionarLivrosPorAutor(string autor);
        Task<int> CadastrarLivro(LivrosModel livro);
        Task<int> DeletarLivroPorNomeLivro(string nomeLivro);
    }
}

using Backend.Mappers;
using Backend.Models;
using Backend.Models.DTO_s;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;

namespace Backend.Services
{
    public class LivrosService : ILivrosService
    {
        private readonly ILivrosRepository _repository;
        public LivrosService(ILivrosRepository repository) 
        {
            _repository = repository;
        }
        public async Task<ResponseModel<List<LivrosDTO>>> SelecionarLivrosPorAutor(string autor)
        {
            try
            {
                List<LivrosModel>? resposta = await _repository.SelecionarLivrosPorAutor(autor);

                if (resposta.Count == 0)
                {
                    return new ResponseModel<List<LivrosDTO>>
                    {
                        Success = false,
                        Message = $"Este autor: {autor}. Não tem livros cadastrados ou não existe em nosso banco de dados",
                        Data = null!
                    };
                }

                List<LivrosDTO> mapper = resposta.Select(
                    x => LivrosMapper.ToDTO(x)
                 ).ToList();

                return new ResponseModel<List<LivrosDTO>>
                {
                    Success = true,
                    Message = "Livros encontrados com sucesso.",
                    Data = mapper
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<LivrosDTO>> 
                { 
                    Success = false, 
                    Message = $"Erro ao buscar livros: {ex.Message}", 
                    Data = null! 
                };
            }
        }

        public async Task<ResponseModel<List<LivrosDTO>>> SelecionarLivrosPorGenero(string genero)
        {
            try
            {
                List<LivrosModel> resposta = await _repository.SelecionarLivrosPorGenero(genero);

                if (resposta.Count == 0)
                {
                    return new ResponseModel<List<LivrosDTO>>
                    {
                        Success = false,
                        Message = $"Nenhum livro disponível para o gênero: '{genero}'.",
                        Data = null!
                    };
                }

                List<LivrosDTO> mapper = resposta.Select(
                    x => LivrosMapper.ToDTO(x)
                 ).ToList();

                return new ResponseModel<List<LivrosDTO>>
                {
                    Success = true,
                    Message = "Livros encontrados com sucesso.",
                    Data = mapper
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<LivrosDTO>>
                {
                    Success = false,
                    Message = $"Erro ao buscar livros: {ex.Message}",
                    Data = null!
                };
            }
        }

        public async Task<ResponseModel<object>> CadastrarLivro(LivrosModel livro)
        {
            try
            {
                int resposta = await _repository.CadastrarLivro(livro);

                if (resposta > 0)
                {
                    return new ResponseModel<object>
                    {
                        Success = true,
                        Message = "Livro cadastrado com sucesso!"
                    };
                }

                return new ResponseModel<object>
                {
                    Success = false,
                    Message = "Falha ao cadastrar livro.",
                };

            }
            catch (Exception ex)
            {
                return new ResponseModel<object>
                {
                    Success = false,
                    Message = $"Erro ao cadastrar livro: {ex.Message}",
                };
            }
        }

        public async Task<ResponseModel<object>> DeletarLivroPorNome(string nomeLivro)
        {
            try
            {
                int resposta = await _repository.DeletarLivroPorNomeLivro(nomeLivro);

                if (resposta == 0)
                {
                    return new ResponseModel<object>
                    {
                        Success = false,
                        Message = $"Nenhum livro encontrado com o nome '{nomeLivro}'.",
                    };
                }

                return new ResponseModel<object>
                {
                    Success = true,
                    Message = $"Livro '{nomeLivro}' foi deletado com sucesso!",
                };
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}

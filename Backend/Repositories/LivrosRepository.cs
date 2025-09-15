using Backend.Models;
using Backend.Models.DTO_s;
using Backend.Repositories.Interfaces;
using Npgsql;

namespace Backend.Repositories
{
    public class LivrosRepository : ILivrosRepository
    {
        private string _connection;

        public LivrosRepository(string connection)
        {
            _connection = connection;
        }

        public async Task<ResponseModel<List<LivrosDTO>>> SelecionarLivrosGenero(string genero)
        {
            try
            {
                List<LivrosDTO> livros = new List<LivrosDTO>();
                string query = @"SELECT Id, NomeLivro, NomeAutor, Genero FROM Livros WHERE Genero = @Genero";

                using (NpgsqlConnection conn = new NpgsqlConnection(_connection))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("Genero", genero);
                        await using NpgsqlDataReader reader = cmd.ExecuteReader();

                        while (await reader.ReadAsync())
                        {
                            livros.Add(new LivrosDTO
                            {
                                Livro = reader.GetString(0),
                                Autor = reader.GetString(1),
                                Genero = reader.GetString(2),
                            });
                        }

                        if (livros.Count == 0)
                        {
                            return new ResponseModel<List<LivrosDTO>>
                            {
                                Success = false,
                                Message = $"Nenhum livro disponível para o gênero: '{genero}'.",
                                Data = null!
                            };
                        }

                        return new ResponseModel<List<LivrosDTO>>
                        {
                            Success = true,
                            Message = "Livros encontrados com sucesso.",
                            Data = livros
                        };
                    }
                }
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

        public async Task<ResponseModel<List<LivrosDTO>>> SelecionarLivrosPorAutor(string autor)
        {
            try
            {
                List<LivrosDTO> livros = new List<LivrosDTO>();
                string query = @"SELECT Id, NomeLivro, NomeAutor, Genero FROM Livros WHERE NomeAutor = @NomeAutor";

                using (NpgsqlConnection conn = new NpgsqlConnection(_connection))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("NomeAutor", autor);
                        await using NpgsqlDataReader reader = cmd.ExecuteReader();

                        while (await reader.ReadAsync())
                        {
                            livros.Add(new LivrosDTO
                            {
                                Livro = reader.GetString(0),
                                Autor = reader.GetString(1),
                                Genero = reader.GetString(2),
                            });
                        }

                        if (livros.Count == 0)
                        {
                            return new ResponseModel<List<LivrosDTO>>
                            {
                                Success = false,
                                Message = $"Nenhum livro disponível para o gênero: '{autor}'.",
                                Data = null!
                            };
                        }

                        return new ResponseModel<List<LivrosDTO>>
                        {
                            Success = true,
                            Message = "Livros encontrados com sucesso.",
                            Data = livros
                        };
                    }
                }
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

        public async Task<object> CadastrarLivro(LivrosModel livro)
        {
			try
			{
                string query = @"INSERT INTO Livros (NomeLivro, NomeAutor, Genero, Active) VALUES (@NomeLivro, @NomeAutor, @Genero, @Active)";

                using (NpgsqlConnection conn = new NpgsqlConnection(_connection))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn)) 
                    {
                        cmd.Parameters.AddWithValue("NomeLivro", livro.NomeLivro);
                        cmd.Parameters.AddWithValue("NomeAutor", livro.NomeAutor);
                        cmd.Parameters.AddWithValue("@Genero", livro.Genero);
                        cmd.Parameters.AddWithValue("@Active", livro.Active);

                        await cmd.ExecuteNonQueryAsync();

                        return new
                        {
                            sucess = true,
                            message = "Livro cadastrado com sucesso!"
                        };
                    }
                }

            }
			catch (Exception ex)
			{
                return new
                {
                    sucess = false,
                    message = $"Erro ao cadastrar livro: {ex.Message}"
                };
			}
        }

        public async Task<object> DeletarLivroPorNomeLivro(string nomeLivro)
        {
            try
            {
                string query = @"DELETE FROM livros WHERE Nomelivro = @NomeLivro";

                using (NpgsqlConnection conn = new NpgsqlConnection(_connection))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("NomeLivro", nomeLivro);
                        int rows = await cmd.ExecuteNonQueryAsync();

                        if (rows == 0)
                        {
                            return new
                            {
                                sucess = false,
                                message = $"Nenhum livro encontrado com o nome '{nomeLivro}'."
                            };
                        }

                        return new
                        {
                            sucess = true,
                            message = $"Livro '{nomeLivro}' deletado com sucesso!"
                        };
                    }
                }
            }

            catch (Exception ex)
            {
                return new
                {
                    sucess = false,
                    message = $"Erro ao deletar livro: {ex.Message}"
                };
            }
        }
    }
}

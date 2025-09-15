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

        public async Task<List<LivrosModel>> SelecionarLivrosPorGenero(string genero)
        {
            List<LivrosModel> livros = new List<LivrosModel>();
            string query = @"SELECT Id, NomeLivro, NomeAutor, Genero FROM Livros WHERE Genero = @Genero";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connection))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("Genero", genero);
                    await using NpgsqlDataReader reader = cmd.ExecuteReader();

                    while (await reader.ReadAsync())
                    {
                        livros.Add(new LivrosModel
                        {
                            NomeLivro = reader.GetString(0),
                            NomeAutor = reader.GetString(1),
                            Genero = reader.GetString(2),
                        });
                    }

                    return livros;
                }
            }
        }

        public async Task<List<LivrosModel>> SelecionarLivrosPorAutor(string autor)
        {
            List<LivrosModel> livros = new List<LivrosModel>();
            string query = @"SELECT Id, NomeLivro, NomeAutor, Genero FROM Livros WHERE NomeAutor = @NomeAutor";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connection))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("NomeAutor", autor);
                    await using NpgsqlDataReader reader = cmd.ExecuteReader();

                    while (await reader.ReadAsync())
                    {
                        livros.Add(new LivrosModel
                        {
                            NomeLivro = reader.GetString(0),
                            NomeAutor = reader.GetString(1),
                            Genero = reader.GetString(2),
                        });
                    }
                    return livros;
                }
            }
        }

        public async Task<int> CadastrarLivro(LivrosModel livro)
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

                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<int> DeletarLivroPorNomeLivro(string nomeLivro)
        {
            string query = @"DELETE FROM livros WHERE Nomelivro = @NomeLivro";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connection))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("NomeLivro", nomeLivro);
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}

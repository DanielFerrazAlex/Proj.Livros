using Backend.Models;
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
            string query = @"SELECT id, livro, autor, genero FROM livros WHERE genero = @genero";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connection))
            {
                await conn.OpenAsync();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@genero", genero);
                    await using NpgsqlDataReader reader = cmd.ExecuteReader();

                    while (await reader.ReadAsync())
                    {
                        livros.Add(new LivrosModel
                        {
                            Id = reader.GetGuid(0),
                            NomeLivro = reader.GetString(1),
                            NomeAutor = reader.GetString(2),
                            Genero = reader.GetString(3),
                        });
                    }

                    return livros;
                }
            }
        }

        public async Task<List<LivrosModel>> SelecionarLivrosPorAutor(string autor)
        {
            List<LivrosModel> livros = new List<LivrosModel>();
            string query = @"SELECT id, livro, autor, genero FROM Livros WHERE autor = @autor";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connection))
            {
                await conn.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("autor", autor);
                    await using NpgsqlDataReader reader = cmd.ExecuteReader();

                    while (await reader.ReadAsync())
                    {
                        livros.Add(new LivrosModel
                        {
                            Id = reader.GetGuid(0),
                            NomeLivro = reader.GetString(1),
                            NomeAutor = reader.GetString(2),
                            Genero = reader.GetString(3),
                        });
                    }
                    return livros;
                }
            }
        }

        public async Task<int> CadastrarLivro(LivrosModel livro)
        {
            string query = @"INSERT INTO livros (livro, autor, genero, active) VALUES (@livro, @autor, @genero, @active)";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connection))
            {
                await conn.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("livro", livro.NomeLivro);
                    cmd.Parameters.AddWithValue("@autor", livro.NomeAutor);
                    cmd.Parameters.AddWithValue("@genero", livro.Genero);
                    cmd.Parameters.AddWithValue("@active", livro.Active);

                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<int> DeletarLivroPorNomeLivro(string nomeLivro)
        {
            string query = @"DELETE FROM livros WHERE livro = @livro";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connection))
            {
                await conn.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("livro", nomeLivro);
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}

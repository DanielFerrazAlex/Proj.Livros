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

        public async Task<List<LivrosModel>> SelecionarLivros()
        {
            List<LivrosModel> livros = new List<LivrosModel>();
            string query = @"SELECT id, livro, autor, genero FROM livros";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connection))
            {
                await conn.OpenAsync();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    await using NpgsqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var livro = new LivrosModel
                        {
                            Id = reader.GetGuid(0),
                            NomeLivro = reader.GetString(1),
                            NomeAutor = reader.GetString(2),
                            Genero = reader.GetString(3),
                        };

                        livros.Add(livro);
                    }
                }
            }

            return livros;
        }

        public async Task<LivrosModel> SelecionarLivrosPorId(Guid id)
        {
            string query = @"SELECT id, livro, autor, genero FROM livros WHERE id = @id";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connection))
            {
                await conn.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    await using NpgsqlDataReader reader = cmd.ExecuteReader();

                    while (await reader.ReadAsync())
                    {
                        return new LivrosModel
                        {
                            Id = reader.GetGuid(0),
                            NomeLivro = reader.GetString(1),
                            NomeAutor = reader.GetString(2),
                            Genero = reader.GetString(3),
                        };
                    }
                    return null!;
                }
            }
        }

        public async Task<List<LivrosModel>> SelecionarLivrosPorTermo(string termo)
        {
            List<LivrosModel> livros = new List<LivrosModel>();
            string query = 
            @"
                SELECT id, livro, autor, genero, active
                FROM livros
                WHERE livro ILIKE @termo
                OR autor ILIKE @termo
                OR genero ILIKE @termo
            ";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connection))
            {
                await conn.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@termo", $"%{termo}%");
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
                    cmd.Parameters.AddWithValue("@livro", livro.NomeLivro);
                    cmd.Parameters.AddWithValue("@autor", livro.NomeAutor);
                    cmd.Parameters.AddWithValue("@genero", livro.Genero);
                    cmd.Parameters.AddWithValue("@active", livro.Active);

                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> EditarLivro(Guid id, LivrosModel livro)
        {
            string query = @" UPDATE livros SET livro = @livro, autor = @autor, genero = @genero WHERE id = @id";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connection))
            {
                await conn.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@livro", livro.NomeLivro);
                    cmd.Parameters.AddWithValue("@autor", livro.NomeAutor);
                    cmd.Parameters.AddWithValue("@genero", livro.Genero);
                    cmd.Parameters.AddWithValue("@active", livro.Active);
                    cmd.Parameters.AddWithValue("@id", id);

                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> DeletarLivro(Guid id)
        {
            string query = @"DELETE FROM livros WHERE id = @id";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connection))
            {
                await conn.OpenAsync();

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}

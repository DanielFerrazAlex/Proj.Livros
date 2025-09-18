using Npgsql;
using Worker.Models;

namespace Worker.Repositories
{
    public class EmprestimoRepository
    {
        private string _connection;

        public EmprestimoRepository(string connection)
        {
            _connection = connection;
        }
        public async Task<List<EmprestimoModel>> SelecionarAtrasados()
        {
            string query = @"SELECT e.aluno, l.livro, e.data_emprestimo, e.data_devolucao FROM emprestimos e INNER JOIN livros l ON e.livro_id = l.id WHERE e.devolvido = false AND e.data_devolucao < NOW()";

            var lista = new List<EmprestimoModel>();

            using (var conn = new NpgsqlConnection(_connection))
            {
                await conn.OpenAsync();

                using var cmd = new NpgsqlCommand(query, conn);
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    lista.Add(new EmprestimoModel
                    {
                        Aluno = reader.GetString(0),
                        Livro = reader.GetString(1),
                        DataEmprestimo = reader.GetDateTime(2),
                        DataDevolucao = reader.GetDateTime(3)
                    });
                }
            }

            return lista;
        }

    }
}

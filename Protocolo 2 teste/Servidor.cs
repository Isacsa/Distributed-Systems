using System; // Usado para tipos básicos e funcionalidades do .NET
using System.Collections.Generic; // Usado para trabalhar com coleções como List<T>
using System.Data; // Usado para trabalhar com banco de dados
using System.Data.SqlClient; 
using System.Linq; // Usado para realizar operações de consulta LINQ
using System.Threading.Tasks; // Usado para trabalhar com tarefas assíncronas

namespace Protocolo_2_teste
{
    public class Servidor
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"Protocolo 2 teste\";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // Substitua pelo seu connection string

        public bool AutenticarOperador(string nome, string senha)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Operadores WHERE Nome = @Nome AND Senha = @Senha";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nome", nome);
                    command.Parameters.AddWithValue("@Senha", senha);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }
    }

}

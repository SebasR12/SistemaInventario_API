using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SistemaInventario_API.Data;
using System.Data;

namespace SistemaInventario_API.Repositories
{
    public class Login_Repositorio
    {
        private readonly ApplicationDbContext _context;

        public Login_Repositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DataSet> Login(string correo, string contrasena)
        {
            var dataSet = new DataSet();

            using (var connection = (SqlConnection)_context.Database.GetDbConnection())
            {
                using (var command = new SqlCommand("LOGIN_USUARIO", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@correo", correo));
                    command.Parameters.Add(new SqlParameter("@contrasena", contrasena));

                    using (var adapter = new SqlDataAdapter(command))
                    {
                        await Task.Run(() => adapter.Fill(dataSet));
                    }
                }
            }

            return dataSet;
        }
    }
}

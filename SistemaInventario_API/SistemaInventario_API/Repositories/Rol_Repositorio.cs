using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SistemaInventario_API.Data;
using System.Data;

namespace SistemaInventario_API.Repositories
{
    public class Rol_Repositorio
    {
        private readonly ApplicationDbContext _context;

        public Rol_Repositorio(ApplicationDbContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<DataSet> EjecutarSpRol(
            int Proceso,
            int idRol,
            string nombreRol,
            string descripcion
        )
        {
            var procesoParam = new SqlParameter("@PROCESO", SqlDbType.TinyInt) { Value = Proceso };
            var idParam = new SqlParameter("@idRol", SqlDbType.Int) { Value = idRol };
            var nombreParam = new SqlParameter("@nombreRol", SqlDbType.VarChar, 50)
            {
                Value = (object)nombreRol ?? DBNull.Value
            };
            var descParam = new SqlParameter("@descripcion", SqlDbType.VarChar, 200)
            {
                Value = (object)descripcion ?? DBNull.Value
            };

            var respuestaParam = new SqlParameter("@RESPUESTA", SqlDbType.VarChar, 200)
            {
                Direction = ParameterDirection.Output
            };

            var ds = new DataSet();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "MAN_ROL";
                command.CommandTimeout = 360;

                command.Parameters.Add(idParam);
                command.Parameters.Add(nombreParam);
                command.Parameters.Add(descParam);
                command.Parameters.Add(procesoParam);
                command.Parameters.Add(respuestaParam);

                using (var adapter = new SqlDataAdapter((SqlCommand)command))
                {
                    await Task.Run(() =>
                    {
                        adapter.Fill(ds);
                    });
                }
            }

            return ds;
        }
    }
}

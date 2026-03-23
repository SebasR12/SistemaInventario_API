using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SistemaInventario_API.Data;
using System.Data;

namespace SistemaInventario_API.Repositories
{
    public class categoria_Repositorio
    {
        private readonly ApplicationDbContext _context;

        public categoria_Repositorio(ApplicationDbContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<DataSet> EjecutarSpCategoria(int Proceso, int IDCategoria, string Nombre, string Descripcion)
        {
            var procesoParam = new SqlParameter("@PROCESO", SqlDbType.Int) { Value = Proceso };
            var idCategoriaParam = new SqlParameter("@idCategoria", SqlDbType.Int) { Value = IDCategoria };
            var nombreParam = new SqlParameter("@nombre", SqlDbType.VarChar, 100) { Value = Nombre };
            var descripcionParam = new SqlParameter("@descripcion", SqlDbType.VarChar, 255) { Value = Descripcion };

            var respuestaParam = new SqlParameter("@RESPUESTA", SqlDbType.VarChar, 200);
            respuestaParam.Direction = ParameterDirection.Output;

            var dataSet = new DataSet();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;

                command.CommandText = "MAN_CATEGORIA";
                command.Parameters.Add(procesoParam);
                command.Parameters.Add(idCategoriaParam);
                command.Parameters.Add(nombreParam);
                command.Parameters.Add(descripcionParam);
                command.Parameters.Add(respuestaParam);

                using (var dataAdapter = new SqlDataAdapter((SqlCommand)command))
                {
                    await Task.Run(() =>
                    {
                        dataAdapter.Fill(dataSet);
                    });
                }
                return dataSet;
            }
        }
    }
}

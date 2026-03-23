using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SistemaInventario_API.Data;
using System.Data;

namespace SistemaInventario_API.Repositories
{
    public class factura_Repositorio
    {
        private readonly ApplicationDbContext _context;

        public factura_Repositorio(ApplicationDbContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<DataSet> EjecutarSpFactura(
            int Proceso,
            int idFactura,
            int idCliente,
            decimal total,
            int idUsuario
        )
        {
            // Parámetros del procedimiento almacenado
            var procesoParam = new SqlParameter("@PROCESO", SqlDbType.Int) { Value = Proceso };
            var idFacturaParam = new SqlParameter("@idFactura", SqlDbType.Int) { Value = idFactura };
            var idClienteParam = new SqlParameter("@idCliente", SqlDbType.Int) { Value = idCliente };
            var totalParam = new SqlParameter("@total", SqlDbType.Decimal) { Value = total };
            var idUsuarioParam = new SqlParameter("@idUsuario", SqlDbType.Int) { Value = idUsuario };

            // Parámetro de salida
            var respuestaParam = new SqlParameter("@RESPUESTA", SqlDbType.VarChar, 200)
            {
                Direction = ParameterDirection.Output
            };

            var dataSet = new DataSet();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "MAN_FACTURA";

                // Agregar los parámetros al comando
                command.Parameters.Add(procesoParam);
                command.Parameters.Add(idFacturaParam);
                command.Parameters.Add(idClienteParam);
                command.Parameters.Add(totalParam);
                command.Parameters.Add(idUsuarioParam);
                command.Parameters.Add(respuestaParam);

                // Ejecutar la consulta y llenar el DataSet
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

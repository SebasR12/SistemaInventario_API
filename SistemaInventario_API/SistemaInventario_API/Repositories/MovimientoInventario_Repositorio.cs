using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SistemaInventario_API.Data;
using System.Data;

namespace SistemaInventario_API.Repositories
{
    public class MovimientoInventario_Repositorio
    {
        private readonly ApplicationDbContext _context;

        public MovimientoInventario_Repositorio(ApplicationDbContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<DataSet> EjecutarSpMovimientoInventario(
            int Proceso,
            int idMovimiento,
            int idProducto,
            string tipoMovimiento,
            int cantidad,
            int idUsuario
        )
        {
            var procesoParam = new SqlParameter("@PROCESO", SqlDbType.TinyInt) { Value = Proceso };
            var idMovParam = new SqlParameter("@idMovimiento", SqlDbType.Int) { Value = idMovimiento };
            var idProdParam = new SqlParameter("@idProducto", SqlDbType.Int) { Value = idProducto };
            var tipoParam = new SqlParameter("@tipoMovimiento", SqlDbType.VarChar, 20) { Value = tipoMovimiento };
            var cantParam = new SqlParameter("@cantidad", SqlDbType.Int) { Value = cantidad };
            var idUsuParam = new SqlParameter("@idUsuario", SqlDbType.Int) { Value = idUsuario };

            var respuestaParam = new SqlParameter("@RESPUESTA", SqlDbType.VarChar, 200);
            respuestaParam.Direction = ParameterDirection.Output;

            var dataSet = new DataSet();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "MAN_MOVIMIENTOINVENTARIO";
                command.CommandTimeout = 360;

                command.Parameters.Add(idMovParam);
                command.Parameters.Add(idProdParam);
                command.Parameters.Add(tipoParam);
                command.Parameters.Add(cantParam);
                command.Parameters.Add(idUsuParam);
                command.Parameters.Add(procesoParam);
                command.Parameters.Add(respuestaParam);

                using (var adapter = new SqlDataAdapter((SqlCommand)command))
                {
                    await Task.Run(() =>
                    {
                        adapter.Fill(dataSet);
                    });
                }
            }

            return dataSet;
        }
    }
}

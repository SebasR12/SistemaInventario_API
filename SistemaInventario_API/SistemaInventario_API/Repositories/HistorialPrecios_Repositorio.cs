using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SistemaInventario_API.Data;
using System.Data;

namespace SistemaInventario_API.Repositories
{
    public class HistorialPrecios_Repositorio
    {
        private readonly ApplicationDbContext _context;

        public HistorialPrecios_Repositorio(ApplicationDbContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<DataSet> EjecutarSpHistorialPrecios(
            int Proceso,
            int idHistorial,
            int idProducto,
            decimal precioCompra,
            decimal precioVenta
        )
        {
            var procesoParam = new SqlParameter("@PROCESO", SqlDbType.TinyInt) { Value = Proceso };
            var idHistParam = new SqlParameter("@idHistorial", SqlDbType.Int) { Value = idHistorial };
            var idProdParam = new SqlParameter("@idProducto", SqlDbType.Int) { Value = idProducto };
            var precioCParam = new SqlParameter("@precioCompra", SqlDbType.Decimal)
            {
                Precision = 18,
                Scale = 2,
                Value = precioCompra
            };
            var precioVParam = new SqlParameter("@precioVenta", SqlDbType.Decimal)
            {
                Precision = 18,
                Scale = 2,
                Value = precioVenta
            };

            var respuestaParam = new SqlParameter("@RESPUESTA", SqlDbType.VarChar, 200);
            respuestaParam.Direction = ParameterDirection.Output;

            var dataSet = new DataSet();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "MAN_HISTORIALPRECIOS";
                command.CommandTimeout = 360;

                command.Parameters.Add(idHistParam);
                command.Parameters.Add(idProdParam);
                command.Parameters.Add(precioCParam);
                command.Parameters.Add(precioVParam);
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

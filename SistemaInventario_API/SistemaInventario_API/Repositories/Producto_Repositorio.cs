using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SistemaInventario_API.Data;
using System.Data;

namespace SistemaInventario_API.Repositories
{
    public class Producto_Repositorio
    {
        private readonly ApplicationDbContext _context;

        public Producto_Repositorio(ApplicationDbContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<DataSet> EjecutarSpProducto(
            int? idProducto,
            string nombre,
            string descripcion,
            decimal precioCompra,
            decimal precioVenta,
            int stockActual,
            int stockMinimo,
            int idCategoria,
            int idProveedor,
            int Proceso
        )
        {
            var idProdParam = new SqlParameter("@idProducto", SqlDbType.Int) { Value = (object?)idProducto ?? DBNull.Value };
            var nombreParam = new SqlParameter("@nombre", SqlDbType.VarChar, 150) { Value = (object?)nombre ?? DBNull.Value };
            var descParam = new SqlParameter("@descripcion", SqlDbType.VarChar, 300) { Value = (object?)descripcion ?? DBNull.Value };
            var precioCParam = new SqlParameter("@precioCompra", SqlDbType.Decimal) { Value = precioCompra };
            var precioVParam = new SqlParameter("@precioVenta", SqlDbType.Decimal) { Value = precioVenta };
            var stockActParam = new SqlParameter("@stockActual", SqlDbType.Int) { Value = stockActual };
            var stockMinParam = new SqlParameter("@stockMinimo", SqlDbType.Int) { Value = stockMinimo };
            var idCatParam = new SqlParameter("@idCategoria", SqlDbType.Int) { Value = idCategoria };
            var idProvParam = new SqlParameter("@idProveedor", SqlDbType.Int) { Value = idProveedor };
            var procesoParam = new SqlParameter("@PROCESO", SqlDbType.TinyInt) { Value = Proceso };

            // Parámetro de salida
            var respuestaParam = new SqlParameter("@RESPUESTA", SqlDbType.VarChar, 200);
            respuestaParam.Direction = ParameterDirection.Output;

            var dataSet = new DataSet();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "MAN_PRODUCTO";
                command.CommandTimeout = 360;

                command.Parameters.Add(idProdParam);
                command.Parameters.Add(nombreParam);
                command.Parameters.Add(descParam);
                command.Parameters.Add(precioCParam);
                command.Parameters.Add(precioVParam);
                command.Parameters.Add(stockActParam);
                command.Parameters.Add(stockMinParam);
                command.Parameters.Add(idCatParam);
                command.Parameters.Add(idProvParam);
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

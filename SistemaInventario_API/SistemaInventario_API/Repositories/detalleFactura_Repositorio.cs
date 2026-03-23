using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SistemaInventario_API.Data;
using System.Data;

namespace SistemaInventario_API.Repositories
{
    public class detalleFactura_Repositorio
    {
        private readonly ApplicationDbContext _context;

        public detalleFactura_Repositorio(ApplicationDbContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<DataSet> EjecutarSpDetalleFactura(
            int Proceso,
            int idDetalle,
            int idFactura,
            int idProducto,
            int cantidad,
            decimal precioUnitario,
            decimal descuento
        )
        {
            var procesoParam = new SqlParameter("@PROCESO", SqlDbType.TinyInt) { Value = Proceso };
            var idDetalleParam = new SqlParameter("@idDetalle", SqlDbType.Int) { Value = idDetalle };
            var idFacturaParam = new SqlParameter("@idFactura", SqlDbType.Int) { Value = idFactura };
            var idProductoParam = new SqlParameter("@idProducto", SqlDbType.Int) { Value = idProducto };
            var cantidadParam = new SqlParameter("@cantidad", SqlDbType.Int) { Value = cantidad };
            var precioParam = new SqlParameter("@precioUnitario", SqlDbType.Decimal) { Value = precioUnitario };
            var descuentoParam = new SqlParameter("@descuento", SqlDbType.Decimal) { Value = descuento };

            var respuestaParam = new SqlParameter("@RESPUESTA", SqlDbType.VarChar, 200)
            {
                Direction = ParameterDirection.Output
            };

            var dataSet = new DataSet();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "MAN_DETALLEFACTURA";

                command.Parameters.Add(idDetalleParam);
                command.Parameters.Add(idFacturaParam);
                command.Parameters.Add(idProductoParam);
                command.Parameters.Add(cantidadParam);
                command.Parameters.Add(precioParam);
                command.Parameters.Add(descuentoParam);
                command.Parameters.Add(procesoParam);
                command.Parameters.Add(respuestaParam);

                using (var dataAdapter = new SqlDataAdapter((SqlCommand)command))
                {
                    await Task.Run(() =>
                    {
                        dataAdapter.Fill(dataSet);
                    });
                }
            }

            return dataSet;
        }
    }
}

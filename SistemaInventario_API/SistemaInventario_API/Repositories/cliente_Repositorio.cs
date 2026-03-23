using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SistemaInventario_API.Data;
using System.Data;

namespace SistemaInventario_API.Repositories
{
    public class Cliente_Repositorio
    {
        private readonly ApplicationDbContext _context;

        public Cliente_Repositorio(ApplicationDbContext dbcontext)
        {
            _context = dbcontext;
        }
        public async Task<DataSet> EjecutarSpCliente(int Proceso,int IDCliente,string Nombre,string Contacto,string Telefono,string Correo,string Direccion)
        {
            var procesoParam = new SqlParameter("@PROCESO", SqlDbType.Int) { Value = Proceso };
            var idClienteParam = new SqlParameter("@idCliente", SqlDbType.Int) { Value = IDCliente };
            var nombreParam = new SqlParameter("@nombre", SqlDbType.VarChar, 100) { Value = Nombre };
            var contactoParam = new SqlParameter("@contacto", SqlDbType.VarChar, 100) { Value = Contacto };
            var telefonoParam = new SqlParameter("@telefono", SqlDbType.VarChar, 20) { Value = Telefono };
            var correoParam = new SqlParameter("@correo", SqlDbType.VarChar,100) { Value = Correo };
            var direccionParam = new SqlParameter("@direccion", SqlDbType.VarChar, 250) { Value = Direccion };

            var respuestaParam = new SqlParameter("@RESPUESTA", SqlDbType.VarChar, 200);
            respuestaParam.Direction = ParameterDirection.Output;

            var dataSet = new DataSet();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;

                command.CommandText = "MAN_CLIENTE";
                command.Parameters.Add(procesoParam);
                command.Parameters.Add(idClienteParam);
                command.Parameters.Add(nombreParam);
                command.Parameters.Add(contactoParam);
                command.Parameters.Add(telefonoParam);
                command.Parameters.Add(correoParam);
                command.Parameters.Add(direccionParam);
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

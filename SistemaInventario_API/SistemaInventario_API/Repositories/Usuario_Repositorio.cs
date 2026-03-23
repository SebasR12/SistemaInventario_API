using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SistemaInventario_API.Data;
using System.Data;

namespace SistemaInventario_API.Repositories
{
    public class Usuario_Repositorio
    {
        private readonly ApplicationDbContext _context;

        public Usuario_Repositorio(ApplicationDbContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<DataSet> EjecutarSpUsuario(int Proceso, int IDUsuario, string Nombre, string Correo, string Contrasena, int IDRol, int Estado, int IntentosLogin)
        {
            var procesoParam = new SqlParameter("@PROCESO", SqlDbType.Int) { Value = Proceso };
            var idUsuarioParam = new SqlParameter("@idUsuario", SqlDbType.Int) { Value = IDUsuario };            
            var nombreParam = new SqlParameter("@nombre", SqlDbType.VarChar, 100) { Value = Nombre };
            var correoParam = new SqlParameter("@correo", SqlDbType.VarChar, 100) { Value = Correo };
            var contrasenaParam = new SqlParameter("@contrasena", SqlDbType.VarChar, 200) { Value = Contrasena };
            var idRolParam = new SqlParameter("@idRol", SqlDbType.Int) { Value = IDRol };
            var estadoParam = new SqlParameter("@estado", SqlDbType.Int) { Value = Estado };            
            var intentosLoginParam = new SqlParameter("@intentosLogin", SqlDbType.Int) { Value = IntentosLogin };


            var respuestaParam = new SqlParameter("@RESPUESTA", SqlDbType.VarChar, 200);
            respuestaParam.Direction = ParameterDirection.Output;

            var dataSet = new DataSet();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 360;

                command.CommandText = "MAN_USUARIO";
                command.Parameters.Add(procesoParam);
                command.Parameters.Add(idUsuarioParam);
                command.Parameters.Add(nombreParam);
                command.Parameters.Add(correoParam);
                command.Parameters.Add(contrasenaParam);
                command.Parameters.Add(idRolParam);
                command.Parameters.Add(estadoParam);
                command.Parameters.Add(intentosLoginParam);                
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

namespace SistemaInventario_API.Models
{
    public class Usuario
    {
        public int proceso { get; set; }
        public int idUsuario { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string correo { get; set; } = string.Empty;
        public string contrasena { get; set; } = string.Empty;
        public int idRol { get; set; }
        public int estado { get; set; }
        public string fechaRegistro { get; set; } = string.Empty;
        public int intentosLogin { get; set; }
    }
}

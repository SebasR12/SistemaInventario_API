namespace SistemaInventario_API.Models
{
    public class Cliente
    {
        public int proceso { get; set; }
        public int idCliente { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string contacto { get; set; } = string.Empty;
        public string telefono { get; set; } = string.Empty;        
        public string correo { get; set; } = string.Empty;
        public string direccion { get; set; } = string.Empty;
    }
}

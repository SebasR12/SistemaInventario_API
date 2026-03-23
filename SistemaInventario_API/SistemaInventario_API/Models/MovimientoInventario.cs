namespace SistemaInventario_API.Models
{
    public class MovimientoInventario
    {
        public int proceso { get; set; }
        public int idMovimiento { get; set; }
        public int idProducto { get; set; }
        public string tipoMovimiento { get; set; } = string.Empty;
        public int cantidad { get; set; }
        public int idUsuario { get; set; }
        
    }
}

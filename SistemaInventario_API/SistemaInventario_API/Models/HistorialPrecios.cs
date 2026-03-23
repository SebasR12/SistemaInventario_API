namespace SistemaInventario_API.Models
{
    public class HistorialPrecios
    {
        public int proceso { get; set; }
        public int idHistorial { get; set; }
        public int idProducto { get; set; }
        public decimal precioCompra { get; set; }
        public decimal precioVenta { get; set; }
        
    }
}

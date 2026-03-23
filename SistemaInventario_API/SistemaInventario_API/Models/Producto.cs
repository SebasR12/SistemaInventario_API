namespace SistemaInventario_API.Models
{
    public class Producto
    {
        public int proceso { get; set; }
        public int idProducto { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public decimal precioCompra { get; set; }
        public decimal precioVenta { get; set; }
        public int margenGanancia { get; set; }
        public int stockActual { get; set; }
        public int stockMinimo { get; set; }
        public int idCategoria { get; set; }
        public int idProveedor { get; set; }
    }
}

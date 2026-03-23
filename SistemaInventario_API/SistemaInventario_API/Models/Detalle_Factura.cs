namespace SistemaInventario_API.Models
{
    public class Detalle_Factura
    {
        public int proceso { get; set; }
        public int idDetalle { get; set; }
        public int idFactura { get; set; }
        public int idProducto { get; set; }
        public int cantidad { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal descuento { get; set; }
    }
}

namespace SistemaInventario_API.Models
{
    public class Factura
    {
        public int proceso { get; set; }
        public int idFactura { get; set; }
        public int idCliente { get; set; }
        public string fecha { get; set; }
        public decimal total { get; set; }
        public int idUsuario { get; set; }
    }
}

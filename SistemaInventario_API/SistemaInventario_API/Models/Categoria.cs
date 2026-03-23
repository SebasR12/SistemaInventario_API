namespace SistemaInventario_API.Models
{
    public class Categoria
    {
        public int proceso { get; set; }
        public int idCategoria { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
    }
}

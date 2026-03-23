using Microsoft.AspNetCore.Mvc;
using SistemaInventario_API.Models;
using SistemaInventario_API.Repositories;
using System.Data;

namespace SistemaInventario_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly Producto_Repositorio _repo;

        public ProductoController(Producto_Repositorio repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProductos()
        {
            var respuesta = await _repo.EjecutarSpProducto(
                null, null, null, 0, 0, 0, 0, 0, 0, 90
            );

            if (respuesta != null && respuesta.Tables.Count > 0)
            {
                var lista = new List<Dictionary<string, object>>();

                foreach (DataRow fila in respuesta.Tables[0].Rows)
                {
                    var filaDatos = new Dictionary<string, object>();
                    foreach (DataColumn col in respuesta.Tables[0].Columns)
                    {
                        filaDatos[col.ColumnName] = fila[col];
                    }
                    lista.Add(filaDatos);
                }

                return Ok(lista);
            }

            return NotFound();
        }

        [HttpGet("{idProducto:int}")]
        public async Task<IActionResult> ObtenerProductoPorId(int idProducto)
        {
            var respuesta = await _repo.EjecutarSpProducto(
                idProducto, null, null, 0, 0, 0, 0, 0, 0,91
            );

            if (respuesta != null &&
                respuesta.Tables.Count > 0 &&
                respuesta.Tables[0].Rows.Count > 0)
            {
                var fila = respuesta.Tables[0].Rows[0];
                var filaDatos = new Dictionary<string, object>();
                foreach (DataColumn col in respuesta.Tables[0].Columns)
                {
                    filaDatos[col.ColumnName] = fila[col];
                }
                return Ok(filaDatos);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] Producto producto)
        {
            if (producto == null || !ModelState.IsValid)
                return BadRequest();

            var respuesta = await _repo.EjecutarSpProducto(
                null,
                producto.nombre,
                producto.descripcion,
                producto.precioCompra,
                producto.precioVenta,
                producto.stockActual,
                producto.stockMinimo,
                producto.idCategoria,
                producto.idProveedor,
                1
            );

            if (respuesta != null)
                return Ok();

            return StatusCode(500);
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarProducto([FromBody] Producto producto)
        {
            if (producto == null || !ModelState.IsValid)
                return BadRequest();

            var respuesta = await _repo.EjecutarSpProducto(
                producto.idProducto,
                producto.nombre,
                producto.descripcion,
                producto.precioCompra,
                producto.precioVenta,
                producto.stockActual,
                producto.stockMinimo,
                producto.idCategoria,
                producto.idProveedor,
                2
            );

            if (respuesta != null)
                return Ok();

            return StatusCode(500);
        }

        [HttpDelete("{idProducto:int}")]
        public async Task<IActionResult> EliminarProducto(int idProducto)
        {
            if (idProducto <= 0)
                return BadRequest();

            var respuesta = await _repo.EjecutarSpProducto(
                idProducto, null, null, 0, 0, 0, 0, 0, 0, 3
            );

            if (respuesta != null)
                return Ok();

            return StatusCode(500);
        }
    }
}

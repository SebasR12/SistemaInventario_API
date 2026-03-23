using Microsoft.AspNetCore.Mvc;
using SistemaInventario_API.Models;
using SistemaInventario_API.Repositories;
using System.Data;

namespace SistemaInventario_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialPrecios_Controller : ControllerBase
    {
        private readonly HistorialPrecios_Repositorio _repo;

        public HistorialPrecios_Controller(HistorialPrecios_Repositorio repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerHistorial()
        {
            var respuesta = await _repo.EjecutarSpHistorialPrecios(90, 0, 0, 0, 0);

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

            return NotFound("No se encontraron datos.");
        }

        [HttpGet("{idHistorial:int}")]
        public async Task<IActionResult> ObtenerHistorialPorId(int idHistorial)
        {
            var respuesta = await _repo.EjecutarSpHistorialPrecios(91, idHistorial, 0, 0, 0);

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
        public async Task<IActionResult> CrearHistorial([FromBody] HistorialPrecios h)
        {
            if (h == null || !ModelState.IsValid)
                return BadRequest("Datos inválidos.");

            var respuesta = await _repo.EjecutarSpHistorialPrecios(
                1,
                h.idHistorial,
                h.idProducto,
                h.precioCompra,
                h.precioVenta
            );

            if (respuesta != null)
                return Ok();

            return StatusCode(500);
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarHistorial([FromBody] HistorialPrecios h)
        {
            if (h == null || !ModelState.IsValid)
                return BadRequest();

            var respuesta = await _repo.EjecutarSpHistorialPrecios(
                2,
                h.idHistorial,
                h.idProducto,
                h.precioCompra,
                h.precioVenta
            );

            if (respuesta != null)
                return Ok();

            return StatusCode(500);
        }

        [HttpDelete("{idHistorial:int}")]
        public async Task<IActionResult> EliminarHistorial(int idHistorial)
        {
            if (idHistorial <= 0)
                return BadRequest("ID inválido.");

            var respuesta = await _repo.EjecutarSpHistorialPrecios(
                3,
                idHistorial,
                0, 0, 0
            );

            if (respuesta != null)
                return Ok();

            return StatusCode(500);
        }
    }
}

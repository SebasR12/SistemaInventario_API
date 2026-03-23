using Microsoft.AspNetCore.Mvc;
using SistemaInventario_API.Models;
using SistemaInventario_API.Repositories;
using System.Data;

namespace SistemaInventario_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoInventario_Controller : ControllerBase
    {
        private readonly MovimientoInventario_Repositorio _repo;

        public MovimientoInventario_Controller(MovimientoInventario_Repositorio repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerMovimientos()
        {
            var respuesta = await _repo.EjecutarSpMovimientoInventario(90, 0, 0, "", 0, 0);

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

        [HttpGet("{idMovimiento:int}")]
        public async Task<IActionResult> ObtenerMovimientoPorId(int idMovimiento)
        {
            var respuesta = await _repo.EjecutarSpMovimientoInventario(91, idMovimiento, 0, "", 0, 0);

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
        public async Task<IActionResult> CrearMovimiento([FromBody] MovimientoInventario mov)
        {
            if (mov == null || !ModelState.IsValid)
                return BadRequest("Datos inválidos.");

            var respuesta = await _repo.EjecutarSpMovimientoInventario(
                1,
                mov.idMovimiento,
                mov.idProducto,
                mov.tipoMovimiento,
                mov.cantidad,
                mov.idUsuario
            );

            if (respuesta != null)
                return Ok();

            return StatusCode(500);
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarMovimiento([FromBody] MovimientoInventario mov)
        {
            if (mov == null || !ModelState.IsValid)
                return BadRequest();

            var respuesta = await _repo.EjecutarSpMovimientoInventario(
                2,
                mov.idMovimiento,
                mov.idProducto,
                mov.tipoMovimiento,
                mov.cantidad,
                mov.idUsuario
            );

            if (respuesta != null)
                return Ok();

            return StatusCode(500);
        }

        [HttpDelete("{idMovimiento:int}")]
        public async Task<IActionResult> EliminarMovimiento(int idMovimiento)
        {
            if (idMovimiento <= 0)
                return BadRequest("ID inválido.");

            var respuesta = await _repo.EjecutarSpMovimientoInventario(
                3,
                idMovimiento,
                0, "", 0, 0
            );

            if (respuesta != null)
                return Ok();

            return StatusCode(500);
        }
    }
}

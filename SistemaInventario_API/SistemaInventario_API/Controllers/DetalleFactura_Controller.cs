using Microsoft.AspNetCore.Mvc;
using SistemaInventario_API.Models;
using SistemaInventario_API.Repositories;
using System.Data;

namespace InventarioBD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleFactura_Controller : ControllerBase
    {
        private readonly detalleFactura_Repositorio _detalleFacturaRepositorio;

        public DetalleFactura_Controller(detalleFactura_Repositorio detalleFacturaRepositorio)
        {
            _detalleFacturaRepositorio = detalleFacturaRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerDetalles()
        {
            var respuesta = await _detalleFacturaRepositorio.EjecutarSpDetalleFactura(90, 0, 0, 0, 0, 0, 0);

            if (respuesta != null && respuesta.Tables.Count > 0)
            {
                var listaResultado = new List<Dictionary<string, object>>();
                foreach (DataRow fila in respuesta.Tables[0].Rows)
                {
                    var filaDatos = new Dictionary<string, object>();
                    foreach (DataColumn columna in respuesta.Tables[0].Columns)
                        filaDatos[columna.ColumnName] = fila[columna];
                    listaResultado.Add(filaDatos);
                }

                return Ok(listaResultado);
            }

            return NotFound("No se encontraron detalles de factura.");
        }

        [HttpGet("{idDetalle:int}")]
        public async Task<IActionResult> ObtenerDetallePorId(int idDetalle)
        {
            var respuesta = await _detalleFacturaRepositorio.EjecutarSpDetalleFactura(91, idDetalle, 0, 0, 0, 0, 0);

            if (respuesta != null && respuesta.Tables.Count > 0 && respuesta.Tables[0].Rows.Count > 0)
            {
                var fila = respuesta.Tables[0].Rows[0];
                var filaDatos = new Dictionary<string, object>();
                foreach (DataColumn columna in respuesta.Tables[0].Columns)
                    filaDatos[columna.ColumnName] = fila[columna];

                return Ok(filaDatos);
            }

            return NotFound();
        }

        [HttpGet("{idFactura:int}")]
        public async Task<IActionResult> ObtenerFacturaPorId(int idFactura)
        {
            var respuesta = await _detalleFacturaRepositorio.EjecutarSpDetalleFactura(92, 0, idFactura, 0, 0, 0, 0);

            if (respuesta != null && respuesta.Tables.Count > 0 && respuesta.Tables[0].Rows.Count > 0)
            {
                var fila = respuesta.Tables[0].Rows[0];
                var filaDatos = new Dictionary<string, object>();
                foreach (DataColumn columna in respuesta.Tables[0].Columns)
                    filaDatos[columna.ColumnName] = fila[columna];

                return Ok(filaDatos);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CrearDetalle([FromBody] Detalle_Factura detalle)
        {
            if (detalle == null || !ModelState.IsValid)
                return BadRequest("Datos inválidos del detalle de factura.");

            try
            {
                var respuesta = await _detalleFacturaRepositorio.EjecutarSpDetalleFactura(
                    1,
                    detalle.idDetalle,
                    detalle.idFactura,
                    detalle.idProducto,
                    detalle.cantidad,
                    detalle.precioUnitario,
                    detalle.descuento
                );

                if (respuesta != null)
                    return Ok();
                else
                    return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error del servidor: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarDetalle([FromBody] Detalle_Factura detalle)
        {
            if (detalle == null || !ModelState.IsValid)
                return BadRequest();

            var respuesta = await _detalleFacturaRepositorio.EjecutarSpDetalleFactura(
                2,
                detalle.idDetalle,
                detalle.idFactura,
                detalle.idProducto,
                detalle.cantidad,
                detalle.precioUnitario,
                detalle.descuento
            );

            if (respuesta != null && respuesta.Tables.Count > 0)
                return Ok();
            else
                return StatusCode(500);
        }

        [HttpDelete("{idDetalle:int}")]
        public async Task<IActionResult> EliminarDetalle(int idDetalle)
        {
            if (idDetalle <= 0)
                return BadRequest("El ID del detalle no es válido.");

            try
            {
                var respuesta = await _detalleFacturaRepositorio.EjecutarSpDetalleFactura(
                    3,
                    idDetalle,
                    0,
                    0,
                    0,
                    0,
                    0
                );

                if (respuesta != null)
                    return Ok();
                else
                    return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error del servidor: {ex.Message}");
            }
        }
    }
}

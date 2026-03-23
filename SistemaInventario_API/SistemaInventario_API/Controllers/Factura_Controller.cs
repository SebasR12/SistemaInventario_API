using Microsoft.AspNetCore.Mvc;
using SistemaInventario_API.Models;
using SistemaInventario_API.Repositories;
using System.Data;

namespace InventarioBD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Factura_Controller : ControllerBase
    {
        private readonly factura_Repositorio _facturaRepositorio;

        public Factura_Controller(factura_Repositorio facturaRepositorio)
        {
            _facturaRepositorio = facturaRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerFacturas()
        {
            var respuesta = await _facturaRepositorio.EjecutarSpFactura(90, 0, 0, 0, 0);

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

            return NotFound("No se encontraron facturas.");
        }

        [HttpGet("{idFactura:int}")]
        public async Task<IActionResult> ObtenerFacturaPorId(int idFactura)
        {
            var respuesta = await _facturaRepositorio.EjecutarSpFactura(91,idFactura, 0, 0, 0);

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
        public async Task<IActionResult> CrearFactura([FromBody] Factura factura)
        {
            if (factura == null || !ModelState.IsValid)
                return BadRequest("Datos de factura inválidos.");

            try
            {
                // Llamada al procedimiento almacenado sin el parámetro "fecha"
                var respuesta = await _facturaRepositorio.EjecutarSpFactura(
                    1,
                    factura.idFactura,
                    factura.idCliente,
                    factura.total,
                    factura.idUsuario
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
        public async Task<IActionResult> ActualizarFactura([FromBody] Factura factura)
        {
            if (factura == null || !ModelState.IsValid)
                return BadRequest();

            // Llamada al procedimiento almacenado sin el parámetro "fecha"
            var respuesta = await _facturaRepositorio.EjecutarSpFactura(
                2,
                factura.idFactura,
                factura.idCliente,               
                factura.total,
                factura.idUsuario
            );

            if (respuesta != null && respuesta.Tables.Count > 0)
                return Ok();
            else
                return StatusCode(500);
        }

        [HttpDelete("{idFactura:int}")]
        public async Task<IActionResult> EliminarFactura(int idFactura)
        {
            if (idFactura <= 0)
                return BadRequest("El ID de la factura no es válido.");

            try
            {
                var respuesta = await _facturaRepositorio.EjecutarSpFactura(
                    3,
                    idFactura,
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaInventario_API.Models;
using SistemaInventario_API.Repositories;
using System.Data;

namespace SistemaInventario_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Proveedor_Controller : ControllerBase
    {

        private readonly Proveedor_Repositorio _proveedor_Repositorio;

        public Proveedor_Controller(Proveedor_Repositorio proveedor_Repositorio)
        {
            _proveedor_Repositorio = proveedor_Repositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProveedores()
        {
            var respuesta = await _proveedor_Repositorio.EjecutarSpProveedor(90, 0, "", "", "", "","");

            if (respuesta == null || respuesta.Tables.Count == 0 || respuesta.Tables[0].Rows.Count == 0)
                return NotFound(new { mensaje = "No se encontraron proveedores." });

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

        [HttpGet("{idProveedor:int}")]
        public async Task<IActionResult> ObtenerProveedorPorId(int idProveedor)
        {
            var respuesta = await _proveedor_Repositorio.EjecutarSpProveedor(91, idProveedor, "", "", "", "","");

            if (respuesta == null || respuesta.Tables.Count == 0 || respuesta.Tables[0].Rows.Count == 0)
                return NotFound(new { mensaje = "Proveedor no encontrado." });

            var fila = respuesta.Tables[0].Rows[0];
            var filaDatos = new Dictionary<string, object>();

            foreach (DataColumn columna in respuesta.Tables[0].Columns)
                filaDatos[columna.ColumnName] = fila[columna];

            return Ok(filaDatos);
        }

        [HttpPost]
        public async Task<IActionResult> CrearProveedor([FromBody] Proveedor proveedor)
        {
            if (proveedor == null)
                return BadRequest(new { mensaje = "El cuerpo de la solicitud está vacío." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var respuesta = await _proveedor_Repositorio.EjecutarSpProveedor(
                    1,
                    proveedor.idProveedor,
                    proveedor.nombre,
                    proveedor.contacto,
                    proveedor.telefono,
                    proveedor.correo,
                    proveedor.direccion
                );

                if (respuesta == null)
                    return StatusCode(500, new { mensaje = "No se pudo crear el proveedor." });

                return CreatedAtAction(nameof(ObtenerProveedorPorId), new { idProveedor = proveedor.idProveedor },
                    new { mensaje = "Proveedor creado correctamente.", proveedor });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error del servidor: {ex.Message}" });
            }
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarProveedor([FromBody] Proveedor proveedor)
        {
            if (proveedor == null)
                return BadRequest(new { mensaje = "Datos inválidos." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var respuesta = await _proveedor_Repositorio.EjecutarSpProveedor(
                2,
                proveedor.idProveedor,
                proveedor.nombre,
                proveedor.contacto,
                proveedor.telefono,
                proveedor.correo,
                proveedor.direccion
            );

            if (respuesta == null)
                return StatusCode(500, new { mensaje = "Error al actualizar el proveedor." });

            return Ok(new { mensaje = "Proveedor actualizado correctamente." });
        }

        [HttpDelete("{idProveedor:int}")]
        public async Task<IActionResult> EliminarProveedor(int idProveedor)
        {
            if (idProveedor <= 0)
                return BadRequest(new { mensaje = "El ID del proveedor no es válido." });

            try
            {
                var respuesta = await _proveedor_Repositorio.EjecutarSpProveedor(3, idProveedor, "", "", "", "", "");

                if (respuesta == null)
                    return StatusCode(500, new { mensaje = "No se pudo eliminar el proveedor." });

                return NoContent(); // 204
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error del servidor: {ex.Message}" });
            }
        }
    }
}

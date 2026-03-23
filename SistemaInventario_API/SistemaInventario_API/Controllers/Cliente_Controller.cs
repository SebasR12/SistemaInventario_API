using Microsoft.AspNetCore.Mvc;
using SistemaInventario_API.Models;
using SistemaInventario_API.Repositories;
using System.Data;

namespace InventarioBD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly Cliente_Repositorio _clienteRepositorio;

        public ClienteController(Cliente_Repositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerClientes()
        {
            var respuesta = await _clienteRepositorio.EjecutarSpCliente(90, 0, "", "", "", "","");

            if (respuesta == null || respuesta.Tables.Count == 0 || respuesta.Tables[0].Rows.Count == 0)
                return NotFound(new { mensaje = "No se encontraron clientes." });

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

        [HttpGet("{idCliente:int}")]
        public async Task<IActionResult> ObtenerClientePorId(int idCliente)
        {
            var respuesta = await _clienteRepositorio.EjecutarSpCliente(91, idCliente,"", "", "", "", "");

            if (respuesta == null || respuesta.Tables.Count == 0 || respuesta.Tables[0].Rows.Count == 0)
                return NotFound(new { mensaje = "Cliente no encontrado." });

            var fila = respuesta.Tables[0].Rows[0];
            var filaDatos = new Dictionary<string, object>();

            foreach (DataColumn columna in respuesta.Tables[0].Columns)
                filaDatos[columna.ColumnName] = fila[columna];

            return Ok(filaDatos);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCliente([FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest(new { mensaje = "El cuerpo de la solicitud está vacío." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var respuesta = await _clienteRepositorio.EjecutarSpCliente(
                    1,
                    cliente.idCliente,
                    cliente.nombre,
                    cliente.contacto,
                    cliente.telefono,
                    cliente.correo,
                    cliente.direccion
                );

                if (respuesta == null)
                    return StatusCode(500, new { mensaje = "No se pudo crear el cliente." });

                return CreatedAtAction(nameof(ObtenerClientePorId), new { idCliente = cliente.idCliente },
                    new { mensaje = "Cliente creado correctamente.", cliente });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error del servidor: {ex.Message}" });
            }
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarCliente([FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest(new { mensaje = "Datos inválidos." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var respuesta = await _clienteRepositorio.EjecutarSpCliente(
                2,
                cliente.idCliente,
                cliente.nombre,
                cliente.contacto,
                cliente.telefono,
                cliente.correo,
                cliente.direccion
            );

            if (respuesta == null)
                return StatusCode(500, new { mensaje = "Error al actualizar el cliente." });

            return Ok(new { mensaje = "Cliente actualizado correctamente." });
        }

        [HttpDelete("{idCliente:int}")]
        public async Task<IActionResult> EliminarCliente(int idCliente)
        {
            if (idCliente <= 0)
                return BadRequest(new { mensaje = "El ID del cliente no es válido." });

            try
            {
                var respuesta = await _clienteRepositorio.EjecutarSpCliente(3, idCliente,"", "", "", "", "");

                if (respuesta == null)
                    return StatusCode(500, new { mensaje = "No se pudo eliminar el cliente." });

                return NoContent(); // 204
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error del servidor: {ex.Message}" });
            }
        }
    }
}

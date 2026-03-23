using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaInventario_API.Models;
using SistemaInventario_API.Repositories;
using System.Data;

namespace SistemaInventario_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Usuario_Controller : ControllerBase
    {
        private readonly Usuario_Repositorio _usuarioRepositorio;

        public Usuario_Controller(Usuario_Repositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var respuesta = await _usuarioRepositorio.EjecutarSpUsuario(90, 0, "", "", "", 0, 0, 0);

            if (respuesta != null && respuesta.Tables.Count > 0)
            {
                var listaResultado = new List<Dictionary<string, object>>();

                foreach (DataRow fila in respuesta.Tables[0].Rows)
                {
                    var filaDatos = new Dictionary<string, object>();

                    foreach (DataColumn columna in respuesta.Tables[0].Columns)
                    {
                        filaDatos[columna.ColumnName] = fila[columna];
                    }

                    listaResultado.Add(filaDatos);
                }

                return Ok(listaResultado);
            }
            else
            {
                return NotFound("No se encontraron los datos");
            }
        }

        [HttpGet("{idUsuario:int}")]
        public async Task<IActionResult> ObtenerUsuarioPorId(int idUsuario)
        {
            var respuesta = await _usuarioRepositorio.EjecutarSpUsuario(91, idUsuario, "", "", "", 0, 0, 0);

            if (respuesta != null && respuesta.Tables.Count > 0 && respuesta.Tables[0].Rows.Count > 0)
            {
                var fila = respuesta.Tables[0].Rows[0];
                var filaDatos = new Dictionary<string, object>();

                foreach (DataColumn columna in respuesta.Tables[0].Columns)
                {
                    filaDatos[columna.ColumnName] = fila[columna];
                }

                return Ok(filaDatos);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null || !ModelState.IsValid)
            {
                return BadRequest("Datos del usuario inválidos.");
            }

            try
            {
                var respuesta = await _usuarioRepositorio.EjecutarSpUsuario(
                    1,
                    usuario.idUsuario,
                    usuario.nombre,
                    usuario.correo,
                    usuario.contrasena,
                    usuario.idRol,
                    usuario.estado,
                    usuario.intentosLogin
                );

                if (respuesta != null)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error del servidor: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarUsuario([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid || usuario == null)
            {
                return BadRequest();
            }

            var respuesta = await _usuarioRepositorio.EjecutarSpUsuario(
                2,
                usuario.idUsuario,
                usuario.nombre,
                usuario.correo,
                usuario.contrasena,
                usuario.idRol,
                usuario.estado,
                usuario.intentosLogin
            );

            if (respuesta != null && respuesta.Tables.Count > 0)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{idUsuario:int}")]
        public async Task<IActionResult> EliminarUsuario(int idUsuario)
        {
            if (idUsuario <= 0)
            {
                return BadRequest("El ID del usuario no es válido.");
            }

            try
            {
                var respuesta = await _usuarioRepositorio.EjecutarSpUsuario(
                    3, // proceso de eliminación
                    idUsuario,
                    "", "", "", 0, 0, 0
                );

                if (respuesta != null)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error del servidor: {ex.Message}");
            }
        }

    }
}

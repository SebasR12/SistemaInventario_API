using Microsoft.AspNetCore.Mvc;
using SistemaInventario_API.Models;
using SistemaInventario_API.Repositories;
using System.Data;

namespace SistemaInventario_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Rol_Controller : ControllerBase
    {
        private readonly Rol_Repositorio _repo;

        public Rol_Controller(Rol_Repositorio repo)
        {
            _repo = repo;
        }

        // =====================
        // GET TODOS LOS ROLES
        // =====================
        [HttpGet]
        public async Task<IActionResult> ObtenerRoles()
        {
            var respuesta = await _repo.EjecutarSpRol(90, 0, null, null);

            if (respuesta != null && respuesta.Tables.Count > 0)
            {
                var lista = new List<Dictionary<string, object>>();

                foreach (DataRow fila in respuesta.Tables[0].Rows)
                {
                    var filaDatos = new Dictionary<string, object>();

                    foreach (DataColumn col in respuesta.Tables[0].Columns)
                        filaDatos[col.ColumnName] = fila[col];

                    lista.Add(filaDatos);
                }

                return Ok(lista);
            }

            return NotFound("No se encontraron roles.");
        }

        // =====================
        // GET POR ID
        // =====================
        [HttpGet("{idRol:int}")]
        public async Task<IActionResult> ObtenerRolPorId(int idRol)
        {
            var respuesta = await _repo.EjecutarSpRol(91, idRol, null, null);

            if (respuesta != null && respuesta.Tables.Count > 0 && respuesta.Tables[0].Rows.Count > 0)
            {
                var fila = respuesta.Tables[0].Rows[0];
                var datos = new Dictionary<string, object>();

                foreach (DataColumn col in respuesta.Tables[0].Columns)
                    datos[col.ColumnName] = fila[col];

                return Ok(datos);
            }

            return NotFound();
        }

        // =====================
        // CREAR
        // =====================
        [HttpPost]
        public async Task<IActionResult> CrearRol([FromBody] Rol r)
        {
            if (r == null)
                return BadRequest("Datos inválidos.");

            var respuesta = await _repo.EjecutarSpRol(
                1,
                r.idRol,
                r.nombreRol,
                r.descripcion
            );

            return Ok();
        }

        // =====================
        // ACTUALIZAR
        // =====================
        [HttpPut]
        public async Task<IActionResult> ActualizarRol([FromBody] Rol r)
        {
            if (r == null)
                return BadRequest("Datos inválidos.");

            var respuesta = await _repo.EjecutarSpRol(
                2,
                r.idRol,
                r.nombreRol,
                r.descripcion
            );

            return Ok();
        }

        // =====================
        // ELIMINAR
        // =====================
        [HttpDelete("{idRol:int}")]
        public async Task<IActionResult> EliminarRol(int idRol)
        {
            var respuesta = await _repo.EjecutarSpRol(
                3,
                idRol,
                "",
                ""
            );

            return Ok();
        }
    }
}

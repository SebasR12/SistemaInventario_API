using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaInventario_API.Models;
using SistemaInventario_API.Repositories;
using System.Data;

namespace InventarioBD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Categoria_Controller : ControllerBase
    {
        private readonly categoria_Repositorio _categoria_Repositorio;

        public Categoria_Controller(categoria_Repositorio categoria_Repositorio)
        {
            _categoria_Repositorio = categoria_Repositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCategorias()
        {
            var respuesta = await _categoria_Repositorio.EjecutarSpCategoria(90, 0, "", "");

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
                return NotFound("No se encontraron categorías");
            }
        }

        [HttpGet("{idCategoria:int}")]
        public async Task<IActionResult> ObtenerCategoriaPorId(int idCategoria)
        {
            var respuesta = await _categoria_Repositorio.EjecutarSpCategoria(91, idCategoria, "", "");

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
        public async Task<IActionResult> CrearCategoria([FromBody] Categoria categoria)
        {
            if (categoria == null || !ModelState.IsValid)
                return BadRequest("Datos de categoría inválidos.");

            try
            {
                var respuesta = await _categoria_Repositorio.EjecutarSpCategoria(
                    1,
                    categoria.idCategoria,
                    categoria.nombre,
                    categoria.descripcion
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
        public async Task<IActionResult> ActualizarCategoria([FromBody] Categoria categoria)
        {
            if (categoria == null || !ModelState.IsValid)
                return BadRequest();

            var respuesta = await _categoria_Repositorio.EjecutarSpCategoria(
                2,
                categoria.idCategoria,
                categoria.nombre,
                categoria.descripcion
            );

            if (respuesta != null && respuesta.Tables.Count > 0)
                return Ok();
            else
                return StatusCode(500);
        }

        [HttpDelete("{idCategoria:int}")]
        public async Task<IActionResult> EliminarCategoria(int idCategoria)
        {
            if (idCategoria <= 0)
                return BadRequest("El ID de la categoría no es válido.");

            try
            {
                var respuesta = await _categoria_Repositorio.EjecutarSpCategoria(
                    3,
                    idCategoria,
                    "", ""
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

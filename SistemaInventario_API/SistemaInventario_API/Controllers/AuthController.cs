using Frontend_Inventario.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SistemaInventario_API.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventarioBD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Login_Repositorio _loginRepositorio;
        private readonly IConfiguration _config;

        public AuthController(Login_Repositorio loginRepositorio, IConfiguration config)
        {
            _loginRepositorio = loginRepositorio;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var ds = await _loginRepositorio.Login(request.Correo, request.Contrasena);

            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return Unauthorized("Error en el proceso.");

            var row = ds.Tables[0].Rows[0];

            int exito = Convert.ToInt32(row["Exito"]);
            string mensaje = row["Mensaje"].ToString()!;

            if (exito == 0)
                return Unauthorized(mensaje);

            int idUsuario = Convert.ToInt32(row["idUsuario"]);
            string nombre = row["nombre"].ToString()!;
            string correo = row["correo"].ToString()!;
            int idRol = Convert.ToInt32(row["idRol"]);

            string token = GenerarToken(idUsuario, nombre, idRol);

            return Ok(new
            {
                token,
                nombre,
                rol = idRol
            });
        }

        private string GenerarToken(int idUsuario, string nombre, int rol)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString()),
                new Claim(ClaimTypes.Name, nombre),
                new Claim(ClaimTypes.Role, rol.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

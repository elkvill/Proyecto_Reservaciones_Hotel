using Hotel.Application.DTOs.Reservas;
using Hotel.Application.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hotel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly IReservaService _service;

        public ReservaController(IReservaService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Recepcionista")]
        public async Task<ActionResult<IEnumerable<ReservaDto>>> ObtenerTodos()
        {
            var registros = await _service.ObtenerTodasAsync();
            return Ok(registros);
        }

        [HttpGet("mis-reservas")]
        [Authorize(Roles = "Admin,Recepcionista,Cliente")]
        public async Task<ActionResult<IEnumerable<ReservaDto>>> MisReservas()
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(usuarioId))
                return Unauthorized("Usuario no autenticado.");

            var registros = await _service.ObtenerPorUsuarioAsync(usuarioId);
            return Ok(registros);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,Recepcionista")]
        public async Task<ActionResult<ReservaDto>> ObtenerPorId(int id)
        {
            var registro = await _service.ObtenerPorIdAsync(id);
            if (registro == null) return NotFound();

            return Ok(registro);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Recepcionista,Cliente")]
        public async Task<ActionResult<ReservaDto>> Crear([FromBody] ReservaCrearDto dto)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (User.IsInRole("Cliente"))
            {
                dto.UsuarioId = currentUserId;
            }
            else if (string.IsNullOrEmpty(dto.UsuarioId))
            {
                dto.UsuarioId = currentUserId;
            }

            var nuevo = await _service.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevo.Id }, nuevo);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Recepcionista")]
        public async Task<ActionResult<ReservaDto>> Actualizar(int id, [FromBody] ReservaActualizarDto dto)
        {
            var registroExistente = await _service.ObtenerPorIdAsync(id);
            if (registroExistente == null) return NotFound();

            var actualizado = await _service.ActualizarAsync(id, dto);
            return Ok(actualizado);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Recepcionista")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var registroExistente = await _service.ObtenerPorIdAsync(id);
            if (registroExistente == null) return NotFound();

            await _service.EliminarAsync(id);
            return NoContent();
        }
    }
}

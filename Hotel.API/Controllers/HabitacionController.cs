using Hotel.Application.DTOs.Habitaciones;
using Hotel.Application.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitacionController : ControllerBase
    {
        private readonly IHabitacionService _service;

        public HabitacionController(IHabitacionService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<HabitacionDto>>> ObtenerTodos()
        {
            var registros = await _service.ObtenerTodasAsync();
            return Ok(registros);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<HabitacionDto>> ObtenerPorId(int id)
        {
            var registro = await _service.ObtenerPorIdAsync(id);
            return Ok(registro);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<HabitacionDto>> Crear([FromBody] HabitacionCrearDto dto)
        {
            var nuevo = await _service.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevo.Id }, nuevo);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<HabitacionDto>> Actualizar(int id, [FromBody] HabitacionEditarDto dto)
        {
            var actualizado = await _service.ActualizarAsync(id, dto);
            return Ok(actualizado);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Eliminar(int id)
        {
            await _service.EliminarAsync(id);
            return NoContent();
        }
    }
}

using Hotel.Application.DTOs.Tipo_habitacion;
using Hotel.Application.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoHabitacionController : ControllerBase
    {
        private readonly ITipoHabitacionService _service;

        public TipoHabitacionController(ITipoHabitacionService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<TipoHabitacionDto>>> ObtenerTodos()
        {
            var registros = await _service.ObtenerTodosAsync();
            return Ok(registros);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<TipoHabitacionDto>> ObtenerPorId(int id)
        {
            var registro = await _service.ObtenerPorIdAsync(id);
            return Ok(registro);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TipoHabitacionDto>> Crear([FromBody] TipoHabitacionCrearDto dto)
        {
            var nuevo = await _service.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevo.Id }, nuevo);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TipoHabitacionDto>> Actualizar(int id, [FromBody] TipoHabitacionActualizarDto dto)
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

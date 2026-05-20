using Hotel.Application.DTOs.Usuario;
using Hotel.Application.Interface.Services;
using Hotel.Application.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;
        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> ObtenerTodos([FromQuery] int pagina = 1, [FromQuery] int tamanio = 10)
        {
            var registros = await _service.ObtenerTodosAsync(pagina, tamanio);
            var total = await _service.ContarAsync();
            return Ok(new RespuestaPaginada<UsuarioDto>(registros, total, pagina, tamanio));
        }


        //[HttpGet("{id}", Name = "ObtenerUsuario")]
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult<IEnumerable<ReservaDto>>> ObtenerUsuario(string id)
        //{
        //    var registro = await _service.ObtenerPorIdAsync(id);
        //    return Ok(registro);
        //}
    }
}

using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadastroController: ControllerBase
    {
        private CadastroService _cadastroService;

        public CadastroController(CadastroService service)
        {
            _cadastroService = service;
        }

        [HttpPost]
        public IActionResult CadastrarUsuario(CreateUsuarioDto createUsuarioDto)
        {
            Result resultado = _cadastroService.CadastrarUsuario(createUsuarioDto);

            if (resultado.IsFailed)
            {
                return StatusCode(500);
            }
            return Ok();
        }
    }
}

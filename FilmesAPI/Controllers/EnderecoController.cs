using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FilmesAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private EnderecoService _enderecoService;

        public EnderecoController(EnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        [HttpPost]
        public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            ReadEnderecoDto endereco = _enderecoService.Adicionar(enderecoDto);

            return CreatedAtAction(nameof(RecuperaEndereco), new { Id = endereco.Id }, endereco);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReadEnderecoDto>> RecuperaEnderecos()
        {
            return Ok(_enderecoService.RecuperarEnderecos());
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaEndereco(int id)
        {
            ReadEnderecoDto endereco = _enderecoService.RecuperarEndereco(id);
            if (endereco != null)
            {
               
                return Ok(endereco);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto)
        {
            Result atualizou = _enderecoService.Atualizar(id, enderecoDto);
            if (atualizou.IsSuccess)
            {
                return NoContent();
               
            }
           
            return NotFound();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletaEndereco(int id)
        {
            Result deletou = _enderecoService.Deletar(id);

            if (deletou.IsSuccess)
            {
                return NoContent();
               
            }
            return NotFound();
        }

    }
}

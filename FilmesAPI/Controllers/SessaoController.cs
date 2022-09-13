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
    public class SessaoController: ControllerBase
    {
        private SessaoService _sessaoService;

        public SessaoController(SessaoService sessaoService)
        {
            _sessaoService = sessaoService;
        }

        [HttpPost]
        public IActionResult AdicionaSessao(CreateSessaoDto dto)
        {
            Sessao sessao = _sessaoService.Adicionar(dto);

            return CreatedAtAction(nameof(RecuperarSessao), new { Id = sessao.Id }, sessao);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReadSessaoDto>> RecuperarSessoes()
        {
            var sessoes = _sessaoService.RecuperarSessoes();

            return Ok(sessoes);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarSessao(int id)
        {
            ReadSessaoDto sessaoDto = _sessaoService.RecuperarSessao(id);

            if(sessaoDto == null)
            {
                return NotFound();
            }

            
            return Ok(sessaoDto);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarSessao([FromBody] UpdateSessaoDto dto,int id)
        {
            Result atualizou = _sessaoService.Atualizar(dto,id);

            if(atualizou.IsSuccess)
            {
                return NoContent();
            }


            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarSessao(int id)
        {
            Result deletou = _sessaoService.Deletar(id);

            if (deletou.IsSuccess)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}

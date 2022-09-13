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
    public class GerenteController: ControllerBase
    {
        private GerenteService _gerenteService;

        public GerenteController(GerenteService gerenteService)
        {
            _gerenteService = gerenteService;
        }

        [HttpPost]
        public IActionResult AdicionarGerente(CreateGerenteDto dto)
        {
            ReadGerenteDto gerente = _gerenteService.Adicionar(dto);
            return CreatedAtAction(nameof(RecuperarGerente), new { Id = gerente.Id },gerente);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReadGerenteDto>> RecuperarGerentes()
        {
            var gerentes = _gerenteService.RecuperarGerentes();

            return Ok(gerentes);

        }

        [HttpGet("{id}")]
        public IActionResult RecuperarGerente(int id)
        {
            ReadGerenteDto gerente = _gerenteService.RecuperarEndereco(id);

            if(gerente != null)
            {
                return Ok(gerente);
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarGerente(int id)
        {
            Result deletou = _gerenteService.Deletar(id);

            if (deletou.IsSuccess)
            {
                return NoContent();
            }

            return NoContent();
        }
    }
}

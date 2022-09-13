using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FilmesAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace FilmesAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FilmeController: ControllerBase
    {
        private FilmeService _filmeService;

        public FilmeController(FilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        [HttpPost]
        public IActionResult AdicionarFilme([FromBody] CreateFilmeDto filmeDto)
        {
            ReadFilmeDto filme = _filmeService.Adicionar(filmeDto);
            
            return CreatedAtAction(nameof(RecuperarFilme), new {Id = filme.Id},filme);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReadFilmeDto>> RecuperarFilmes([FromQuery] int? classificacaoEtaria = null)
        {
            IEnumerable<ReadFilmeDto> retorno = _filmeService.RecuperarFilmes(classificacaoEtaria);
           
            if(retorno != null)
            {
                return Ok(retorno);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarFilme([FromRoute] int id)
        {
            ReadFilmeDto filmeDto =_filmeService.RecuperarFilme(id);

            if(filmeDto != null)
            {
              
                return Ok(filmeDto);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarFilme([FromBody] UpdateFilmeDto filmeParaAtualizar,
            int id)
        {
            Result atualizou = _filmeService.Atualizar(filmeParaAtualizar, id);

            if (atualizou.IsSuccess)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarFilme(int id)
        {
            Result deletou = _filmeService.Deletar(id);

            if (deletou.IsSuccess)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}

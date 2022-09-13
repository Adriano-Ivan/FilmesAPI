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
    public class CinemaController : ControllerBase
    {
        private CinemaService _cinemaService;

        public CinemaController(CinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }


        [HttpPost]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            ReadCinemaDto cinema = _cinemaService.Adicionar(cinemaDto);

            return CreatedAtAction(nameof(RecuperaCinema), new { Id = cinema.Id }, cinema);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReadCinemaDto>> RecuperarCinemas([FromQuery] string? nomeDoFilme=null)
        {
            List<ReadCinemaDto> cinemasDto = _cinemaService.RecuperarCinemas(nomeDoFilme);

            if(cinemasDto == null)
            {
                return NotFound();
            }

            return Ok(cinemasDto);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaCinema(int id)
        {
            ReadCinemaDto cinema = _cinemaService.RecuperarCinema(id);
           
            if (cinema != null)
            {
                return Ok(cinema);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            Result atualizou = _cinemaService.Atualizar(cinemaDto, id);

            if (atualizou.IsSuccess)
            {
                return NoContent();
            }

            return NotFound();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletarCinema(int id)
        {
            Result deletou = _cinemaService.Deletar(id);

            if (deletou.IsSuccess)
            {
                return NoContent();
            }

            return NotFound();
        }

    }
}

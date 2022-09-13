using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FluentResults;

namespace FilmesAPI.Services
{
    public class CinemaService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public CinemaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }

        public ReadCinemaDto Adicionar(CreateCinemaDto cinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();

            ReadCinemaDto dto = _mapper.Map<ReadCinemaDto>(cinema);
            return dto;
        }

        public List<ReadCinemaDto> RecuperarCinemas(string? nomeDoFilme)
        {
            List<Cinema> cinemas = _context.Cinemas.ToList();

            if (cinemas == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(nomeDoFilme))
            {
                IEnumerable<Cinema> query = from cinema in cinemas
                                            where
                                                 cinema.Sessoes.Any(sessao =>
                                                     sessao.Filme.Titulo.ToLower().Trim().Contains(nomeDoFilme.Trim().ToLower())
                                                 )
                                            select cinema;

                cinemas = query.ToList();
            }

            List<ReadCinemaDto> filmesDto = cinemas.Select(c => _mapper.Map<ReadCinemaDto>(c)).ToList();

            return filmesDto;
        }

        public Result Atualizar(UpdateCinemaDto cinemaDto, int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            
            if (cinema == null)
            {
                return Result.Fail("Cinema não encontrado");
            }

            _mapper.Map(cinemaDto, cinema);
            _context.SaveChanges();

            return Result.Ok();
        }

        public Result Deletar(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return Result.Fail("Cinema não encontrado");
            }

            _context.Remove(cinema);
            _context.SaveChanges();

            return Result.Ok();
        }

        public ReadCinemaDto RecuperarCinema(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
            return cinemaDto;
        }
    }
}

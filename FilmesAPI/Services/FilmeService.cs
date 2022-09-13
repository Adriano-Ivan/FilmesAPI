using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Services
{
    public class FilmeService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public FilmeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadFilmeDto Adicionar(CreateFilmeDto filmeDto)
        {
            Filme filme = _mapper.Map<Filme>(filmeDto);

            _context.Filmes.Add(filme);
            _context.SaveChanges();

            ReadFilmeDto dto = _mapper.Map<ReadFilmeDto>(filme);
            return dto;
        }

        public IEnumerable<ReadFilmeDto> RecuperarFilmes(int? classificacaoEtaria)
        {
            if (classificacaoEtaria == null)
            {
                return _context.Filmes.ToList().Select(f => _mapper.Map<ReadFilmeDto>(f));
            }

            var filmes = _context.Filmes
                            .Where(filme => filme.ClassificacaoEtaria <= classificacaoEtaria);

            if (filmes != null)
            {
                var filmesDto = filmes.ToList().Select(f => _mapper.Map<ReadFilmeDto>(f));
                return filmesDto;
            }

            return null;
        }

        public ReadFilmeDto RecuperarFilme(int id)
        {
            Filme filme = _context.Filmes.Find(id);

            if(filme != null)
            {

                ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);
                filmeDto.HoraDaConsulta = DateTime.Now;

                return filmeDto;
            }

            return null;
        }

        public Result Atualizar(UpdateFilmeDto dto, int id)
        {
            Filme filme = _context.Filmes.Find(id);

            if (filme == null)
            {
                return Result.Fail("Filme não encontrado");
            }

            _mapper.Map(dto, filme);

            _context.SaveChanges();

            return Result.Ok();
        }

        public Result Deletar(int id)
        {
            Filme filme = _context.Filmes.Find(id);

            if (filme == null)
            {
                return Result.Fail("Filme não encontrado");
            }

            _context.Filmes.Remove(filme);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}

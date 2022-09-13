using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FluentResults;

namespace FilmesAPI.Services
{
    public class SessaoService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public SessaoService(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public Sessao Adicionar(CreateSessaoDto dto)
        {
            Sessao sessao = _mapper.Map<Sessao>(dto);

            _context.Sessoes.Add(sessao);
            _context.SaveChanges();

            return sessao;
        }

        public IEnumerable<ReadSessaoDto> RecuperarSessoes()
        {
           return _context.Sessoes.ToList().Select(s => _mapper.Map<ReadSessaoDto>(s));

        }

        public ReadSessaoDto RecuperarSessao(int id)
        {
            Sessao sessao = _context.Sessoes.Find(id);

            ReadSessaoDto sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);

            return sessaoDto;
        }

        public Result Deletar(int id)
        {
            Sessao sessao = _context.Sessoes.Find(id);

            if(sessao == null)
            {
                return Result.Fail("Sessão não encontrada");
            }

            _context.Sessoes.Remove(sessao);
            _context.SaveChanges();

            return Result.Ok();
        }

        public Result Atualizar(UpdateSessaoDto dto, int id)
        {
            Sessao sessao = _context.Sessoes.Find(id);

            if(sessao == null)
            {
                return Result.Fail("Sessão não encontrada");
            }

            _mapper.Map(dto, sessao);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}


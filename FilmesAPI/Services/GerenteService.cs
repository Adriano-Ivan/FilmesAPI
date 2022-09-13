using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FluentResults;

namespace FilmesAPI.Services
{
    public class GerenteService
    {
        private IMapper _mapper;
        private AppDbContext _context;

        public GerenteService(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public ReadGerenteDto Adicionar(CreateGerenteDto dto)
        {
            Gerente gerente = _mapper.Map<Gerente>(dto);

            _context.Gerentes.Add(gerente);
            _context.SaveChanges();

            ReadGerenteDto readDto = _mapper.Map<ReadGerenteDto>(gerente);
            return readDto;
        }

        public IEnumerable<ReadGerenteDto> RecuperarGerentes()
        {
            return _context.Gerentes.ToList().Select(g =>
               _mapper.Map<ReadGerenteDto>(g)
              );
        }

        public ReadGerenteDto RecuperarEndereco(int id)
        {
            Gerente gerente = _context.Gerentes.Find(id);

            ReadGerenteDto dto = _mapper.Map<ReadGerenteDto>(gerente);

            return dto;
        }

        public Result Deletar(int id)
        {
            Gerente gerente = _context.Gerentes.Find(id);

            if(gerente == null)
            {
                return Result.Fail("Gerente não encontrado");
            }

            _context.Gerentes.Remove(gerente);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}

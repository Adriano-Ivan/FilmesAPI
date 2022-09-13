using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FluentResults;

namespace FilmesAPI.Services
{
    public class EnderecoService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public EnderecoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadEnderecoDto Adicionar(CreateEnderecoDto enderecoDto)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();

            ReadEnderecoDto dto = _mapper.Map<ReadEnderecoDto>(endereco);
            return dto;
        }

        public IEnumerable<ReadEnderecoDto> RecuperarEnderecos()
        {
            return _context.Enderecos.ToList().Select(e => _mapper.Map<ReadEnderecoDto>(e));
        }

        public ReadEnderecoDto RecuperarEndereco(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);

            ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);

            return enderecoDto;
        }

        public Result Atualizar(int id, UpdateEnderecoDto enderecoDto)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
       
            if(endereco == null)
            {
                return Result.Fail("Endereço não encontrado");
            }

            _mapper.Map(enderecoDto, endereco);
            _context.SaveChanges();

            return Result.Ok();
        }

        public Result Deletar( int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(en => en.Id == id);

            if(endereco == null)
            {
                return Result.Fail("Endereço não encontrado");
            }

            _context.Enderecos.Remove(endereco);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}

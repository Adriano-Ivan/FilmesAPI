using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class CadastroService
    {
        private IMapper _mapper;
      
        private UserManager<IdentityUser<int>> _userManager;

        public CadastroService(IMapper  mapper, UserManager<IdentityUser<int>> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }
        public Result CadastrarUsuario(CreateUsuarioDto createUsuarioDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(createUsuarioDto);

            IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);

            var resultadoIdentity = _userManager.CreateAsync(usuarioIdentity,createUsuarioDto.Password);

            if (resultadoIdentity.Result.Succeeded)
            {
                
                return Result.Ok();
            }

            return Result.Fail("Falha ao cadastrar usuário");
        }
    }
}

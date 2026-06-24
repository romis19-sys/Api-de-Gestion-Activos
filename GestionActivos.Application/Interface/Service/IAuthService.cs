using GestionActivos.Application.DTOs.Usuario;
using GestionActivos.Application.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionActivos.Application.Interface.Service
{
    public interface IAuthService
    {
        Task<RespuestaLoginDto> LoginAsync(UsuarioLoginDto dto);
        Task<UsuarioDto> RegistrarUsuarioAsync(UsuarioRegistroDto dto);
        Task<RespuestaLoginDto> RefreshTokenAsync(string refreshToken);

    }
}

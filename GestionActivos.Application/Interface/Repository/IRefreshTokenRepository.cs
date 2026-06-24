
using GestionActivos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionActivos.Application.Interface.Repository
{
    public interface IRefreshTokenRepository
    {
        Task GuardarAsync(RefreshToken token);
        Task<RefreshToken?> ObtenerAsync(string token);
        Task ActualizarAsync(RefreshToken token);
    }
}

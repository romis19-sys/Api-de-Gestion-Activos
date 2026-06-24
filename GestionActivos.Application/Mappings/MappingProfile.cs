using AutoMapper;
using GestionActivos.Application.DTOs.Cliente;
using GestionActivos.Application.DTOs.Multa;
using GestionActivos.Application.DTOs.Pago;
using GestionActivos.Application.DTOs.Prestamo;
using GestionActivos.Application.DTOs.Usuario;
using GestionActivos.Domain.Entities;

namespace GestionActivos.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Mapeo de Usuario

            CreateMap<ApplicationUser, UsuarioDto>()
                .ForMember(dest => dest.Rol, opt => opt.Ignore());

            CreateMap<UsuarioRegistroDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<UsuarioActualizarDto, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.Ignore())
                .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
                .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
                .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore());

            #endregion

            #region Mapeo de Clientes

            CreateMap<Clientes, ClienteDto>();
            CreateMap<ClienteCrearDto, Clientes>();
            CreateMap<ClienteActualizarDto, Clientes>()
                .ForMember(c => c.ClienteId, opt => opt.Ignore());

            #endregion

            #region Mapeo de Prestamos
            CreateMap<Prestamos, PrestamoDto>();
            CreateMap<PrestamoCrearDto, Prestamos>();
            CreateMap<PrestamoActualizarDto, Prestamos>()
                .ForMember(p => p.PrestamoId, opt => opt.Ignore());
            #endregion

            #region Mapeo de Pagos
            CreateMap<Pagos, PagoDto>();
            CreateMap<PagoCrearDto, Pagos>()
                .ForMember(p => p.DetallesPagos, opt => opt.Ignore());
            CreateMap<PagoActualizarDto, Pagos>()
                .ForMember(p => p.PagoId, opt => opt.Ignore());

            CreateMap<DetallesPagos, DetallePagoDto>();
            CreateMap<DetallePagoCrearDto, DetallesPagos>()
                .ForMember(dest => dest.DetallesPagoId, opt => opt.Ignore())
                .ForMember(dest => dest.PagoId, opt => opt.Ignore())
                .ForMember(dest => dest.FechaRegistro, opt => opt.Ignore());
            #endregion

            #region Mapeo de Multas
            CreateMap<Multas, MultaDto>();
            CreateMap<MultaCrearDto, Multas>()
                .ForMember(m => m.DetallesMultas, opt => opt.Ignore());
            CreateMap<MultaActualizarDto, Multas>()
                .ForMember(m => m.MultasId, opt => opt.Ignore());

            CreateMap<DetallesMultas, DetalleMultaDto>();
            CreateMap<DetalleMultaCrearDto, DetallesMultas>()
                .ForMember(dest => dest.DetallesMultaId, opt => opt.Ignore())
                .ForMember(dest => dest.MultasId, opt => opt.Ignore())
                .ForMember(dest => dest.FechaRegistro, opt => opt.Ignore());
            #endregion
        }
    }
}

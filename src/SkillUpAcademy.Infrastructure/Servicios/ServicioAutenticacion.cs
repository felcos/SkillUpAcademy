using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SkillUpAcademy.Core.DTOs.Autenticacion;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Excepciones;
using SkillUpAcademy.Core.Interfaces.Servicios;

namespace SkillUpAcademy.Infrastructure.Servicios;

/// <summary>
/// Servicio de autenticación con ASP.NET Identity y JWT.
/// </summary>
public class ServicioAutenticacion(
    UserManager<UsuarioApp> _userManager,
    SignInManager<UsuarioApp> _signInManager,
    IConfiguration _configuracion) : IServicioAutenticacion
{
    /// <inheritdoc />
    public async Task<RespuestaLogin> RegistrarAsync(PeticionRegistro peticion)
    {
        if (string.IsNullOrWhiteSpace(peticion.Email))
            throw new ExcepcionValidacion("El email es obligatorio");

        UsuarioApp? existente = await _userManager.FindByEmailAsync(peticion.Email);
        if (existente != null)
            throw new ExcepcionValidacion("El email ya está registrado");

        UsuarioApp usuario = new UsuarioApp
        {
            UserName = peticion.Email,
            Email = peticion.Email,
            Nombre = peticion.Nombre,
            Apellidos = peticion.Apellidos,
            FechaCreacion = DateTime.UtcNow,
            UltimoAcceso = DateTime.UtcNow
        };

        IdentityResult resultado = await _userManager.CreateAsync(usuario, peticion.Contrasena);
        if (!resultado.Succeeded)
        {
            Dictionary<string, string[]> errores = resultado.Errors
                .GroupBy(e => e.Code)
                .ToDictionary(g => g.Key, g => g.Select(e => e.Description).ToArray());
            throw new ExcepcionValidacion(errores);
        }

        return GenerarRespuestaLogin(usuario);
    }

    /// <inheritdoc />
    public async Task<RespuestaLogin> IniciarSesionAsync(PeticionLogin peticion)
    {
        if (string.IsNullOrWhiteSpace(peticion.Email))
            throw new ExcepcionValidacion("El email es obligatorio");

        UsuarioApp? usuario = await _userManager.FindByEmailAsync(peticion.Email);
        if (usuario == null || !usuario.Activo)
            throw new ExcepcionValidacion("Credenciales inválidas");

        SignInResult resultado = await _signInManager.CheckPasswordSignInAsync(usuario, peticion.Contrasena, lockoutOnFailure: true);

        if (resultado.IsLockedOut)
            throw new ExcepcionValidacion("Cuenta bloqueada temporalmente. Inténtalo más tarde.");

        if (!resultado.Succeeded)
            throw new ExcepcionValidacion("Credenciales inválidas");

        usuario.UltimoAcceso = DateTime.UtcNow;
        await _userManager.UpdateAsync(usuario);

        return GenerarRespuestaLogin(usuario);
    }

    /// <inheritdoc />
    public async Task<RespuestaLogin> RenovarTokenAsync(PeticionRenovarToken peticion)
    {
        // En una implementación completa, validaríamos el refresh token contra BD.
        // Por ahora, decodificamos el JWT expirado para obtener el usuario.
        if (string.IsNullOrWhiteSpace(peticion.TokenRenovacion))
            throw new ExcepcionValidacion("El token de renovación es obligatorio");

        // Buscar usuario por el refresh token almacenado
        // TODO: Implementar tabla de refresh tokens
        throw new ExcepcionValidacion("Funcionalidad de renovación de token pendiente de implementar con tabla de refresh tokens");
    }

    /// <inheritdoc />
    public async Task CerrarSesionAsync(Guid usuarioId)
    {
        UsuarioApp? usuario = await _userManager.FindByIdAsync(usuarioId.ToString());
        if (usuario == null)
            throw new ExcepcionNoEncontrado("Usuario", usuarioId);

        // Invalidar refresh token
        await _userManager.UpdateSecurityStampAsync(usuario);
    }

    /// <inheritdoc />
    public async Task<PerfilUsuarioDto> ObtenerPerfilAsync(Guid usuarioId)
    {
        UsuarioApp? usuario = await _userManager.FindByIdAsync(usuarioId.ToString());
        if (usuario == null)
            throw new ExcepcionNoEncontrado("Usuario", usuarioId);

        return MapearAPerfil(usuario);
    }

    /// <inheritdoc />
    public async Task<PerfilUsuarioDto> ActualizarPerfilAsync(Guid usuarioId, PeticionActualizarPerfil peticion)
    {
        UsuarioApp? usuario = await _userManager.FindByIdAsync(usuarioId.ToString());
        if (usuario == null)
            throw new ExcepcionNoEncontrado("Usuario", usuarioId);

        if (!string.IsNullOrWhiteSpace(peticion.Nombre))
            usuario.Nombre = peticion.Nombre;

        if (!string.IsNullOrWhiteSpace(peticion.Apellidos))
            usuario.Apellidos = peticion.Apellidos;

        if (peticion.UrlAvatar != null)
            usuario.UrlAvatar = peticion.UrlAvatar;

        if (peticion.AudioHabilitado.HasValue)
            usuario.AudioHabilitado = peticion.AudioHabilitado.Value;

        if (!string.IsNullOrWhiteSpace(peticion.IdiomaPreferido))
            usuario.IdiomaPreferido = peticion.IdiomaPreferido;

        await _userManager.UpdateAsync(usuario);
        return MapearAPerfil(usuario);
    }

    /// <inheritdoc />
    public async Task CambiarContrasenaAsync(Guid usuarioId, string contrasenaActual, string contrasenaNueva)
    {
        UsuarioApp? usuario = await _userManager.FindByIdAsync(usuarioId.ToString());
        if (usuario == null)
            throw new ExcepcionNoEncontrado("Usuario", usuarioId);

        IdentityResult resultado = await _userManager.ChangePasswordAsync(usuario, contrasenaActual, contrasenaNueva);
        if (!resultado.Succeeded)
        {
            string errores = string.Join(", ", resultado.Errors.Select(e => e.Description));
            throw new ExcepcionValidacion($"Error al cambiar contraseña: {errores}");
        }
    }

    private RespuestaLogin GenerarRespuestaLogin(UsuarioApp usuario)
    {
        string secret = _configuracion["Jwt:Secret"] ?? throw new InvalidOperationException("JWT Secret no configurado");
        string issuer = _configuracion["Jwt:Issuer"] ?? "SkillUpAcademy";
        string audience = _configuracion["Jwt:Audience"] ?? "SkillUpAcademy";
        int expiracionMinutos = int.Parse(_configuracion["Jwt:AccessTokenExpirationMinutes"] ?? "60");

        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email ?? string.Empty),
            new Claim(ClaimTypes.Name, $"{usuario.Nombre} {usuario.Apellidos}"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        SymmetricSecurityKey clave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        SigningCredentials credenciales = new SigningCredentials(clave, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiracionMinutos),
            signingCredentials: credenciales
        );

        string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        string refreshToken = GenerarRefreshToken();

        return new RespuestaLogin
        {
            TokenAcceso = tokenString,
            TokenRenovacion = refreshToken,
            ExpiraEnSegundos = expiracionMinutos * 60,
            Usuario = MapearAPerfil(usuario)
        };
    }

    private static string GenerarRefreshToken()
    {
        byte[] bytes = new byte[64];
        using RandomNumberGenerator rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }

    private static PerfilUsuarioDto MapearAPerfil(UsuarioApp usuario)
    {
        return new PerfilUsuarioDto
        {
            Id = usuario.Id,
            Email = usuario.Email ?? string.Empty,
            Nombre = usuario.Nombre,
            Apellidos = usuario.Apellidos,
            UrlAvatar = usuario.UrlAvatar,
            PuntosTotales = usuario.PuntosTotales,
            RachaDias = usuario.RachaDias,
            AudioHabilitado = usuario.AudioHabilitado,
            IdiomaPreferido = usuario.IdiomaPreferido
        };
    }
}

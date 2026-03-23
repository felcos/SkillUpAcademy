using Microsoft.AspNetCore.Identity;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos;

/// <summary>
/// Sembrado del rol Admin y del usuario administrador por defecto.
/// </summary>
public static class SembradoAdmin
{
    /// <summary>
    /// Crea el rol "Admin" y el usuario admin@skillupacademy.com si no existen.
    /// </summary>
    public static async Task SembrarAsync(
        RoleManager<IdentityRole<Guid>> roleManager,
        UserManager<UsuarioApp> userManager)
    {
        // Crear rol Admin si no existe
        bool rolExiste = await roleManager.RoleExistsAsync("Admin");
        if (!rolExiste)
        {
            IdentityRole<Guid> rolAdmin = new IdentityRole<Guid>("Admin");
            await roleManager.CreateAsync(rolAdmin);
        }

        // Crear usuario admin si no existe
        string emailAdmin = "admin@skillupacademy.com";
        UsuarioApp? usuarioAdmin = await userManager.FindByEmailAsync(emailAdmin);
        if (usuarioAdmin == null)
        {
            usuarioAdmin = new UsuarioApp
            {
                UserName = emailAdmin,
                Email = emailAdmin,
                Nombre = "Administrador",
                Apellidos = "SkillUp",
                EmailConfirmed = true,
                FechaCreacion = DateTime.UtcNow,
                UltimoAcceso = DateTime.UtcNow,
                Activo = true
            };

            IdentityResult resultado = await userManager.CreateAsync(usuarioAdmin, "Admin123!");
            if (resultado.Succeeded)
            {
                await userManager.AddToRoleAsync(usuarioAdmin, "Admin");
            }
        }
        else
        {
            // Si ya existe pero no tiene el rol, asignarlo
            bool tieneRol = await userManager.IsInRoleAsync(usuarioAdmin, "Admin");
            if (!tieneRol)
            {
                await userManager.AddToRoleAsync(usuarioAdmin, "Admin");
            }
        }
    }
}

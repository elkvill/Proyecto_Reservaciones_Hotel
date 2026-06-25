using Hotel.Application.Interface.Repositorys;
using Hotel.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Hotel.Infrastructure.Repository
{
    public class DbSeederRepository : IdbSeederRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbSeederRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeederAsync()
        {
            // Crear roles si no existen
            string[] roleNames = { "Admin", "Recepcionista", "Cliente" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Crear administrador por defecto si no hay usuarios en la base de datos
            if (!await _userManager.Users.AnyAsync())
            {
                var adminUser = new ApplicationUser
                {
                    NombreCompleto = "Administrador del Sistema",
                    UserName = "admin@hotel.com",
                    Email = "admin@hotel.com",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    FechaRegistro = DateTime.UtcNow,
                    Estado = "Activo"
                };

                var resultado = await _userManager.CreateAsync(adminUser, "Admin123!");
                if (resultado.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement
{
    public class Program
    {
        //**Changes from DevLocal Branch**//
        //**Changes from DevLocal Branch amend changes**//
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await Seeds.DefaultRoles.SeedAsync(roleManager);
                await Seeds.DefaultUsers.SeedBasicUserAsync(userManager);
                await Seeds.DefaultUsers.SeedAdminUserAsync(userManager);
                await Seeds.DefaultUsers.SeedSuperAdminUserAsync(userManager,roleManager);


            }
            catch (System.Exception)
            {

                throw;
            }


            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

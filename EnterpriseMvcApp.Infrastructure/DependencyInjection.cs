using EnterpriseMvcApp.Application.Services;
using EnterpriseMvcApp.Domain.Interfaces;
using EnterpriseMvcApp.Infrastructure.Data.Context;
using EnterpriseMvcApp.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseMvcApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AplicacaoDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IClienteService, ClienteService>();

        return services;
    }
}

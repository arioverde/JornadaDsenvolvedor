using ApiTarefa.Repositories;
using ApiTarefa.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTarefa.Ioc
{
    public class ContainerDependencia
    {
        public static void RegistrarServicos(IServiceCollection services)
        {
            //repositorios
            services.AddScoped<EmpresaRepositorio, EmpresaRepositorio>();
            services.AddScoped<TarefaRepositorio, TarefaRepositorio>();
            services.AddScoped<UsuarioRepositorio, UsuarioRepositorio>();

            //services
            services.AddScoped<EmpresaService, EmpresaService>();
            services.AddScoped<TarefaService, TarefaService>();
            services.AddScoped<UsuarioService, UsuarioService>();
            services.AddScoped<AutorizacaoService, AutorizacaoService>();
        }
    }
}

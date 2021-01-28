using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SupplyChain.Client.HelperService;
using Syncfusion.Blazor;
using Microsoft.AspNetCore.Components.Authorization;
using SupplyChain.Client.Auth;

namespace SupplyChain.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider
                .RegisterLicense("MzcxMzE0QDMxMzgyZTM0MmUzMG0wbHRCMGNDZi9oUFhMVlp1V3l4NG10OFNmejVQVnpEMzFBQ3p2M0VqYms9");
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddSingleton(sp => 
                new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            ConfigureServices(builder.Services);
            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();//Sistema de Autorizacion
            services.AddAuthorizationCore();
            services.AddScoped<ProveedorAutenticacion>();
            services.AddScoped<AuthenticationStateProvider, ProveedorAutenticacion>(
                provider=>  provider.GetRequiredService<ProveedorAutenticacion>());

            services.AddScoped<ILoginService, ProveedorAutenticacion>(
                provider => provider.GetRequiredService<ProveedorAutenticacion>());

            services.AddSingleton<ToastService>();
        }
    }
}

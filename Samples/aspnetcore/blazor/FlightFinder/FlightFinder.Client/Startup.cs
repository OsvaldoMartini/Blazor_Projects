﻿using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using FlightFinder.Client.Services;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FlightFinder.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<AppState>();
            services.AddStorage();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<Main>("body");
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenWebRXAntennaSwitcher.WebApi.Extensions;
using OpenWebRXAntennaSwitcher.WebApi.Services.AntennaSwitching;
using System.Linq;

namespace OpenWebRXAntennaSwitcher.WebApi;

public class Program
{
    private const string AllowedOriginsAny = "*";

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        var config = builder.Configuration;

        // Configuration:
        services.Configure<SerialAntennaSwitchOptions>(config.GetSection(nameof(SerialAntennaSwitchOptions)));

        var allowedOriginsValue = config.GetValue<string>("AllowedOrigins") ?? AllowedOriginsAny;
        var allowedOrigins = allowedOriginsValue.Split(';');

        // Add services:
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyMethod()
                    .AllowAnyHeader();

                if (allowedOrigins.Contains(AllowedOriginsAny))
                {
                    policy.AllowAnyOrigin();
                }
                else
                {
                    policy.WithOrigins(allowedOrigins);
                }
            });
        });
        services.AddControllers();
        services.AddRazorPages();
        services.AddSwaggerGen();

        services.AddHostedService<ISerialAntennaSwitch, SerialAntennaSwitch>();

        var app = builder.Build();

        // Configure the HTTP request pipeline:
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            //app.UseHsts();
        }

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseCors();

        app.UseAuthorization();

        app.MapRazorPages();

        app.MapControllers();

        app.Run();
    }
}

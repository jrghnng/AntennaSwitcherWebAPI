using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenWebRXAntennaSwitcher.WebApi.Extensions;
using OpenWebRXAntennaSwitcher.WebApi.Services.AntennaSwitching;

namespace OpenWebRXAntennaSwitcher.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        var config = builder.Configuration;

        // Configuration:
        services.Configure<SerialAntennaSwitchOptions>(config.GetSection(nameof(SerialAntennaSwitchOptions)));

        // Add services:
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

        app.UseAuthorization();

        app.MapRazorPages();

        app.MapControllers();

        app.Run();
    }
}

using Data;
using Data.Intarfaces;
using Data.RepoData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Mqtt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domotica.Api
{
    public class Startup
    {
       // MsgMqttNet MQTTNET = new MsgMqttNet();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;



            Task.Run(async () => {
                await MsgMqttNet.ConnectMqttServerAsync(
                   // Guid.NewGuid().ToString(),
                   "123",
                    "ioticos.org",
                    "fwTvabtM4ugdeX7",
                    "5lf9ZgQ5YZJLY3D",
                    1883);
            });

            //Task.Run(async () => {
            //    await MsgMqttNet.ConnectMqttServerAsync(
            //        "client001",
            //        "localhost",
            //        "username001",
            //        "psw001",
            //        8222);
            //});

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SparkDBContext>(
            options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionMain")));

            services.AddScoped<IDispositivoData, DispositivoData>();
            services.AddScoped<ICuentaData, CuentaData>();
            services.AddScoped<ILugarData, LugarData>();
            services.AddScoped<ILugarRegionData, LugarRegionData>();
            services.AddScoped<ILoginData, LoginData>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Domotica.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
               
            }
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Domotica.Api v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(builder => builder.
          AllowAnyOrigin().
          AllowAnyHeader().
          AllowAnyMethod()
          //.AllowCredentials()
          );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

using AutoMapper;
using ProductService.Configuration;
using ProductService.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProductService.Clients;
using Refit;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ProductService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductService", Version = "v1" });
            });

            services.AddAutoMapper(typeof(Startup));

            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new AutoMapping());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            
            var refitSettings = new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                })
            };
            services.TryAddTransient<IImageClient>(_ => RestService.For<IImageClient>(new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:44328")
            }, refitSettings));

            services.TryAddTransient<IPriceClient>(_ => RestService.For<IPriceClient>(new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:44391")
            }, refitSettings));
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ProductContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<IProductService, ProductService>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
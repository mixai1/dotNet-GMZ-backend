using dotNet_GMZ_backend.Models.AppSettingsModels;
using dotNet_GMZ_backend.Services.MapperService;
using dotNet_GMZ_backend.Models.IdentityModels;
using dotNet_GMZ_backend.DAL.Repository;
using dotNet_GMZ_backend.Core;
using dotNet_GMZ_backend.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text;
using AutoMapper;
using Serilog;
using System;
using MediatR;


namespace dotNet_GMZ_backend
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
            services.Configure<AppSettings>(Configuration.GetSection("ApplicationSettings"));
            services.AddCors();
            services.AddControllers();
            var assembly = AppDomain.CurrentDomain.Load("dotNet-GMZ-backend.CQRS");
            services.AddMediatR(assembly);
            services.AddTransient<INewsRecordRepository, NewsRecordRepository>();
            services.AddAutoMapper(typeof(AutoMapperApp).Assembly);
            services.AddDbContext<AppDbContext>(op =>
            {
                op.UseSqlServer(Configuration.GetConnectionString("Default"));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "dotNet_GMZ_backend", Version = "v1" });
            });
            services.AddIdentity<UserApp, RoleApp>(opts =>
                {
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequiredLength = 6;
                    opts.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<AppDbContext>();

            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());
            Console.WriteLine(key);
            services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(j =>
                {
                    j.RequireHttpsMetadata = false;
                    j.SaveToken = false;
                    j.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (ctx, next) =>
            {
                await next();
                if (ctx.Response.StatusCode == 204)
                {
                    ctx.Response.ContentLength = 0;
                }
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint(
                    "/swagger/v1/swagger.json", "dotNet_GMZ_backend v1"));
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
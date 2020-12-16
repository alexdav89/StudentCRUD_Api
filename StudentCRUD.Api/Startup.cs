using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using StudentCRUD.Business.Entities.DomainModels;
using StudentCRUD.Data;
using Core.Common.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetCore.AutoRegisterDi;
using Microsoft.AspNetCore.HttpOverrides;

namespace StudentCRUD.Api {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddCors(options => {
                options.AddPolicy("AllowAll",
                builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            });
            services.AddControllers()
                .AddJsonOptions(configure =>
                {
                    configure.JsonSerializerOptions.AllowTrailingCommas = true;
                });

            #region Dependency Injections
            // AutoRegisterDi package using
            var assembliesToScan = new[] {
                Assembly.GetExecutingAssembly(),
                Assembly.GetAssembly(typeof(Business.IDependency)),
                Assembly.GetAssembly(typeof(Data.IDependency))
            };
            // register services only
            services.RegisterAssemblyPublicNonGenericClasses(assembliesToScan)
                .Where(c => c.Name.EndsWith("Service") || c.Name.EndsWith("Repository")).AsPublicImplementedInterfaces();
            #endregion
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            services.AddHttpContextAccessor();
            InitSettings();
            #region Entity Framework
            services.AddDbContext<Data.StudentCRUDContext>(options => options.UseSqlite("Filename=./StudentsDb.db"));
            services.AddScoped(typeof(Core.Common.Data.IRepository<,>), typeof(Core.Common.Data.EF.Repository<,>));


            #endregion           
                    

            #region Swagger
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Core Cases Service", Version = "v1" });
                // Authorization header
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Description = @"Authorization header using the Bearer scheme. <br/>
                      Enter 'Bearer' [space] and then your token in the text input below.
                      <br/> Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityDefinition("token", new OpenApiSecurityScheme {
                    Description = @"JWT user encrypted token header",
                    Name = "token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                {
                  new OpenApiSecurityScheme
                  {
                    Reference = new OpenApiReference
                      {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                      },
                      Scheme = "oauth2",
                      Name = "Bearer",
                      In = ParameterLocation.Header,

                    },
                    new List<string>()
                  }
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                {
                  new OpenApiSecurityScheme
                  {
                    Reference = new OpenApiReference
                      {
                        Type = ReferenceType.SecurityScheme,
                        Id = "token"
                      },
                      Scheme = "oauth2",
                      Name = "token",
                      In = ParameterLocation.Header,

                    },
                    new List<string>()
                  }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            #endregion
            services.AddMvc(options => options.EnableEndpointRouting = false).AddNewtonsoftJson();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            
            app.UseCors("AllowAll");
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseHsts();
            }
            app.UseForwardedHeaders(new ForwardedHeadersOptions {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseHttpsRedirection();            

            app.UseRouting();
            app.UseMvc();
            //app.UseAuthentication();
            app.UseAuthorization();
            #region Swagger
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentCRUD API V1");
            }); 
            #endregion
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }

        private void InitSettings() {
            const string CONNECTIONS_SECTION = "ConnectionStrings";
            const string APPSETTINGS_SECTION = "AppSettings";
            //Connections
            if (Configuration.GetSection(CONNECTIONS_SECTION).Exists()) {
                foreach (var item in Configuration.GetSection(CONNECTIONS_SECTION).AsEnumerable()) {
                    var key = item.Key.Replace(CONNECTIONS_SECTION, "");
                    if (!string.IsNullOrWhiteSpace(key)) {
                        ConfigurationManager.ConnectionStrings.Add(key.TrimStart(':'), new ConfigConnection { ConnectionString = item.Value });
                    }
                }
            }

            //AppSettings
            if (Configuration.GetSection(APPSETTINGS_SECTION).Exists()) {
                foreach (var item in Configuration.GetSection(APPSETTINGS_SECTION).AsEnumerable()) {
                    var key = item.Key.Replace(APPSETTINGS_SECTION, "");
                    if (!string.IsNullOrWhiteSpace(key)) {
                        ConfigurationManager.AppSettings.Add(key.TrimStart(':'), item.Value);
                    }
                }
            }
        }
    }
}

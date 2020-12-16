using StudentCRUD.Business.Entities.DomainModels;
using StudentCRUD.Data;
using Core.Common.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace StudentCRUD.Tests {
    public class TestBase {
        protected ServiceProvider ServiceProvider;
        private readonly ITestOutputHelper Output;

        public TestBase(ITestOutputHelper output) {
            Output = output;
            InitSettings();
            var services = new ServiceCollection();
            #region Dependency Injections
            // AutoRegisterDi package using
            var assembliesToScan = new[] {
                Assembly.GetExecutingAssembly(),
                Assembly.GetAssembly(typeof(StudentCRUD.Business.IDependency)),
                Assembly.GetAssembly(typeof(Data.IDependency))
            };
            // register services only
            services.RegisterAssemblyPublicNonGenericClasses(assembliesToScan)
                .Where(c => c.Name.EndsWith("Service") || c.Name.EndsWith("Repository")).AsPublicImplementedInterfaces();
            #endregion

            #region Entity Framework
            services.AddDbContext<Data.StudentCRUDContext>(options => options.UseSqlite("Filename=./StudentsDb.db"));
            services.AddScoped(typeof(Core.Common.Data.IRepository<,>), typeof(Core.Common.Data.EF.Repository<,>));

            //services.AddIdentity<User, IdentityRole>()
            //    .AddEntityFrameworkStores<StudentCRUDContext>()
            //    .AddDefaultTokenProviders();


            #endregion

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);            
            ServiceProvider = services.BuildServiceProvider();
        }

        public T ResolveDependency<T>() {
            return ServiceProvider.GetService<T>();
        }

        public T ParseJson<T>(string json) {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public void PrintOutput(object obj) {
            Output.WriteLine(JsonConvert.SerializeObject(obj, Formatting.Indented));
            //Console.WriteLine(JsonConvert.SerializeObject(obj, Formatting.Indented));
        }


        public static void InitSettings() {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables();
            IConfiguration Configuration = builder.Build();

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

using AutoMapper;
using FluentValidation.AspNetCore;
using Gtm.Business.Domain.Managers.Product;
using Gtm.Business.Domain.Managers.Product.ReadAll;
using Gtm.Business.Domain.Managers.ProductPrice;
using Gtm.Business.Domain.Managers.ProductPrice.Read;
using Gtm.Business.Domain.Managers.ProductPrice.Save;
using Gtm.Business.Domain.Managers.User.SignIn;
using Gtm.Business.Infrastructure.Helpers.Filters;
using Gtm.Business.Infrastructure.Helpers.Interfaces;
using Gtm.Business.Infrastructure.Repositories.Country;
using Gtm.Business.Infrastructure.Repositories.Product;
using Gtm.Business.Infrastructure.Repositories.ProductPrice;
using Gtm.Business.Infrastructure.Repositories.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Gtm.Business.Infrastructure.Helpers
{
    /// <summary>
    /// Extension methods for adding services
    /// </summary>
    public static class DependencyGroupServiceCollection
    {
        #region Methods

        #region public

        /// <summary>
        /// Method to add the services group to the container.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        /// <param name="allowSpecificOrigins">Allow specific origins.</param>
        /// <param name="apiVersion">Api version</param>
        /// <param name="loggerType">Logger Type</param>
        /// <exception cref="ProviderServiceNotFoundException">Represents errors that occur when service provider tries to get a service.</exception>
        public static void AddDependencyGroup(this IServiceCollection services, IConfiguration configuration, string allowSpecificOrigins, Version apiVersion, Type loggerType)
        {
            AppConfig appConfig = configuration.GetSection("AppConfig").Get<AppConfig>();

            services.AddSingleton<IAppConfig>(c => appConfig);

            services.AddScoped<IApplicationContext, ApplicationContext>();

            services.AddScoped<ApplicationContext>();
            services.AddScoped<IApplicationContext>(c => c.GetRequiredService<ApplicationContext>());
            services.AddScoped<ISetApplicationContext>(c => c.GetRequiredService<ApplicationContext>());

            services.AddScoped<ITokenJwtHelper, TokenJwtHelper>();

            services.AddControllers();
            services.AddControllers(
                options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true
            );

            services.AddHttpContextAccessor();

            services.AddFluentValidationAutoValidation();

            services.AddApiVersioning(version =>
            {
                version.DefaultApiVersion = new ApiVersion(apiVersion.Major, apiVersion.Minor);
                version.AssumeDefaultVersionWhenUnspecified = true;
                version.ReportApiVersions = true;
                version.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            });

            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(t => t.ToString());
                c.EnableAnnotations();
                c.OperationFilter<HeaderParameterOperationFilter>();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                foreach (IConfigurationOpenApiInfo item in appConfig.Documents)
                {
                    c.SwaggerDoc(item.DocumentName, new OpenApiInfo
                    {
                        Version = apiVersion.ToString(2),
                        Title = item.Title,
                        Description = item.Description,
                        TermsOfService = new Uri(appConfig.TermsOfServiceOpenApi),
                        Contact = new OpenApiContact
                        {
                            Name = appConfig.NameOpenApiContact,
                            Email = appConfig.EmailOpenApiContact,
                            Url = new Uri(appConfig.UrlOpenApiContact),
                        },
                        License = new OpenApiLicense
                        {
                            Name = appConfig.NameOpenApiLicense,
                            Url = new Uri(appConfig.UrlOpenApiLicense),
                        }
                    });
                }
            });
            services.AddSwaggerGenNewtonsoftSupport();

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            services.AddMvcCore(options => options.EnableEndpointRouting = false)
                    .AddCors(options =>
                    {
                        options.AddPolicy(name: allowSpecificOrigins,
                                          builder =>
                                          {
                                              builder
                                                .WithMethods(appConfig.AllowedMethods)
                                                .WithHeaders(appConfig.AllowedHeaders)
                                                .AllowCredentials();

                                              if (appConfig.AllowedOrigins?.Length > 0)
                                              {
                                                  builder
                                                    .WithOrigins(appConfig.AllowedOrigins);
                                              }
                                          });
                    });

            services
                .AddControllers(options =>
                {
                    options.Filters.Add(typeof(HttpResponseExceptionFilter));
                })
                .AddNewtonsoftJson(opts =>
                {
                    opts.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, configureOptions: null);

            services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();

            AddMapperConfiguration(services);
            RegisterManager(services);
            RegisterRepository(services);
        }

        #endregion

        #region private

        /// <summary>
        /// Adds object mappers to configuration source for generated mappers
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        private static void AddMapperConfiguration(IServiceCollection services)
        {
            var configuration = new MapperConfiguration(c =>
            {
                c.AddProductProfiles();
                c.AddProductPriceProfiles();
            });

            configuration.CreateMapper();

            services.AddSingleton<IMapper>(c => new Mapper(configuration));
        }

        /// <summary>
        /// Registers manager services
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        private static void RegisterManager(IServiceCollection services)
        {
            services.AddScoped<ISignInManager, SignInManager>();
            services.AddScoped<IReadAllManager, ReadAllManager>();
            services.AddScoped<IReadManager, ReadManager>();
            services.AddScoped<ISaveManager, SaveManager>();
        }

        /// <summary>
        /// Registers repository services
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        private static void RegisterRepository(IServiceCollection services)
        {
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductPriceRepository, ProductPriceRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        #endregion

        #endregion
    }
}

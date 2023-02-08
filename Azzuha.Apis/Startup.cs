using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azzuha.Common;
using Azzuha.DataViewModels.Common;
using Azzuha.Services.Implementation.admin;
using Azzuha.Services.Implementation.v1;
using Azzuha.Services.Interface.admin;
using Azzuha.Services.Interface.v1;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Azzuha.Apis
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _env = environment;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


        private readonly IWebHostEnvironment _env;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;
            });

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
                x.MultipartHeadersLengthLimit = int.MaxValue;
            });

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger  Documentation", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
   {
     new OpenApiSecurityScheme
     {
       Reference = new OpenApiReference
       {
         Type = ReferenceType.SecurityScheme,
         Id = "Bearer"
       }
      },
      new string[] { }
    }
  });
                c.CustomSchemaIds(x => x.FullName);
            });
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200",
                            "http://localhost:4000",
                            "https://azzuhaislamicinstitute.com",
                            "http://azzuhaislamicinstitute.com")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });
            services.AddScoped<IAccountService, AccountService>();
         
            services.AddScoped<ICommonService, CommonService>();
        
            services.AddScoped<IEmailSubscriptionService, EmailSubscriptionService>();
            services.AddScoped<IEmailSubscriptionService, EmailSubscriptionService>();

            services.AddScoped<IEmailSubscriptionService, EmailSubscriptionService>();

            services.AddScoped<IUpEventAdminService, UpEventAdminService>();
            services.AddScoped<IUpEventAdminService, UpEventAdminService>();

            services.AddScoped<IGalleryService, GalleryService>();
            services.AddScoped<ICoursesService, CoursesService>();
            services.AddScoped<IAboutUsService, AboutUsService>();
            services.AddScoped<IMediaService, MediaService>();








        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Use(async (context, next) =>
                {
                    var error = context.Features[typeof(IExceptionHandlerFeature)] as IExceptionHandlerFeature;

                    if (error != null && error.Error != null)
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";

                        var path = context.Request.Path.Value;
                        var controller = path.Split('/')[2];
                        var action = path.Split('/')[3] ?? "";

                        var userId = context?.GetRouteData()?.Values["userId"]?.ToString();

                        MakeLog Err = new MakeLog();
                        Err.ErrorLog(_env.WebRootPath, "/" + controller + "/" + action + ".txt", "UserId: " + userId + " Error: " + error.Error?.Message ?? "" + "Inner Exception => " + error.Error.InnerException?.Message ?? "");

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new Response<bool>
                        {
                            isError = true,
                            messages = Error.ServerError,
                            data = false
                        }));
                    }
                    //when no error, do next.

                    else await next();
                });
            });



            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
                    ctx.Context.Response.Headers.Append("Access-Control-Allow-Headers",
                        "Origin, X-Requested-With, Content-Type, Accept");
                },

            });

            app.UseRouting();


            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Documentation V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}

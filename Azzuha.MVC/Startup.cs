using Azzuha.AdminPanel.Data;
using Azzuha.Services.Implementation.admin;
using Azzuha.Services.Interface.admin;
using Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Azzuha.AdminPanel
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
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetValue<String>("ConnectionStrings:DefaultConnection")));
            services.AddHangfireServer();

            services
    .AddMvc()
    .AddRazorRuntimeCompilation()
    .AddJsonOptions(jsonOptions =>
    {
        jsonOptions.JsonSerializerOptions.IgnoreNullValues = true;
    });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Expiration = TimeSpan.FromDays(2);
                options.LoginPath = new PathString("/Account/Login");
            });
            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Cookie.Name = "Cookie";
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
            //    options.LoginPath = new PathString("/Account/Login");
            //    options.LogoutPath = $"/Account/Logout";
            //    options.AccessDeniedPath = $"/Account/AccessDenied";
            //    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            //    options.SlidingExpiration = true;
            //});
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();

            services.AddScoped<IAdminAccountService, AdminAccountService>();
            services.AddScoped<IEmailSubscriptionAdminService, EmailSubscriptionAdminService>();
            services.AddScoped<IAdminAboutUsService, AdminAboutUsService>();
            services.AddScoped<IAdminGalleryService, AdminGalleryService>();
            services.AddScoped<IAdminCoursesService, AdminCoursesService>();
            services.AddScoped<IUpEventAdminService, UpEventAdminService>();
            services.AddScoped<IAdminMediaService, AdminMediaService>();
            services.AddScoped<IAdminListingService, AdminListingService>();
            services.AddScoped<IAdminJsonEditorService, AdminJsonEditorService>();
            services.AddScoped<IAdminSliderService, AdminSliderService>();





            #region BackgroundJob
            #endregion

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            services.AddMvc().AddRazorPagesOptions(o => o.Conventions.AddAreaFolderRouteModelConvention("Identity", "/Account/", model =>
            {
                foreach (var selector in model.Selectors)
                {
                    var attributeRouteModel = selector.AttributeRouteModel;
                    attributeRouteModel.Order = -1;
                    attributeRouteModel.Template = attributeRouteModel.Template.Remove(0, "Identity".Length);
                }
            })
   ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //services.AddMvc(o =>
            //{
            //    //Add Authentication to all Controllers by default.
            //    var policy = new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        .Build();
            //    o.Filters.Add(new AuthorizeFilter(policy));
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRecurringJobManager recurringJobManager, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHangfireDashboard("/mydashboard");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Path.Value.ToLower().StartsWith("/identity/account/login"))
            //    {
            //        context.Response.StatusCode = 404; //Not found
            //        return;
            //    }
            //    await next();
            //});
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
                endpoints.MapRazorPages();
            });


        }
    }
}

using BidToBuy.Auth;
using BidToBuy.Auth.Model;
using BidToBuy.Data;
using BidToBuy.Data.Dtos.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Data.Repositories;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors(c =>
            //{
            //c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            //}
            //);
            services.AddIdentity<RestUser,IdentityRole>().AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters.ValidAudience = Configuration["JWT:ValidAudience"];
                options.TokenValidationParameters.ValidIssuer = Configuration["JWT:ValidIssuer"];
                options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]));
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyNames.SameUser, policy => policy.Requirements.Add(new SameUserRequirement()));
            });
            services.AddDbContext<DataContext>(options => {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddSingleton<IAuthorizationHandler, SameUserAuthorizationHandler>();
            //services.AddDbContext<DataContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDbContext<DataContext>();
            //services.AddScoped(provider=>provider.GetService<DataContext>());
            services.AddScoped<UserManager<RestUser>>();
            //services.AddScoped<>
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            
            services.AddTransient<IItemsRepository, ItemsRepository>();
            services.AddTransient<IImagesRepository, ImagesRepository>();
            services.AddTransient<IEventsRepository, EventsRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IRatingsRepository, RatingsRepository>();
            services.AddTransient<ITokenManager, TokenManager>();
            services.AddTransient<DatabaseSeeder, DatabaseSeeder>();
            //services.AddTransient<IDataContext,DataContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection(); //Nauja eilute
            app.UseRouting();
            app.UseStaticFiles(); //Nauja eilute
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });
           // string aaa = Directory.GetCurrentDirectory();
            //app.UseStaticFiles(new StaticFileOptions
            //{
                
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Data/Photos")),
            //    RequestPath = "/Data/Photos"

            //});


        }
    }
}


/*  "ConnectionStrings": {
    "DefaultConnection": "Host=ec2-52-208-221-89.eu-west-1.compute.amazonaws.com;Port=5432;Database=d8bscav2fabvet;User Id=axrmtykrtufjhf;Password=e3b51a3934a6d21d1fb99d098ae157889d4fe89113166a7c9e074d91e8758069;"
  },*/
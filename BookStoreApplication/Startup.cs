using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreManagerLayer.IManager;
using BookStoreManagerLayer.ManagerImplemantation;
using BookStoreRepositoryLayer;
using BookStoreRepositoryLayer.IRepository;
using BookStoreRepositoryLayer.RepositoryImplemantation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace BookStoreApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddTransient<IAccountManager, AccountDetialsManager>();
            services.AddTransient<IAccountDetialsRepository, AccountDetialsRepositoy>();
            services.AddTransient<IBookStoreManager, BookStoreManager>();
            services.AddTransient<IBookDetialsRepository, BookDetialsRepository>();
            services.AddTransient<ICartDetailsManager, CartDetailsManager>();
            services.AddTransient<ICartDetialsRepository, CartDetailsRepository>();
            services.AddTransient<IWishListManager, WishListManager>();
            services.AddTransient<IWishListRepository, WishListRepository>();
            services.AddTransient<IOrderDetialsManager,OrderDetialsMaganer>();
            services.AddTransient<IOrderDetialsRepository, OrderDetialsReposiory>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("V1", new Info { Title = "BookStoreApplication", Version = "V1" });

                //// this service handles the jwt token suppying in headers
                options.AddSecurityDefinition("oauth2", new ApiKeyScheme
                {
                    Description = "Using the jwt bearer token",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            ///*******************
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(options =>
                  {
                      var serverSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:key"]));
                      options.TokenValidationParameters = new TokenValidationParameters
                      {
                          IssuerSigningKey = serverSecret,
                          ValidIssuer = Configuration["JWT:Issuer"],
                          ValidAudience = Configuration["JWT:Audience"]
                      };
                  });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/V1/Swagger.json", "BookStoreApplication v1");
                //c.RoutePrefix = "";
            });
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

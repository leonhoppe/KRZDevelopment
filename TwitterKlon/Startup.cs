using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using TwitterKlon.Contract.Logic.Users;
using TwitterKlon.Contract.Logic.Categories;
using TwitterKlon.Contract.Logic.Posts;
using TwitterKlon.Contract.Logic.Comments;
using TwitterKlon.Persistence.Categories;
using TwitterKlon.Persistence.Comments;
using TwitterKlon.Persistence.Posts;
using TwitterKlon.Persistence.Users;
using TwitterKlon.Persistence;
using TwitterKlon.Logic.Categories;
using TwitterKlon.Logic.Comments;
using TwitterKlon.Logic.Posts;
using TwitterKlon.Logic.Users;
using TwitterKlon.Contract.Persistence.Categories;
using TwitterKlon.Contract.Persistence.Comments;
using TwitterKlon.Contract.Persistence.Posts;
using TwitterKlon.Contract.Persistence.Users;
using TwitterKlon.Persistence.Sessions;
using TwitterKlon.Contract.Persistence.Sessions;
using TwitterKlon.Security;
using TwitterKlon.Security.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using TwitterKlon.Logic.Sessions;
using TwitterKlon.Contract.Logic.Tokens;
using TwitterKlon.Options;

namespace TwitterKlon
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
          services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
          services.AddScoped<ISessionContext, SessionContext>();
          services.AddScoped<ITokenRepository, TokenRepository>();
          services.AddScoped<IUserRepository, UserRepository>();
          services.AddScoped<IPostRepository, PostRepository>();
          services.AddScoped<ICommentRepository, CommentRepository>();
          services.AddScoped<ICategoryRepository, CategoryRepository>();
          services.AddScoped<ITokenLogic, TokenLogic>();
          services.AddScoped<IUserLogic, UserLogic>();
          services.AddScoped<IPostLogic, PostLogic>();
          services.AddScoped<ICommentLogic, CommentLogic>();
          services.AddScoped<ICategoryLogic, CategoryLogic>();
          services.AddOptionsFromConfiguration<JwtTokenAuthenticationOptions>(this.Configuration);
          services.AddDbContext<DatabaseContext>();
          services.AddCors(); // Make sure you call this previous to AddMvc
          services.AddAuthentication(JwtTokenAuthentication.Scheme)
            .AddJwtTokenAuthentication(this.Configuration);
          services.AddControllers();
          services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
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
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TwitterKlon v1"));
            }

            app.UseCors(
                options => options
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            );

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

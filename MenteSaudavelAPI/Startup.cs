using MenteSaudavelAPI._02.Services;
using MenteSaudavelAPI._02.Services.Interfaces.Services;
using MenteSaudavelAPI._02.Services.Services;
using Microsoft.EntityFrameworkCore;

namespace MenteSaudavelAPI
{
    public static class Startup
    {
        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            IServiceCollection services = builder.Services;

            services.AddCors(options =>
            {
                options.AddPolicy("AllowReact",
                    policy =>
                    {
                        policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithOrigins("http://localhost:5173"); // Alterar a porta se necessário
                    }
                );
            });

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("A connection string 'DefaultConnection' não foi configurada.");

            services.AddDbContext<DataBaseContext>(options =>
                options.UseSqlServer(connectionString)
            );

            services.AddTransient<DataBaseContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IPasswordHashService, PasswordHashService>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IQuestionarioService, QuestionarioService>();
        }

        public static void ConfigureApplication(WebApplication app)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowReact");

            app.MapControllers();
        }
    }
}
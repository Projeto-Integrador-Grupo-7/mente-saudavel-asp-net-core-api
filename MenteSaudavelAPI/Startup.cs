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

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowReactDev", policy =>
            //    {
            //        policy.AllowAnyOrigin()
            //              .AllowAnyHeader()
            //              .AllowAnyMethod();
            //    });
            //});

            services.AddControllers();
            services.AddAuthentication();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<DataBaseContext>(options =>
                options.UseSqlServer("Server=localhost;Database=MenteSaudavelAPI;Trusted_Connection=True;TrustServerCertificate=True;")
            );

            services.AddTransient<DataBaseContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IQuestionarioService, QuestionarioService>();
        }

        public static void ConfigureApplication(WebApplication app)
        {
            //app.UseCors("AllowReactDev");

            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseHttpsRedirection();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            //app.MapFallbackToFile("/index.html");
        }
    }
}
using System.Text;
using System.Threading.RateLimiting;
using MenteSaudavelAPI._02.Services;
using MenteSaudavelAPI._02.Services.Interfaces.Services;
using MenteSaudavelAPI._02.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IQuestionarioService, QuestionarioService>();

            ConfigureAuthentication(builder);
            ConfigureRateLimiter(services);
        }

        private static void ConfigureAuthentication(WebApplicationBuilder builder)
        {
            IConfigurationSection jwt = builder.Configuration.GetSection("Jwt");
            string chave = jwt["Key"]
                ?? throw new InvalidOperationException("A chave JWT ('Jwt:Key') não foi configurada.");

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwt["Issuer"],
                        ValidAudience = jwt["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chave)),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            builder.Services.AddAuthorization();
        }

        private static void ConfigureRateLimiter(IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                // Limite por IP no login: dificulta força bruta. BCrypt é caro, então
                // poucas tentativas por minuto já bastam para uso legítimo.
                options.AddPolicy("login", httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "desconhecido",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 5,
                            Window = TimeSpan.FromMinutes(1),
                            QueueLimit = 0
                        }));
            });
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

            app.UseRateLimiter();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
        }
    }
}

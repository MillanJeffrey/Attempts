using Attempt1.Data;
using Attempt1.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Attempt1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<DapperContext>();

            var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
            var secretKey = jwtSettingsSection["SecretKey"]!;

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {                     
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettingsSection["Issuer"],
                        ValidAudience = jwtSettingsSection["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });

            var app = builder.Build();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI();
            

            app.UseHttpsRedirection();
            app.MapControllers();
            app.Run();
        }
    }
}

using APIGOTinforcavado.Data;
using APIGOTinforcavado.Repositories;
using APIGOTinforcavado.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using APIGOTinforcavado.Models;

namespace APIGOTinforcavado
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<ConnectionStrings>(
                builder.Configuration.GetSection("ConnectionStrings")
            );


            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddScoped<ComentarioRepository>();
            builder.Services.AddScoped<EventoRepository>();
            builder.Services.AddScoped<TicketRepository>();
            builder.Services.AddScoped<UtilizadorRepository>();
            builder.Services.AddScoped<ComentarioService>();
            builder.Services.AddScoped<TicketService>();
            builder.Services.AddScoped<EmailSender>();
            builder.Services.AddScoped<JwtGenerator>();
            builder.Services.AddScoped<UtilizadorService>();
            builder.Services.AddScoped<EventoService>();
            builder.Services.AddScoped<NewsLetterRepository>();
            builder.Services.AddScoped<NewsLetterService>();


            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!);
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            builder.Services.AddAuthorization();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    policy => policy.WithOrigins("https://localhost:7026")
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials());
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseCors("AllowSpecificOrigin");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}

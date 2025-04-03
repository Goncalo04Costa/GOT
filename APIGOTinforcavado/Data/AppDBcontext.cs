using Microsoft.EntityFrameworkCore;
using Shared.models;

namespace APIGOTinforcavado.Data
{

    public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
            {
            }

            public DbSet<Utilizador> utilizadores { get; set; }
            public DbSet<Ticket> Tickets { get; set; }
            public DbSet<Comentario> Comentarios { get; set; }
            public DbSet<Evento> Eventos { get; set; }
            public DbSet<UploadedFiles> UploadedFiles { get; set; }

        }
    

}

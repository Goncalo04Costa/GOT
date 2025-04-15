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

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ClientesContactos> ClientesContactos { get; set; }
        public DbSet<ClientesContactosTel> ClientesContactosTels { get; set; }

      
        public DbSet<Equipas> Equipas { get; set; } // Verifique se a classe é Equipa ou Equipas
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Tarefas> Tarefas { get; set; } // Caso a entidade seja Tarefa e não Tarefas
        public DbSet<TicketNotificacao> TicketsNotificacao { get; set; }

        public DbSet<NewsLetter> NewsLetter { get; set; }
      
        public DbSet<Utilizador> Utilizadores { get; set; }
   
        public DbSet<UtilizadorEquipa> UtilizadorEquipas { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<UploadedFiles> UploadedFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            // Configuração da chave composta para UtilizadorEquipa
            modelBuilder.Entity<UtilizadorEquipa>()
                .HasKey(ue => new { ue.UtilizadorId, ue.EquipaId });

            modelBuilder.Entity<UtilizadorEquipa>()
                .HasOne(ue => ue.Utilizador)
                .WithMany(u => u.UtilizadorEquipas)
                .HasForeignKey(ue => ue.UtilizadorId);

            modelBuilder.Entity<UtilizadorEquipa>()
                .HasOne(ue => ue.Equipas)
                .WithMany(e => e.UtilizadorEquipas)
                .HasForeignKey(ue => ue.EquipaId);
        }
    }
}

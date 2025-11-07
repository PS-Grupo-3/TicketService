using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }
        public DbSet<EventSeat> EventSeats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ticket
            modelBuilder.Entity<Ticket>(entity => 
            {
                entity.ToTable("Ticket");
                entity.HasKey(t => t.TicketId);
                entity.Property(t => t.UserId)
                    .IsRequired();
                entity.Property(t => t.EventId)
                    .IsRequired();
                entity.Property(t => t.StatusId)
                    .IsRequired();               
                entity.Property(t => t.Created)
                    .IsRequired();
                entity.Property(t => t.Updated);
            });

            // TicketStatus
            modelBuilder.Entity<TicketStatus>(entity =>
            {
                entity.ToTable("TicketStatus");
                entity.HasKey(s => s.StatusID);
                entity.Property(s => s.Name)
                    .IsRequired()
                    .HasColumnType("varchar(25)");
                entity.HasData(
                    new TicketStatus { StatusID = 1, Name = "Available"},
                    new TicketStatus { StatusID = 2, Name = "Reserverd"},
                    new TicketStatus { StatusID = 3, Name = "Sold"},
                    new TicketStatus { StatusID = 4, Name = "Expired"}
                    );
            });

            // EventSeat
            modelBuilder.Entity<EventSeat>(entity =>
            {
                entity.ToTable("EventSeat");
                entity.HasKey(e => e.EventSeatId);
                entity.Property(e => e.EventSectorId)
                    .IsRequired();
                entity.Property(e => e.SeatId)
                    .IsRequired();
                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();
            });

            // Relationships

            // uno a muchos - TicketStatus  Ticket
            modelBuilder.Entity<Ticket>()
                .HasOne(ticket => ticket.StatusRef)
                .WithMany(TStatus => TStatus.Tickets)
                .HasForeignKey(t => t.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            // uno a muchos - Ticket EventSeat
            modelBuilder.Entity<EventSeat>()
                .HasOne(ES => ES.TicketRef)
                .WithMany(ticket => ticket.EventSeats)
                .HasForeignKey(ES => ES.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

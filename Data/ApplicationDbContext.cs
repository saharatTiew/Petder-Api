using Microsoft.EntityFrameworkCore;
using petder.Models;
using petder.Models.DataModels;
using System;
using System.Threading.Tasks;

namespace petder.data
{
    public class ApplicationDbContext : DbContext
    {
       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option) { }

       public DbSet<User> User { get; set; }
       public DbSet<Breed> Breed { get; set; }
       public DbSet<Pet> Pet { get; set; }
       public DbSet<PetImage> PetImage { get; set; }
       public DbSet<BlockList> BlockList { get; set; }
       public DbSet<RequestList> RequestList { get; set; }
       public DbSet<Session> Sessions  {get; set; }
       public DbSet<Message> Messages { get; set; }
       public DbSet<Status> Statuses { get; set; }
       public DbSet<Address> Addresses { get; set; }
       protected override void OnModelCreating(ModelBuilder builder)
       {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                   .ToTable("Users")
                   .HasQueryFilter(x => x.is_active);

            builder.Entity<Breed>()
                   .ToTable("Breeds")
                   .HasQueryFilter(x => x.is_active);

            builder.Entity<Pet>()
                   .ToTable("Pets")
                   .HasQueryFilter(x => x.is_active);

            builder.Entity<PetImage>()
                   .ToTable("PetImages")
                   .HasQueryFilter(x => x.is_active);

            builder.Entity<BlockList>()
                   .ToTable("BlockLists");

            builder.Entity<BlockList>()
                   .HasOne(x => x.BlockerPet)
                   .WithMany(x => x.BlockerLists)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<BlockList>()
                   .HasOne(x => x.BlockedPet)
                   .WithMany(x => x.BlockedLists)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<RequestList>()
                   .ToTable("RequestLists");

            builder.Entity<RequestList>()
                   .HasOne(x => x.RequestedPet)
                   .WithMany(x => x.RequestedLists)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<RequestList>()
                   .HasOne(x => x.RequesterPet)
                   .WithMany(x => x.RequesterLists)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Session>()
                   .ToTable("Sessions");

            builder.Entity<Message>()
                   .ToTable("Messages");
            
            builder.Entity<Status>()
                   .ToTable("Statuses");

            builder.Entity<Address>()
                   .ToTable("Addresses");

            //      //scenarico : 1fk point to one of the composite key
            //      //first define has one from table that fk point
            //      //then with many from table that has fk
            //      //then has principal key to the column that fk point to
            //      //then specify delete behavior by ourself
            //      builder.Entity<PetImage>()
            //             .ToTable("PetImages")
            //             .HasQueryFilter(x => x.is_active)
            //             .HasOne(x => x.Pet)
            //             .WithMany(x => x.PetImages)
            //             .HasForeignKey(x => x.pet_id)
            //             .HasPrincipalKey(x => x.pet_id)
            //             .OnDelete(DeleteBehavior.Cascade);

            //    builder.Entity<BlockList>()
            //      .HasOne(x => x.BlockerPet)
            //      .WithMany(x => x.BlockerLists)
            //      //    .HasForeignKey(x => x.pet_id)
            //      //    .HasPrincipalKey(x => x.pet_id)
            //      .OnDelete(DeleteBehavior.Restrict);
       }
    }
}
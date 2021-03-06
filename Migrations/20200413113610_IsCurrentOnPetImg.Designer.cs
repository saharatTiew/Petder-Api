﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using petder.data;

namespace petder.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200413113610_IsCurrentOnPetImg")]
    partial class IsCurrentOnPetImg
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("petder.Models.DataModels.BlockList", b =>
                {
                    b.Property<long>("block_list_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("block_datetime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<long>("blocked_pet_id")
                        .HasColumnType("bigint");

                    b.Property<bool>("is_active")
                        .HasColumnType("bit");

                    b.Property<long>("pet_id")
                        .HasColumnType("bigint");

                    b.HasKey("block_list_id");

                    b.HasIndex("blocked_pet_id");

                    b.HasIndex("pet_id");

                    b.ToTable("BlockLists");
                });

            modelBuilder.Entity("petder.Models.DataModels.Breed", b =>
                {
                    b.Property<long>("breed_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("is_active")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("breed_id");

                    b.ToTable("Breeds");
                });

            modelBuilder.Entity("petder.Models.DataModels.Message", b =>
                {
                    b.Property<long>("message_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("is_active")
                        .HasColumnType("bit");

                    b.Property<bool>("is_unsent")
                        .HasColumnType("bit");

                    b.Property<string>("message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("sender_pet_id")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("sent_datetime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<long>("session_id")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("unsent_datetime")
                        .HasColumnType("datetime2");

                    b.HasKey("message_id");

                    b.HasIndex("session_id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("petder.Models.DataModels.Pet", b =>
                {
                    b.Property<long>("pet_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("birth_datetime")
                        .HasColumnType("datetime2");

                    b.Property<long>("breed_id")
                        .HasColumnType("bigint");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gender")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<bool>("is_active")
                        .HasColumnType("bit");

                    b.Property<bool>("is_current")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<long>("number_of_like")
                        .HasColumnType("bigint");

                    b.Property<long>("user_id")
                        .HasColumnType("bigint");

                    b.HasKey("pet_id");

                    b.HasIndex("breed_id");

                    b.HasIndex("user_id");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("petder.Models.DataModels.PetImage", b =>
                {
                    b.Property<long>("image_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("image_url")
                        .HasColumnType("varchar(MAX)");

                    b.Property<bool>("is_active")
                        .HasColumnType("bit");

                    b.Property<bool>("is_current")
                        .HasColumnType("bit");

                    b.Property<long>("pet_id")
                        .HasColumnType("bigint");

                    b.HasKey("image_id");

                    b.HasIndex("pet_id");

                    b.ToTable("PetImages");
                });

            modelBuilder.Entity("petder.Models.DataModels.RequestList", b =>
                {
                    b.Property<long>("request_list_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("is_accepted")
                        .HasColumnType("bit");

                    b.Property<bool>("is_active")
                        .HasColumnType("bit");

                    b.Property<long>("pet_id")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("request_datetime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<long>("requested_pet_id")
                        .HasColumnType("bigint");

                    b.HasKey("request_list_id");

                    b.HasIndex("pet_id");

                    b.HasIndex("requested_pet_id");

                    b.ToTable("RequestLists");
                });

            modelBuilder.Entity("petder.Models.DataModels.Session", b =>
                {
                    b.Property<long>("session_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("create_datetime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<bool>("is_active")
                        .HasColumnType("bit");

                    b.Property<long>("request_id")
                        .HasColumnType("bigint");

                    b.HasKey("session_id");

                    b.HasIndex("request_id");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("petder.Models.DataModels.User", b =>
                {
                    b.Property<long>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("is_active")
                        .HasColumnType("bit");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone_number")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("user_id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("petder.Models.DataModels.BlockList", b =>
                {
                    b.HasOne("petder.Models.DataModels.Pet", "BlockedPet")
                        .WithMany("BlockedLists")
                        .HasForeignKey("blocked_pet_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("petder.Models.DataModels.Pet", "BlockerPet")
                        .WithMany("BlockerLists")
                        .HasForeignKey("pet_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("petder.Models.DataModels.Message", b =>
                {
                    b.HasOne("petder.Models.DataModels.Session", "Session")
                        .WithMany("Messages")
                        .HasForeignKey("session_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("petder.Models.DataModels.Pet", b =>
                {
                    b.HasOne("petder.Models.DataModels.Breed", "Breed")
                        .WithMany("Pets")
                        .HasForeignKey("breed_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("petder.Models.DataModels.User", "User")
                        .WithMany("Pets")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("petder.Models.DataModels.PetImage", b =>
                {
                    b.HasOne("petder.Models.DataModels.Pet", "Pet")
                        .WithMany("PetImages")
                        .HasForeignKey("pet_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("petder.Models.DataModels.RequestList", b =>
                {
                    b.HasOne("petder.Models.DataModels.Pet", "RequesterPet")
                        .WithMany("RequesterLists")
                        .HasForeignKey("pet_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("petder.Models.DataModels.Pet", "RequestedPet")
                        .WithMany("RequestedLists")
                        .HasForeignKey("requested_pet_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("petder.Models.DataModels.Session", b =>
                {
                    b.HasOne("petder.Models.DataModels.RequestList", "RequestList")
                        .WithMany("Sessions")
                        .HasForeignKey("request_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

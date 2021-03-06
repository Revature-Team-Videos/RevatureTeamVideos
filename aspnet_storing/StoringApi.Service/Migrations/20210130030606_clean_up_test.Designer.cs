﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoringApi.Service;

namespace StoringApi.Service.Migrations
{
    [DbContext(typeof(VWFContext))]
    [Migration("20210130030606_clean_up_test")]
    partial class clean_up_test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("StoringApi.Service.Models.ChatBox", b =>
                {
                    b.Property<long>("EntityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("RoomEntityID")
                        .HasColumnType("bigint");

                    b.HasKey("EntityID");

                    b.HasIndex("RoomEntityID")
                        .IsUnique();

                    b.ToTable("ChatBox");
                });

            modelBuilder.Entity("StoringApi.Service.Models.Message", b =>
                {
                    b.Property<long>("EntityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("ChatBoxEntityID")
                        .HasColumnType("bigint");

                    b.Property<string>("Sentence")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<long?>("UserEntityID")
                        .HasColumnType("bigint");

                    b.HasKey("EntityID");

                    b.HasIndex("ChatBoxEntityID");

                    b.HasIndex("UserEntityID");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("StoringApi.Service.Models.Room", b =>
                {
                    b.Property<long>("EntityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("EntityID");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("StoringApi.Service.Models.User", b =>
                {
                    b.Property<long>("EntityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("RoomEntityID")
                        .HasColumnType("bigint");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EntityID");

                    b.HasIndex("RoomEntityID");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            EntityID = 1L,
                            Email = "test@jim.com",
                            Username = "TestJim"
                        },
                        new
                        {
                            EntityID = 2L,
                            Email = "test@zach.com",
                            Username = "TestZach"
                        },
                        new
                        {
                            EntityID = 3L,
                            Email = "test@yichen.com",
                            Username = "TestYiChen"
                        });
                });

            modelBuilder.Entity("StoringApi.Service.Models.ChatBox", b =>
                {
                    b.HasOne("StoringApi.Service.Models.Room", null)
                        .WithOne("RoomChat")
                        .HasForeignKey("StoringApi.Service.Models.ChatBox", "RoomEntityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StoringApi.Service.Models.Message", b =>
                {
                    b.HasOne("StoringApi.Service.Models.ChatBox", null)
                        .WithMany("Chat")
                        .HasForeignKey("ChatBoxEntityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoringApi.Service.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserEntityID");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StoringApi.Service.Models.User", b =>
                {
                    b.HasOne("StoringApi.Service.Models.Room", null)
                        .WithMany("Party")
                        .HasForeignKey("RoomEntityID");
                });

            modelBuilder.Entity("StoringApi.Service.Models.ChatBox", b =>
                {
                    b.Navigation("Chat");
                });

            modelBuilder.Entity("StoringApi.Service.Models.Room", b =>
                {
                    b.Navigation("Party");

                    b.Navigation("RoomChat");
                });
#pragma warning restore 612, 618
        }
    }
}

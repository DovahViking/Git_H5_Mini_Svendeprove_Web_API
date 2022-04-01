﻿// <auto-generated />
using System;
using H5_Svendeprove_Web_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace H5_Svendeprove_Web_API.Migrations
{
    [DbContext(typeof(Customer_Context))]
    partial class Customer_ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("H5_Svendeprove_Web_API.Models.Device", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("device_id")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("manufacturer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("platform")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("userId")
                        .HasColumnType("integer");

                    b.Property<string>("version")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("userId")
                        .IsUnique();

                    b.ToTable("device");
                });

            modelBuilder.Entity("H5_Svendeprove_Web_API.Models.Score", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int?>("highest_score")
                        .HasColumnType("integer");

                    b.Property<DateTime>("last_updated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("recent_score")
                        .HasColumnType("integer");

                    b.Property<int>("userId")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("userId")
                        .IsUnique();

                    b.ToTable("score");
                });

            modelBuilder.Entity("H5_Svendeprove_Web_API.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<DateTime>("date_created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("H5_Svendeprove_Web_API.Models.Device", b =>
                {
                    b.HasOne("H5_Svendeprove_Web_API.Models.User", "user")
                        .WithOne("device")
                        .HasForeignKey("H5_Svendeprove_Web_API.Models.Device", "userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("H5_Svendeprove_Web_API.Models.Score", b =>
                {
                    b.HasOne("H5_Svendeprove_Web_API.Models.User", "user")
                        .WithOne("score")
                        .HasForeignKey("H5_Svendeprove_Web_API.Models.Score", "userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("H5_Svendeprove_Web_API.Models.User", b =>
                {
                    b.Navigation("device")
                        .IsRequired();

                    b.Navigation("score")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
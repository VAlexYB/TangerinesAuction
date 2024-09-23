﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TangerineAuction.Infrastructure.Persistence;

#nullable disable

namespace TangerineAuction.Infrastructure.Migrations
{
    [DbContext(typeof(TADbContext))]
    [Migration("20240922145222__initial")]
    partial class _initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TangerinesAuction.Core.Models.Bid", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<Guid>("BidderId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("TangerineId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("BidderId");

                    b.HasIndex("TangerineId");

                    b.ToTable("Bids");
                });

            modelBuilder.Entity("TangerinesAuction.Core.Models.Tangerine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("CurrentPrice")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsSpoiled")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsWinnerNotified")
                        .HasColumnType("boolean");

                    b.Property<decimal>("StartPrice")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Tangerines");
                });

            modelBuilder.Entity("TangerinesAuction.Core.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TangerinesAuction.Core.Models.Bid", b =>
                {
                    b.HasOne("TangerinesAuction.Core.Models.User", "Bidder")
                        .WithMany("Bids")
                        .HasForeignKey("BidderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TangerinesAuction.Core.Models.Tangerine", "Tangerine")
                        .WithMany("Bids")
                        .HasForeignKey("TangerineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bidder");

                    b.Navigation("Tangerine");
                });

            modelBuilder.Entity("TangerinesAuction.Core.Models.Tangerine", b =>
                {
                    b.Navigation("Bids");
                });

            modelBuilder.Entity("TangerinesAuction.Core.Models.User", b =>
                {
                    b.Navigation("Bids");
                });
#pragma warning restore 612, 618
        }
    }
}

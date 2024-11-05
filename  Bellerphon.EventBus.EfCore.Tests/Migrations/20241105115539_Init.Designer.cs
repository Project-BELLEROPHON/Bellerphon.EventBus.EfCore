﻿// <auto-generated />
using System;
using Bellerphon.EventBus.EfCore.Tests.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bellerphon.EventBus.EfCore.Tests.Migrations
{
    [DbContext(typeof(TestDbContext))]
    [Migration("20241105115539_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618

            modelBuilder.Entity("Bellerophon.EventBus.EfCore.Abstractions.Entities.OutBoxMessage", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("ExpireDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Headers")
                        .HasColumnType("text");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsSent")
                        .HasColumnType("boolean");

                    b.Property<int>("TryCount")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("OutBoxMessage");
                });

            modelBuilder.Entity("Bellerphon.EventBus.EfCore.Tests.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}

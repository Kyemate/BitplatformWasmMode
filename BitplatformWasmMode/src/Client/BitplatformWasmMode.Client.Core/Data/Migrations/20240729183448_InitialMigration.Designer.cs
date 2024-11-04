﻿// <auto-generated />
using System;
using BitplatformWasmMode.Client.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BitplatformWasmMode.Client.Core.Data.Migrations
{
    [DbContext(typeof(OfflineDbContext))]
    [Migration("20240729183448_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("BitplatformWasmMode.Shared.Dtos.Identity.UserDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<long?>("BirthDate")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProfileImageName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8ff71671-a1d6-4f97-abb9-d87d7b47d6e7"),
                            BirthDate = 1306790461440000000L,
                            Email = "test@bitplatform.dev",
                            FullName = "BitplatformWasmMode test account",
                            Gender = 2,
                            Password = "123456",
                            PhoneNumber = "+31684207362",
                            UserName = "test"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

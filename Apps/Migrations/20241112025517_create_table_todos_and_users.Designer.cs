﻿// <auto-generated />
using System;
using Apps.Data.Ctx;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Apps.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241112025517_create_table_todos_and_users")]
    partial class create_table_todos_and_users
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Apps.Data.Models.Todo", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(26)")
                        .HasColumnName("id");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIME(6)")
                        .HasColumnName("created_at");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<DateTime?>("CreatedAt"));

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("DATETIME(6)")
                        .HasColumnName("deleted_at");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_complete");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("DATETIME(6)")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted")
                        .HasFilter("is_deleted = 0");

                    b.ToTable("todos");
                });

            modelBuilder.Entity("Apps.Data.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(26)")
                        .HasColumnName("id");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIME(6)")
                        .HasColumnName("created_at");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<DateTime?>("CreatedAt"));

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("DATETIME(6)")
                        .HasColumnName("deleted_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("email");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("VARCHAR(60)")
                        .HasColumnName("password");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("DATETIME(6)")
                        .HasColumnName("updated_at");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("IsDeleted")
                        .HasFilter("is_deleted = 0");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("users");
                });
#pragma warning restore 612, 618
        }
    }
}

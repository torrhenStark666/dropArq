﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UploadFile.WebApi.Context;

namespace UploadFile.WebApi.Migrations
{
    [DbContext(typeof(UploadFileDbContext))]
    [Migration("20200212121514_UploadFile_V_3")]
    partial class UploadFile_V_3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("UploadFile.WebApi.Models.Document", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<byte[]>("File")
                        .IsRequired()
                        .HasColumnName("file")
                        .HasColumnType("bytea");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnName("file_name")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<long>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("documents");
                });

            modelBuilder.Entity("UploadFile.WebApi.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Email = "sysadmin@email.com",
                            Name = "Sys Admin",
                            Password = "123456"
                        });
                });

            modelBuilder.Entity("UploadFile.WebApi.Models.Document", b =>
                {
                    b.HasOne("UploadFile.WebApi.Models.User", "User")
                        .WithMany("Documents")
                        .HasForeignKey("UserId")
                        .HasConstraintName("document_user_id")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SEDC.NotesScaffoldedApp.DataAccess.Models;

#nullable disable

namespace SEDC.NotesScaffoldedApp.DataAccess.Migrations
{
    [DbContext(typeof(NoteScaffoldedDbContext))]
    [Migration("20230817161717_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SEDC.NotesScaffoldedApp.DataAccess.Models.Note", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int>("Tag")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Notes__3214EC0772172F3E");

                    b.HasIndex("UserId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("SEDC.NotesScaffoldedApp.DataAccess.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK__Users__3214EC07CB2C10F3");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SEDC.NotesScaffoldedApp.DataAccess.Models.Note", b =>
                {
                    b.HasOne("SEDC.NotesScaffoldedApp.DataAccess.Models.User", "User")
                        .WithMany("Notes")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__Notes__UserId__398D8EEE");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SEDC.NotesScaffoldedApp.DataAccess.Models.User", b =>
                {
                    b.Navigation("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}

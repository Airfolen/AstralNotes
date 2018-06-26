﻿// <auto-generated />
using AstralNotes.Database;
using AstralNotes.Database.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace AstralNotes.API.Migrations
{
    [DbContext(typeof(NotesContext))]
    [Migration("20180626152128_AddNoteAndUserModels")]
    partial class AddNoteAndUserModels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("AstralNotes.Database.Entities.Note", b =>
                {
                    b.Property<Guid>("NoteGuid");

                    b.Property<int>("Category");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreationDate");

                    b.Property<Guid>("UserGuid");

                    b.HasKey("NoteGuid");

                    b.HasIndex("UserGuid");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("AstralNotes.Database.Entities.User", b =>
                {
                    b.Property<Guid>("UserGuid");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("UserGuid");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AstralNotes.Database.Entities.Note", b =>
                {
                    b.HasOne("AstralNotes.Database.Entities.User", "User")
                        .WithMany("Notes")
                        .HasForeignKey("UserGuid")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

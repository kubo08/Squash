﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Squash.Data;

namespace Squash.Data.Migrations
{
    [DbContext(typeof(SquashContext))]
    [Migration("20200903202325_currentPoints")]
    partial class currentPoints
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0-preview.8.20407.4");

            modelBuilder.Entity("Squash.Domain.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("MatchId")
                        .HasColumnType("int");

                    b.Property<int>("Player1Score")
                        .HasColumnType("int");

                    b.Property<int>("Player2Score")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Squash.Domain.Info", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("LastRun")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastTournamentProcessed")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Info");
                });

            modelBuilder.Entity("Squash.Domain.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("IsJogo")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTraining")
                        .HasColumnType("bit");

                    b.Property<int?>("Player1Id")
                        .HasColumnType("int");

                    b.Property<int?>("Player2Id")
                        .HasColumnType("int");

                    b.Property<int?>("TournamentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Player1Id");

                    b.HasIndex("Player2Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("Squash.Domain.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<double>("ActualPoints")
                        .HasColumnType("float");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Squash.Domain.PlayerTournament", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("TournamentId")
                        .HasColumnType("int");

                    b.HasKey("PlayerId", "TournamentId");

                    b.HasIndex("TournamentId");

                    b.ToTable("PlayerTournament");
                });

            modelBuilder.Entity("Squash.Domain.PlayerTournamentResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.Property<double?>("Points")
                        .HasColumnType("float");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<int?>("TournamentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TournamentId");

                    b.ToTable("PlayerTournamentResults");
                });

            modelBuilder.Entity("Squash.Domain.Points", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Points");
                });

            modelBuilder.Entity("Squash.Domain.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("Squash.Domain.Game", b =>
                {
                    b.HasOne("Squash.Domain.Match", null)
                        .WithMany("Games")
                        .HasForeignKey("MatchId");
                });

            modelBuilder.Entity("Squash.Domain.Match", b =>
                {
                    b.HasOne("Squash.Domain.Player", "Player1")
                        .WithMany()
                        .HasForeignKey("Player1Id");

                    b.HasOne("Squash.Domain.Player", "Player2")
                        .WithMany()
                        .HasForeignKey("Player2Id");

                    b.HasOne("Squash.Domain.Tournament", "Tournament")
                        .WithMany("Matches")
                        .HasForeignKey("TournamentId");
                });

            modelBuilder.Entity("Squash.Domain.PlayerTournament", b =>
                {
                    b.HasOne("Squash.Domain.Player", "Player")
                        .WithMany("PlayerTournaments")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Squash.Domain.Tournament", "Tournament")
                        .WithMany("PlayerTournaments")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Squash.Domain.PlayerTournamentResult", b =>
                {
                    b.HasOne("Squash.Domain.Player", "Player")
                        .WithMany("TournamentResults")
                        .HasForeignKey("PlayerId");

                    b.HasOne("Squash.Domain.Tournament", "Tournament")
                        .WithMany()
                        .HasForeignKey("TournamentId");
                });

            modelBuilder.Entity("Squash.Domain.Points", b =>
                {
                    b.HasOne("Squash.Domain.Player", null)
                        .WithMany("Points")
                        .HasForeignKey("PlayerId");
                });
#pragma warning restore 612, 618
        }
    }
}

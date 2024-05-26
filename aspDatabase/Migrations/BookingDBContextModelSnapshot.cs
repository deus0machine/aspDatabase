﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using aspDatabase.Models;

#nullable disable

namespace aspDatabase.Migrations
{
    [DbContext(typeof(BookingDBContext))]
    partial class BookingDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("aspDatabase.Models.Agreement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("clientID")
                        .HasColumnType("int");

                    b.Property<int>("cost")
                        .HasColumnType("int");

                    b.Property<DateTime>("dateDoc")
                        .HasColumnType("datetime2");

                    b.Property<int?>("hotelId")
                        .HasColumnType("int");

                    b.Property<DateTime>("reservEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("reservStart")
                        .HasColumnType("datetime2");

                    b.Property<int>("roomID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("clientID");

                    b.HasIndex("hotelId");

                    b.HasIndex("roomID");

                    b.ToTable("Agreements");
                });

            modelBuilder.Entity("aspDatabase.Models.BookingRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AdressRoom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BookingDateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("BookingDateStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Idhotel")
                        .HasColumnType("int");

                    b.Property<string>("NumberofPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThirdName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("nubmerPassport")
                        .HasColumnType("int");

                    b.Property<int>("passportSeries")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Idhotel");

                    b.ToTable("BookingRequests");
                });

            modelBuilder.Entity("aspDatabase.Models.Client", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberofPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThirdName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("nubmerPassport")
                        .HasColumnType("int");

                    b.Property<int>("passportSeries")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("aspDatabase.Models.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("aspDatabase.Models.Room", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("AdressRoom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<int?>("Idhotel")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("Idhotel");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("aspDatabase.Models.Agreement", b =>
                {
                    b.HasOne("aspDatabase.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("clientID");

                    b.HasOne("aspDatabase.Models.Hotel", "Hotel")
                        .WithMany()
                        .HasForeignKey("hotelId");

                    b.HasOne("aspDatabase.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("roomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Hotel");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("aspDatabase.Models.BookingRequest", b =>
                {
                    b.HasOne("aspDatabase.Models.Hotel", "Hotel")
                        .WithMany()
                        .HasForeignKey("Idhotel");

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("aspDatabase.Models.Room", b =>
                {
                    b.HasOne("aspDatabase.Models.Hotel", "Hotel")
                        .WithMany()
                        .HasForeignKey("Idhotel");

                    b.Navigation("Hotel");
                });
#pragma warning restore 612, 618
        }
    }
}

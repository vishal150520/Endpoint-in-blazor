﻿// <auto-generated />
using BlazorAppCRUD.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlazorAppCRUD.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240130073044_intialmigration")]
    partial class intialmigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BlazorAppCRUD.Data.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<string>("OrderName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderId");

                    b.ToTable("order");
                });

            modelBuilder.Entity("BlazorAppCRUD.Data.SubElement", b =>
                {
                    b.Property<int>("SubElementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubElementId"), 1L, 1);

                    b.Property<int>("Element")
                        .HasColumnType("int");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.Property<int>("WindowId")
                        .HasColumnType("int");

                    b.HasKey("SubElementId");

                    b.HasIndex("WindowId");

                    b.ToTable("subelement");
                });

            modelBuilder.Entity("BlazorAppCRUD.Data.Window", b =>
                {
                    b.Property<int>("WindowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WindowId"), 1L, 1);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("QuantityOfWindows")
                        .HasColumnType("int");

                    b.Property<int>("TotalSubElements")
                        .HasColumnType("int");

                    b.Property<string>("WindowName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WindowId");

                    b.HasIndex("OrderId");

                    b.ToTable("window");
                });

            modelBuilder.Entity("BlazorAppCRUD.Data.SubElement", b =>
                {
                    b.HasOne("BlazorAppCRUD.Data.Window", "Windows")
                        .WithMany("SubElements")
                        .HasForeignKey("WindowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Windows");
                });

            modelBuilder.Entity("BlazorAppCRUD.Data.Window", b =>
                {
                    b.HasOne("BlazorAppCRUD.Data.Order", "Orders")
                        .WithMany("Windows")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("BlazorAppCRUD.Data.Order", b =>
                {
                    b.Navigation("Windows");
                });

            modelBuilder.Entity("BlazorAppCRUD.Data.Window", b =>
                {
                    b.Navigation("SubElements");
                });
#pragma warning restore 612, 618
        }
    }
}

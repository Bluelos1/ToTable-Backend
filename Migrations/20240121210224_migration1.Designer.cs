﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ToTable.Models;

#nullable disable

namespace ToTable.Migrations
{
    [DbContext(typeof(ToTableDbContext))]
    [Migration("20240121210224_migration1")]
    partial class migration1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ToTable.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderId"));

                    b.Property<string>("OrderComment")
                        .HasColumnType("text");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("integer");

                    b.Property<DateTime>("OrderTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("text");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("integer");

                    b.Property<int>("TableId")
                        .HasColumnType("integer");

                    b.Property<int?>("WaiterId")
                        .HasColumnType("integer");

                    b.HasKey("OrderId");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("TableId");

                    b.HasIndex("WaiterId");

                    b.ToTable("OrderObject");
                });

            modelBuilder.Entity("ToTable.Models.OrderItem", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ItemId"));

                    b.Property<double>("ItemPrice")
                        .HasColumnType("double precision");

                    b.Property<int>("ItemQuantity")
                        .HasColumnType("integer");

                    b.Property<int?>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.HasKey("ItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItemObject");
                });

            modelBuilder.Entity("ToTable.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProductId"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("ProductPrice")
                        .HasColumnType("double precision");

                    b.Property<string>("ProductStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("integer");

                    b.HasKey("ProductId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("ProductObject");
                });

            modelBuilder.Entity("ToTable.Models.Restaurant", b =>
                {
                    b.Property<int>("RestaurantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RestaurantId"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RestaurantName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TableQuantity")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WaiterQantity")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RestaurantId");

                    b.ToTable("RestaurantObject");
                });

            modelBuilder.Entity("ToTable.Models.Table", b =>
                {
                    b.Property<int>("TabId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TabId"));

                    b.Property<int>("RestaurantId")
                        .HasColumnType("integer");

                    b.Property<int>("TabNum")
                        .HasColumnType("integer");

                    b.Property<bool>("TabStatus")
                        .HasColumnType("boolean");

                    b.HasKey("TabId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("TableObject");
                });

            modelBuilder.Entity("ToTable.Models.Waiter", b =>
                {
                    b.Property<int>("WaiterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("WaiterId"));

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("integer");

                    b.Property<string>("WaiterLogin")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WaiterName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WaiterPassw")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WaiterSurname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("WaiterId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("WaiterObject");
                });

            modelBuilder.Entity("ToTable.Models.Order", b =>
                {
                    b.HasOne("ToTable.Models.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToTable.Models.Table", "Table")
                        .WithMany()
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToTable.Models.Waiter", "Waiter")
                        .WithMany()
                        .HasForeignKey("WaiterId");

                    b.Navigation("Restaurant");

                    b.Navigation("Table");

                    b.Navigation("Waiter");
                });

            modelBuilder.Entity("ToTable.Models.OrderItem", b =>
                {
                    b.HasOne("ToTable.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.HasOne("ToTable.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ToTable.Models.Product", b =>
                {
                    b.HasOne("ToTable.Models.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("ToTable.Models.Table", b =>
                {
                    b.HasOne("ToTable.Models.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("ToTable.Models.Waiter", b =>
                {
                    b.HasOne("ToTable.Models.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });
#pragma warning restore 612, 618
        }
    }
}

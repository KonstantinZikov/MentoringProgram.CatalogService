﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Domain.Entities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.HasOne("Domain.Entities.Category", "ParentCategory")
                        .WithMany()
                        .HasForeignKey("ParentCategoryId");

                    b.OwnsOne("Domain.ValueObjects.Image", "Image", b1 =>
                        {
                            b1.Property<int>("CategoryId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Alt")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("CategoryId");

                            b1.ToTable("Categories");

                            b1.WithOwner()
                                .HasForeignKey("CategoryId");
                        });

                    b.Navigation("Image");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("Domain.Entities.Item", b =>
                {
                    b.HasOne("Domain.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.ValueObjects.Image", "Image", b1 =>
                        {
                            b1.Property<int>("ItemId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Alt")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("ItemId");

                            b1.ToTable("Items");

                            b1.WithOwner()
                                .HasForeignKey("ItemId");
                        });

                    b.OwnsOne("Domain.ValueObjects.Money", "Price", b1 =>
                        {
                            b1.Property<int>("ItemId")
                                .HasColumnType("INTEGER");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Currency")
                                .HasColumnType("INTEGER");

                            b1.HasKey("ItemId");

                            b1.ToTable("Items");

                            b1.WithOwner()
                                .HasForeignKey("ItemId");
                        });

                    b.Navigation("Category");

                    b.Navigation("Image");

                    b.Navigation("Price")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

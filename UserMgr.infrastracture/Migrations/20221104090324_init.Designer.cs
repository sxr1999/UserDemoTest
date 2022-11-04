﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserMgr.infrastracture;

#nullable disable

namespace UserMgr.infrastracture.Migrations
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20221104090324_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("UserMgr.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("passwordHash")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("T_Users", (string)null);
                });

            modelBuilder.Entity("UserMgr.Domain.UserAccessFail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("lockOut")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("T_UserAccessFails", (string)null);
                });

            modelBuilder.Entity("UserMgr.Domain.UserLoginHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("T_UserAccessFailConfigs", (string)null);
                });

            modelBuilder.Entity("UserMgr.Domain.User", b =>
                {
                    b.OwnsOne("UserMgr.Domain.ValueObject.PhoneNumber", "phoneNumber", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasMaxLength(20)
                                .IsUnicode(false)
                                .HasColumnType("varchar(20)");

                            b1.Property<int>("RegionNumber")
                                .HasMaxLength(5)
                                .IsUnicode(false)
                                .HasColumnType("int");

                            b1.HasKey("UserId");

                            b1.ToTable("T_Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("phoneNumber")
                        .IsRequired();
                });

            modelBuilder.Entity("UserMgr.Domain.UserAccessFail", b =>
                {
                    b.HasOne("UserMgr.Domain.User", "User")
                        .WithOne("_userAccessFail")
                        .HasForeignKey("UserMgr.Domain.UserAccessFail", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("UserMgr.Domain.UserLoginHistory", b =>
                {
                    b.OwnsOne("UserMgr.Domain.ValueObject.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<Guid>("UserLoginHistoryId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasMaxLength(20)
                                .IsUnicode(false)
                                .HasColumnType("varchar(20)");

                            b1.Property<int>("RegionNumber")
                                .HasMaxLength(5)
                                .IsUnicode(false)
                                .HasColumnType("int");

                            b1.HasKey("UserLoginHistoryId");

                            b1.ToTable("T_UserAccessFailConfigs");

                            b1.WithOwner()
                                .HasForeignKey("UserLoginHistoryId");
                        });

                    b.Navigation("PhoneNumber")
                        .IsRequired();
                });

            modelBuilder.Entity("UserMgr.Domain.User", b =>
                {
                    b.Navigation("_userAccessFail")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

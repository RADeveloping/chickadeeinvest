﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using chickadee.Data;

#nullable disable

namespace chickadee.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("chickadee.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<byte[]>("ProfilePicture")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<int?>("UnitId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("UsernameChangeLimit")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("UnitId");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "215f0df8-b94a-49f0-9fae-c22c74614ecb",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "a988c259-4d65-4225-98c8-1e065870f8d2",
                            Email = "superadmin@chickadeeinvest.ca",
                            EmailConfirmed = true,
                            FirstName = "Matt",
                            LastName = "Hardwick",
                            LockoutEnabled = false,
                            NormalizedEmail = "SUPERADMIN@CHICKADEEINVEST.CA",
                            NormalizedUserName = "SUPERADMIN@CHICKADEEINVEST.CA",
                            PasswordHash = "AQAAAAEAACcQAAAAEGl8d2wU+zkwfHAKT3V6Zmi4TUOd8kzkH32Aq+GhGItxmzuAN7mk3jWgno6J9bF+rQ==",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "cfed882f-d0ce-436e-95b5-4edebdaa510d",
                            TwoFactorEnabled = false,
                            UserName = "superadmin@chickadeeinvest.ca",
                            UsernameChangeLimit = 10
                        },
                        new
                        {
                            Id = "0cc25564-5133-4769-a2b9-b8bd37996d3d",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "9ab5f754-f3ac-45c6-a724-fb27c919dcdb",
                            Email = "propertymanager@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Property",
                            LastName = "Manager",
                            LockoutEnabled = false,
                            NormalizedEmail = "PROPERTYMANAGER@GMAIL.COM",
                            NormalizedUserName = "PROPERTYMANAGER@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEJCmrJ3Bdwib2e3xLiiGMQU1kYzBzHg/tC618TPAcMp1/CwsvWDDwbzmL+ntX8Z1qw==",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "79205821-a649-4b33-8b78-820a940e743b",
                            TwoFactorEnabled = false,
                            UserName = "propertymanager@gmail.com",
                            UsernameChangeLimit = 10
                        },
                        new
                        {
                            Id = "7e972781-25af-46ad-b5d6-051e3e857d3d",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "7ad1a89b-7b91-4379-bafa-3f8769ffcffa",
                            Email = "propertymanager2@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Manager",
                            LastName = "Property",
                            LockoutEnabled = false,
                            NormalizedEmail = "PROPERTYMANAGER2@GMAIL.COM",
                            NormalizedUserName = "PROPERTYMANAGER2@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEEB6UsuyGJ3DCUuQEkKHxVQIQD06j0gjC5t/7D+dkM/fOkYCY744tFCLa08356ju4A==",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "cee92c9d-5c0f-43b7-acca-32e657c1f8e6",
                            TwoFactorEnabled = false,
                            UserName = "propertymanager2@gmail.com",
                            UsernameChangeLimit = 10
                        },
                        new
                        {
                            Id = "72904ebc-cb0c-4b97-a5e7-98313e9a59f5",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "49298b3b-4342-4894-b4a1-af1e95b7d011",
                            Email = "tenant@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Tenant",
                            LastName = "User",
                            LockoutEnabled = false,
                            NormalizedEmail = "TENANT@GMAIL.COM",
                            NormalizedUserName = "TENANT@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAELfG0j6HhYwxZ/cIqLDZ08PCs11Mw0kWUgoizTxHDgk6093hiT5HqTilExgZ8QVmZQ==",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "e8789163-157f-422f-a3cb-5aa23edd279d",
                            TwoFactorEnabled = false,
                            UnitId = 1,
                            UserName = "tenant@gmail.com",
                            UsernameChangeLimit = 10
                        },
                        new
                        {
                            Id = "646750e8-a6f2-4a9c-b75f-53e1686cf5b7",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "284573b3-3c0c-445d-9ed4-ecad8750ae9a",
                            Email = "tenant2@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "User",
                            LastName = "Tenant",
                            LockoutEnabled = false,
                            NormalizedEmail = "TENANT2@GMAIL.COM",
                            NormalizedUserName = "TENANT2@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEDMWKqB4we+z7Q/xU+hD2DDfZ5LA9sEg7Soz0vcgn0iA2LeHpLAHUaHzJGN3nl3nZw==",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "8578139b-3dc6-4622-9724-f7846e0fb3a1",
                            TwoFactorEnabled = false,
                            UnitId = 2,
                            UserName = "tenant2@gmail.com",
                            UsernameChangeLimit = 10
                        });
                });

            modelBuilder.Entity("chickadee.Models.Property", b =>
                {
                    b.Property<int>("PropertyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PropertyId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PropertyManagerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PropertyId");

                    b.HasIndex("PropertyManagerId");

                    b.ToTable("Properties");

                    b.HasData(
                        new
                        {
                            PropertyId = 1,
                            Address = "742 Evergreen Terrace",
                            PropertyManagerId = "0cc25564-5133-4769-a2b9-b8bd37996d3d"
                        },
                        new
                        {
                            PropertyId = 2,
                            Address = "The Montana",
                            PropertyManagerId = "7e972781-25af-46ad-b5d6-051e3e857d3d"
                        });
                });

            modelBuilder.Entity("chickadee.Models.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketId"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EstimatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Problem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Severity")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.HasKey("TicketId");

                    b.HasIndex("UnitId");

                    b.ToTable("Tickets");

                    b.HasData(
                        new
                        {
                            TicketId = 1,
                            CreatedOn = new DateTime(2022, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Massive Leak from the Kitchen pipe",
                            EstimatedDate = new DateTime(2022, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Problem = "Massive Leakage",
                            Severity = 2,
                            Status = 0,
                            UnitId = 1
                        },
                        new
                        {
                            TicketId = 2,
                            CreatedOn = new DateTime(2021, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Need repairing the floors from last earthquake",
                            EstimatedDate = new DateTime(2022, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Problem = "Earthquake repair",
                            Severity = 1,
                            Status = 0,
                            UnitId = 1
                        },
                        new
                        {
                            TicketId = 3,
                            CreatedOn = new DateTime(1999, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Need to fix the roof that was damaged by the tornado",
                            EstimatedDate = new DateTime(2000, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Problem = "Tornado damage",
                            Severity = 2,
                            Status = 1,
                            UnitId = 2
                        },
                        new
                        {
                            TicketId = 4,
                            CreatedOn = new DateTime(2019, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Currently getting by with rat traps",
                            EstimatedDate = new DateTime(2022, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Problem = "Rat infestation",
                            Severity = 1,
                            Status = 0,
                            UnitId = 2
                        });
                });

            modelBuilder.Entity("chickadee.Models.Unit", b =>
                {
                    b.Property<int>("UnitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UnitId"), 1L, 1);

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<int>("UnitNo")
                        .HasColumnType("int");

                    b.HasKey("UnitId");

                    b.HasIndex("PropertyId");

                    b.ToTable("Units");

                    b.HasData(
                        new
                        {
                            UnitId = 1,
                            PropertyId = 1,
                            UnitNo = 101
                        },
                        new
                        {
                            UnitId = 2,
                            PropertyId = 2,
                            UnitNo = 500
                        });
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.DeviceFlowCodes", b =>
                {
                    b.Property<string>("UserCode")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DeviceCode")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("UserCode");

                    b.HasIndex("DeviceCode")
                        .IsUnique();

                    b.HasIndex("Expiration");

                    b.ToTable("DeviceCodes", (string)null);
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.Key", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Algorithm")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DataProtected")
                        .HasColumnType("bit");

                    b.Property<bool>("IsX509Certificate")
                        .HasColumnType("bit");

                    b.Property<string>("Use")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Use");

                    b.ToTable("Keys");
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.PersistedGrant", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("ConsumedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Key");

                    b.HasIndex("ConsumedTime");

                    b.HasIndex("Expiration");

                    b.HasIndex("SubjectId", "ClientId", "Type");

                    b.HasIndex("SubjectId", "SessionId", "Type");

                    b.ToTable("PersistedGrants", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "da1d50ab-8df8-4c21-a2df-eec2647f8100",
                            ConcurrencyStamp = "84e19c77-8b23-4073-949b-8213a1f62d45",
                            Name = "SuperAdmin",
                            NormalizedName = "SUPERADMIN"
                        },
                        new
                        {
                            Id = "322df380-6af1-4aaa-a1c0-cb5d59195373",
                            ConcurrencyStamp = "830c44fa-ee44-4dda-8a16-0d618f853fa8",
                            Name = "PropertyManager",
                            NormalizedName = "PROPERTYMANAGER"
                        },
                        new
                        {
                            Id = "9bcae295-e06e-4137-b302-e9caed02280e",
                            ConcurrencyStamp = "96f5501d-b49f-42ae-949e-78d7bc1e8c0b",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "efc90636-a2dc-478d-9501-f9797dbcbf8a",
                            ConcurrencyStamp = "1aacc8c1-84b6-4875-bdb3-559ca559c035",
                            Name = "Tenant",
                            NormalizedName = "TENANT"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "215f0df8-b94a-49f0-9fae-c22c74614ecb",
                            RoleId = "efc90636-a2dc-478d-9501-f9797dbcbf8a"
                        },
                        new
                        {
                            UserId = "215f0df8-b94a-49f0-9fae-c22c74614ecb",
                            RoleId = "322df380-6af1-4aaa-a1c0-cb5d59195373"
                        },
                        new
                        {
                            UserId = "215f0df8-b94a-49f0-9fae-c22c74614ecb",
                            RoleId = "9bcae295-e06e-4137-b302-e9caed02280e"
                        },
                        new
                        {
                            UserId = "215f0df8-b94a-49f0-9fae-c22c74614ecb",
                            RoleId = "da1d50ab-8df8-4c21-a2df-eec2647f8100"
                        },
                        new
                        {
                            UserId = "0cc25564-5133-4769-a2b9-b8bd37996d3d",
                            RoleId = "322df380-6af1-4aaa-a1c0-cb5d59195373"
                        },
                        new
                        {
                            UserId = "7e972781-25af-46ad-b5d6-051e3e857d3d",
                            RoleId = "322df380-6af1-4aaa-a1c0-cb5d59195373"
                        },
                        new
                        {
                            UserId = "72904ebc-cb0c-4b97-a5e7-98313e9a59f5",
                            RoleId = "efc90636-a2dc-478d-9501-f9797dbcbf8a"
                        },
                        new
                        {
                            UserId = "646750e8-a6f2-4a9c-b75f-53e1686cf5b7",
                            RoleId = "efc90636-a2dc-478d-9501-f9797dbcbf8a"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("chickadee.Models.ApplicationUser", b =>
                {
                    b.HasOne("chickadee.Models.Unit", "Unit")
                        .WithMany("Tenants")
                        .HasForeignKey("UnitId");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("chickadee.Models.Property", b =>
                {
                    b.HasOne("chickadee.Models.ApplicationUser", "PropertyManager")
                        .WithMany()
                        .HasForeignKey("PropertyManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PropertyManager");
                });

            modelBuilder.Entity("chickadee.Models.Ticket", b =>
                {
                    b.HasOne("chickadee.Models.Unit", "Unit")
                        .WithMany("Tickets")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("chickadee.Models.Unit", b =>
                {
                    b.HasOne("chickadee.Models.Property", "Property")
                        .WithMany("Units")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("chickadee.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("chickadee.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("chickadee.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("chickadee.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("chickadee.Models.Property", b =>
                {
                    b.Navigation("Units");
                });

            modelBuilder.Entity("chickadee.Models.Unit", b =>
                {
                    b.Navigation("Tenants");

                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}

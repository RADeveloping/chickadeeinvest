﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chickadee.Migrations
{
    public partial class seedingUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "Keys",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Use = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Algorithm = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsX509Certificate = table.Column<bool>(type: "bit", nullable: false),
                    DataProtected = table.Column<bool>(type: "bit", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConsumedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.PropertyId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsernameChangeLimit = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfilePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UnitId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LeaseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsIdVerified = table.Column<bool>(type: "bit", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    UnitId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitNo = table.Column<int>(type: "int", nullable: false),
                    UnitType = table.Column<int>(type: "int", nullable: false),
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyManagerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PropertyManagerId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.UnitId);
                    table.ForeignKey(
                        name: "FK_Unit_AspNetUsers_PropertyManagerId",
                        column: x => x.PropertyManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Unit_AspNetUsers_PropertyManagerId1",
                        column: x => x.PropertyManagerId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Unit_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VerificationDocuments",
                columns: table => new
                {
                    VerificationDocumentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DocumentType = table.Column<int>(type: "int", nullable: false),
                    ResponseMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationDocuments", x => x.VerificationDocumentId);
                    table.ForeignKey(
                        name: "FK_VerificationDocuments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Problem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Severity = table.Column<int>(type: "int", nullable: false),
                    ClosedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UnitId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnitImage",
                columns: table => new
                {
                    UnitImageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UnitId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitImage", x => x.UnitImageId);
                    table.ForeignKey(
                        name: "FK_UnitImage_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnitNote",
                columns: table => new
                {
                    UnitNoteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UnitId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitNote", x => x.UnitNoteId);
                    table.ForeignKey(
                        name: "FK_UnitNote_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_UnitId",
                        column: x => x.UnitId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketImage",
                columns: table => new
                {
                    TicketImageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketImage", x => x.TicketImageId);
                    table.ForeignKey(
                        name: "FK_TicketImage_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketImage_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
<<<<<<<< HEAD:Migrations/20221104190359_seedingUpdate.cs
                    { "0a5e6ca9-c02d-4772-969e-78cc8419b670", "2d79f4bd-b28c-4660-a5b3-ca5574802e1f", "Admin", "ADMIN" },
                    { "3706ca29-1aa8-4dea-b918-bdb9ed4bd8e6", "894be57f-5e09-4ca4-a866-bb6be69fe6ba", "PropertyManager", "PROPERTYMANAGER" },
                    { "b45a8548-ace4-4de3-8731-cdeb9ea8f908", "eab66916-2d88-4df9-ab83-fcf8bbd5c3f7", "Tenant", "TENANT" },
                    { "f2895e8d-f04e-4eec-9017-c13b11e37ba0", "95e46a30-f624-4141-8f37-cb1c523455e9", "SuperAdmin", "SUPERADMIN" }
========
                    { "14c159b3-77be-4fbd-8b7a-5e6ec5a78292", "1b19c2ff-7e1e-4536-86ca-788d2b74c244", "SuperAdmin", "SUPERADMIN" },
                    { "302058e4-bc56-4ae9-811f-be300d1d95b7", "323e9bde-17e3-4766-9e47-1eee5ff524d6", "Tenant", "TENANT" },
                    { "35db6d53-0991-4808-8626-13b00a200308", "56e492f1-074b-4273-a3ad-bcc1255c1e45", "Admin", "ADMIN" },
                    { "c6052060-83f2-4fad-b805-45f14ba303d2", "cdbc588f-8b5d-47c9-9a99-4142b2fd651e", "PropertyManager", "PROPERTYMANAGER" }
>>>>>>>> bec6a65 (updated access for property):Migrations/20221104064608_initialMigrations.cs
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UnitId", "UserName", "UsernameChangeLimit" },
<<<<<<<< HEAD:Migrations/20221104190359_seedingUpdate.cs
                values: new object[] { "39cba547-a033-43b2-9c0e-76fceb082103", 0, "64f5f756-9aa4-4e0b-aaf6-aec28c13e98e", new DateTime(1992, 5, 25, 0, 0, 0, 0, DateTimeKind.Local), "ApplicationUser", "superadmin@chickadeeinvest.ca", true, "Matt", "Hardwick", false, null, "SUPERADMIN@CHICKADEEINVEST.CA", "SUPERADMIN@CHICKADEEINVEST.CA", "AQAAAAEAACcQAAAAEFgkFs2VWGXddkqxmEJ7Jxi5Fgujo1Y+FNzy5d2knACwxu1Q65jRflvrxo/egkYlwA==", null, true, null, "12f6c62a-b09d-4be2-9ada-97decc1053f4", false, null, "superadmin@chickadeeinvest.ca", 10 });
========
                values: new object[] { "946b7ca1-e8f1-45a1-b132-3dbbf0a197ce", 0, "ed480fa1-741a-4789-b5b7-ef8389fa6b2a", new DateTime(1992, 5, 24, 0, 0, 0, 0, DateTimeKind.Local), "ApplicationUser", "superadmin@chickadeeinvest.ca", true, "Matt", "Hardwick", false, null, "SUPERADMIN@CHICKADEEINVEST.CA", "SUPERADMIN@CHICKADEEINVEST.CA", "AQAAAAEAACcQAAAAEL4Wvzw1yxJcJDovO/7nQt9dYQKc0tNAnvvNbjGknylHHjgIJTJow9bq338RsadSog==", null, true, null, "3945737d-9a04-4a4d-8432-83538ca5ea96", false, null, "superadmin@chickadeeinvest.ca", 10 });
>>>>>>>> bec6a65 (updated access for property):Migrations/20221104064608_initialMigrations.cs

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "CompanyId", "Address", "Email", "Logo", "Name", "Phone" },
                values: new object[,]
                {
<<<<<<<< HEAD:Migrations/20221104190359_seedingUpdate.cs
                    { "1e494e7d-9e97-4495-b84f-2a999765267c", "123 Main St", "main@companyOne.com", null, "Company One", "604-235-7890" },
                    { "890645eb-3e2c-4d89-b309-67422150d800", "Wall street", "main@companyTwo.com", null, "Company Two", "778-334-4594" }
========
                    { "158cd2b9-38a8-43e4-9bc7-0269f0288edf", "123 Main St", "main@companyOne.com", null, "Company One", "604-235-7890" },
                    { "f3ec507d-5304-4c7a-850c-bb71f15cb788", "Wall street", "main@companyTwo.com", null, "Company Two", "778-334-4594" }
>>>>>>>> bec6a65 (updated access for property):Migrations/20221104064608_initialMigrations.cs
                });

            migrationBuilder.InsertData(
                table: "Property",
                columns: new[] { "PropertyId", "Address", "Name" },
                values: new object[,]
                {
<<<<<<<< HEAD:Migrations/20221104190359_seedingUpdate.cs
                    { "2acf0b96-7851-4504-acdf-c1970245cc08", "123 Sesame Street", "Montana Apartments Managed By PM 2" },
                    { "aa6ccdbb-8405-4130-a99d-3476a3115639", "742 Evergreen Terrace", "The Evergreen Managed By PM 1" },
                    { "bf51d90a-2a46-4587-89d8-edbe3f1f96a3", "7488 Hazel Street", "Arcola Managed by PM 2" }
========
                    { "12e85941-3ebd-4450-a84c-153df5c062b1", "7488 Hazel Street", "Arcola Managed by PM 2" },
                    { "3770500e-bd60-4a6d-9e4c-07b637854c6e", "742 Evergreen Terrace", "The Evergreen Managed By PM 1" },
                    { "ca0f860c-d36c-4d24-978d-e585d3403d5c", "123 Sesame Street", "Montana Apartments Managed By PM 2" }
>>>>>>>> bec6a65 (updated access for property):Migrations/20221104064608_initialMigrations.cs
                });

            migrationBuilder.InsertData(
                table: "VerificationDocuments",
                columns: new[] { "VerificationDocumentId", "DocumentType", "ResponseMessage", "TenantId", "UserId", "data" },
<<<<<<<< HEAD:Migrations/20221104190359_seedingUpdate.cs
                values: new object[] { "00000000-0000-0000-0000-000000000000", 0, null, "283f49b8-c882-43db-a0ea-77e998a6e511", null, new byte[0] });
========
                values: new object[] { "00000000-0000-0000-0000-000000000000", 0, null, "37e6ac65-77b0-4260-83e7-2e9135af187c", null, new byte[0] });
>>>>>>>> bec6a65 (updated access for property):Migrations/20221104064608_initialMigrations.cs

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
<<<<<<<< HEAD:Migrations/20221104190359_seedingUpdate.cs
                    { "0a5e6ca9-c02d-4772-969e-78cc8419b670", "39cba547-a033-43b2-9c0e-76fceb082103" },
                    { "3706ca29-1aa8-4dea-b918-bdb9ed4bd8e6", "39cba547-a033-43b2-9c0e-76fceb082103" },
                    { "b45a8548-ace4-4de3-8731-cdeb9ea8f908", "39cba547-a033-43b2-9c0e-76fceb082103" },
                    { "f2895e8d-f04e-4eec-9017-c13b11e37ba0", "39cba547-a033-43b2-9c0e-76fceb082103" }
========
                    { "14c159b3-77be-4fbd-8b7a-5e6ec5a78292", "946b7ca1-e8f1-45a1-b132-3dbbf0a197ce" },
                    { "302058e4-bc56-4ae9-811f-be300d1d95b7", "946b7ca1-e8f1-45a1-b132-3dbbf0a197ce" },
                    { "35db6d53-0991-4808-8626-13b00a200308", "946b7ca1-e8f1-45a1-b132-3dbbf0a197ce" },
                    { "c6052060-83f2-4fad-b805-45f14ba303d2", "946b7ca1-e8f1-45a1-b132-3dbbf0a197ce" }
>>>>>>>> bec6a65 (updated access for property):Migrations/20221104064608_initialMigrations.cs
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CompanyId", "ConcurrencyStamp", "DateOfBirth", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UnitId", "UserName", "UsernameChangeLimit" },
                values: new object[,]
                {
<<<<<<<< HEAD:Migrations/20221104190359_seedingUpdate.cs
                    { "4cdf772f-438c-4793-872c-7e6c04a75855", 0, "1e494e7d-9e97-4495-b84f-2a999765267c", "766d6ab3-fff7-485a-a44f-24d259dc548f", new DateTime(1992, 5, 25, 0, 0, 0, 0, DateTimeKind.Local), "PropertyManager", "propertymanager@gmail.com", true, "Property", "Manager", false, null, "PROPERTYMANAGER@GMAIL.COM", "PROPERTYMANAGER@GMAIL.COM", "AQAAAAEAACcQAAAAELaTbs2u2aUiZoJOH41uG8ugZG2cGaPlcyZg2j7K9RkmER2VlNIJGNJ7GG2BmZKEzA==", null, true, null, "6d9d97ae-c2cb-4ac3-a611-5f1f34eaf8fc", false, null, "propertymanager@gmail.com", 10 },
                    { "8dcce1c2-f0b6-4bf1-bf08-8be4b12ae1f2", 0, "890645eb-3e2c-4d89-b309-67422150d800", "5a567431-d522-4adb-8ce4-e51a665f36ea", new DateTime(1992, 7, 23, 0, 0, 0, 0, DateTimeKind.Local), "PropertyManager", "propertymanager2@gmail.com", true, "Manager", "Property", false, null, "PROPERTYMANAGER2@GMAIL.COM", "PROPERTYMANAGER2@GMAIL.COM", "AQAAAAEAACcQAAAAEPLf3AUK47YeBoaN8o1YrQhAqFtrDN8Uqc1NImlEg1SbhumPNRWYvqqF9wAessA84w==", null, true, null, "a57b449e-2206-4486-9f67-ab5a82bd552e", false, null, "propertymanager2@gmail.com", 10 }
========
                    { "5eef464a-0f7a-4541-9559-1b4509133c78", 0, "f3ec507d-5304-4c7a-850c-bb71f15cb788", "84196957-d2b5-4695-a3cb-a3b9eefe4edd", new DateTime(1992, 7, 22, 0, 0, 0, 0, DateTimeKind.Local), "PropertyManager", "propertymanager2@gmail.com", true, "Manager", "Property", false, null, "PROPERTYMANAGER2@GMAIL.COM", "PROPERTYMANAGER2@GMAIL.COM", "AQAAAAEAACcQAAAAEOKGxizUXa+MqvGnC0sbIgmEmBwn1rORwaehAbl6x06c05C9M+u7M4jQWNWtJtRm1Q==", null, true, null, "4089b94e-bf66-4641-a627-26ad9a9fb86d", false, null, "propertymanager2@gmail.com", 10 },
                    { "849f08cb-22ce-4334-99fd-14df3eb26274", 0, "158cd2b9-38a8-43e4-9bc7-0269f0288edf", "d15a0411-bb58-4c0c-a33f-4e8924ca52e7", new DateTime(1992, 5, 24, 0, 0, 0, 0, DateTimeKind.Local), "PropertyManager", "propertymanager@gmail.com", true, "Property", "Manager", false, null, "PROPERTYMANAGER@GMAIL.COM", "PROPERTYMANAGER@GMAIL.COM", "AQAAAAEAACcQAAAAEHvmiIcq0nz7dCO4oqUZyCpLImf4+Ny7/btk7O5r73v6AhD6x05gsG38AQ10k7pI8Q==", null, true, null, "cc13b7ad-bdb7-4a0b-9983-8a1bb5db79e7", false, null, "propertymanager@gmail.com", 10 }
>>>>>>>> bec6a65 (updated access for property):Migrations/20221104064608_initialMigrations.cs
                });

            migrationBuilder.InsertData(
                table: "Unit",
<<<<<<<< HEAD:Migrations/20221104190359_seedingUpdate.cs
                columns: new[] { "UnitId", "PropertyId", "PropertyManagerId", "PropertyManagerId1", "UnitNo", "UnitType" },
                values: new object[] { "de6f1e86-d3c1-4e6a-9986-9361792219e3", "2acf0b96-7851-4504-acdf-c1970245cc08", null, null, 300, 1 });
========
                columns: new[] { "UnitId", "PropertyId", "PropertyManagerId", "UnitNo", "UnitType" },
                values: new object[] { "430edb6d-c913-4c9e-a615-958715410dc3", "ca0f860c-d36c-4d24-978d-e585d3403d5c", null, 300, 1 });
>>>>>>>> bec6a65 (updated access for property):Migrations/20221104064608_initialMigrations.cs

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
<<<<<<<< HEAD:Migrations/20221104190359_seedingUpdate.cs
                    { "3706ca29-1aa8-4dea-b918-bdb9ed4bd8e6", "4cdf772f-438c-4793-872c-7e6c04a75855" },
                    { "3706ca29-1aa8-4dea-b918-bdb9ed4bd8e6", "8dcce1c2-f0b6-4bf1-bf08-8be4b12ae1f2" }
========
                    { "c6052060-83f2-4fad-b805-45f14ba303d2", "5eef464a-0f7a-4541-9559-1b4509133c78" },
                    { "c6052060-83f2-4fad-b805-45f14ba303d2", "849f08cb-22ce-4334-99fd-14df3eb26274" }
>>>>>>>> bec6a65 (updated access for property):Migrations/20221104064608_initialMigrations.cs
                });

            migrationBuilder.InsertData(
                table: "Unit",
                columns: new[] { "UnitId", "PropertyId", "PropertyManagerId", "PropertyManagerId1", "UnitNo", "UnitType" },
                values: new object[,]
                {
<<<<<<<< HEAD:Migrations/20221104190359_seedingUpdate.cs
                    { "617a788c-8a55-46a2-b730-33337cb86469", "2acf0b96-7851-4504-acdf-c1970245cc08", "8dcce1c2-f0b6-4bf1-bf08-8be4b12ae1f2", null, 200, 1 },
                    { "636ae42a-c0da-43b8-ae66-1fe5d9fe2b41", "aa6ccdbb-8405-4130-a99d-3476a3115639", "4cdf772f-438c-4793-872c-7e6c04a75855", null, 100, 0 }
========
                    { "42a808da-24df-4649-8ba0-0bfa0d286a88", "ca0f860c-d36c-4d24-978d-e585d3403d5c", "5eef464a-0f7a-4541-9559-1b4509133c78", 200, 1 },
                    { "510f05fc-f33a-4437-8fd0-6aa2d49fc1b1", "3770500e-bd60-4a6d-9e4c-07b637854c6e", "849f08cb-22ce-4334-99fd-14df3eb26274", 100, 0 }
>>>>>>>> bec6a65 (updated access for property):Migrations/20221104064608_initialMigrations.cs
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
<<<<<<<< HEAD:Migrations/20221104190359_seedingUpdate.cs
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LeaseNumber", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UnitId", "UserName", "UsernameChangeLimit" },
                values: new object[] { "283f49b8-c882-43db-a0ea-77e998a6e511", 0, "27664623-af1c-4172-a2ba-a9493b467134", new DateTime(2002, 5, 25, 0, 0, 0, 0, DateTimeKind.Local), "Tenant", "tenant@gmail.com", true, "Tenant", "User", null, false, null, "TENANT@GMAIL.COM", "TENANT@GMAIL.COM", "AQAAAAEAACcQAAAAEE5MbOpUOa/PXMYutLIJPmU7uqW/yXvT2QDMzkN10JSSriVe9mfpjYHJT6puIid6Ew==", null, true, null, "e3fdbca4-7d7c-4437-96e1-4977f6be6164", false, "636ae42a-c0da-43b8-ae66-1fe5d9fe2b41", "tenant@gmail.com", 10 });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LeaseNumber", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UnitId", "UserName", "UsernameChangeLimit" },
                values: new object[] { "d7b85bd1-baaf-4ab7-b898-e78d70fed473", 0, "5da66453-67d6-41e7-b758-9d56641019c6", new DateTime(2002, 5, 25, 0, 0, 0, 0, DateTimeKind.Local), "Tenant", "tenant2@gmail.com", true, "User", "Tenant", null, false, null, "TENANT2@GMAIL.COM", "TENANT2@GMAIL.COM", "AQAAAAEAACcQAAAAEJLXWnbAUeYKEz3x1ExhhJp9hyU5GG+lWnIHvYMiTcmge0m/kWrwbc4vgoy+m0WAvQ==", null, true, null, "aeae3b2f-d69e-4d86-ba33-37874e6f7ed9", false, "617a788c-8a55-46a2-b730-33337cb86469", "tenant2@gmail.com", 10 });
========
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Discriminator", "Email", "EmailConfirmed", "FirstName", "IsIdVerified", "LastName", "LeaseNumber", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UnitId", "UserName", "UsernameChangeLimit" },
                values: new object[] { "37e6ac65-77b0-4260-83e7-2e9135af187c", 0, "da2e78d2-4364-4417-9cdc-c249eb63b5d6", new DateTime(2002, 5, 24, 0, 0, 0, 0, DateTimeKind.Local), "Tenant", "tenant@gmail.com", true, "Tenant", false, "User", null, false, null, "TENANT@GMAIL.COM", "TENANT@GMAIL.COM", "AQAAAAEAACcQAAAAEIO10ya4Cs7CGw1iUaXbL11qdIroIJlNAu5ZYWUtB/BkaHBJtVc1I71iJ7YEYwHKtw==", null, true, null, "06994bb8-b1de-4c9e-9373-1f09e1870620", false, "510f05fc-f33a-4437-8fd0-6aa2d49fc1b1", "tenant@gmail.com", 10 });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Discriminator", "Email", "EmailConfirmed", "FirstName", "IsIdVerified", "LastName", "LeaseNumber", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UnitId", "UserName", "UsernameChangeLimit" },
                values: new object[] { "774a63f5-803d-4364-80a1-a46ec1ad60ba", 0, "f69c3283-1e2b-4046-af43-c5e8360dd6c0", new DateTime(2002, 5, 24, 0, 0, 0, 0, DateTimeKind.Local), "Tenant", "tenant2@gmail.com", true, "User", false, "Tenant", null, false, null, "TENANT2@GMAIL.COM", "TENANT2@GMAIL.COM", "AQAAAAEAACcQAAAAECilocDd0pF/6lbREdcTkLwXyxPpoMM9DtxIFkD+chsRv7DcFCeWwCz8WkWbYzQlNw==", null, true, null, "6b160dcd-8ef9-4a25-8275-a397344b2765", false, "42a808da-24df-4649-8ba0-0bfa0d286a88", "tenant2@gmail.com", 10 });
>>>>>>>> bec6a65 (updated access for property):Migrations/20221104064608_initialMigrations.cs

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
<<<<<<<< HEAD:Migrations/20221104190359_seedingUpdate.cs
                    { "b45a8548-ace4-4de3-8731-cdeb9ea8f908", "283f49b8-c882-43db-a0ea-77e998a6e511" },
                    { "b45a8548-ace4-4de3-8731-cdeb9ea8f908", "d7b85bd1-baaf-4ab7-b898-e78d70fed473" }
========
                    { "302058e4-bc56-4ae9-811f-be300d1d95b7", "37e6ac65-77b0-4260-83e7-2e9135af187c" },
                    { "302058e4-bc56-4ae9-811f-be300d1d95b7", "774a63f5-803d-4364-80a1-a46ec1ad60ba" }
>>>>>>>> bec6a65 (updated access for property):Migrations/20221104064608_initialMigrations.cs
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "ClosedDate", "CreatedById", "CreatedOn", "Description", "EstimatedDate", "Problem", "Severity", "Status", "UnitId" },
                values: new object[,]
                {
<<<<<<<< HEAD:Migrations/20221104190359_seedingUpdate.cs
                    { 1, null, "283f49b8-c882-43db-a0ea-77e998a6e511", new DateTime(2022, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Massive Leak from the Kitchen pipe", new DateTime(2022, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Massive Leakage", 2, 0, "636ae42a-c0da-43b8-ae66-1fe5d9fe2b41" },
                    { 2, null, "283f49b8-c882-43db-a0ea-77e998a6e511", new DateTime(2021, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Need repairing the floors from last earthquake", new DateTime(2022, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Earthquake repair", 1, 0, "636ae42a-c0da-43b8-ae66-1fe5d9fe2b41" },
                    { 3, null, "d7b85bd1-baaf-4ab7-b898-e78d70fed473", new DateTime(1999, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Need to fix the roof that was damaged by the tornado", new DateTime(2000, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tornado damage", 2, 1, "617a788c-8a55-46a2-b730-33337cb86469" },
                    { 4, null, "d7b85bd1-baaf-4ab7-b898-e78d70fed473", new DateTime(2019, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Currently getting by with rat traps", new DateTime(2022, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rat infestation", 1, 0, "617a788c-8a55-46a2-b730-33337cb86469" }
========
                    { 1, null, "37e6ac65-77b0-4260-83e7-2e9135af187c", new DateTime(2022, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Massive Leak from the Kitchen pipe", new DateTime(2022, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Massive Leakage", 2, 0, "510f05fc-f33a-4437-8fd0-6aa2d49fc1b1" },
                    { 2, null, "37e6ac65-77b0-4260-83e7-2e9135af187c", new DateTime(2021, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Need repairing the floors from last earthquake", new DateTime(2022, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Earthquake repair", 1, 0, "510f05fc-f33a-4437-8fd0-6aa2d49fc1b1" },
                    { 3, null, "774a63f5-803d-4364-80a1-a46ec1ad60ba", new DateTime(1999, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Need to fix the roof that was damaged by the tornado", new DateTime(2000, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tornado damage", 2, 1, "42a808da-24df-4649-8ba0-0bfa0d286a88" },
                    { 4, null, "774a63f5-803d-4364-80a1-a46ec1ad60ba", new DateTime(2019, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Currently getting by with rat traps", new DateTime(2022, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rat infestation", 1, 0, "42a808da-24df-4649-8ba0-0bfa0d286a88" }
>>>>>>>> bec6a65 (updated access for property):Migrations/20221104064608_initialMigrations.cs
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "MessageId", "CreatedDate", "SenderId", "TicketId", "UnitId", "UserId", "content" },
<<<<<<<< HEAD:Migrations/20221104190359_seedingUpdate.cs
                values: new object[] { "5a08a285-b979-4126-b3f7-a34780420b81", new DateTime(2022, 11, 3, 13, 3, 59, 109, DateTimeKind.Local).AddTicks(6910), "283f49b8-c882-43db-a0ea-77e998a6e511", 1, null, null, "This is a message" });
========
                values: new object[] { "f9f1a5fa-e188-4d06-8979-f27e77377b96", new DateTime(2022, 11, 3, 0, 46, 8, 351, DateTimeKind.Local).AddTicks(8930), "37e6ac65-77b0-4260-83e7-2e9135af187c", 1, null, null, "This is a message" });
>>>>>>>> bec6a65 (updated access for property):Migrations/20221104064608_initialMigrations.cs

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UnitId",
                table: "AspNetUsers",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_Expiration",
                table: "DeviceCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_Keys_Use",
                table: "Keys",
                column: "Use");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_TicketId",
                table: "Messages",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UnitId",
                table: "Messages",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_ConsumedTime",
                table: "PersistedGrants",
                column: "ConsumedTime");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "SessionId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_TicketImage_CreatedById",
                table: "TicketImage",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TicketImage_TicketId",
                table: "TicketImage",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CreatedById",
                table: "Tickets",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UnitId",
                table: "Tickets",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_PropertyId",
                table: "Unit",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_PropertyManagerId",
                table: "Unit",
                column: "PropertyManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_PropertyManagerId1",
                table: "Unit",
                column: "PropertyManagerId1");

            migrationBuilder.CreateIndex(
                name: "IX_UnitImage_UnitId",
                table: "UnitImage",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitNote_UnitId",
                table: "UnitNote",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_VerificationDocuments_UserId",
                table: "VerificationDocuments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Unit_UnitId",
                table: "AspNetUsers",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unit_AspNetUsers_PropertyManagerId",
                table: "Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_AspNetUsers_PropertyManagerId1",
                table: "Unit");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "Keys");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "TicketImage");

            migrationBuilder.DropTable(
                name: "UnitImage");

            migrationBuilder.DropTable(
                name: "UnitNote");

            migrationBuilder.DropTable(
                name: "VerificationDocuments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "Property");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chickadee.Migrations
{
    public partial class initialMigrations : Migration
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
                    PropertyManagerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
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
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
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
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
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
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
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
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
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
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
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
                    { "146ab15e-8aab-4722-9c33-edaf39a3c575", "97ba5ccf-c178-4204-8600-f6a4142b7d82", "PropertyManager", "PROPERTYMANAGER" },
                    { "36b7c879-499b-4822-938a-5314ec882e6c", "4932b039-d5a8-4490-b6f3-4a49efc77290", "SuperAdmin", "SUPERADMIN" },
                    { "9030afa9-9134-476d-a760-50b2bd7862ab", "7701c362-8e0f-4467-85b7-508452305858", "Admin", "ADMIN" },
                    { "95f7bf62-5107-4a30-bb3c-e8cfd6e7b555", "d07477c6-b4b8-430b-a70e-87daa8213509", "Tenant", "TENANT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UnitId", "UserName", "UsernameChangeLimit" },
                values: new object[] { "1ac79de8-ef90-476f-a185-e107cff3f6f0", 0, "920e354a-6903-4026-b28c-5c19b4e0c812", new DateTime(1992, 5, 30, 0, 0, 0, 0, DateTimeKind.Local), "ApplicationUser", "superadmin@chickadeeinvest.ca", true, "Matt", "Hardwick", false, null, "SUPERADMIN@CHICKADEEINVEST.CA", "SUPERADMIN@CHICKADEEINVEST.CA", "AQAAAAEAACcQAAAAEHGWTgjW+RvYLO8RfL1StV4bqsyy60mEKIdFyqmI2AKb4WDVcNWFOKJEVu4ee6zSrA==", null, true, null, "043df8c2-8c50-44c2-a79c-68379bd2bbd7", false, null, "superadmin@chickadeeinvest.ca", 10 });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "CompanyId", "Address", "Email", "Logo", "Name", "Phone" },
                values: new object[,]
                {
                    { "68c7af0d-7452-4a22-89db-7823d1a7bcdc", "Wall street", "main@companyTwo.com", null, "Company Two", "778-334-4594" },
                    { "d0a26493-2da4-44c1-8c37-1d7ddf44cc1a", "123 Main St", "main@companyOne.com", null, "Company One", "604-235-7890" }
                });

            migrationBuilder.InsertData(
                table: "Property",
                columns: new[] { "PropertyId", "Address", "Name" },
                values: new object[,]
                {
                    { "4660f5df-733f-420e-9cdd-a82d152b9c24", "123 Sesame Street", "Montana Apartments Managed By PM 2" },
                    { "513245b3-b258-47e6-9d59-dd320254bcfa", "7488 Hazel Street", "Arcola Managed by PM 2" },
                    { "9542ac6c-a86d-4ef6-9cca-675bc89fc95d", "742 Evergreen Terrace", "The Evergreen Managed By PM 1" }
                });

            migrationBuilder.InsertData(
                table: "VerificationDocuments",
                columns: new[] { "VerificationDocumentId", "Data", "DocumentType", "ResponseMessage", "TenantId", "UserId" },
                values: new object[] { "fb386199-3199-455a-baf3-19d80da23bc4", new byte[0], 0, null, "7948657c-f75f-4baf-9ebe-bc4f8a6e2f4a", null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "146ab15e-8aab-4722-9c33-edaf39a3c575", "1ac79de8-ef90-476f-a185-e107cff3f6f0" },
                    { "36b7c879-499b-4822-938a-5314ec882e6c", "1ac79de8-ef90-476f-a185-e107cff3f6f0" },
                    { "9030afa9-9134-476d-a760-50b2bd7862ab", "1ac79de8-ef90-476f-a185-e107cff3f6f0" },
                    { "95f7bf62-5107-4a30-bb3c-e8cfd6e7b555", "1ac79de8-ef90-476f-a185-e107cff3f6f0" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CompanyId", "ConcurrencyStamp", "DateOfBirth", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UnitId", "UserName", "UsernameChangeLimit" },
                values: new object[,]
                {
                    { "b1098c8c-72ce-4e5f-8eac-7be966d61f7e", 0, "68c7af0d-7452-4a22-89db-7823d1a7bcdc", "36d8c40d-f8d4-43dc-8e37-599f3003a531", new DateTime(1992, 7, 28, 0, 0, 0, 0, DateTimeKind.Local), "PropertyManager", "propertymanager2@gmail.com", true, "Manager", "Property", false, null, "PROPERTYMANAGER2@GMAIL.COM", "PROPERTYMANAGER2@GMAIL.COM", "AQAAAAEAACcQAAAAENjEiHfTvhtFY65FHlq+P2fT6g5fMDeyrqSjeYs5K9YwllHz0qreW76JJXIURjzhtA==", null, true, null, "29ce89c9-b475-4da2-84ec-5ce35993a627", false, null, "propertymanager2@gmail.com", 10 },
                    { "cc985839-5b61-40bd-a638-c5bd97cb0ef4", 0, "d0a26493-2da4-44c1-8c37-1d7ddf44cc1a", "d43ce774-50fd-4cfb-92f3-c0f6b043819a", new DateTime(1992, 5, 30, 0, 0, 0, 0, DateTimeKind.Local), "PropertyManager", "propertymanager@gmail.com", true, "Property", "Manager", false, null, "PROPERTYMANAGER@GMAIL.COM", "PROPERTYMANAGER@GMAIL.COM", "AQAAAAEAACcQAAAAELziqow7U5Np5yr9HX45C47B6gkhweSrlOUBHR+LQ/dVBWIJ9tfa8sglFUC7JWEHpw==", null, true, null, "818c04d1-fcc4-4244-928e-0e7f268896b9", false, null, "propertymanager@gmail.com", 10 }
                });

            migrationBuilder.InsertData(
                table: "Unit",
                columns: new[] { "UnitId", "PropertyId", "PropertyManagerId", "UnitNo", "UnitType" },
                values: new object[] { "fa11afc6-27d6-4a1e-ba7b-a836c630f821", "4660f5df-733f-420e-9cdd-a82d152b9c24", null, 300, 1 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "146ab15e-8aab-4722-9c33-edaf39a3c575", "b1098c8c-72ce-4e5f-8eac-7be966d61f7e" },
                    { "146ab15e-8aab-4722-9c33-edaf39a3c575", "cc985839-5b61-40bd-a638-c5bd97cb0ef4" }
                });

            migrationBuilder.InsertData(
                table: "Unit",
                columns: new[] { "UnitId", "PropertyId", "PropertyManagerId", "UnitNo", "UnitType" },
                values: new object[,]
                {
                    { "8aef5704-631b-40d0-8f60-9491143c5c40", "4660f5df-733f-420e-9cdd-a82d152b9c24", "b1098c8c-72ce-4e5f-8eac-7be966d61f7e", 200, 1 },
                    { "9b9f305f-26e3-40a2-b068-8a8a9160f304", "9542ac6c-a86d-4ef6-9cca-675bc89fc95d", "cc985839-5b61-40bd-a638-c5bd97cb0ef4", 100, 0 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Discriminator", "Email", "EmailConfirmed", "FirstName", "IsIdVerified", "LastName", "LeaseNumber", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UnitId", "UserName", "UsernameChangeLimit" },
                values: new object[] { "302dcfda-782b-4e2e-b012-dbdf4e8c6dea", 0, "93a64c02-bb66-450c-b7f0-1e3585fc06a0", new DateTime(2002, 5, 30, 0, 0, 0, 0, DateTimeKind.Local), "Tenant", "tenant2@gmail.com", true, "User", false, "Tenant", null, false, null, "TENANT2@GMAIL.COM", "TENANT2@GMAIL.COM", "AQAAAAEAACcQAAAAEL0JNagYND3D/NnaKpN6bVF4MCg/jgHIjpH/C6jH05mVL10kuvdCFQRNql4SYVQBtg==", null, true, null, "b35df390-83eb-4171-be80-5c661cbe99ad", false, "8aef5704-631b-40d0-8f60-9491143c5c40", "tenant2@gmail.com", 10 });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Discriminator", "Email", "EmailConfirmed", "FirstName", "IsIdVerified", "LastName", "LeaseNumber", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UnitId", "UserName", "UsernameChangeLimit" },
                values: new object[] { "7948657c-f75f-4baf-9ebe-bc4f8a6e2f4a", 0, "cec1bb4e-f0a0-42a5-9e5a-5a70c408d1e5", new DateTime(2002, 5, 30, 0, 0, 0, 0, DateTimeKind.Local), "Tenant", "tenant@gmail.com", true, "Tenant", false, "User", null, false, null, "TENANT@GMAIL.COM", "TENANT@GMAIL.COM", "AQAAAAEAACcQAAAAEGFe8cieAeXsaFf6e4H2hA30t4BTM6S9LHr+ZAOxCa6YR+U7eXKMb9JM48vRHTV4SQ==", null, true, null, "136a5fbc-31e1-4f36-88c7-b0fe90942355", false, "9b9f305f-26e3-40a2-b068-8a8a9160f304", "tenant@gmail.com", 10 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "95f7bf62-5107-4a30-bb3c-e8cfd6e7b555", "302dcfda-782b-4e2e-b012-dbdf4e8c6dea" },
                    { "95f7bf62-5107-4a30-bb3c-e8cfd6e7b555", "7948657c-f75f-4baf-9ebe-bc4f8a6e2f4a" }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "ClosedDate", "CreatedById", "CreatedOn", "Description", "EstimatedDate", "Problem", "Severity", "Status", "UnitId" },
                values: new object[,]
                {
                    { 1, null, "7948657c-f75f-4baf-9ebe-bc4f8a6e2f4a", new DateTime(2022, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Massive Leak from the Kitchen pipe", new DateTime(2022, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Massive Leakage", 2, 0, "9b9f305f-26e3-40a2-b068-8a8a9160f304" },
                    { 2, null, "7948657c-f75f-4baf-9ebe-bc4f8a6e2f4a", new DateTime(2021, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Need repairing the floors from last earthquake", new DateTime(2022, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Earthquake repair", 1, 0, "9b9f305f-26e3-40a2-b068-8a8a9160f304" },
                    { 3, null, "302dcfda-782b-4e2e-b012-dbdf4e8c6dea", new DateTime(1999, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Need to fix the roof that was damaged by the tornado", new DateTime(2000, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tornado damage", 2, 1, "8aef5704-631b-40d0-8f60-9491143c5c40" },
                    { 4, null, "302dcfda-782b-4e2e-b012-dbdf4e8c6dea", new DateTime(2019, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Currently getting by with rat traps", new DateTime(2022, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rat infestation", 1, 0, "8aef5704-631b-40d0-8f60-9491143c5c40" }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "MessageId", "Content", "CreatedDate", "SenderId", "TicketId", "UnitId", "UserId" },
                values: new object[] { "9b49980f-b59f-4540-a775-1bfacfce11d9", "This is a message", new DateTime(2022, 11, 8, 16, 56, 57, 720, DateTimeKind.Local).AddTicks(8090), "7948657c-f75f-4baf-9ebe-bc4f8a6e2f4a", 1, null, null });

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

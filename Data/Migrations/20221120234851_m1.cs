using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chickadee.Data.Migrations
{
    public partial class m1 : Migration
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
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    UnitId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                    TicketId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
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
                    { "080d511b-34a3-400f-8a57-86ef6aa5084d", "c7fa0336-4e90-448b-961c-57b9482cc34f", "Admin", "ADMIN" },
                    { "1ada4d5a-7168-4c61-87a1-ec49c58288c7", "9bd256a7-7291-4340-a1c4-db20a20c0fbe", "Tenant", "TENANT" },
                    { "8b7ca908-d32c-4822-b36f-d017aa0ca332", "0a7cea90-a53b-44a9-be2b-9065dabbbb85", "SuperAdmin", "SUPERADMIN" },
                    { "d9abff3c-7ffc-4b30-8f57-e2a4b1e027e5", "b5de29d3-5b38-4148-bf17-a7fb1358ea72", "PropertyManager", "PROPERTYMANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UnitId", "UserName", "UsernameChangeLimit" },
                values: new object[] { "c0dce385-f332-4000-b001-f0491e7870d4", 0, "7ee6ab2b-fa7d-450e-bf27-f0541e9717d3", new DateTime(1977, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "ApplicationUser", "superadmin@chickadeeinvest.ca", true, "Matt", "Hardwick", false, null, "SUPERADMIN@CHICKADEEINVEST.CA", "SUPERADMIN@CHICKADEEINVEST.CA", "AQAAAAEAACcQAAAAEIfh9MX9iu5mNfAMmelKYDglcrzl/CK7MZNuVyACFWESbZU1/dz9yuvq5aym39citg==", null, true, null, "0aca5daa-b0c6-48ea-8b75-6fad92ab790d", false, null, "superadmin@chickadeeinvest.ca", 10 });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "CompanyId", "Address", "Email", "Logo", "Name", "Phone" },
                values: new object[,]
                {
                    { "4789c283-6916-4ec2-9ea4-500ecccc47f4", "65070 Einar Lock", "stephan@smitham.ca", null, "Hoeger-Kuphal", "(987)127-9027" },
                    { "f23ea52b-a4fc-43a4-a0e0-9f8cb5824721", "71160 Katrina Island", "sofia@sawayn.info", null, "Jewess, Schowalter and Grimes", "(903)586-6265" }
                });

            migrationBuilder.InsertData(
                table: "Property",
                columns: new[] { "PropertyId", "Address", "Name" },
                values: new object[,]
                {
                    { "27452fed-f2c3-4b27-b7f8-89b61c867456", "742 Evergreen Terrace", "The Evergreen Managed By PM 1" },
                    { "97f2ba24-2e10-4bcd-8db3-c0d8012ad1c5", "7488 Hazel Street", "Arcola Managed by PM 2" },
                    { "b5692ce6-a5cc-4d7d-88f4-658fbe8354df", "123 Sesame Street", "Montana Apartments Managed By PM 2" }
                });

            migrationBuilder.InsertData(
                table: "VerificationDocuments",
                columns: new[] { "VerificationDocumentId", "Data", "DocumentType", "ResponseMessage", "TenantId", "UserId" },
                values: new object[] { "d822f748-f02c-4f34-bd2c-d848dd8c34fe", new byte[0], 0, null, "28d5cf81-1bb2-4873-a186-a7d12810b3a8", null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "080d511b-34a3-400f-8a57-86ef6aa5084d", "c0dce385-f332-4000-b001-f0491e7870d4" },
                    { "1ada4d5a-7168-4c61-87a1-ec49c58288c7", "c0dce385-f332-4000-b001-f0491e7870d4" },
                    { "8b7ca908-d32c-4822-b36f-d017aa0ca332", "c0dce385-f332-4000-b001-f0491e7870d4" },
                    { "d9abff3c-7ffc-4b30-8f57-e2a4b1e027e5", "c0dce385-f332-4000-b001-f0491e7870d4" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CompanyId", "ConcurrencyStamp", "DateOfBirth", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UnitId", "UserName", "UsernameChangeLimit" },
                values: new object[,]
                {
                    { "34062685-7e28-468d-90e9-03de2e3413fe", 0, "4789c283-6916-4ec2-9ea4-500ecccc47f4", "3ed90ae6-cfdf-486c-b90b-25964e28120b", new DateTime(1970, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "PropertyManager", "propertymanager2@gmail.com", true, "Shanelle", "Cartwright", false, null, "PROPERTYMANAGER2@GMAIL.COM", "PROPERTYMANAGER2@GMAIL.COM", "AQAAAAEAACcQAAAAEB07ZusOj1MHuPUQOvuBLM/hkGN5FwxIp8yf8ztbCmJZQk8Ssx3Gy+OpmmqDEgZj1w==", null, true, null, "0fff7e8b-b90d-4677-96d1-001a9b06074a", false, null, "propertymanager2@gmail.com", 10 },
                    { "98bf7476-e8cc-48be-b280-d40832d2448b", 0, "f23ea52b-a4fc-43a4-a0e0-9f8cb5824721", "7aaced96-a8d3-442c-a101-d3a9ccb5423c", new DateTime(1960, 6, 23, 0, 0, 0, 0, DateTimeKind.Utc), "PropertyManager", "propertymanager@gmail.com", true, "Kaitlyn", "Volkman", false, null, "PROPERTYMANAGER@GMAIL.COM", "PROPERTYMANAGER@GMAIL.COM", "AQAAAAEAACcQAAAAENLYzbZTH/H1yVgjMpfplyj16QOA63yBeshr/GdpSC+vd6aLE6ayI63/A81+yJRhbg==", null, true, null, "01bddd67-a9fe-451a-8ecb-28dd9c9f6558", false, null, "propertymanager@gmail.com", 10 }
                });

            migrationBuilder.InsertData(
                table: "Unit",
                columns: new[] { "UnitId", "PropertyId", "PropertyManagerId", "UnitNo", "UnitType" },
                values: new object[] { "a58f8e21-d759-4059-93a0-cf43f5af7fc0", "b5692ce6-a5cc-4d7d-88f4-658fbe8354df", null, 300, 1 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "d9abff3c-7ffc-4b30-8f57-e2a4b1e027e5", "34062685-7e28-468d-90e9-03de2e3413fe" },
                    { "d9abff3c-7ffc-4b30-8f57-e2a4b1e027e5", "98bf7476-e8cc-48be-b280-d40832d2448b" }
                });

            migrationBuilder.InsertData(
                table: "Unit",
                columns: new[] { "UnitId", "PropertyId", "PropertyManagerId", "UnitNo", "UnitType" },
                values: new object[,]
                {
                    { "286eed22-4721-4b46-a161-c16cae4eac1b", "b5692ce6-a5cc-4d7d-88f4-658fbe8354df", "34062685-7e28-468d-90e9-03de2e3413fe", 200, 1 },
                    { "477444b5-884c-4949-9dba-7430052527dd", "27452fed-f2c3-4b27-b7f8-89b61c867456", "98bf7476-e8cc-48be-b280-d40832d2448b", 100, 0 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Discriminator", "Email", "EmailConfirmed", "FirstName", "IsIdVerified", "LastName", "LeaseNumber", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UnitId", "UserName", "UsernameChangeLimit" },
                values: new object[] { "28d5cf81-1bb2-4873-a186-a7d12810b3a8", 0, "464e9266-fcbf-4159-a39d-139db64d44a4", new DateTime(2007, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Tenant", "tenant@gmail.com", true, "Anais", false, "Turcotte", null, false, null, "TENANT@GMAIL.COM", "TENANT@GMAIL.COM", "AQAAAAEAACcQAAAAEB6bbwXjk3+oqOXyI1xNL9sQprEEXRj+PGRBV33b9iI2zl2+QM+Gbm/AoIzbq1/ixA==", null, true, null, "3570a494-5dda-4554-ad98-eb452342cfad", false, "477444b5-884c-4949-9dba-7430052527dd", "tenant@gmail.com", 10 });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Discriminator", "Email", "EmailConfirmed", "FirstName", "IsIdVerified", "LastName", "LeaseNumber", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UnitId", "UserName", "UsernameChangeLimit" },
                values: new object[] { "b4133710-7944-4e32-8bda-0307ece77b3f", 0, "53717d0b-c47b-4154-82ef-33803b9f6e6a", new DateTime(1960, 11, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Tenant", "tenant2@gmail.com", true, "Roslyn", false, "Pollich", null, false, null, "TENANT2@GMAIL.COM", "TENANT2@GMAIL.COM", "AQAAAAEAACcQAAAAEE2aICkPegB0ACmq9ijgJ1xmlXCaTr75QNIv2QRuDqsY30ok5Gxmd47zZ27J8NCyLQ==", null, true, null, "de2b428a-c719-41bd-96f3-8af3af7eaa07", false, "286eed22-4721-4b46-a161-c16cae4eac1b", "tenant2@gmail.com", 10 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1ada4d5a-7168-4c61-87a1-ec49c58288c7", "28d5cf81-1bb2-4873-a186-a7d12810b3a8" },
                    { "1ada4d5a-7168-4c61-87a1-ec49c58288c7", "b4133710-7944-4e32-8bda-0307ece77b3f" }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "ClosedDate", "CreatedById", "CreatedOn", "Description", "EstimatedDate", "Problem", "Severity", "Status", "UnitId" },
                values: new object[,]
                {
                    { 1, null, "28d5cf81-1bb2-4873-a186-a7d12810b3a8", new DateTime(2022, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Massive Leak from the Kitchen pipe", new DateTime(2022, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Massive Leakage", 2, 0, "477444b5-884c-4949-9dba-7430052527dd" },
                    { 2, null, "28d5cf81-1bb2-4873-a186-a7d12810b3a8", new DateTime(2021, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Need repairing the floors from last earthquake", new DateTime(2022, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Earthquake repair", 1, 0, "477444b5-884c-4949-9dba-7430052527dd" },
                    { 3, new DateTime(2000, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "b4133710-7944-4e32-8bda-0307ece77b3f", new DateTime(1999, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Need to fix the roof that was damaged by the tornado", new DateTime(2000, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tornado damage", 2, 1, "286eed22-4721-4b46-a161-c16cae4eac1b" },
                    { 4, null, "b4133710-7944-4e32-8bda-0307ece77b3f", new DateTime(2019, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Currently getting by with rat traps", new DateTime(2022, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rat infestation", 1, 0, "286eed22-4721-4b46-a161-c16cae4eac1b" }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "MessageId", "Content", "CreatedDate", "SenderId", "TicketId", "UserId" },
                values: new object[] { "5948469b-3436-4657-a02c-d837353f93fd", "Porro aut adipisci eius aut minima consectetur.", new DateTime(2022, 11, 19, 16, 48, 51, 383, DateTimeKind.Local).AddTicks(9950), "28d5cf81-1bb2-4873-a186-a7d12810b3a8", 1, null });

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

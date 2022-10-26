using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chickadee.Migrations
{
    public partial class seedingUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "483fff16-0884-4c48-8475-787729c468aa", "38c7ae53-1ecf-43a8-b500-595353f7d357" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "83c8947b-8282-488a-a194-2559724a2d81", "38c7ae53-1ecf-43a8-b500-595353f7d357" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "98380b88-f97b-4de7-94aa-c86f003cfcc4", "38c7ae53-1ecf-43a8-b500-595353f7d357" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e0785cb1-5dcd-4248-9430-de123b674f87", "38c7ae53-1ecf-43a8-b500-595353f7d357" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "483fff16-0884-4c48-8475-787729c468aa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83c8947b-8282-488a-a194-2559724a2d81");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98380b88-f97b-4de7-94aa-c86f003cfcc4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e0785cb1-5dcd-4248-9430-de123b674f87");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "38c7ae53-1ecf-43a8-b500-595353f7d357");

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyManagerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_Properties_AspNetUsers_PropertyManagerId",
                        column: x => x.PropertyManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    UnitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitNo = table.Column<int>(type: "int", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.UnitId);
                    table.ForeignKey(
                        name: "FK_Units_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Problem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Severity = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "322df380-6af1-4aaa-a1c0-cb5d59195373", "830c44fa-ee44-4dda-8a16-0d618f853fa8", "PropertyManager", "PROPERTYMANAGER" },
                    { "9bcae295-e06e-4137-b302-e9caed02280e", "96f5501d-b49f-42ae-949e-78d7bc1e8c0b", "Admin", "ADMIN" },
                    { "da1d50ab-8df8-4c21-a2df-eec2647f8100", "84e19c77-8b23-4073-949b-8213a1f62d45", "SuperAdmin", "SUPERADMIN" },
                    { "efc90636-a2dc-478d-9501-f9797dbcbf8a", "1aacc8c1-84b6-4875-bdb3-559ca559c035", "Tenant", "TENANT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UnitId", "UserName", "UsernameChangeLimit" },
                values: new object[,]
                {
                    { "0cc25564-5133-4769-a2b9-b8bd37996d3d", 0, "9ab5f754-f3ac-45c6-a724-fb27c919dcdb", "propertymanager@gmail.com", true, "Property", "Manager", false, null, "PROPERTYMANAGER@GMAIL.COM", "PROPERTYMANAGER@GMAIL.COM", "AQAAAAEAACcQAAAAEJCmrJ3Bdwib2e3xLiiGMQU1kYzBzHg/tC618TPAcMp1/CwsvWDDwbzmL+ntX8Z1qw==", null, true, null, "79205821-a649-4b33-8b78-820a940e743b", false, null, "propertymanager@gmail.com", 10 },
                    { "215f0df8-b94a-49f0-9fae-c22c74614ecb", 0, "a988c259-4d65-4225-98c8-1e065870f8d2", "superadmin@chickadeeinvest.ca", true, "Matt", "Hardwick", false, null, "SUPERADMIN@CHICKADEEINVEST.CA", "SUPERADMIN@CHICKADEEINVEST.CA", "AQAAAAEAACcQAAAAEGl8d2wU+zkwfHAKT3V6Zmi4TUOd8kzkH32Aq+GhGItxmzuAN7mk3jWgno6J9bF+rQ==", null, true, null, "cfed882f-d0ce-436e-95b5-4edebdaa510d", false, null, "superadmin@chickadeeinvest.ca", 10 },
                    { "7e972781-25af-46ad-b5d6-051e3e857d3d", 0, "7ad1a89b-7b91-4379-bafa-3f8769ffcffa", "propertymanager2@gmail.com", true, "Manager", "Property", false, null, "PROPERTYMANAGER2@GMAIL.COM", "PROPERTYMANAGER2@GMAIL.COM", "AQAAAAEAACcQAAAAEEB6UsuyGJ3DCUuQEkKHxVQIQD06j0gjC5t/7D+dkM/fOkYCY744tFCLa08356ju4A==", null, true, null, "cee92c9d-5c0f-43b7-acca-32e657c1f8e6", false, null, "propertymanager2@gmail.com", 10 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "322df380-6af1-4aaa-a1c0-cb5d59195373", "0cc25564-5133-4769-a2b9-b8bd37996d3d" },
                    { "322df380-6af1-4aaa-a1c0-cb5d59195373", "215f0df8-b94a-49f0-9fae-c22c74614ecb" },
                    { "9bcae295-e06e-4137-b302-e9caed02280e", "215f0df8-b94a-49f0-9fae-c22c74614ecb" },
                    { "da1d50ab-8df8-4c21-a2df-eec2647f8100", "215f0df8-b94a-49f0-9fae-c22c74614ecb" },
                    { "efc90636-a2dc-478d-9501-f9797dbcbf8a", "215f0df8-b94a-49f0-9fae-c22c74614ecb" },
                    { "322df380-6af1-4aaa-a1c0-cb5d59195373", "7e972781-25af-46ad-b5d6-051e3e857d3d" }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "PropertyId", "Address", "PropertyManagerId" },
                values: new object[,]
                {
                    { 1, "742 Evergreen Terrace", "0cc25564-5133-4769-a2b9-b8bd37996d3d" },
                    { 2, "The Montana", "7e972781-25af-46ad-b5d6-051e3e857d3d" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "UnitId", "PropertyId", "UnitNo" },
                values: new object[] { 1, 1, 101 });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "UnitId", "PropertyId", "UnitNo" },
                values: new object[] { 2, 2, 500 });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UnitId", "UserName", "UsernameChangeLimit" },
                values: new object[,]
                {
                    { "646750e8-a6f2-4a9c-b75f-53e1686cf5b7", 0, "284573b3-3c0c-445d-9ed4-ecad8750ae9a", "tenant2@gmail.com", true, "User", "Tenant", false, null, "TENANT2@GMAIL.COM", "TENANT2@GMAIL.COM", "AQAAAAEAACcQAAAAEDMWKqB4we+z7Q/xU+hD2DDfZ5LA9sEg7Soz0vcgn0iA2LeHpLAHUaHzJGN3nl3nZw==", null, true, null, "8578139b-3dc6-4622-9724-f7846e0fb3a1", false, 2, "tenant2@gmail.com", 10 },
                    { "72904ebc-cb0c-4b97-a5e7-98313e9a59f5", 0, "49298b3b-4342-4894-b4a1-af1e95b7d011", "tenant@gmail.com", true, "Tenant", "User", false, null, "TENANT@GMAIL.COM", "TENANT@GMAIL.COM", "AQAAAAEAACcQAAAAELfG0j6HhYwxZ/cIqLDZ08PCs11Mw0kWUgoizTxHDgk6093hiT5HqTilExgZ8QVmZQ==", null, true, null, "e8789163-157f-422f-a3cb-5aa23edd279d", false, 1, "tenant@gmail.com", 10 }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "CreatedOn", "Description", "EstimatedDate", "Problem", "Severity", "Status", "UnitId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Massive Leak from the Kitchen pipe", new DateTime(2022, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Massive Leakage", 2, 0, 1 },
                    { 2, new DateTime(2021, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Need repairing the floors from last earthquake", new DateTime(2022, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Earthquake repair", 1, 0, 1 },
                    { 3, new DateTime(1999, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Need to fix the roof that was damaged by the tornado", new DateTime(2000, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tornado damage", 2, 1, 2 },
                    { 4, new DateTime(2019, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Currently getting by with rat traps", new DateTime(2022, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rat infestation", 1, 0, 2 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "efc90636-a2dc-478d-9501-f9797dbcbf8a", "646750e8-a6f2-4a9c-b75f-53e1686cf5b7" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "efc90636-a2dc-478d-9501-f9797dbcbf8a", "72904ebc-cb0c-4b97-a5e7-98313e9a59f5" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UnitId",
                table: "AspNetUsers",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyManagerId",
                table: "Properties",
                column: "PropertyManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UnitId",
                table: "Tickets",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_PropertyId",
                table: "Units",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Units_UnitId",
                table: "AspNetUsers",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "UnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Units_UnitId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UnitId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "322df380-6af1-4aaa-a1c0-cb5d59195373", "0cc25564-5133-4769-a2b9-b8bd37996d3d" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "322df380-6af1-4aaa-a1c0-cb5d59195373", "215f0df8-b94a-49f0-9fae-c22c74614ecb" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9bcae295-e06e-4137-b302-e9caed02280e", "215f0df8-b94a-49f0-9fae-c22c74614ecb" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "da1d50ab-8df8-4c21-a2df-eec2647f8100", "215f0df8-b94a-49f0-9fae-c22c74614ecb" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "efc90636-a2dc-478d-9501-f9797dbcbf8a", "215f0df8-b94a-49f0-9fae-c22c74614ecb" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "efc90636-a2dc-478d-9501-f9797dbcbf8a", "646750e8-a6f2-4a9c-b75f-53e1686cf5b7" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "efc90636-a2dc-478d-9501-f9797dbcbf8a", "72904ebc-cb0c-4b97-a5e7-98313e9a59f5" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "322df380-6af1-4aaa-a1c0-cb5d59195373", "7e972781-25af-46ad-b5d6-051e3e857d3d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "322df380-6af1-4aaa-a1c0-cb5d59195373");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9bcae295-e06e-4137-b302-e9caed02280e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da1d50ab-8df8-4c21-a2df-eec2647f8100");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "efc90636-a2dc-478d-9501-f9797dbcbf8a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "215f0df8-b94a-49f0-9fae-c22c74614ecb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "646750e8-a6f2-4a9c-b75f-53e1686cf5b7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "72904ebc-cb0c-4b97-a5e7-98313e9a59f5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0cc25564-5133-4769-a2b9-b8bd37996d3d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7e972781-25af-46ad-b5d6-051e3e857d3d");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "483fff16-0884-4c48-8475-787729c468aa", "42bb2071-f7c0-4b27-a01f-4765b63a6e4e", "Tenant", "TENANT" },
                    { "83c8947b-8282-488a-a194-2559724a2d81", "48ff58a1-ad30-4c55-8e33-8db18c6c93fe", "Admin", "ADMIN" },
                    { "98380b88-f97b-4de7-94aa-c86f003cfcc4", "d81f85ad-99f6-4f6e-8dfe-aa6dfc24d0fb", "PropertyManager", "PROPERTYMANAGER" },
                    { "e0785cb1-5dcd-4248-9430-de123b674f87", "ab001740-a5f0-4b71-b0e3-d154757118b6", "SuperAdmin", "SUPERADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName", "UsernameChangeLimit" },
                values: new object[] { "38c7ae53-1ecf-43a8-b500-595353f7d357", 0, "f78c0b83-292f-4c4e-816f-98c17bd17b01", "superadmin@chickadeeinvest.ca", true, "Matt", "Hardwick", false, null, "SUPERADMIN@CHICKADEEINVEST.CA", "SUPERADMIN@CHICKADEEINVEST.CA", "AQAAAAEAACcQAAAAELQ1Zp2rDXZJdwqlFCoqLYt6ACTuwL0z1eInyABGdfHpZzL7qaQt4Xe9DnTBowdfyg==", null, true, null, "53f8835f-7278-47d4-ab98-347bb44bb3bc", false, "superadmin@chickadeeinvest.ca", 10 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "483fff16-0884-4c48-8475-787729c468aa", "38c7ae53-1ecf-43a8-b500-595353f7d357" },
                    { "83c8947b-8282-488a-a194-2559724a2d81", "38c7ae53-1ecf-43a8-b500-595353f7d357" },
                    { "98380b88-f97b-4de7-94aa-c86f003cfcc4", "38c7ae53-1ecf-43a8-b500-595353f7d357" },
                    { "e0785cb1-5dcd-4248-9430-de123b674f87", "38c7ae53-1ecf-43a8-b500-595353f7d357" }
                });
        }
    }
}

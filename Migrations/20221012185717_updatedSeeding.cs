using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chickadee.Migrations
{
    public partial class updatedSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "033cad03-6574-4cda-9a9a-7c20a38a2fc6", "b15287d3-ebfd-4ce6-8c67-59117ae17a6a" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6be51102-0055-4670-85de-7ce1509c756a", "b15287d3-ebfd-4ce6-8c67-59117ae17a6a" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6f09ad28-c19a-44c7-a479-6b77a25a075d", "b15287d3-ebfd-4ce6-8c67-59117ae17a6a" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d3c16b81-4a71-4a04-ae7c-852ad839904a", "b15287d3-ebfd-4ce6-8c67-59117ae17a6a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "033cad03-6574-4cda-9a9a-7c20a38a2fc6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6be51102-0055-4670-85de-7ce1509c756a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f09ad28-c19a-44c7-a479-6b77a25a075d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3c16b81-4a71-4a04-ae7c-852ad839904a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b15287d3-ebfd-4ce6-8c67-59117ae17a6a");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "033cad03-6574-4cda-9a9a-7c20a38a2fc6", "45a78b8b-7353-4655-b77a-01ce03ba2bb1", "Admin", "ADMIN" },
                    { "6be51102-0055-4670-85de-7ce1509c756a", "01eb91b8-52cc-43dd-b2cc-2c6798acc2c0", "PropertyManager", "PROPERTYMANAGER" },
                    { "6f09ad28-c19a-44c7-a479-6b77a25a075d", "c7580e1c-2ec6-4ddd-8769-f05c70c59345", "SuperAdmin", "SUPERADMIN" },
                    { "d3c16b81-4a71-4a04-ae7c-852ad839904a", "69bb48a9-d9dd-4467-a7b1-94b835592e60", "Tenant", "TENANT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName", "UsernameChangeLimit" },
                values: new object[] { "b15287d3-ebfd-4ce6-8c67-59117ae17a6a", 0, "57f56d2c-3c61-4c51-a0ea-89bf4f95accf", "superadmin@chickadeeinvest.ca", true, "Matt", "Hardwick", false, null, "SUPERADMIN@CHICKADEEINVEST.CA", "SUPERADMIN@CHICKADEEINVEST.CA", "AQAAAAEAACcQAAAAEKoJ1p3FILnMuIonM9pCwAnVo8B0B3bKQCQ0mKZoTZ9LuJCyVKb4OvhO71GxYmU29Q==", null, true, null, "cf30db27-1bd5-420d-8590-6e847079677a", false, "superadmin@chickadeeinvest.ca", 10 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "033cad03-6574-4cda-9a9a-7c20a38a2fc6", "b15287d3-ebfd-4ce6-8c67-59117ae17a6a" },
                    { "6be51102-0055-4670-85de-7ce1509c756a", "b15287d3-ebfd-4ce6-8c67-59117ae17a6a" },
                    { "6f09ad28-c19a-44c7-a479-6b77a25a075d", "b15287d3-ebfd-4ce6-8c67-59117ae17a6a" },
                    { "d3c16b81-4a71-4a04-ae7c-852ad839904a", "b15287d3-ebfd-4ce6-8c67-59117ae17a6a" }
                });
        }
    }
}

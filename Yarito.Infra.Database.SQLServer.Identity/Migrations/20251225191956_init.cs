using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Yarito.Infra.Database.SQLServer.Identity.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "identity");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "identity",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "identity",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "identity",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "identity",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "identity",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
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
                        principalSchema: "identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "A1B2C3D4-E5F6-4789-A1B2-C3D4E5F6A7B8", "Admin", "ADMIN" },
                    { 2, "B2C3D4E5-F6A7-4890-B2C3-D4E5F6A7B8C9", "Customer", "CUSTOMER" },
                    { 3, "C3D4E5F6-A7B8-4901-C3D4-E5F6A7B8C9D0", "Expert", "EXPERT" }
                });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "A1B2C3D4-E5F6-G7H8-I9J0-K1L2M3N4O5P6", "ali.mohammadi@example.com", true, false, null, "ALI.MOHAMMADI@EXAMPLE.COM", "09121234567", "AQAAAAIAAYagAAAAEFykpVjaFHjZ8J31r75jpNHxIPYzT5xUFsNkvFVphNRlouUSWoVKkRepCMH6kBnUYQ==", "09121234567", true, "1A2B3C4D5E6F7G8H9I0J", false, "09121234567" },
                    { 2, 0, "B2C3D4E5-F6G7-H8I9-J0K1-L2M3N4O5P6Q7", "zahra.ahmadi@example.com", true, false, null, "ZAHRA.AHMADI@EXAMPLE.COM", "09131234567", "AQAAAAIAAYagAAAAECHn95ugRhAA+p/6hRkU3s29v833ekn4ttZCcg1XkIyPKpCzSxrutOUMWgyiFZmMgg==", "09131234567", true, "2B3C4D5E6F7G8H9I0J1K", false, "09131234567" },
                    { 3, 0, "C3D4E5F6-G7H8-I9J0-K1L2-M3N4O5P6Q7R8", "mohammad.rezaei@example.com", true, false, null, "MOHAMMAD.REZAEI@EXAMPLE.COM", "09141234567", "AQAAAAIAAYagAAAAEHzdgSPC0+20Rp40UvoytLYyv7gDPWPDrSusd5KjT9UUqdBa5DvFcXOQqto6E8hw/g==", "09141234567", true, "3C4D5E6F7G8H9I0J1K2L", false, "09141234567" },
                    { 4, 0, "D4E5F6G7-H8I9-J0K1-L2M3-N4O5P6Q7R8S9", "fatemeh.hosseini@example.com", true, false, null, "FATEMEH.HOSSEINI@EXAMPLE.COM", "09151234567", "AQAAAAIAAYagAAAAEGec1bA7r+AACoAZia1vkmeUbuKynHWtZcVuPWHEORmGlhN6M9ELuqKP1niTm5IC6Q==", "09151234567", true, "4D5E6F7G8H9I0J1K2L3M", false, "09151234567" },
                    { 5, 0, "E5F6G7H8-I9J0-K1L2-M3N4-O5P6Q7R8S9T0", "hossein.karimi@example.com", true, false, null, "HOSSEIN.KARIMI@EXAMPLE.COM", "09161234567", "AQAAAAIAAYagAAAAECOlvPkFmUnlxKp1kWj6kpPgJHQTRAVP7ArIG9RkzssNr/uVwLLOpbSTpGcDGebj/g==", "09161234567", true, "5E6F7G8H9I0J1K2L3M4N", false, "09161234567" },
                    { 6, 0, "F6G7H8I9-J0K1-L2M3-N4O5-P6Q7R8S9T0U1", "reza.bargkar@example.com", true, false, null, "REZA.BARGKAR@EXAMPLE.COM", "09171234567", "AQAAAAIAAYagAAAAELJk7HtwVNdMeT9O9jHzz3e0jGQNi+0iUa4nfqCKeizyUZYmA7BzxNy+dhCa62Omcg==", "09171234567", true, "6F7G8H9I0J1K2L3M4N5O", false, "09171234567" },
                    { 7, 0, "G7H8I9J0-K1L2-M3N4-O5P6-Q7R8S9T0U1V2", "mehdi.loolehkesh@example.com", true, false, null, "MEHDI.LOOLEHKESH@EXAMPLE.COM", "09181234567", "AQAAAAIAAYagAAAAEDQzc1mXRInkOvgntx1U+1T8iaJjBHFmkIIuz2Krl0f/r+dYq6sY954GUOFtzAGFEw==", "09181234567", true, "7G8H9I0J1K2L3M4N5O6P", false, "09181234567" },
                    { 8, 0, "H8I9J0K1-L2M3-N4O5-P6Q7-R8S9T0U1V2W3", "sara.nezafatchi@example.com", true, false, null, "SARA.NEZAFATCHI@EXAMPLE.COM", "09191234567", "AQAAAAIAAYagAAAAEBCPFOTPEAP8pAE3vVgQ93k0vI8Eibet2OiKbaL0lu/WQVDUCpJBs6DpjcDrD92enQ==", "09191234567", true, "8H9I0J1K2L3M4N5O6P7Q", false, "09191234567" },
                    { 9, 0, "I9J0K1L2-M3N4-O5P6-Q7R8-S9T0U1V2W3X4", "ahmad.naghash@example.com", true, false, null, "AHMAD.NAGHASH@EXAMPLE.COM", "09201234567", "AQAAAAIAAYagAAAAELB8tardsGM2Ls6Q5eVCfyNZO91bm/bsrrRbQVJ2/tv6LO3V20J/Xy14MaAjzt2s3Q==", "09201234567", true, "9I0J1K2L3M4N5O6P7Q8R", false, "09201234567" },
                    { 10, 0, "J0K1L2M3-N4O5-P6Q7-R8S9-T0U1V2W3X4Y5", "narges.tamirkar@example.com", true, false, null, "NARGES.TAMIRKAR@EXAMPLE.COM", "09211234567", "AQAAAAIAAYagAAAAEOmNtXiTdtxroAlLLf/rJa9+WMWnjoCRTODVO2lRQLLLWSEEO4GbpRI0Pf2UkgoAgw==", "09211234567", true, "0J1K2L3M4N5O6P7Q8R9S", false, "09211234567" },
                    { 11, 0, "K1L2M3N4-O5P6-Q7R8-S9T0-U1V2W3X4Y5Z6", "admin@yarito.com", true, false, null, "ADMIN@YARITO.COM", "ADMIN", "AQAAAAIAAYagAAAAEOGPXUtwv/pg8KuqNxjVMN9ro9QFtSf7ugA0dJ4WA4QgpFU7HCFnWpW6pX1zAC5tkQ==", "09991234567", true, "1K2L3M4N5O6P7Q8R9S0T", false, "admin" }
                });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 3, 6 },
                    { 3, 7 },
                    { 3, 8 },
                    { 3, 9 },
                    { 3, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "identity",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "identity",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "identity",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "identity",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "identity",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "identity",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "identity",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "identity");
        }
    }
}

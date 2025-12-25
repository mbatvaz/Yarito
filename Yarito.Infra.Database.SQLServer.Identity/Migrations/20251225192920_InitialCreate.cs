using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yarito.Infra.Database.SQLServer.Identity.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEGnOQvwOcxEf8JN1dyn0DcnzOzGCHTej4fE1KTLW0iht3rh0Ic12qEiR3vSOR5X6iA==");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEL0SVuaghfh5hsSVQI/T5k14bnaWpD+9E8b3HZU6NVNehFIrlT6HFIz4YTanejzIDA==");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAECDijNSiz2w+vqyQgRvbv1EnmMU5nsnLXE/TINpKadS37imVYsw7zIgIbUdjBYUXmg==");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEGfNyghhaEYd/XZPYR4V5khU5stRk2LLmqBLosBvnXBMaNFOHiJX9yZBkaA7kgdSaw==");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEJnUfzORLHnWiZ275waK8BfN6PQc9yHmV4DaMoSyj71we3HGg4fLlum4pSJ+2P6hGw==");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEONrjpAbgA6cYOdlRyrSE4QbihOpWoYcTBdL5dcdveG5Ve13xtON20agoVZzh4IKsg==");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 7,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEEl7NXT3vAZoJKbL5Kcon5mEGpTT1D4gHiQ5xkdOHCC2D4CjSfgb7HP5FTfvblV7aQ==");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 8,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEHDewutS4HMeCo5VWBTfWhHE8YtHwdUxTFZpVbxPQqlpnnV13V9PsUCLh5ND58qygw==");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEKTu/sQhqXfPI8zqrj64y6Kd6qF+AfGFUvzsU0xiDLyVZsqg6+qzeKxuoT4GBKUg3A==");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 10,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEIO/f+DZhUb3O35lY6n10pxWHuGm0wSQIfxKmTmtVdceBBWK9HfRt0azRZ/dRwG9Zg==");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 11,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEHhN/j3HYyaZzYYPHenA6ah4LqHavRvRSgoDkoroEeUwmQXnuJjPG+tOdsXVS5IclQ==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFykpVjaFHjZ8J31r75jpNHxIPYzT5xUFsNkvFVphNRlouUSWoVKkRepCMH6kBnUYQ==");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAECHn95ugRhAA+p/6hRkU3s29v833ekn4ttZCcg1XkIyPKpCzSxrutOUMWgyiFZmMgg==");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEHzdgSPC0+20Rp40UvoytLYyv7gDPWPDrSusd5KjT9UUqdBa5DvFcXOQqto6E8hw/g==");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEGec1bA7r+AACoAZia1vkmeUbuKynHWtZcVuPWHEORmGlhN6M9ELuqKP1niTm5IC6Q==");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAECOlvPkFmUnlxKp1kWj6kpPgJHQTRAVP7ArIG9RkzssNr/uVwLLOpbSTpGcDGebj/g==");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELJk7HtwVNdMeT9O9jHzz3e0jGQNi+0iUa4nfqCKeizyUZYmA7BzxNy+dhCa62Omcg==");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 7,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDQzc1mXRInkOvgntx1U+1T8iaJjBHFmkIIuz2Krl0f/r+dYq6sY954GUOFtzAGFEw==");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 8,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEBCPFOTPEAP8pAE3vVgQ93k0vI8Eibet2OiKbaL0lu/WQVDUCpJBs6DpjcDrD92enQ==");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELB8tardsGM2Ls6Q5eVCfyNZO91bm/bsrrRbQVJ2/tv6LO3V20J/Xy14MaAjzt2s3Q==");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 10,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOmNtXiTdtxroAlLLf/rJa9+WMWnjoCRTODVO2lRQLLLWSEEO4GbpRI0Pf2UkgoAgw==");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 11,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOGPXUtwv/pg8KuqNxjVMN9ro9QFtSf7ugA0dJ4WA4QgpFU7HCFnWpW6pX1zAC5tkQ==");
        }
    }
}

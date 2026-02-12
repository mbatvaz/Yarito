using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yarito.Infra.Database.SQLServer.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class RefactorExpertWorkJoinEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpertWorks_AppUsers_ExpertsId",
                table: "ExpertWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpertWorks_Works_WorksId",
                table: "ExpertWorks");

            migrationBuilder.RenameColumn(
                name: "WorksId",
                table: "ExpertWorks",
                newName: "WorkId");

            migrationBuilder.RenameColumn(
                name: "ExpertsId",
                table: "ExpertWorks",
                newName: "ExpertId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpertWorks_WorksId",
                table: "ExpertWorks",
                newName: "IX_ExpertWorks_WorkId");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileImgPath",
                table: "AppUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImgPath",
                value: "/images/request/1.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImgPath",
                value: "/images/request/2.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImgPath",
                value: "/images/request/1.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImgPath",
                value: "/images/request/1.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImgPath",
                value: "/images/request/2.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImgPath",
                value: "/images/request/3.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImgPath",
                value: "/images/request/1.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImgPath",
                value: "/images/request/2.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImgPath",
                value: "/images/request/1.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImgPath",
                value: "/images/request/1.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImgPath",
                value: "/images/request/2.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImgPath",
                value: "/images/request/1.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImgPath",
                value: "/images/request/1.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 14,
                column: "ImgPath",
                value: "/images/request/1.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 15,
                column: "ImgPath",
                value: "/images/request/2.jpg");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertWorks_AppUsers_ExpertId",
                table: "ExpertWorks",
                column: "ExpertId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertWorks_Works_WorkId",
                table: "ExpertWorks",
                column: "WorkId",
                principalTable: "Works",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpertWorks_AppUsers_ExpertId",
                table: "ExpertWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpertWorks_Works_WorkId",
                table: "ExpertWorks");

            migrationBuilder.RenameColumn(
                name: "WorkId",
                table: "ExpertWorks",
                newName: "WorksId");

            migrationBuilder.RenameColumn(
                name: "ExpertId",
                table: "ExpertWorks",
                newName: "ExpertsId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpertWorks_WorkId",
                table: "ExpertWorks",
                newName: "IX_ExpertWorks_WorksId");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileImgPath",
                table: "AppUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImgPath",
                value: "/images/requests/req1_img1_a7f3d8e2.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImgPath",
                value: "/images/requests/req1_img2_b4e9c1f6.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImgPath",
                value: "/images/requests/req2_img1_c2d8a5b9.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImgPath",
                value: "/images/requests/req3_img1_d9f6e3c7.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImgPath",
                value: "/images/requests/req3_img2_e5a8b4d1.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImgPath",
                value: "/images/requests/req3_img3_f1c9d7e2.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImgPath",
                value: "/images/requests/req4_img1_g8b5f2a6.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImgPath",
                value: "/images/requests/req4_img2_h3d7c9e4.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImgPath",
                value: "/images/requests/req5_img1_i6e2a8b5.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImgPath",
                value: "/images/requests/req6_img1_j9f4c3d7.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImgPath",
                value: "/images/requests/req6_img2_k2a5e8b1.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImgPath",
                value: "/images/requests/req7_img1_l7c9d2f6.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImgPath",
                value: "/images/requests/req8_img1_m4e6a3b8.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 14,
                column: "ImgPath",
                value: "/images/requests/req10_img1_n8f5d7c2.jpg");

            migrationBuilder.UpdateData(
                table: "RequestImages",
                keyColumn: "Id",
                keyValue: 15,
                column: "ImgPath",
                value: "/images/requests/req10_img2_o1b9e4a6.jpg");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertWorks_AppUsers_ExpertsId",
                table: "ExpertWorks",
                column: "ExpertsId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertWorks_Works_WorksId",
                table: "ExpertWorks",
                column: "WorksId",
                principalTable: "Works",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

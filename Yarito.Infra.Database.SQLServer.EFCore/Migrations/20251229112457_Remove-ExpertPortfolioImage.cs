using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Yarito.Infra.Database.SQLServer.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class RemoveExpertPortfolioImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpertPortfolioImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpertPortfolioImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpertId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImgPath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertPortfolioImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpertPortfolioImages_AppUsers_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ExpertPortfolioImages",
                columns: new[] { "Id", "CreatedAt", "ExpertId", "ImgPath", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 26, 0, 0, 0, 0, DateTimeKind.Utc), 6, "/images/portfolio/expert1_p1_a3f8d2e7.jpg", false, "سیم‌کشی ساختمان مسکونی" },
                    { 2, new DateTime(2025, 11, 10, 0, 0, 0, 0, DateTimeKind.Utc), 6, "/images/portfolio/expert1_p2_b9c5e4f1.jpg", false, "نصب تابلو برق صنعتی" },
                    { 3, new DateTime(2025, 11, 25, 0, 0, 0, 0, DateTimeKind.Utc), 6, "/images/portfolio/expert1_p3_c6d2a7e9.jpg", false, "نصب لوستر کریستالی" },
                    { 4, new DateTime(2025, 10, 31, 0, 0, 0, 0, DateTimeKind.Utc), 7, "/images/portfolio/expert2_p1_d8f3c5b2.jpg", false, "لوله‌کشی آشپزخانه مدرن" },
                    { 5, new DateTime(2025, 11, 15, 0, 0, 0, 0, DateTimeKind.Utc), 7, "/images/portfolio/expert2_p2_e4a9d6f8.jpg", false, "نصب شیرآلات لوکس" },
                    { 6, new DateTime(2025, 11, 30, 0, 0, 0, 0, DateTimeKind.Utc), 7, "/images/portfolio/expert2_p3_f7c2e8d5.jpg", false, "تعمیر سیستم فاضلاب" },
                    { 7, new DateTime(2025, 11, 5, 0, 0, 0, 0, DateTimeKind.Utc), 8, "/images/portfolio/expert3_p1_g2d5a8f3.jpg", false, "نظافت ویلای لوکس" },
                    { 8, new DateTime(2025, 11, 20, 0, 0, 0, 0, DateTimeKind.Utc), 8, "/images/portfolio/expert3_p2_h9e6b4c7.jpg", false, "شستشوی نما و پنجره‌های برج" },
                    { 9, new DateTime(2025, 12, 5, 0, 0, 0, 0, DateTimeKind.Utc), 8, "/images/portfolio/expert3_p3_i5f8c2d9.jpg", false, "قالیشویی فرش دستباف" },
                    { 10, new DateTime(2025, 10, 21, 0, 0, 0, 0, DateTimeKind.Utc), 9, "/images/portfolio/expert4_p1_j8c3d7e4.jpg", false, "نقاشی آپارتمان 150 متری" },
                    { 11, new DateTime(2025, 11, 7, 0, 0, 0, 0, DateTimeKind.Utc), 9, "/images/portfolio/expert4_p2_k4e9f5a2.jpg", false, "رنگ‌آمیزی نمای ساختمان" },
                    { 12, new DateTime(2025, 11, 23, 0, 0, 0, 0, DateTimeKind.Utc), 9, "/images/portfolio/expert4_p3_l7a2c6d8.jpg", false, "کاغذ دیواری اتاق کودک" },
                    { 13, new DateTime(2025, 11, 3, 0, 0, 0, 0, DateTimeKind.Utc), 10, "/images/portfolio/expert5_p1_m3d8e5f9.jpg", false, "تعمیر یخچال فریزر دوقلو" },
                    { 14, new DateTime(2025, 11, 17, 0, 0, 0, 0, DateTimeKind.Utc), 10, "/images/portfolio/expert5_p2_n6f4a7c2.jpg", false, "سرویس کولر گازی اسپلیت" },
                    { 15, new DateTime(2025, 12, 3, 0, 0, 0, 0, DateTimeKind.Utc), 10, "/images/portfolio/expert5_p3_o9c5d8e6.jpg", false, "تعمیر ماشین لباسشویی اتوماتیک" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpertPortfolioImages_ExpertId",
                table: "ExpertPortfolioImages",
                column: "ExpertId");
        }
    }
}

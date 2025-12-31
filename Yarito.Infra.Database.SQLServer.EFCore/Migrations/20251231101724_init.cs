using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Yarito.Infra.Database.SQLServer.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Cities_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Works_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nchar(11)", fixedLength: true, maxLength: 11, nullable: false),
                    WalletBalance = table.Column<decimal>(type: "decimal(18,0)", nullable: false, defaultValue: 0m),
                    ProfileImgPath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUsers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpertWorks",
                columns: table => new
                {
                    ExpertsId = table.Column<int>(type: "int", nullable: false),
                    WorksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertWorks", x => new { x.ExpertsId, x.WorksId });
                    table.ForeignKey(
                        name: "FK_ExpertWorks_AppUsers_ExpertsId",
                        column: x => x.ExpertsId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertWorks_Works_WorksId",
                        column: x => x.WorksId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ProposedPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    ProposedVisitDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    ExpertId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bids_AppUsers_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ProposedPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PreferredVisitDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    WorkId = table.Column<int>(type: "int", nullable: false),
                    AcceptedBidId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_AppUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_Bids_AcceptedBidId",
                        column: x => x.AcceptedBidId,
                        principalTable: "Bids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImgPath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestImages_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ReviewStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ExpertId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AppUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_AppUsers_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), "نصب، تعمیر و نگهداری تاسیسات برقی ساختمان", false, "خدمات برق" },
                    { 2, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), "نصب، تعمیر و رفع نشتی لوله‌های آب و فاضلاب", false, "خدمات لوله‌کشی" },
                    { 3, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), "نظافت منزل، راه پله، پنجره و فرش", false, "خدمات نظافت" },
                    { 4, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), "رنگ‌آمیزی داخلی و خارجی ساختمان", false, "خدمات نقاشی" },
                    { 5, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), "تعمیر و سرویس انواع لوازم خانگی", false, "تعمیر لوازم خانگی" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "Name", "ParentId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "تهران", null },
                    { 2, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "اصفهان", null },
                    { 3, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "فارس", null },
                    { 4, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "تهران", 1 },
                    { 5, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "کرج", 1 },
                    { 6, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "ری", 1 },
                    { 7, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "اصفهان", 2 },
                    { 8, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "نجف‌آباد", 2 },
                    { 9, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "کاشان", 2 },
                    { 10, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "شیراز", 3 },
                    { 11, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "مرودشت", 3 },
                    { 12, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "جهرم", 3 }
                });

            migrationBuilder.InsertData(
                table: "Works",
                columns: new[] { "Id", "BasePrice", "CategoryId", "CreatedAt", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { 1, 2000000m, 1, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "سیم‌کشی ساختمان" },
                    { 2, 500000m, 1, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "نصب لوستر و چراغ" },
                    { 3, 300000m, 1, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "تعمیر کلید و پریز برق" },
                    { 4, 800000m, 2, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "تعمیر و رفع نشتی لوله" },
                    { 5, 400000m, 2, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "نصب شیر آلات" },
                    { 6, 600000m, 2, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "تعمیر توالت فرنگی" },
                    { 7, 1500000m, 3, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "نظافت منزل" },
                    { 8, 700000m, 3, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "شستشوی پنجره" },
                    { 9, 2500000m, 3, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "قالیشویی" },
                    { 10, 3000000m, 4, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "نقاشی داخلی ساختمان" },
                    { 11, 5000000m, 4, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "نقاشی نمای ساختمان" },
                    { 12, 2000000m, 4, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "کاغذ دیواری" },
                    { 13, 1000000m, 5, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "تعمیر ماشین لباسشویی" },
                    { 14, 1200000m, 5, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "تعمیر یخچال" },
                    { 15, 1500000m, 5, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "تعمیر کولر گازی" }
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "Address", "CityId", "CreatedAt", "Email", "FirstName", "IsDeleted", "LastName", "PhoneNumber", "ProfileImgPath", "UserType", "WalletBalance" },
                values: new object[,]
                {
                    { 1, "تهران، خیابان ولیعصر، پلاک 123", 4, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), "ali.mohammadi@example.com", "علی", false, "محمدی", "09121234567", "/images/profiles/customer1.jpg", 0, 5000000m },
                    { 2, "اصفهان، خیابان چهارباغ، پلاک 45", 7, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), "zahra.ahmadi@example.com", "زهرا", false, "احمدی", "09131234567", "/images/profiles/customer2.jpg", 0, 3000000m },
                    { 3, "شیراز، خیابان زند، پلاک 78", 10, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), "mohammad.rezaei@example.com", "محمد", false, "رضایی", "09141234567", "/images/profiles/customer3.jpg", 0, 8000000m },
                    { 4, "کرج، میدان آزادگان، پلاک 56", 5, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), "fatemeh.hosseini@example.com", "فاطمه", false, "حسینی", "09151234567", "/images/profiles/customer4.jpg", 0, 2000000m },
                    { 5, "کاشان، خیابان کمال الملک، پلاک 90", 9, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), "hossein.karimi@example.com", "حسین", false, "کریمی", "09161234567", "/images/profiles/customer5.jpg", 0, 10000000m }
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "CityId", "CreatedAt", "Email", "FirstName", "IsDeleted", "LastName", "PhoneNumber", "ProfileImgPath", "UserType", "WalletBalance" },
                values: new object[,]
                {
                    { 6, 4, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), "reza.bargkar@example.com", "رضا", false, "برقکار", "09171234567", "/images/profiles/expert1.jpg", 1, 15000000m },
                    { 7, 7, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), "mehdi.loolehkesh@example.com", "مهدی", false, "لوله‌کش", "09181234567", "/images/profiles/expert2.jpg", 1, 12000000m },
                    { 8, 10, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), "sara.nezafatchi@example.com", "سارا", false, "نظافتچی", "09191234567", "/images/profiles/expert3.jpg", 1, 8000000m },
                    { 9, 5, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), "ahmad.naghash@example.com", "احمد", false, "نقاش", "09201234567", "/images/profiles/expert4.jpg", 1, 20000000m },
                    { 10, 4, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), "narges.tamirkar@example.com", "نرگس", false, "تعمیرکار", "09211234567", "/images/profiles/expert5.jpg", 1, 18000000m }
                });

            migrationBuilder.InsertData(
                table: "ExpertWorks",
                columns: new[] { "ExpertsId", "WorksId" },
                values: new object[,]
                {
                    { 6, 1 },
                    { 6, 2 },
                    { 6, 3 },
                    { 7, 4 },
                    { 7, 5 },
                    { 7, 6 },
                    { 8, 7 },
                    { 8, 8 },
                    { 8, 9 },
                    { 9, 10 },
                    { 9, 11 },
                    { 9, 12 },
                    { 10, 2 },
                    { 10, 13 },
                    { 10, 14 },
                    { 10, 15 }
                });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "AcceptedBidId", "Address", "CreatedAt", "CustomerId", "Description", "IsDeleted", "PreferredVisitDateTime", "ProposedPrice", "Status", "Title", "WorkId" },
                values: new object[,]
                {
                    { 1, null, "تهران، خیابان ولیعصر، پلاک 123، واحد 5", new DateTime(2025, 11, 30, 0, 0, 0, 0, DateTimeKind.Utc), 1, "نیاز به سیم‌کشی کامل آشپزخانه برای نصب هود و کابینت", false, new DateTime(2025, 12, 5, 0, 0, 0, 0, DateTimeKind.Utc), 2500000m, 2, "سیم‌کشی آشپزخانه", 1 },
                    { 2, null, "اصفهان، خیابان چهارباغ، پلاک 45، طبقه سوم", new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Utc), 2, "شیر آب آشپزخانه نشتی دارد و نیاز به تعویض دارد", false, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), 500000m, 2, "تعمیر شیر آب آشپزخانه", 5 },
                    { 3, null, "شیراز، خیابان زند، پلاک 78، واحد 12", new DateTime(2025, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), 3, "نظافت کامل منزل 120 متری قبل از نوروز", false, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 1800000m, 2, "نظافت کامل منزل", 7 },
                    { 4, null, "کرج، میدان آزادگان، پلاک 56، واحد 8", new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc), 4, "نقاشی و رنگ‌آمیزی دو اتاق خواب", false, new DateTime(2025, 12, 28, 0, 0, 0, 0, DateTimeKind.Utc), 3500000m, 1, "رنگ اتاق خواب", 10 },
                    { 5, null, "کاشان، خیابان کمال الملک، پلاک 90، طبقه دوم", new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Utc), 5, "یخچال یخ نمی‌زند و نیاز به بررسی دارد", false, new DateTime(2025, 12, 30, 0, 0, 0, 0, DateTimeKind.Utc), 1500000m, 1, "تعمیر یخچال سامسونگ", 14 }
                });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "AcceptedBidId", "Address", "CreatedAt", "CustomerId", "Description", "IsDeleted", "PreferredVisitDateTime", "ProposedPrice", "Title", "WorkId" },
                values: new object[,]
                {
                    { 6, null, "تهران، خیابان انقلاب، پلاک 200، واحد 3", new DateTime(2025, 12, 24, 0, 0, 0, 0, DateTimeKind.Utc), 1, "نصب یک لوستر سنگین در سالن پذیرایی", false, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 600000m, "نصب لوستر سالن", 2 },
                    { 7, null, "اصفهان، خیابان سپاهان، پلاک 150، واحد 6", new DateTime(2025, 12, 24, 12, 0, 0, 0, DateTimeKind.Utc), 2, "قالیشویی یک فرش دستباف 12 متری", false, new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), 3000000m, "شستشوی فرش ایرانی", 9 },
                    { 8, null, "شیراز، خیابان مطهری، پلاک 85، طبقه اول", new DateTime(2025, 12, 24, 16, 0, 0, 0, DateTimeKind.Utc), 3, "توالت فرنگی به درستی آب نمی‌کشد", false, new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Utc), 700000m, "تعمیر توالت فرنگی", 6 }
                });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "AcceptedBidId", "Address", "CreatedAt", "CustomerId", "Description", "IsDeleted", "PreferredVisitDateTime", "ProposedPrice", "Status", "Title", "WorkId" },
                values: new object[,]
                {
                    { 9, null, "کرج، خیابان فردوسی، پلاک 320", new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "رنگ‌آمیزی نمای یک ساختمان سه طبقه", false, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc), 8000000m, 3, "نقاشی نمای ساختمان", 11 },
                    { 10, null, "تهران، خیابان آزادی، پلاک 450، واحد 15", new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), 5, "ماشین لباسشویی در حین کار خاموش می‌شود", false, new DateTime(2025, 12, 17, 0, 0, 0, 0, DateTimeKind.Utc), 1200000m, 2, "تعمیر ماشین لباسشویی", 13 }
                });

            migrationBuilder.InsertData(
                table: "Bids",
                columns: new[] { "Id", "CreatedAt", "Description", "ExpertId", "IsDeleted", "ProposedPrice", "ProposedVisitDateTime", "RequestId", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "سلام، تجربه 10 ساله در سیم‌کشی دارم. کار رو با کیفیت عالی انجام میدم.", 6, false, 2300000m, new DateTime(2025, 12, 5, 0, 0, 0, 0, DateTimeKind.Utc), 1, 1 },
                    { 2, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "آماده انجام کار هستم با قیمت مناسب", 10, false, 2600000m, new DateTime(2025, 12, 6, 0, 0, 0, 0, DateTimeKind.Utc), 1, 2 },
                    { 3, new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Utc), "متخصص لوله کشی با 8 سال سابقه، همراه با گارانتی", 7, false, 480000m, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), 2, 1 },
                    { 4, new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), "با ضمانت کیفیت و قیمت عالی", 8, false, 1750000m, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, 1 },
                    { 5, new DateTime(2025, 12, 21, 0, 0, 0, 0, DateTimeKind.Utc), "نقاش حرفه‌ای با سابقه کار روی پروژه‌های بزرگ", 9, false, 3800000m, new DateTime(2025, 12, 29, 0, 0, 0, 0, DateTimeKind.Utc), 4, 2 },
                    { 6, new DateTime(2025, 12, 23, 0, 0, 0, 0, DateTimeKind.Utc), "تعمیر فوری یخچال با بهترین قیمت", 10, false, 1400000m, new DateTime(2025, 12, 30, 0, 0, 0, 0, DateTimeKind.Utc), 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "Bids",
                columns: new[] { "Id", "CreatedAt", "Description", "ExpertId", "IsDeleted", "ProposedPrice", "ProposedVisitDateTime", "RequestId" },
                values: new object[,]
                {
                    { 7, new DateTime(2025, 12, 24, 4, 0, 0, 0, DateTimeKind.Utc), "نصب لوستر با تجربه بالا", 6, false, 550000m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 8, new DateTime(2025, 12, 24, 6, 0, 0, 0, DateTimeKind.Utc), "آماده نصب در کمترین زمان", 10, false, 650000m, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 9, new DateTime(2025, 12, 24, 14, 0, 0, 0, DateTimeKind.Utc), "قالیشویی با دستگاه‌های مدرن", 8, false, 2800000m, new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 10, new DateTime(2025, 12, 24, 18, 0, 0, 0, DateTimeKind.Utc), "تعمیر فوری توالت فرنگی", 7, false, 650000m, new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Utc), 8 }
                });

            migrationBuilder.InsertData(
                table: "Bids",
                columns: new[] { "Id", "CreatedAt", "Description", "ExpertId", "IsDeleted", "ProposedPrice", "ProposedVisitDateTime", "RequestId", "Status" },
                values: new object[] { 11, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "تعمیر ماشین لباسشویی با گارانتی کامل", 10, false, 1150000m, new DateTime(2025, 12, 17, 0, 0, 0, 0, DateTimeKind.Utc), 10, 1 });

            migrationBuilder.InsertData(
                table: "RequestImages",
                columns: new[] { "Id", "CreatedAt", "ImgPath", "IsDeleted", "RequestId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 30, 0, 0, 0, 0, DateTimeKind.Utc), "/images/requests/req1_img1_a7f3d8e2.jpg", false, 1 },
                    { 2, new DateTime(2025, 11, 30, 0, 0, 0, 0, DateTimeKind.Utc), "/images/requests/req1_img2_b4e9c1f6.jpg", false, 1 },
                    { 3, new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Utc), "/images/requests/req2_img1_c2d8a5b9.jpg", false, 2 },
                    { 4, new DateTime(2025, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), "/images/requests/req3_img1_d9f6e3c7.jpg", false, 3 },
                    { 5, new DateTime(2025, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), "/images/requests/req3_img2_e5a8b4d1.jpg", false, 3 },
                    { 6, new DateTime(2025, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), "/images/requests/req3_img3_f1c9d7e2.jpg", false, 3 },
                    { 7, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc), "/images/requests/req4_img1_g8b5f2a6.jpg", false, 4 },
                    { 8, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc), "/images/requests/req4_img2_h3d7c9e4.jpg", false, 4 },
                    { 9, new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Utc), "/images/requests/req5_img1_i6e2a8b5.jpg", false, 5 },
                    { 10, new DateTime(2025, 12, 24, 0, 0, 0, 0, DateTimeKind.Utc), "/images/requests/req6_img1_j9f4c3d7.jpg", false, 6 },
                    { 11, new DateTime(2025, 12, 24, 0, 0, 0, 0, DateTimeKind.Utc), "/images/requests/req6_img2_k2a5e8b1.jpg", false, 6 },
                    { 12, new DateTime(2025, 12, 24, 12, 0, 0, 0, DateTimeKind.Utc), "/images/requests/req7_img1_l7c9d2f6.jpg", false, 7 },
                    { 13, new DateTime(2025, 12, 24, 16, 0, 0, 0, DateTimeKind.Utc), "/images/requests/req8_img1_m4e6a3b8.jpg", false, 8 },
                    { 14, new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), "/images/requests/req10_img1_n8f5d7c2.jpg", false, 10 },
                    { 15, new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), "/images/requests/req10_img2_o1b9e4a6.jpg", false, 10 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "CreatedAt", "CustomerId", "ExpertId", "IsDeleted", "Rating", "RequestId", "ReviewStatus" },
                values: new object[,]
                {
                    { 1, "کار بسیار عالی و حرفه‌ای انجام شد. آقای برقکار بسیار دقیق و وقت‌شناس بودند. پیشنهاد می‌کنم.", new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Utc), 1, 6, false, 5, 1, 1 },
                    { 2, "کار خوبی انجام شد. فقط کمی دیرتر از موعد مقرر آمدند.", new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), 2, 7, false, 4, 2, 1 },
                    { 3, "نظافت فوق‌العاده دقیق و تمیز. خانم نظافتچی بسیار محترم و مودب بودند.", new DateTime(2025, 12, 17, 0, 0, 0, 0, DateTimeKind.Utc), 3, 8, false, 5, 3, 1 },
                    { 4, "خانم تعمیرکار بسیار متخصص و حرفه‌ای بودند. مشکل به سرعت حل شد.", new DateTime(2025, 12, 19, 0, 0, 0, 0, DateTimeKind.Utc), 5, 10, false, 5, 10, 1 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "CreatedAt", "CustomerId", "ExpertId", "IsDeleted", "Rating", "RequestId" },
                values: new object[] { 5, "کار خوب بود ولی قیمت کمی بالا بود نسبت به بازار.", new DateTime(2025, 12, 24, 0, 0, 0, 0, DateTimeKind.Utc), 4, 9, false, 3, 4 });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "CreatedAt", "CustomerId", "ExpertId", "IsDeleted", "Rating", "RequestId", "ReviewStatus" },
                values: new object[,]
                {
                    { 6, "متاسفانه کار طبق توافق انجام نشد. از کیفیت راضی نیستم.", new DateTime(2025, 12, 24, 14, 0, 0, 0, DateTimeKind.Utc), 5, 10, false, 2, 5, 2 },
                    { 7, "به طور کلی راضی هستم. کار خوبی انجام شد و قیمت مناسب بود.", new DateTime(2025, 12, 24, 19, 0, 0, 0, DateTimeKind.Utc), 1, 6, false, 4, 6, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_CityId",
                table: "AppUsers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_PhoneNumber",
                table: "AppUsers",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bids_ExpertId",
                table: "Bids",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_RequestId_ExpertId",
                table: "Bids",
                columns: new[] { "RequestId", "ExpertId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ParentId",
                table: "Cities",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertWorks_WorksId",
                table: "ExpertWorks",
                column: "WorksId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestImages_RequestId",
                table: "RequestImages",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_AcceptedBidId",
                table: "Requests",
                column: "AcceptedBidId",
                unique: true,
                filter: "[AcceptedBidId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CustomerId",
                table: "Requests",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_WorkId",
                table: "Requests",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CustomerId",
                table: "Reviews",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ExpertId",
                table: "Reviews",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RequestId",
                table: "Reviews",
                column: "RequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Works_CategoryId",
                table: "Works",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Requests_RequestId",
                table: "Bids",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Cities_CityId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_AppUsers_ExpertId",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AppUsers_CustomerId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Requests_RequestId",
                table: "Bids");

            migrationBuilder.DropTable(
                name: "ExpertWorks");

            migrationBuilder.DropTable(
                name: "RequestImages");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "Works");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}

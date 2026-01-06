# Identity Seed Data

## 🔐 Roles (نقش‌ها)

| Role ID | Role Name | Description |
|---------|-----------|-------------|
| 1 | Admin | مدیر سیستم |
| 2 | Customer | مشتری |
| 3 | Expert | متخصص |

---

## 👥 Users (کاربران)

### Customers (مشتریان)

| ID | Username | Email | Password | Role |
|----|----------|-------|----------|------|
| 1 | 09121234567 | ali.mohammadi@example.com | `Customer@123` | Customer |
| 2 | 09131234567 | zahra.ahmadi@example.com | `Customer@123` | Customer |
| 3 | 09141234567 | mohammad.rezaei@example.com | `Customer@123` | Customer |
| 4 | 09151234567 | fatemeh.hosseini@example.com | `Customer@123` | Customer |
| 5 | 09161234567 | hossein.karimi@example.com | `Customer@123` | Customer |

### Experts (متخصصان)

| ID | Username | Email | Password | Role |
|----|----------|-------|----------|------|
| 6 | 09171234567 | reza.bargkar@example.com | `Expert@123` | Expert |
| 7 | 09181234567 | mehdi.loolehkesh@example.com | `Expert@123` | Expert |
| 8 | 09191234567 | sara.nezafatchi@example.com | `Expert@123` | Expert |
| 9 | 09201234567 | ahmad.naghash@example.com | `Expert@123` | Expert |
| 10 | 09211234567 | narges.tamirkar@example.com | `Expert@123` | Expert |

---

## 📝 نکات مهم

1. **همگام‌سازی ID ها**: ID های کاربران Identity با ID های `AppUser` در `Yarito.Infra.Database.SQLServer.EFCore` یکسان است.

2. **نام کاربری**: شماره تلفن به عنوان نام کاربری استفاده شده است.

3. **پسوردها**:
   - مشتریان: `Customer@123`
   - متخصصان: `Expert@123`

4. **تأیید ایمیل و شماره تلفن**: همه کاربران به صورت پیش‌فرض تأیید شده‌اند (`EmailConfirmed = true`, `PhoneNumberConfirmed = true`).

5. **امنیت**: این پسوردها فقط برای محیط Development و Testing هستند. در محیط Production حتماً باید تغییر کنند.

6. **⚠️ SecurityStamp و ConcurrencyStamp**: از مقادیر ثابت استفاده شده تا در هر Migration مقادیر یکسان باشند.

---

## 🔄 Migration

برای ساخت و اعمال Migration:

### 1. ساخت Migration برای Identity

```bash
dotnet ef migrations add InitialIdentityWithSeedData --project Yarito.Infra.Database.SQLServer.Identity --startup-project Yarito.Endpoint.WebApp.MVC
```

### 2. اعمال Migration

```bash
dotnet ef database update --project Yarito.Infra.Database.SQLServer.Identity --startup-project Yarito.Endpoint.WebApp.MVC
```

### 3. (اختیاری) ساخت Migration برای EFCore

```bash
dotnet ef migrations add InitialEFCoreWithSeedData --project Yarito.Infra.Database.SQLServer.EFCore --startup-project Yarito.Endpoint.WebApp.MVC
```

### 4. اعمال Migration EFCore

```bash
dotnet ef database update --project Yarito.Infra.Database.SQLServer.EFCore --startup-project Yarito.Endpoint.WebApp.MVC
```

---

## ✅ بررسی Migration

پس از اعمال Migration، می‌توانید در دیتابیس بررسی کنید:

```sql
-- بررسی Roles
SELECT * FROM [identity].[AspNetRoles]

-- بررسی Users
SELECT * FROM [identity].[AspNetUsers]

-- بررسی UserRoles
SELECT * FROM [identity].[AspNetUserRoles]
```

---

## 🔍 نکات Migration

- ✅ **SecurityStamp و ConcurrencyStamp**: از مقادیر ثابت استفاده شده برای جلوگیری از ایجاد Migration های غیرضروری
- ✅ **PasswordHash**: هر بار که `PasswordHasher` اجرا می‌شود، Hash جدید تولید می‌کند، اما این مشکلی ایجاد نمی‌کند چون Hash همیشه متفاوت است
- ✅ **Schema**: تمام جداول Identity در schema با نام `identity` ایجاد می‌شوند

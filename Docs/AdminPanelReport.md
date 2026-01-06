# 📊 گزارش کامل پنل مدیریت یاریتو

**تاریخ تهیه:** خرداد ۱۴۰۴  
**نسخه:** 1.0  
**پروژه:** Yarito - سامانه درخواست خدمات آنلاین  
**فریمورک:** ASP.NET Core MVC (.NET 10)

---

## 📑 فهرست مطالب

1. [معرفی پروژه](#معرفی-پروژه)
2. [معماری پروژه](#معماری-پروژه)
3. [ساختار پنل ادمین](#ساختار-پنل-ادمین)
4. [کنترلرها](#کنترلرها)
5. [ویومدل‌ها](#ویومدلها)
6. [نگاشت‌ها (Mappings)](#نگاشتها-mappings)
7. [ویوها](#ویوها)
8. [روتینگ‌ها](#روتینگها)
9. [امنیت](#امنیت)
10. [نتیجه‌گیری](#نتیجهگیری)

---

## 🎯 معرفی پروژه

**یاریتو** یک پلتفرم درخواست خدمات آنلاین است که مشتریان را به متخصصان مختلف متصل می‌کند. این سامانه شامل سه بخش اصلی است:

- **پنل مشتری:** ثبت درخواست خدمات و انتخاب پیشنهاد متخصصان
- **پنل متخصص:** مشاهده درخواست‌ها و ارسال پیشنهاد قیمت
- **پنل مدیریت:** مدیریت کاربران، دسته‌بندی‌ها، درخواست‌ها و نظرات

---

## 🏗️ معماری پروژه

پروژه بر اساس معماری **Onion Architecture** طراحی شده است:

```
┌─────────────────────────────────────────────────────────────┐
│                    Endpoint Layer                           │
│              (Yarito.Endpoint.WebApp.MVC)                   │
├─────────────────────────────────────────────────────────────┤
│                   Application Layer                         │
│              (Yarito.Domain.AppServices)                    │
├─────────────────────────────────────────────────────────────┤
│                    Domain Layer                             │
│     (Yarito.Domain.Core + Yarito.Domain.Services)           │
├─────────────────────────────────────────────────────────────┤
│                Infrastructure Layer                         │
│  (Yarito.Infra.DataAccess.EFCore + Storage + Database)      │
└─────────────────────────────────────────────────────────────┘
```

### لایه‌های پروژه:

| لایه | پروژه | توضیحات |
|------|-------|---------|
| Domain Core | `Yarito.Domain.Core` | موجودیت‌ها، اینترفیس‌ها، DTOها و Enumها |
| Domain Services | `Yarito.Domain.Services` | پیاده‌سازی سرویس‌های دامنه |
| Application | `Yarito.Domain.AppServices` | سرویس‌های کاربردی و منطق تجاری |
| Infrastructure | `Yarito.Infra.DataAccess.EFCore` | پیاده‌سازی Repository با EF Core |
| Infrastructure | `Yarito.Infra.DataAccess.Storage` | مدیریت فایل‌ها |
| Infrastructure | `Yarito.Infra.Database.SQLServer.*` | DbContext و کانفیگ‌های دیتابیس |
| Endpoint | `Yarito.Endpoint.WebApp.MVC` | کنترلرها، ویوها و ViewModelها |
| Framework | `Yarito.Framework` | ابزارهای مشترک و Extension Methods |

---

## 📁 ساختار پنل ادمین

پنل ادمین در مسیر `Areas/Admin` قرار دارد:

```
Areas/Admin/
├── Controllers/
│   ├── DashboardController.cs
│   ├── UsersController.cs
│   ├── CategoriesController.cs
│   ├── WorksController.cs
│   ├── RequestsController.cs
│   └── ReviewsController.cs
├── Models/
│   ├── DashboardViewModel.cs
│   ├── UsersViewModel.cs
│   ├── CustomerDetailsViewModel.cs
│   ├── ExpertDetailsViewModel.cs
│   ├── UserFormViewModel.cs
│   ├── UserDetailsViewModel.cs
│   ├── CategoriesViewModel.cs
│   ├── CategoryFormViewModel.cs
│   ├── WorkFormViewModel.cs
│   ├── RequestsViewModel.cs
│   ├── RequestDetailsViewModel.cs
│   ├── RequestDetailsPartialViewModel.cs
│   ├── BidDetailsViewModel.cs
│   ├── ReviewsViewModel.cs
│   └── PaginationViewModel.cs
└── Views/
    ├── Dashboard/
    │   └── Index.cshtml
    ├── Users/
    │   ├── Index.cshtml
    │   ├── CustomerDetails.cshtml
    │   ├── ExpertDetails.cshtml
    │   └── UserForm.cshtml
    ├── Categories/
    │   ├── Index.cshtml
    │   └── CategoryForm.cshtml
    ├── Works/
    │   └── WorkForm.cshtml
    ├── Requests/
    │   ├── Index.cshtml
    │   ├── RequestDetails.cshtml
    │   └── BidDetails.cshtml
    ├── Reviews/
    │   └── Index.cshtml
    └── Shared/
        ├── _Layout.cshtml
        ├── _Pagination.cshtml
        ├── _UserDetailsPartial.cshtml
        └── _RequestDetailsPartial.cshtml
```

---

## 🎮 کنترلرها

### 1. DashboardController

**مسیر:** `/Admin/Dashboard`

**اکشن‌ها:**
| اکشن | HTTP Method | توضیحات |
|------|-------------|---------|
| `Index` | GET | نمایش داشبورد با آمار، درخواست‌های اخیر و نظرات در انتظار |

**وابستگی‌ها:**
- `IAppUserAppServices` - دریافت آمار کاربران
- `IReviewsAppServices` - دریافت نظرات در انتظار تایید
- `IRequestAppServices` - دریافت درخواست‌های اخیر

**ویژگی‌های داشبورد:**
- نمایش تعداد مشتریان
- نمایش تعداد متخصصین
- نمایش تعداد درخواست‌ها
- نمایش تعداد پیشنهادات
- لیست ۵ درخواست اخیر
- لیست ۶ نظر در انتظار تایید

---

### 2. UsersController

**مسیر:** `/Admin/Users`

**اکشن‌ها:**
| اکشن | HTTP Method | توضیحات |
|------|-------------|---------|
| `Index` | GET | لیست کاربران با فیلتر و صفحه‌بندی |
| `CustomerDetails` | GET | جزئیات مشتری و لیست درخواست‌هایش |
| `ExpertDetails` | GET | جزئیات متخصص، خدمات و پیشنهاداتش |
| `UserForm` | GET | فرم افزودن کاربر جدید |
| `UserForm` | POST | ثبت کاربر جدید |
| `Delete` | POST | حذف نرم کاربر |

**وابستگی‌ها:**
- `IAppUserAppServices` - مدیریت کاربران
- `IAuthenticationAppServices` - ثبت‌نام کاربر
- `IRequestAppServices` - درخواست‌های مشتری
- `IBidAppServices` - پیشنهادات متخصص
- `ICityAppServices` - لیست شهرها
- `IMapper` - نگاشت AutoMapper

**فیلترهای لیست کاربران:**
- جستجو بر اساس نام و شماره تلفن
- فیلتر بر اساس نوع کاربر (مشتری/متخصص)
- صفحه‌بندی

---

### 3. CategoriesController

**مسیر:** `/Admin/Categories`

**اکشن‌ها:**
| اکشن | HTTP Method | توضیحات |
|------|-------------|---------|
| `Index` | GET | لیست دسته‌بندی‌ها با سرویس‌های زیرمجموعه |
| `Create` | GET | فرم افزودن دسته‌بندی |
| `Create` | POST | ثبت دسته‌بندی جدید |
| `Edit` | GET | فرم ویرایش دسته‌بندی |
| `Edit` | POST | ذخیره تغییرات دسته‌بندی |
| `Delete` | POST | حذف دسته‌بندی |

**وابستگی‌ها:**
- `ICategoryAppServices` - مدیریت دسته‌بندی‌ها
- `IMapper` - نگاشت AutoMapper

**ویژگی‌ها:**
- نمایش سرویس‌های هر دسته‌بندی به صورت آکاردئون
- امکان ویرایش و حذف سرویس‌ها از همین صفحه

---

### 4. WorksController

**مسیر:** `/Admin/Works`

**اکشن‌ها:**
| اکشن | HTTP Method | توضیحات |
|------|-------------|---------|
| `Create` | GET | فرم افزودن سرویس |
| `Create` | POST | ثبت سرویس جدید |
| `Edit` | GET | فرم ویرایش سرویس |
| `Edit` | POST | ذخیره تغییرات سرویس |
| `Delete` | POST | حذف سرویس |

**وابستگی‌ها:**
- `ICategoryAppServices` - دریافت لیست دسته‌بندی‌ها
- `IWorkAppServices` - مدیریت سرویس‌ها
- `IMapper` - نگاشت AutoMapper

---

### 5. RequestsController

**مسیر:** `/Admin/Requests`

**اکشن‌ها:**
| اکشن | HTTP Method | توضیحات |
|------|-------------|---------|
| `Index` | GET | لیست درخواست‌ها با فیلتر |
| `RequestDetails` | GET | جزئیات درخواست با لیست پیشنهادات |
| `ChangeStatus` | POST | تغییر وضعیت درخواست |
| `RejectBid` | POST | رد کردن پیشنهاد |
| `BidDetails` | GET | جزئیات کامل پیشنهاد |

**وابستگی‌ها:**
- `IRequestAppServices` - مدیریت درخواست‌ها
- `IBidAppServices` - مدیریت پیشنهادات
- `ICityAppServices` - لیست شهرها برای فیلتر

**فیلترهای لیست درخواست‌ها:**
- جستجو بر اساس عنوان و توضیحات
- فیلتر بر اساس وضعیت
- فیلتر بر اساس شهر
- صفحه‌بندی

**وضعیت‌های درخواست:**
| وضعیت | توضیحات |
|-------|---------|
| `Pending` | در حال دریافت پیشنهاد |
| `InProgress` | در انتظار انجام |
| `Completed` | تکمیل شده |
| `Cancelled` | لغو شده |

---

### 6. ReviewsController

**مسیر:** `/Admin/Reviews`

**اکشن‌ها:**
| اکشن | HTTP Method | توضیحات |
|------|-------------|---------|
| `Index` | GET | لیست نظرات با فیلتر |
| `Approve` | POST | تایید نظر |
| `Reject` | POST | رد نظر |

**وابستگی‌ها:**
- `IReviewsAppServices` - مدیریت نظرات

**فیلترهای لیست نظرات:**
- جستجو در متن نظر
- فیلتر بر اساس وضعیت تایید
- صفحه‌بندی

---

## 📦 ویومدل‌ها

### DashboardViewModel
```csharp
public class DashboardViewModel
{
    public AppStatisticsDto AppStatistics { get; set; }
    public IReadOnlyList<ReviewFullDto> CommentsAwaitingApproval { get; set; }
    public IReadOnlyList<RequestsSummaryDto> RequestsSummary { get; set; }
}
```

### UsersViewModel
```csharp
public class UsersViewModel
{
    public IReadOnlyList<AppUserSummaryDto> UserList { get; set; }
    public string? Search { get; set; }
    public UserTypeEnum? UserType { get; set; }
    public int Page { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
```

### CustomerDetailsViewModel
```csharp
public class CustomerDetailsViewModel
{
    public AppUserFullDto UserDetails { get; set; }
    public IReadOnlyList<RequestsSummaryDto> Requests { get; set; }
    public int CustomerId { get; set; }
    public string? Search { get; set; }
    public int Page { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
```

### ExpertDetailsViewModel
```csharp
public class ExpertDetailsViewModel
{
    public AppUserFullDto UserDetails { get; set; }
    public IReadOnlyList<CategoryFullDto> ExpertWorks { get; set; }
    public IReadOnlyList<BidSummaryDto> Bids { get; set; }
    public int ExpertId { get; set; }
    public string? Search { get; set; }
    public int Page { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
```

### UserFormViewModel
```csharp
public class UserFormViewModel
{
    public IReadOnlyList<CityFullDto?> CityList { get; set; }
    
    [Required] public string? FirstName { get; set; }
    [Required] public string? LastName { get; set; }
    [Required] public string? PhoneNumber { get; set; }
    [Required] public string? Password { get; set; }
    [Required] public UserTypeEnum? UserType { get; set; }
    public string? Email { get; init; }
    public decimal BaseWalletBalance { get; init; }
    public int? CityId { get; init; }
    public string? Address { get; init; }
    public IFormFile? ProfileImage { get; init; }
}
```

### CategoriesViewModel
```csharp
public class CategoriesViewModel
{
    public IReadOnlyList<CategoryFullDto> Categories { get; set; }
    public string? Search { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
```

### CategoryFormViewModel
```csharp
public class CategoryFormViewModel
{
    public int? Id { get; init; }
    [Required] public string Title { get; init; }
    public string? Description { get; init; }
    public bool IsEditMode => Id.HasValue;
}
```

### WorkFormViewModel
```csharp
public class WorkFormViewModel
{
    public int? Id { get; set; }
    [Required] public string Title { get; init; }
    [Required] public int CategoryId { get; init; }
    [Required] public decimal BasePrice { get; init; }
    public IReadOnlyList<CategoryDto> Categories { get; set; }
    public bool IsEditMode => Id.HasValue;
}
```

### RequestsViewModel
```csharp
public class RequestsViewModel
{
    public IReadOnlyList<RequestCardDto> Requests { get; set; }
    public IReadOnlyList<CityFullDto> Cities { get; set; }
    public string? Search { get; set; }
    public RequestStatusEnum? Status { get; set; }
    public int? CityId { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
```

### RequestDetailsViewModel
```csharp
public class RequestDetailsViewModel
{
    public required AppUserFullDto CustomerInfo { get; init; }
    public required RequestFullDto RequestInfo { get; init; }
    public required IReadOnlyList<string> RequestImagesPath { get; init; }
    public required IReadOnlyList<BidSummaryDto> BidList { get; init; }
    public BidSummaryDto? AcceptedBid { get; init; }
    public string? Search { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
```

### BidDetailsViewModel
```csharp
public class BidDetailsViewModel
{
    public required AppUserFullDto Customer { get; init; }
    public required AppUserFullDto Expert { get; init; }
    public required RequestFullDto Request { get; init; }
    public required BidFullDto Bid { get; init; }
}
```

### ReviewsViewModel
```csharp
public class ReviewsViewModel
{
    public IReadOnlyList<ReviewFullDto> Reviews { get; set; }
    public ReviewStatusEnum? Status { get; set; }
    public string? Search { get; set; }
    public int Page { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
```

### PaginationViewModel
```csharp
public class PaginationViewModel
{
    public int Page { get; set; }
    public int TotalPages { get; set; }
    public string? Area { get; set; }
    public string Controller { get; set; }
    public string Action { get; set; }
    public Dictionary<string, string?> RouteValues { get; set; }
}
```

---

## 🔄 نگاشت‌ها (Mappings)

### CategoryMapProfile
```csharp
public class CategoryMapProfile : Profile
{
    public CategoryMapProfile()
    {
        // لیست دسته‌بندی‌ها
        CreateMap<PagedResult<CategoryFullDto>, CategoriesViewModel>()
            .ForMember(d => d.Categories, opt => opt.MapFrom(s => s.Items))
            .ForMember(d => d.TotalPages, opt => opt.MapFrom(s => 
                (int)Math.Ceiling((double)s.TotalCount / s.PageSize)));

        // فرم دسته‌بندی
        CreateMap<CategoryFormViewModel, CategoryDto>();
        CreateMap<CategoryDto, CategoryFormViewModel>();
    }
}
```

### UserMapProfile
```csharp
public class UserMapProfile : Profile
{
    public UserMapProfile()
    {
        // لیست کاربران
        CreateMap<PagedResult<AppUserSummaryDto>, UsersViewModel>()
            .ForMember(d => d.UserList, opt => opt.MapFrom(s => s.Items))
            .ForMember(d => d.TotalPages, opt => opt.MapFrom(s => 
                (int)Math.Ceiling((double)s.TotalCount / s.PageSize)));

        // جزئیات مشتری
        CreateMap<PagedResult<RequestsSummaryDto>, CustomerDetailsViewModel>()
            .ForMember(d => d.Requests, opt => opt.MapFrom(s => s.Items))
            .ForMember(d => d.TotalPages, opt => opt.MapFrom(s => 
                (int)Math.Ceiling((double)s.TotalCount / s.PageSize)));

        // جزئیات متخصص
        CreateMap<PagedResult<BidSummaryDto>, ExpertDetailsViewModel>()
            .ForMember(d => d.Bids, opt => opt.MapFrom(s => s.Items))
            .ForMember(d => d.TotalPages, opt => opt.MapFrom(s => 
                (int)Math.Ceiling((double)s.TotalCount / s.PageSize)));

        // فرم کاربر
        CreateMap<UserFormViewModel, RegisterDto>()
            .ForMember(d => d.ProfileImage, opt => opt.MapFrom(s => 
                s.ProfileImage != null ? s.ProfileImage.OpenReadStream() : null))
            .ForMember(d => d.ProfileImageUrl, opt => opt.MapFrom(s => 
                s.ProfileImage != null ? Path.GetExtension(s.ProfileImage.FileName) : null));
    }
}
```

### WorkMapProfile
```csharp
public class WorkMapProfile : Profile
{
    public WorkMapProfile()
    {
        CreateMap<WorkFormViewModel, WorkDto>();
        CreateMap<WorkDto, WorkFormViewModel>();
    }
}
```

---

## 🖼️ ویوها

### Dashboard/Index.cshtml
صفحه اصلی داشبورد شامل:
- **گرید KPI:** نمایش ۴ کارت آماری (مشتریان، متخصصین، درخواست‌ها، پیشنهادات)
- **جدول درخواست‌های اخیر:** ۵ درخواست آخر با لینک مشاهده
- **کارت‌های نظرات:** نظرات در انتظار تایید با دکمه‌های تایید/رد

### Users/Index.cshtml
لیست کاربران شامل:
- **فیلتر:** جستجو و فیلتر بر اساس نقش
- **جدول:** شناسه، تصویر، نام، شماره تلفن، نقش
- **عملیات:** جزئیات و حذف
- **صفحه‌بندی:** کامپوننت مشترک

### Users/CustomerDetails.cshtml
جزئیات مشتری شامل:
- **کارت اطلاعات:** تصویر، نام، تاریخ عضویت، شهر، موجودی، شماره، ایمیل، آدرس
- **جدول درخواست‌ها:** لیست درخواست‌های این مشتری

### Users/ExpertDetails.cshtml
جزئیات متخصص شامل:
- **کارت اطلاعات:** مشابه مشتری
- **کارت خدمات:** دسته‌بندی شده بر اساس Category
- **جدول پیشنهادات:** لیست پیشنهادات این متخصص

### Users/UserForm.cshtml
فرم افزودن کاربر شامل:
- **اطلاعات پایه:** نام، نام خانوادگی، شماره موبایل، ایمیل، رمز عبور، شهر، آدرس، موجودی
- **انتخاب نقش:** مشتری یا متخصص
- **آپلود تصویر:** با پیش‌نمایش

### Categories/Index.cshtml
لیست دسته‌بندی‌ها شامل:
- **جستجو:** بر اساس عنوان
- **کارت‌ها:** هر دسته‌بندی با آکاردئون سرویس‌ها
- **عملیات:** ویرایش و حذف دسته‌بندی و سرویس

### Categories/CategoryForm.cshtml
فرم دسته‌بندی شامل:
- **فیلدها:** عنوان و توضیحات
- **حالت دوگانه:** افزودن/ویرایش

### Works/WorkForm.cshtml
فرم سرویس شامل:
- **فیلدها:** عنوان، دسته‌بندی (Select2)، قیمت پایه
- **حالت دوگانه:** افزودن/ویرایش

### Requests/Index.cshtml
لیست درخواست‌ها شامل:
- **فیلترها:** جستجو، وضعیت، شهر (Select2)
- **کارت‌ها:** عنوان، وضعیت، توضیحات، تاریخ، تعداد پیشنهادات، مشتری
- **عملیات:** جزئیات و تغییر وضعیت

### Requests/RequestDetails.cshtml
جزئیات درخواست شامل:
- **اطلاعات مشتری:** کارت Partial
- **اطلاعات درخواست:** کارت Partial
- **گالری تصاویر:** با GLightbox
- **پیشنهاد پذیرفته شده:** در صورت وجود
- **جدول پیشنهادات:** با امکان مشاهده جزئیات و رد

### Requests/BidDetails.cshtml
جزئیات پیشنهاد شامل:
- **اطلاعات مشتری:** کارت Partial
- **اطلاعات متخصص:** کارت Partial
- **اطلاعات درخواست:** کارت Partial
- **اطلاعات پیشنهاد:** تاریخ ثبت، تاریخ مراجعه، قیمت، توضیحات

### Reviews/Index.cshtml
لیست نظرات شامل:
- **فیلترها:** جستجو و وضعیت
- **کارت‌ها:** وضعیت، تاریخ، امتیاز (ستاره)، نام مشتری، متن نظر
- **عملیات:** تایید و رد

### Shared/_Layout.cshtml
لایه‌بندی اصلی شامل:
- **سایدبار:** منوی ناوبری با آیکون‌ها
- **محتوای اصلی:** RenderBody
- **کتابخانه‌ها:** jQuery, Select2, GLightbox

### Shared/_Pagination.cshtml
کامپوننت صفحه‌بندی:
- **دکمه قبلی/بعدی**
- **شماره صفحات با حفظ فیلترها**
- **نقاط میانی برای صفحات زیاد**

### Shared/_UserDetailsPartial.cshtml
کارت اطلاعات کاربر:
- **تصویر و نام با Badge نقش**
- **دو ستون اطلاعات**
- **آدرس (فقط برای مشتری)**

### Shared/_RequestDetailsPartial.cshtml
کارت اطلاعات درخواست:
- **عنوان با Badge وضعیت**
- **دو ستون اطلاعات**
- **آدرس و توضیحات**

---

## 🛣️ روتینگ‌ها

| مسیر | کنترلر | اکشن | توضیحات |
|------|--------|------|---------|
| `/Admin` | Dashboard | Index | داشبورد |
| `/Admin/Dashboard` | Dashboard | Index | داشبورد |
| `/Admin/Users` | Users | Index | لیست کاربران |
| `/Admin/Users/CustomerDetails/{id}` | Users | CustomerDetails | جزئیات مشتری |
| `/Admin/Users/ExpertDetails/{id}` | Users | ExpertDetails | جزئیات متخصص |
| `/Admin/Users/UserForm` | Users | UserForm | فرم کاربر |
| `/Admin/Users/Delete/{id}` | Users | Delete | حذف کاربر |
| `/Admin/Categories` | Categories | Index | لیست دسته‌بندی |
| `/Admin/Categories/Create` | Categories | Create | افزودن دسته‌بندی |
| `/Admin/Categories/Edit/{id}` | Categories | Edit | ویرایش دسته‌بندی |
| `/Admin/Categories/Delete/{id}` | Categories | Delete | حذف دسته‌بندی |
| `/Admin/Works/Create` | Works | Create | افزودن سرویس |
| `/Admin/Works/Edit/{id}` | Works | Edit | ویرایش سرویس |
| `/Admin/Works/Delete/{id}` | Works | Delete | حذف سرویس |
| `/Admin/Requests` | Requests | Index | لیست درخواست‌ها |
| `/Admin/Requests/RequestDetails/{id}` | Requests | RequestDetails | جزئیات درخواست |
| `/Admin/Requests/ChangeStatus/{id}` | Requests | ChangeStatus | تغییر وضعیت |
| `/Admin/Requests/RejectBid/{id}` | Requests | RejectBid | رد پیشنهاد |
| `/Admin/Requests/BidDetails/{id}` | Requests | BidDetails | جزئیات پیشنهاد |
| `/Admin/Reviews` | Reviews | Index | لیست نظرات |
| `/Admin/Reviews/Approve/{id}` | Reviews | Approve | تایید نظر |
| `/Admin/Reviews/Reject/{id}` | Reviews | Reject | رد نظر |

---

## 🔒 امنیت

### AntiForgeryToken
تمام فرم‌های POST دارای `@Html.AntiForgeryToken()` هستند و در `Program.cs` فیلتر `AutoValidateAntiforgeryToken` اعمال شده:

```csharp
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});
```

### Area Attribute
تمام کنترلرها دارای `[Area("Admin")]` هستند.

### اعتبارسنجی ورودی
- استفاده از Data Annotations در ViewModelها
- بررسی `ModelState.IsValid` در کنترلرها
- پیام‌های خطای فارسی

### نوتیفیکیشن‌ها
سیستم نوتیفیکیشن با TempData:
```csharp
private void Notification<T>(Result<T> result)
{
    var r = result.Status switch
    {
        ResultStatusEnum.Failure => Result<string>.Failure(result.Message),
        ResultStatusEnum.Warning => Result<string>.Warning(result.Message),
        _ => Result<string>.Success(result.Message),
    };
    TempData["Notification"] = JsonConvert.SerializeObject(r);
}
```

---

## ✅ نتیجه‌گیری

پنل مدیریت یاریتو به صورت کامل پیاده‌سازی شده و شامل قابلیت‌های زیر است:

### ✅ امکانات پیاده‌سازی شده

| بخش | امکانات |
|-----|---------|
| **داشبورد** | آمار کلی، درخواست‌های اخیر، نظرات در انتظار |
| **کاربران** | لیست، جستجو، فیلتر، جزئیات مشتری، جزئیات متخصص، افزودن، حذف |
| **دسته‌بندی** | لیست، جستجو، افزودن، ویرایش، حذف |
| **سرویس‌ها** | افزودن، ویرایش، حذف (از صفحه دسته‌بندی) |
| **درخواست‌ها** | لیست، فیلتر پیشرفته، جزئیات، تغییر وضعیت، مدیریت پیشنهادات |
| **نظرات** | لیست، فیلتر، تایید، رد |

### 📊 آمار کد

| معیار | تعداد |
|-------|-------|
| کنترلرها | ۶ |
| ویومدل‌ها | ۱۵ |
| ویوها | ۱۴ |
| Partial Views | ۴ |
| Mapping Profiles | ۳ |

### 🎨 تکنولوژی‌های استفاده شده

- **Backend:** ASP.NET Core MVC (.NET 10)
- **ORM:** Entity Framework Core
- **Authentication:** ASP.NET Core Identity
- **Mapping:** AutoMapper
- **Frontend:** HTML5, CSS3, JavaScript
- **Libraries:** jQuery, Select2, GLightbox
- **Database:** SQL Server

---

**تهیه شده توسط:** GitHub Copilot  
**تاریخ:** خرداد ۱۴۰۴

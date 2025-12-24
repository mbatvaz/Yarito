namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations;

/// <summary>
/// کلاس مرکزی برای نگهداری Guid های ثابت برای SeedData
/// </summary>
public static class SeedDataGuids
{
    #region Cities - شهرها و استان‌ها

    // استان‌ها
    public static readonly Guid TehranProvinceId = Guid.Parse("10000000-0000-0000-0000-000000000001");
    public static readonly Guid IsfahanProvinceId = Guid.Parse("10000000-0000-0000-0000-000000000002");
    public static readonly Guid KhorasanProvinceId = Guid.Parse("10000000-0000-0000-0000-000000000003");
    public static readonly Guid FarsProvinceId = Guid.Parse("10000000-0000-0000-0000-000000000004");
    public static readonly Guid AzerbaijanProvinceId = Guid.Parse("10000000-0000-0000-0000-000000000005");
    public static readonly Guid KhuzestanProvinceId = Guid.Parse("10000000-0000-0000-0000-000000000006");
    public static readonly Guid GilanProvinceId = Guid.Parse("10000000-0000-0000-0000-000000000007");
    public static readonly Guid MazandaranProvinceId = Guid.Parse("10000000-0000-0000-0000-000000000008");

    // شهرها - استان تهران
    public static readonly Guid TehranCityId = Guid.Parse("10000000-0000-0000-0001-000000000101");
    public static readonly Guid KarajCityId = Guid.Parse("10000000-0000-0000-0001-000000000102");
    public static readonly Guid ReyCityId = Guid.Parse("10000000-0000-0000-0001-000000000103");

    // شهرها - استان اصفهان
    public static readonly Guid IsfahanCityId = Guid.Parse("10000000-0000-0000-0002-000000000201");
    public static readonly Guid KashanCityId = Guid.Parse("10000000-0000-0000-0002-000000000202");
    public static readonly Guid NajafAbadCityId = Guid.Parse("10000000-0000-0000-0002-000000000203");

    // شهرها - استان خراسان رضوی
    public static readonly Guid MashhadCityId = Guid.Parse("10000000-0000-0000-0003-000000000301");
    public static readonly Guid NeishaborCityId = Guid.Parse("10000000-0000-0000-0003-000000000302");
    public static readonly Guid SabzevarCityId = Guid.Parse("10000000-0000-0000-0003-000000000303");

    // شهرها - استان فارس
    public static readonly Guid ShirazCityId = Guid.Parse("10000000-0000-0000-0004-000000000401");
    public static readonly Guid MarvDashtCityId = Guid.Parse("10000000-0000-0000-0004-000000000402");

    // شهرها - استان آذربایجان شرقی
    public static readonly Guid TabrizCityId = Guid.Parse("10000000-0000-0000-0005-000000000501");
    public static readonly Guid MaragheCityId = Guid.Parse("10000000-0000-0000-0005-000000000502");

    // شهرها - استان خوزستان
    public static readonly Guid AhvazCityId = Guid.Parse("10000000-0000-0000-0006-000000000601");
    public static readonly Guid AbadanCityId = Guid.Parse("10000000-0000-0000-0006-000000000602");

    // شهرها - استان گیلان
    public static readonly Guid RashtCityId = Guid.Parse("10000000-0000-0000-0007-000000000701");
    public static readonly Guid AnzaliCityId = Guid.Parse("10000000-0000-0000-0007-000000000702");

    // شهرها - استان مازندران
    public static readonly Guid SariCityId = Guid.Parse("10000000-0000-0000-0008-000000000801");
    public static readonly Guid BabolCityId = Guid.Parse("10000000-0000-0000-0008-000000000802");

    #endregion

    #region Categories - دسته‌بندی‌ها

    // دسته‌بندی‌های اصلی
    public static readonly Guid BuildingRepairId = Guid.Parse("20000000-0000-0000-0000-000000000001");
    public static readonly Guid HomeServicesId = Guid.Parse("20000000-0000-0000-0000-000000000002");
    public static readonly Guid CarServicesId = Guid.Parse("20000000-0000-0000-0000-000000000003");
    public static readonly Guid EducationId = Guid.Parse("20000000-0000-0000-0000-000000000004");
    public static readonly Guid BeautyHealthId = Guid.Parse("20000000-0000-0000-0000-000000000005");

    // زیر دسته‌بندی‌های تعمیرات ساختمان
    public static readonly Guid PlumbingId = Guid.Parse("20000000-0000-0000-0001-000000000101");
    public static readonly Guid ElectricalId = Guid.Parse("20000000-0000-0000-0001-000000000102");
    public static readonly Guid PaintingId = Guid.Parse("20000000-0000-0000-0001-000000000103");
    public static readonly Guid TilingId = Guid.Parse("20000000-0000-0000-0001-000000000104");

    // زیر دسته‌بندی‌های خدمات منزل
    public static readonly Guid CleaningId = Guid.Parse("20000000-0000-0000-0002-000000000201");
    public static readonly Guid MovingId = Guid.Parse("20000000-0000-0000-0002-000000000202");
    public static readonly Guid ApplianceRepairId = Guid.Parse("20000000-0000-0000-0002-000000000203");
    public static readonly Guid CookingId = Guid.Parse("20000000-0000-0000-0002-000000000204");

    // زیر دسته‌بندی‌های خدمات خودرو
    public static readonly Guid MechanicId = Guid.Parse("20000000-0000-0000-0003-000000000301");
    public static readonly Guid BodyWorkId = Guid.Parse("20000000-0000-0000-0003-000000000302");
    public static readonly Guid CarWashId = Guid.Parse("20000000-0000-0000-0003-000000000303");

    // زیر دسته‌بندی‌های آموزش
    public static readonly Guid LanguageId = Guid.Parse("20000000-0000-0000-0004-000000000401");
    public static readonly Guid MusicId = Guid.Parse("20000000-0000-0000-0004-000000000402");
    public static readonly Guid ComputerId = Guid.Parse("20000000-0000-0000-0004-000000000403");

    // زیر دسته‌بندی‌های زیبایی و سلامت
    public static readonly Guid MenHairdresserId = Guid.Parse("20000000-0000-0000-0005-000000000501");
    public static readonly Guid WomenHairdresserId = Guid.Parse("20000000-0000-0000-0005-000000000502");
    public static readonly Guid MassageId = Guid.Parse("20000000-0000-0000-0005-000000000503");

    #endregion

    #region Users - کاربران

    // مشتریان
    public static readonly Guid Customer1Id = Guid.Parse("30000000-0000-0000-0001-000000001001");
    public static readonly Guid Customer2Id = Guid.Parse("30000000-0000-0000-0001-000000001002");
    public static readonly Guid Customer3Id = Guid.Parse("30000000-0000-0000-0001-000000001003");
    public static readonly Guid Customer4Id = Guid.Parse("30000000-0000-0000-0001-000000001004");
    public static readonly Guid Customer5Id = Guid.Parse("30000000-0000-0000-0001-000000001005");

    // متخصصان
    public static readonly Guid Expert1Id = Guid.Parse("30000000-0000-0000-0002-000000002001");
    public static readonly Guid Expert2Id = Guid.Parse("30000000-0000-0000-0002-000000002002");
    public static readonly Guid Expert3Id = Guid.Parse("30000000-0000-0000-0002-000000002003");
    public static readonly Guid Expert4Id = Guid.Parse("30000000-0000-0000-0002-000000002004");
    public static readonly Guid Expert5Id = Guid.Parse("30000000-0000-0000-0002-000000002005");
    public static readonly Guid Expert6Id = Guid.Parse("30000000-0000-0000-0002-000000002006");
    public static readonly Guid Expert7Id = Guid.Parse("30000000-0000-0000-0002-000000002007");
    public static readonly Guid Expert8Id = Guid.Parse("30000000-0000-0000-0002-000000002008");

    #endregion

    #region ExpertImages - تصاویر متخصصان

    public static readonly Guid Expert1Image1Id = Guid.Parse("50000000-0000-0001-0000-000000000001");
    public static readonly Guid Expert1Image2Id = Guid.Parse("50000000-0000-0001-0000-000000000002");
    public static readonly Guid Expert1Image3Id = Guid.Parse("50000000-0000-0001-0000-000000000003");

    public static readonly Guid Expert2Image1Id = Guid.Parse("50000000-0000-0002-0000-000000000004");
    public static readonly Guid Expert2Image2Id = Guid.Parse("50000000-0000-0002-0000-000000000005");
    public static readonly Guid Expert2Image3Id = Guid.Parse("50000000-0000-0002-0000-000000000006");

    public static readonly Guid Expert3Image1Id = Guid.Parse("50000000-0000-0003-0000-000000000007");
    public static readonly Guid Expert3Image2Id = Guid.Parse("50000000-0000-0003-0000-000000000008");

    public static readonly Guid Expert4Image1Id = Guid.Parse("50000000-0000-0004-0000-000000000009");
    public static readonly Guid Expert4Image2Id = Guid.Parse("50000000-0000-0004-0000-000000000010");
    public static readonly Guid Expert4Image3Id = Guid.Parse("50000000-0000-0004-0000-000000000011");

    public static readonly Guid Expert5Image1Id = Guid.Parse("50000000-0000-0005-0000-000000000012");
    public static readonly Guid Expert5Image2Id = Guid.Parse("50000000-0000-0005-0000-000000000013");

    public static readonly Guid Expert6Image1Id = Guid.Parse("50000000-0000-0006-0000-000000000014");
    public static readonly Guid Expert6Image2Id = Guid.Parse("50000000-0000-0006-0000-000000000015");

    public static readonly Guid Expert7Image1Id = Guid.Parse("50000000-0000-0007-0000-000000000016");
    public static readonly Guid Expert7Image2Id = Guid.Parse("50000000-0000-0007-0000-000000000017");

    public static readonly Guid Expert8Image1Id = Guid.Parse("50000000-0000-0008-0000-000000000018");
    public static readonly Guid Expert8Image2Id = Guid.Parse("50000000-0000-0008-0000-000000000019");

    #endregion

    #region Requests - درخواست‌ها

    public static readonly Guid Request1Id = Guid.Parse("60000000-0000-0000-0000-000000000001");
    public static readonly Guid Request2Id = Guid.Parse("60000000-0000-0000-0000-000000000002");
    public static readonly Guid Request3Id = Guid.Parse("60000000-0000-0000-0000-000000000003");
    public static readonly Guid Request4Id = Guid.Parse("60000000-0000-0000-0000-000000000004");
    public static readonly Guid Request5Id = Guid.Parse("60000000-0000-0000-0000-000000000005");
    public static readonly Guid Request6Id = Guid.Parse("60000000-0000-0000-0000-000000000006");
    public static readonly Guid Request7Id = Guid.Parse("60000000-0000-0000-0000-000000000007");
    public static readonly Guid Request8Id = Guid.Parse("60000000-0000-0000-0000-000000000008");
    public static readonly Guid Request9Id = Guid.Parse("60000000-0000-0000-0000-000000000009");
    public static readonly Guid Request10Id = Guid.Parse("60000000-0000-0000-0000-000000000010");
    public static readonly Guid Request11Id = Guid.Parse("60000000-0000-0000-0000-000000000011");

    #endregion

    #region Bids - پیشنهادات

    public static readonly Guid Bid1Id = Guid.Parse("70000000-0000-0000-0000-000000000001");
    public static readonly Guid Bid2Id = Guid.Parse("70000000-0000-0000-0000-000000000002");
    public static readonly Guid Bid3Id = Guid.Parse("70000000-0000-0000-0000-000000000003");
    public static readonly Guid Bid4Id = Guid.Parse("70000000-0000-0000-0000-000000000004");
    public static readonly Guid Bid5Id = Guid.Parse("70000000-0000-0000-0000-000000000005");
    public static readonly Guid Bid6Id = Guid.Parse("70000000-0000-0000-0000-000000000006");
    public static readonly Guid Bid7Id = Guid.Parse("70000000-0000-0000-0000-000000000007");
    public static readonly Guid Bid8Id = Guid.Parse("70000000-0000-0000-0000-000000000008");
    public static readonly Guid Bid9Id = Guid.Parse("70000000-0000-0000-0000-000000000009");
    public static readonly Guid Bid10Id = Guid.Parse("70000000-0000-0000-0000-000000000010");
    public static readonly Guid Bid11Id = Guid.Parse("70000000-0000-0000-0000-000000000011");
    public static readonly Guid Bid12Id = Guid.Parse("70000000-0000-0000-0000-000000000012");
    public static readonly Guid Bid13Id = Guid.Parse("70000000-0000-0000-0000-000000000013");
    public static readonly Guid Bid14Id = Guid.Parse("70000000-0000-0000-0000-000000000014");
    public static readonly Guid Bid15Id = Guid.Parse("70000000-0000-0000-0000-000000000015");
    public static readonly Guid Bid16Id = Guid.Parse("70000000-0000-0000-0000-000000000016");
    public static readonly Guid Bid17Id = Guid.Parse("70000000-0000-0000-0000-000000000017");
    public static readonly Guid Bid18Id = Guid.Parse("70000000-0000-0000-0000-000000000018");
    public static readonly Guid Bid19Id = Guid.Parse("70000000-0000-0000-0000-000000000019");
    public static readonly Guid Bid20Id = Guid.Parse("70000000-0000-0000-0000-000000000020");
    public static readonly Guid Bid21Id = Guid.Parse("70000000-0000-0000-0000-000000000021");

    #endregion
}

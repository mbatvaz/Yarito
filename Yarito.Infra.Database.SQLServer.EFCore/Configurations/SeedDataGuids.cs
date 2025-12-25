namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations;

/// <summary>
/// کلاس مرکزی برای نگهداری ID های ثابت برای SeedData
/// </summary>
public static class SeedDataIds
{
    public static readonly DateTime SeedDataBaseDate = new (2025, 12, 25, 0, 0, 0, DateTimeKind.Utc);

    #region City IDs
    // استان‌ها
    public const int TehranProvinceId = 1;
    public const int IsfahanProvinceId = 2;
    public const int ShirazProvinceId = 3;

    // شهرهای تهران
    public const int TehranCityId = 4;
    public const int KarajCityId = 5;
    public const int RayCityId = 6;

    // شهرهای اصفهان
    public const int IsfahanCityId = 7;
    public const int NajafabadCityId = 8;
    public const int KashanCityId = 9;

    // شهرهای شیراز
    public const int ShirazCityId = 10;
    public const int MarvsdashtCityId = 11;
    public const int JahromCityId = 12;
    #endregion

    #region Category IDs
    public const int ElectricCategoryId = 1;
    public const int PlumbingCategoryId = 2;
    public const int CleaningCategoryId = 3;
    public const int PaintingCategoryId = 4;
    public const int ApplianceRepairCategoryId = 5;
    #endregion

    #region Work IDs
    // خدمات برق
    public const int HomeWiringWorkId = 1;
    public const int LightInstallationWorkId = 2;
    public const int SwitchRepairWorkId = 3;

    // خدمات لوله کشی
    public const int PipeRepairWorkId = 4;
    public const int FaucetInstallationWorkId = 5;
    public const int ToiletRepairWorkId = 6;

    // خدمات نظافت
    public const int HomeCleaningWorkId = 7;
    public const int WindowCleaningWorkId = 8;
    public const int CarpetCleaningWorkId = 9;

    // خدمات نقاشی
    public const int InteriorPaintingWorkId = 10;
    public const int ExteriorPaintingWorkId = 11;
    public const int WallPaperingWorkId = 12;

    // خدمات تعمیر لوازم خانگی
    public const int WashingMachineRepairWorkId = 13;
    public const int RefrigeratorRepairWorkId = 14;
    public const int ACRepairWorkId = 15;
    #endregion

    #region Customer IDs
    public const int Customer1Id = 1;
    public const int Customer2Id = 2;
    public const int Customer3Id = 3;
    public const int Customer4Id = 4;
    public const int Customer5Id = 5;
    #endregion

    #region Expert IDs
    public const int Expert1Id = 6;
    public const int Expert2Id = 7;
    public const int Expert3Id = 8;
    public const int Expert4Id = 9;
    public const int Expert5Id = 10;
    #endregion

    #region Request IDs
    public const int Request1Id = 1;
    public const int Request2Id = 2;
    public const int Request3Id = 3;
    public const int Request4Id = 4;
    public const int Request5Id = 5;
    public const int Request6Id = 6;
    public const int Request7Id = 7;
    public const int Request8Id = 8;
    public const int Request9Id = 9;
    public const int Request10Id = 10;
    #endregion

    #region Bid IDs
    public const int Bid1Id = 1;
    public const int Bid2Id = 2;
    public const int Bid3Id = 3;
    public const int Bid4Id = 4;
    public const int Bid5Id = 5;
    public const int Bid6Id = 6;
    public const int Bid7Id = 7;
    public const int Bid8Id = 8;
    public const int Bid9Id = 9;
    public const int Bid10Id = 10;
    public const int Bid11Id = 11;
    public const int Bid12Id = 12;
    public const int Bid13Id = 13;
    public const int Bid14Id = 14;
    public const int Bid15Id = 15;
    public const int Bid16Id = 16;
    public const int Bid17Id = 17;
    public const int Bid18Id = 18;
    public const int Bid19Id = 19;
    public const int Bid20Id = 20;
    #endregion

    #region Review IDs
    public const int Review1Id = 1;
    public const int Review2Id = 2;
    public const int Review3Id = 3;
    public const int Review4Id = 4;
    public const int Review5Id = 5;
    public const int Review6Id = 6;
    public const int Review7Id = 7;
    #endregion

    #region RequestImage IDs
    public const int RequestImage1Id = 1;
    public const int RequestImage2Id = 2;
    public const int RequestImage3Id = 3;
    public const int RequestImage4Id = 4;
    public const int RequestImage5Id = 5;
    public const int RequestImage6Id = 6;
    public const int RequestImage7Id = 7;
    public const int RequestImage8Id = 8;
    public const int RequestImage9Id = 9;
    public const int RequestImage10Id = 10;
    public const int RequestImage11Id = 11;
    public const int RequestImage12Id = 12;
    public const int RequestImage13Id = 13;
    public const int RequestImage14Id = 14;
    public const int RequestImage15Id = 15;
    #endregion

    #region ExpertPortfolioImage IDs
    public const int ExpertPortfolioImage1Id = 1;
    public const int ExpertPortfolioImage2Id = 2;
    public const int ExpertPortfolioImage3Id = 3;
    public const int ExpertPortfolioImage4Id = 4;
    public const int ExpertPortfolioImage5Id = 5;
    public const int ExpertPortfolioImage6Id = 6;
    public const int ExpertPortfolioImage7Id = 7;
    public const int ExpertPortfolioImage8Id = 8;
    public const int ExpertPortfolioImage9Id = 9;
    public const int ExpertPortfolioImage10Id = 10;
    public const int ExpertPortfolioImage11Id = 11;
    public const int ExpertPortfolioImage12Id = 12;
    public const int ExpertPortfolioImage13Id = 13;
    public const int ExpertPortfolioImage14Id = 14;
    public const int ExpertPortfolioImage15Id = 15;
    #endregion
}

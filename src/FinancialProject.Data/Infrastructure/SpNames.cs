namespace FinancialProject.Data.Infrastructure;

internal static class SpNames
{
    public const string UserValidateLogin = "dbo.usp_User_ValidateLogin";
    public const string UserGetProfile = "dbo.usp_User_GetProfile";

    public const string LikeListGetListByUser = "dbo.usp_LikeList_GetListByUser";
    public const string LikeListGetById = "dbo.usp_LikeList_GetById";
    public const string LikeListCreate = "dbo.usp_LikeList_Create";
    public const string LikeListUpdate = "dbo.usp_LikeList_Update";
    public const string LikeListDelete = "dbo.usp_LikeList_Delete";
}

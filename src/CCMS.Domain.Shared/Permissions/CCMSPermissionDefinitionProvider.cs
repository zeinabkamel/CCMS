using CCMS.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace CCMS.Permissions;

public class CCMSPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(CCMSPermissions.GroupName);

        var booksPermission = myGroup.AddPermission(CCMSPermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(CCMSPermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(CCMSPermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(CCMSPermissions.Books.Delete, L("Permission:Books.Delete"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(CCMSPermissions.MyPermission1, L("Permission:MyPermission1"));
        var patients = myGroup.AddPermission(CCMSPermissions.Patients.Default, L("Permission:Patients"));
        patients.AddChild(CCMSPermissions.Patients.Create, L("Permission:Patients.Create"));
        patients.AddChild(CCMSPermissions.Patients.Update, L("Permission:Patients.Update"));
        patients.AddChild(CCMSPermissions.Patients.Delete, L("Permission:Patients.Delete"));

        var staff = myGroup.AddPermission(CCMSPermissions.Staff.Default, L("Permission:Staff"));
        staff.AddChild(CCMSPermissions.Staff.Create, L("Permission:Staff.Create"));
        staff.AddChild(CCMSPermissions.Staff.Update, L("Permission:Staff.Update"));
        staff.AddChild(CCMSPermissions.Staff.Delete, L("Permission:Staff.Delete"));

        var sessions = myGroup.AddPermission(CCMSPermissions.Sessions.Default, L("Permission:Sessions"));
        //sessions.AddChild(CCMSPermissions.Sessions.Complete, L("Permission:Sessions.Complete"));
        sessions.AddChild(CCMSPermissions.Sessions.Delete, L("Permission:Sessions.Delete"));

        var store = myGroup.AddPermission(CCMSPermissions.Store.Default, L("Permission:Store"));
        store.AddChild(CCMSPermissions.Store.Create, L("Permission:Store.Create"));
        store.AddChild(CCMSPermissions.Store.Update, L("Permission:Store.Update"));
        store.AddChild(CCMSPermissions.Store.Delete, L("Permission:Store.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CCMSResource>(name);
    }
}

using System.Threading.Tasks;
using CCMS.Localization;
using CCMS.Permissions;
using CCMS.MultiTenancy;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;

namespace CCMS.Web.Menus;

public class CCMSMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<CCMSResource>();

        //Home
        context.Menu.AddItem(
            new ApplicationMenuItem(
                CCMSMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fa fa-home",
                order: 1
            )
        );


        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 6;

        //Administration->Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 1);
    
        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }
        
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        //Administration->Settings
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 7);
    
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "BooksStore",
                l["Menu:CCMS"],
                icon: "fa fa-book"
            ).AddItem(
            new ApplicationMenuItem(
                "BooksStore.Books",
                l["Menu:Books"],
                url: "/Books"
                ).RequirePermissions(CCMSPermissions.Books.Default) 
            )
        );
        // CCMS Root Menu
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "CCMS",
                l["Menu:CCMS"],
                icon: "fa fa-stethoscope",
                order: 2
            )
            .AddItem(new ApplicationMenuItem(
                "CCMS.Patients",
                l["Menu:Patients"],
                url: "/Patients"
            ).RequirePermissions(CCMSPermissions.Patients.Default))
            .AddItem(new ApplicationMenuItem(
                "CCMS.Staff",
                l["Menu:Staff"],
                url: "/Staff"
            ).RequirePermissions(CCMSPermissions.Staff.Default))
            .AddItem(new ApplicationMenuItem(
                "CCMS.Doctors",
                l["Menu:Doctors"],
                url: "/Doctors"
            ).RequirePermissions(CCMSPermissions.Staff.Default))
            .AddItem(new ApplicationMenuItem(
                "CCMS.Sessions",
                l["Menu:Sessions"],
                url: "/Sessions"
            ).RequirePermissions(CCMSPermissions.Sessions.Default))
            .AddItem(new ApplicationMenuItem(
                "CCMS.Store",
                l["Menu:Store"],
                url: "/Store"
            ).RequirePermissions(CCMSPermissions.Store.Default))
            .AddItem(new ApplicationMenuItem(
                "CCMS.RawMaterials",
                l["Menu:RawMaterials"],
                url: "/RawMaterials"
            ).RequirePermissions(CCMSPermissions.Store.Default))
        );

        return Task.CompletedTask;
    }
}

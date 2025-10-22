using CCMS.Enums;
using CCMS.Permissions;
using CCMS.Staffing;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp; // Check.NotNullOrWhiteSpace
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;           // <== استخدمي Types ABP هنا
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;

namespace CCMS
{
    public class CCMSDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IdentityUserManager _userManager;
        private readonly IdentityRoleManager _roleManager;
        private readonly IPermissionManager _permissionManager;
        private readonly IRepository<Staff, Guid> _staffRepo;
        private readonly IRepository<DoctorProfile, Guid> _doctorProfileRepo;
        private readonly IGuidGenerator _guid;
        private readonly ICurrentTenant _currentTenant;

        public CCMSDataSeedContributor(
            IdentityUserManager userManager,
            IdentityRoleManager roleManager,
            IPermissionManager permissionManager,
            IRepository<Staff, Guid> staffRepo,
            IRepository<DoctorProfile, Guid> doctorProfileRepo,
            IGuidGenerator guid,
            ICurrentTenant currentTenant)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _permissionManager = permissionManager;
            _staffRepo = staffRepo;
            _doctorProfileRepo = doctorProfileRepo;
            _guid = guid;
            _currentTenant = currentTenant;
        }

        [UnitOfWork]
        public async Task SeedAsync(DataSeedContext context)
        {
            using var _ = _currentTenant.Change(context?.TenantId);

            // 1) Roles
            await EnsureRoleAsync("BusinessOwner");
            await EnsureRoleAsync("Doctor");
            await EnsureRoleAsync("Receptionist");
            await EnsureRoleAsync("StoreKeeper");
            await EnsureRoleAsync("Manager");

            // 2) Permissions
            await GrantAllCcmsPermissionsAsync("Admin");
            await GrantAllCcmsPermissionsAsync("BusinessOwner");

            await GrantRolePermissionsAsync("Doctor",
                CCMSPermissions.Patients.Default,
                CCMSPermissions.Sessions.Default,
                CCMSPermissions.Sessions.Create
            );

            await GrantRolePermissionsAsync("Receptionist",
                CCMSPermissions.Patients.Default,
                CCMSPermissions.Patients.Create,
                CCMSPermissions.Patients.Update,
                CCMSPermissions.Sessions.Default
            );

            await GrantRolePermissionsAsync("StoreKeeper",
                CCMSPermissions.Store.Default,
                CCMSPermissions.Store.Create,
                CCMSPermissions.Store.Update,
                CCMSPermissions.Store.Delete
            );

            await GrantRolePermissionsAsync("Manager", CCMSPermissions.All);

            // 3) Users
            var mona = await EnsureUserAsync(
                userName: "mona",
                email: "mona@clinic.local",
                password: "Abp1234*",
                roles: new[] { "BusinessOwner", "Doctor" }
            );

            var secretary = await EnsureUserAsync(
                userName: "secretary",
                email: "secretary@clinic.local",
                password: "Abp1234*",
                roles: new[] { "Admin", "Receptionist" }
            );

            // 4) Staff links
            await EnsureStaffForUserAsync(mona, StaffRole.Doctor, phone: "01000000001", isDoctor: true, specialty: "Dermatology", licenseNo: "DR-0001");
            await EnsureStaffForUserAsync(secretary, StaffRole.Receptionist, phone: "01000000002");
        }

        private async Task EnsureRoleAsync(string roleName)
        {
            if (await _roleManager.FindByNameAsync(roleName) == null)
            {
                var role = new IdentityRole(_guid.Create(), roleName, _currentTenant.Id);
                (await _roleManager.CreateAsync(role)).CheckErrors();
            }
        }

        private async Task GrantAllCcmsPermissionsAsync(string roleName)
        {
            foreach (var perm in CCMSPermissions.All)
            {
                await _permissionManager.SetForRoleAsync(roleName, perm, true);
            }
        }

        private async Task GrantRolePermissionsAsync(string roleName, params string[] permissions)
        {
            foreach (var p in permissions.Distinct())
            {
                await _permissionManager.SetForRoleAsync(roleName, p, true);
            }
        }

        private async Task<IdentityUser> EnsureUserAsync(string userName, string email, string password, string[] roles)
        {
            // تحصين المدخلات (اختياري مفيد)
            userName = Check.NotNullOrWhiteSpace(userName, nameof(userName));
            email = Check.NotNullOrWhiteSpace(email, nameof(email));
            password = Check.NotNullOrWhiteSpace(password, nameof(password));

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                // IMPORTANT: أنشيء الـUser قبل CreateAsync
                user = new IdentityUser(_guid.Create(), userName, email, _currentTenant.Id);

                (await _userManager.CreateAsync(user, password)).CheckErrors();

                // Confirm email بالطريقة الصحيحة في ABP
                 user.SetEmailConfirmed(true);
                (await _userManager.UpdateAsync(user)).CheckErrors();
            }

            foreach (var role in roles)
            {
                if (!await _userManager.IsInRoleAsync(user, role))
                {
                    (await _userManager.AddToRoleAsync(user, role)).CheckErrors();
                }
            }

            return user;
        }

        private async Task EnsureStaffForUserAsync(
            IdentityUser user,
            StaffRole role,
            string? phone = null,
            bool isDoctor = false,
            string? specialty = null,
            string? licenseNo = null,
            string? bio = null)
        {
            var exists = (await _staffRepo.GetListAsync(x => x.UserId == user.Id)).FirstOrDefault();
            if (exists != null) return;

            var staff = new Staff(_guid.Create(), userNameOrFullName(user), role);
            staff.LinkUser(user.Id);
            staff.SetPhone(phone);

            await _staffRepo.InsertAsync(staff, autoSave: true);

            if (isDoctor)
            {
                var prof = new DoctorProfile(_guid.Create(), staff.Id);
                prof.Update(specialty, licenseNo, bio);
                await _doctorProfileRepo.InsertAsync(prof, autoSave: true);
            }
        }

        private static string userNameOrFullName(IdentityUser u) =>
            string.IsNullOrWhiteSpace(u.Name) ? u.UserName : u.Name;
    }
}

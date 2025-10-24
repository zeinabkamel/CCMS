
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCMS.Enums;
using CCMS.Patients;
using CCMS.Sessions;
using CCMS.Staffing;
using CCMS.Stores;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace CCMS
{
    /// <summary>
    /// Clinic seed aligned with current domain models:
    /// - Patient ctor requires (id, code, fullName, phone)
    /// - Session ctor requires (id, patientId, doctorStaffId, SessionType type, scheduledAt, durationMins)
    /// - SessionStatus uses 'Cancelled' (double-l); uses Complete()/Cancel() domain methods
    /// - Adds materials through Session.AddMaterial(...)
    /// </summary>
    public class CCMSClinicDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IGuidGenerator _guid;
        private readonly ICurrentTenant _currentTenant;

        private readonly IRepository<Store, Guid> _storeRepo;
        private readonly IRepository<RawMaterial, Guid> _rawRepo;

        private readonly IRepository<Staff, Guid> _staffRepo;
        private readonly IRepository<DoctorProfile, Guid> _doctorProfileRepo;

        private readonly IRepository<Patient, Guid> _patientRepo;

        private readonly IRepository<Session, Guid> _sessionRepo;

        public CCMSClinicDataSeedContributor(
            IGuidGenerator guid,
            ICurrentTenant currentTenant,
            IRepository<Store, Guid> storeRepo,
            IRepository<RawMaterial, Guid> rawRepo,
            IRepository<Staff, Guid> staffRepo,
            IRepository<DoctorProfile, Guid> doctorProfileRepo,
            IRepository<Patient, Guid> patientRepo,
            IRepository<Session, Guid> sessionRepo
        )
        {
            _guid = guid;
            _currentTenant = currentTenant;
            _storeRepo = storeRepo;
            _rawRepo = rawRepo;
            _staffRepo = staffRepo;
            _doctorProfileRepo = doctorProfileRepo;
            _patientRepo = patientRepo;
            _sessionRepo = sessionRepo;
        }

        [UnitOfWork]
        public async Task SeedAsync(DataSeedContext context)
        {
            using var _ = _currentTenant.Change(context?.TenantId);

            // 1) Stores
            var mainStore  = await EnsureStoreAsync("Main Clinic Storage", "Ground Floor", "Injections & fillers");
            var laserStore = await EnsureStoreAsync("Laser Room Storage", "2nd Floor", "Laser/skin-care consumables");

            // 2) Raw Materials (supplier stored as free text SupplierName)
            var botox     = await EnsureRawAsync("Botox",               "BTX-001", "ml",     1200m, "BeautyPro Egypt",               mainStore.Id);
            var filler    = await EnsureRawAsync("Hyaluronic Filler",   "FLR-002", "ml",      900m, "DermaLine Medical Supplies",    mainStore.Id);
            var lido      = await EnsureRawAsync("Lidocaine 2%",        "LD-003",  "ml",      100m, "SkinGlow Pharma",               mainStore.Id);
            var serum     = await EnsureRawAsync("Rejuvenation Serum",  "SRM-010", "bottle",  350m, "SkinGlow Pharma",               mainStore.Id);

            var laserGel  = await EnsureRawAsync("Laser Gel",           "LSR-101", "bottle",  200m, "LaserTech Co.",                 laserStore.Id);
            var cooling   = await EnsureRawAsync("Cooling Spray",       "CLS-102", "bottle",  150m, "LaserTech Co.",                 laserStore.Id);
            var cotton    = await EnsureRawAsync("Cotton Pads",         "CTN-103", "pack",     50m, "Clinic Essentials",             laserStore.Id);

            // 3) Staff/Doctors
            var drMona   = await EnsureDoctorAsync("Dr. Mona",   StaffRole.Doctor, "Dermatologist",    "DR-0001", "01000000001");
            var drKareem = await EnsureDoctorAsync("Dr. Kareem", StaffRole.Doctor, "Laser Specialist", "DR-0002", "01000000002");
            var drSara   = await EnsureDoctorAsync("Dr. Sara",   StaffRole.Doctor, "Plastic Surgeon",  "DR-0003", "01000000003");
            var tanent_scope = await EnsureStaffAsync("Noura (Receptionist)", StaffRole.Receptionist, "01022220000");

            // 4) Patients (constructor requires phone)
            var pSara    = await EnsurePatientAsync("PT-0001", "Sara Mohamed",  "01022223333", "Female");
            var pYoussef = await EnsurePatientAsync("PT-0002", "Youssef Ali",   "01033334444", "Male");
            var pNour    = await EnsurePatientAsync("PT-0003", "Nour ElSherif", "01055556666", "Female");
            var pMariam  = await EnsurePatientAsync("PT-0004", "Mariam Hany",   "01077778888", "Female");
            var pOmar    = await EnsurePatientAsync("PT-0005", "Omar Fathy",    "01099990000", "Male");

            // 5) Sessions (use SessionType & domain methods Complete/Cancel)
            var today = DateTime.Today;
            var yest  = today.AddDays(-1);

            await EnsureSessionAsync(
                type: SessionType.Laser,
                scheduledAt: today.AddHours(10),
                durationMins: 45,
                doctor: drKareem,
                patient: pSara,
                finalize: s => { s.Complete("Smooth result; apply cooling spray."); s.AddMaterial(laserGel.Id, 1m); s.AddMaterial(cooling.Id, 0.5m); s.AddMaterial(cotton.Id, 1m); }
            );

            await EnsureSessionAsync(
                type: SessionType.Injection,
                scheduledAt: today.AddHours(12),
                durationMins: 30,
                doctor: drMona,
                patient: pNour,
                finalize: s => { s.Complete("Lidocaine topical before injection."); s.AddMaterial(botox.Id, 0.8m); s.AddMaterial(lido.Id, 0.5m); s.AddMaterial(cotton.Id, 1m); }
            );

            await EnsureSessionAsync(
                type: SessionType.Filler,
                scheduledAt: today.AddHours(14),
                durationMins: 35,
                doctor: drSara,
                patient: pMariam,
                finalize: s => { /* still scheduled */ s.AddMaterial(filler.Id, 1.2m); s.AddMaterial(lido.Id, 0.5m); s.AddMaterial(cotton.Id, 1m); }
            );

            await EnsureSessionAsync(
                type: SessionType.Mesotherapy,
                scheduledAt: yest.AddHours(16),
                durationMins: 40,
                doctor: drMona,
                patient: pOmar,
                finalize: s => { s.Complete("Post care serum."); s.AddMaterial(serum.Id, 1m); s.AddMaterial(cotton.Id, 1m); }
            );
        }

        // -------------------- Helpers --------------------

        private async Task<Store> EnsureStoreAsync(string name, string? location, string? desc)
        {
            var q = await _storeRepo.GetQueryableAsync();
            var existing = await q.FirstOrDefaultAsync(x => x.Name == name);
            if (existing != null) return existing;

            var store = new Store(_guid.Create(), name);
            store.Location = location;
            store.Description = desc;

            return await _storeRepo.InsertAsync(store, autoSave: true);
        }

        private async Task<RawMaterial> EnsureRawAsync(string name, string sku, string unit, decimal price, string supplierName, Guid storeId)
        {
            var q = await _rawRepo.GetQueryableAsync();
            var existing = await q.FirstOrDefaultAsync(x => x.SKU == sku);
            if (existing != null) return existing;

            var rm = new RawMaterial(_guid.Create(), name, sku, unit, storeId);
            rm.SupplierName = supplierName;
            rm.Price = price;

            return await _rawRepo.InsertAsync(rm, autoSave: true);
        }

        private async Task<Staff> EnsureDoctorAsync(string fullName, StaffRole role, string specialty, string licenseNo, string? phone)
        {
            var q = await _staffRepo.GetQueryableAsync();
            var existing = await q.FirstOrDefaultAsync(x => x.FullName == fullName);
            if (existing != null)
            {
                await EnsureDoctorProfileAsync(existing, specialty, licenseNo);
                return existing;
            }

            var staff = new Staff(_guid.Create(), fullName, role);
            staff.SetPhone(phone);
            staff = await _staffRepo.InsertAsync(staff, autoSave: true);

            await EnsureDoctorProfileAsync(staff, specialty, licenseNo);
            return staff;
        }

        private async Task EnsureDoctorProfileAsync(Staff staff, string specialty, string licenseNo)
        {
            var q = await _doctorProfileRepo.GetQueryableAsync();
            var has = await q.AnyAsync(x => x.StaffId == staff.Id);
            if (has) return;

            var prof = new DoctorProfile(_guid.Create(), staff.Id);
            prof.Update(specialty, licenseNo, bio: $"Experienced {specialty}");
            await _doctorProfileRepo.InsertAsync(prof, autoSave: true);
        }

        private async Task<Staff> EnsureStaffAsync(string fullName, StaffRole role, string? phone)
        {
            var q = await _staffRepo.GetQueryableAsync();
            var existing = await q.FirstOrDefaultAsync(x => x.FullName == fullName);
            if (existing != null) return existing;

            var staff = new Staff(_guid.Create(), fullName, role);
            staff.SetPhone(phone);
            return await _staffRepo.InsertAsync(staff, autoSave: true);
        }

        private async Task<Patient> EnsurePatientAsync(string code, string fullName, string phone, string? gender)
        {
            var q = await _patientRepo.GetQueryableAsync();
            var existing = await q.FirstOrDefaultAsync(x => x.Code == code);
            if (existing != null) return existing;

            var patient = new Patient(_guid.Create(), code, fullName, phone, $"Seeded patient ({gender})");
           

            return await _patientRepo.InsertAsync(patient, autoSave: true);
        }

        private async Task<Session> EnsureSessionAsync(
            SessionType type,
            DateTime scheduledAt,
            int durationMins,
            Staff doctor,
            Patient patient,
            Action<Session> finalize)
        {
            var q = await _sessionRepo.GetQueryableAsync();
            var exists = await q.FirstOrDefaultAsync(x =>
                x.PatientId == patient.Id && x.DoctorStaffId == doctor.Id && x.ScheduledAt == scheduledAt);

            if (exists != null) return exists;

            var session = new Session(
                id: _guid.Create(),
                patientId: patient.Id,
                doctorStaffId: doctor.Id,
                type: type,
                scheduledAt: scheduledAt,
                durationMins: durationMins);

            finalize?.Invoke(session);

            return await _sessionRepo.InsertAsync(session, autoSave: true);
        }
    }
}

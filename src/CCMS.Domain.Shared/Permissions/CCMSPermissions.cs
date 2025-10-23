namespace CCMS.Permissions;

public static class CCMSPermissions
{
    public const string GroupName = "CCMS";


    public static class Books
    {
        public const string Default = GroupName + ".Books";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Patients
    {
        public const string Default = GroupName + ".Patients";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
        public const string ViewHistory = Default + ".ViewHistory";      // مشاهدة السجل الطبي
        public const string Export = Default + ".Export";                // تصدير بيانات المرضى
    }

    // 👩 Staff
    public static class Staff
    {
        public const string Default = GroupName + ".Staff";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
        public const string AssignRole = Default + ".AssignRole";        // تعيين الأدوار
        public const string ViewSchedule = Default + ".ViewSchedule";    // عرض الجدول الزمني للموظفين
    }

    // 🩺 Doctors / Sessions
    public static class Sessions
    {
        public const string Default = GroupName + ".Sessions";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
        public const string Complete = Default + ".Complete";            // إتمام الجلسة
        public const string Approve = Default + ".Approve";              // الموافقة على خطة علاج
        public const string AssignDoctor = Default + ".AssignDoctor";    // تعيين الطبيب
    }

    // 🏬 Store
    public static class Store
    {
        public const string Default = GroupName + ".Store";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
        public const string Import = Default + ".Import";                // استيراد الأصناف
        public const string Export = Default + ".Export";                // تصدير تقرير المخزون
        public const string AdjustStock = Default + ".AdjustStock";      // تسوية المخزون
    }

    // 📦 Raw Materials
    public static class RawMaterials
    {
        public const string Default = GroupName + ".RawMaterials";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
        public const string Audit = Default + ".Audit";                  // مراجعة الكميات المستهلكة
    }

    // 🧾 Reports
    public static class Reports
    {
        public const string Default = GroupName + ".Reports";
        public const string Financial = Default + ".Financial";          // تقارير مالية
        public const string Operational = Default + ".Operational";      // تقارير تشغيلية
        public const string Dashboard = Default + ".Dashboard";          // لوحة التحكم الإحصائية
    }

    // ⚙️ System / Settings
    public static class System
    {
        public const string Default = GroupName + ".System";
        public const string ManageSettings = Default + ".ManageSettings";    // إعدادات النظام
        public const string ManageRoles = Default + ".ManageRoles";          // إدارة الأدوار والصلاحيات
        public const string ViewAuditLogs = Default + ".ViewAuditLogs";      // سجلات النظام
    }

    public static class Doctors
    {
        public const string Default = GroupName + ".Doctors";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }
    public static class Dashboard
    {
        public const string Default = GroupName + ".Dashboard";
    }

    public static readonly string[] All =
{
    Patients.Default, Patients.Create, Patients.Update, Patients.Delete,
    Staff.Default, Staff.Create, Staff.Update, Staff.Delete,
    Sessions.Default, Sessions.Create, Sessions.Update, Sessions.Delete,
    Sessions.Complete, Sessions.Approve, Sessions.AssignDoctor,
    Store.Default, Store.Create, Store.Update, Store.Delete,
    Store.Import, Store.Export, Store.AdjustStock,
    RawMaterials.Default, RawMaterials.Create, RawMaterials.Update, RawMaterials.Delete, RawMaterials.Audit,
    Reports.Default, Reports.Financial, Reports.Operational, Reports.Dashboard,
    System.Default, System.ManageSettings, System.ManageRoles, System.ViewAuditLogs
};

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
}

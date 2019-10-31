namespace RoverController.Lib
{
    public static class UserRoles
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string User = "User";
    }

    public static class UserRolesAtLeast
    {
        public const string SuperAdmin = UserRoles.SuperAdmin;
        public const string Admin = UserRoles.SuperAdmin + "," + UserRoles.Admin;
        public const string User = UserRoles.SuperAdmin + "," + UserRoles.Admin + "," + UserRoles.User;
    }

    public enum UserRolesOrder
    {
        SuperAdmin = 0,
        Admin,
        User,
    }

    public enum OperatorTypeEnum
    {
        AND,
        OR
    }

    public struct RegisterTypes
    {
        public readonly static string Company = "Register a new company";
        public readonly static string Individual = "Join an existing company";
    }

    public static class Status
    {
        public const string Active = "A";
        public const string Deleted = "D";
    }

    public static class Country
    {
        public const string Canada = "Canada";
        public const string USA = "United States";
    }

    public enum ClientColumns
    {
        Action_Button,
        Name,
        City,
        Phone,
        Email,
        ProvinceState,
        Country,
        Expiration,
        Demo,
        UsersCount,
    }

    public enum UserColumns
    {
        Action_Button,
        UserName,
        FullName,
        Email,
        Phone,
        Roles,
        PrimaryClient,
        CreatedDate
    }
}
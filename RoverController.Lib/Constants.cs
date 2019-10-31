namespace RoverController.Lib
{
    public static class UserRoles
    {
        public const string Admin = "Admin";
    }

    public enum UserRolesOrder
    {
        Admin,
    }

    public enum OperatorTypeEnum
    {
        AND,
        OR
    }

    public enum UserColumns
    {
        Action_Button,
        UserName,
        FullName,
        Email,
        Phone,
        Roles,
        CreatedDate
    }
}
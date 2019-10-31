namespace RoverController.Web.Services.Base
{
    public interface IBaseService
    {
        string BaseUrl { get; }

        bool IsConnectedToProd();

        //IAppMapper Mapper { get; }
        bool UserIsAtLeast(string userId, string minimumRole);
    }
}
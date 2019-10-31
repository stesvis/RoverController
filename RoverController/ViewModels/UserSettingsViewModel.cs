using System.ComponentModel.DataAnnotations;

namespace RoverController.Web.ViewModels
{
    public class UserSettingsViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        //public string Theme { get; set; }

        //[Display(Name = "Allowed Categories")]
        //public IList<UserCategoryDTO> AllowedCategories { get; set; }

        [Display(Name = "Receive Notifications")]
        public bool IsPushNotificationsEnabled { get; set; }

        public bool IsEditOrderNotificationEnabled { get; set; }

        public bool IsAssignOrderNotificationEnabled { get; set; }

        public bool IsStartedOrderNotificationEnabled { get; set; }

        public bool IsCompleteOrderNotificationEnabled { get; set; }

        public bool IsDeleteOrderNotificationEnabled { get; set; }

        [Display(Name = "Show/Hide Order Types")]
        public bool HideWorkOrders { get; set; }

        public bool HideDirectDrops { get; set; }
        public bool HideServiceOrders { get; set; }

        public UserSettingsViewModel()
        {
            //AllowedCategories = new List<UserCategoryDTO>();
        }
    }
}
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using RoverController.Lib;
using RoverController.Mobile.DTOs;
using RoverController.Mobile.Misc;
using RoverController.Mobile.Services;
using RoverController.Mobile.Services.DependencyServices;
using RoverController.Mobile.Services.Navigation;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace RoverController.Mobile.ViewModels
{
    public class MissionDetailsPageViewModel : ViewModelBase
    {
        #region Commands

        private DelegateCommand _moreCommand;
        public DelegateCommand MoreCommand =>
            _moreCommand ?? (_moreCommand = new DelegateCommand(ExecuteMoreCommand));

        private DelegateCommand<string> _attachmentCommand;
        public DelegateCommand<string> AttachmentCommand =>
            _attachmentCommand ?? (_attachmentCommand = new DelegateCommand<string>(ExecuteAttachmentCommand, CanExecuteAttachmentCommand)
            .ObservesProperty(() => IsBusy)
            .ObservesProperty(() => Mission.Attachment));

        private async void ExecuteAttachmentCommand(string parameter)
        {
            try
            {
                IsBusy = true;

                await Browser.OpenAsync(new Uri(parameter), BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                base.DisplayExceptionMessage(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool CanExecuteAttachmentCommand(string parameter)
        {
            return IsNotBusy && !Mission.Attachment.IsEmpty();
        }

        #endregion Commands

        #region Properties

        private MissionDTO _mission;
        public MissionDTO Mission
        {
            get { return _mission; }
            set { SetProperty(ref _mission, value); }
        }

        private byte[] _screenshot;
        public byte[] Screenshot
        {
            get { return _screenshot; }
            set { SetProperty(ref _screenshot, value); }
        }

        private string _attachmentLink;
        public string AttachmentLink
        {
            get { return _attachmentLink; }
            set { SetProperty(ref _attachmentLink, value); }
        }

        #endregion Properties

        public MissionDetailsPageViewModel(INavigationService navigationService, IModalNavigationService modalNavigationService, IPageDialogService dialogService, IAppService appService)
            : base(navigationService, modalNavigationService, dialogService, appService)
        {
            Mission = new MissionDTO();
            Title = "Mission";
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            try
            {
                IsBusy = true;

                if (parameters.ContainsKey("id"))
                {
                    if (int.TryParse(parameters["id"].ToString(), out int missionId))
                    {
                        using (Helper.Loading())
                        {
                            await ReloadPage(missionId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                base.DisplayExceptionMessage(ex);
            }
            finally
            {
                IsBusy = false;
                base.Initialize(parameters);
            }
        }

        #region More Command

        private async void ExecuteMoreCommand()
        {
            try
            {
                IsBusy = true;

                var choice = await DialogService.DisplayActionSheetAsync("Menu", "Cancel", null, "Continue", "Upload Screenshot");

                switch (choice)
                {
                    case "Upload Screenshot":
                        Screenshot = Xamarin.Forms.DependencyService.Get<IScreenshotService>().Capture();

                        if (Screenshot != null)
                        {
                            using (Helper.Loading("Uploading Screenshot"))
                            {
                                var uploadUrl = $"{Api.ApiBaseUrl}{Api.Missions.Upload.Replace("{id}", Mission.Id.ToString())}";
                                var filename = $"mission-{Mission.Id}-screenshot-{Guid.NewGuid()}";
                                var token = await SecureStorage.GetAsync(SecureStorageProperties.AccessToken);

                                var uploadResponse = await AppService.Api.Missions.Upload(Mission.Id, Screenshot);
                                if (uploadResponse == null)
                                {
                                    base.DisplayNoConnectionMessage();
                                    return;
                                }

                                if (uploadResponse.Item2 != null)
                                {
                                    base.DisplayErrorMessage(uploadResponse.Item2);
                                    return;
                                }

                                if (uploadResponse.Item1 != null)
                                {
                                    Mission.Attachment = uploadResponse.Item1.AwsPublicUrl;
                                    AttachmentLink = Path.GetFileName(Mission.Attachment);
                                    Helper.Toast("Screenshot uploaded!", ToastType.Success);
                                }
                            }
                        }
                        break;

                    case "Continue":
                        await NavigationService.NavigateAsync($"NewMission?id={Mission.Id}");
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                base.DisplayExceptionMessage(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion More Command

        private async Task ReloadPage(int missionId)
        {
            var apiResponse = await AppService.Api.Missions.Get(missionId);
            if (apiResponse == null)
            {
                base.DisplayNoConnectionMessage();
                return;
            }

            if (apiResponse.Item2 != null)
            {
                base.DisplayErrorMessage(apiResponse.Item2);
                return;
            }

            if (apiResponse.Item1 != null)
            {
                Mission = apiResponse.Item1;

                var firstPinPoint = Mission.PinPoints.FirstOrDefault();
                if (firstPinPoint != null)
                {
                    firstPinPoint.Type = PinPointType.Start;
                }

                var finalPinPoint = Mission.PinPoints.LastOrDefault();
                if (finalPinPoint != null)
                {
                    finalPinPoint.Type = PinPointType.Finish;
                }

                AttachmentLink = Mission.Attachment.IsEmpty() ? null : Path.GetFileName(Mission.Attachment);
                Title = $"Mission #{Mission.Id}";
            }
        }
    }
}
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using RoverController.Mobile.DTOs;
using RoverController.Mobile.Misc;
using RoverController.Mobile.Services;
using RoverController.Mobile.Services.Navigation;
using System;
using System.Threading.Tasks;

namespace RoverController.Mobile.ViewModels
{
    public class MissionDetailsPageViewModel : ViewModelBase
    {
        #region Commands

        private DelegateCommand _moreCommand;
        public DelegateCommand MoreCommand =>
            _moreCommand ?? (_moreCommand = new DelegateCommand(ExecuteMoreCommand));

        private async void ExecuteMoreCommand()
        {
            try
            {
                IsBusy = true;

                if (await DialogService.DisplayActionSheetAsync("Menu", "Cancel", null, "Upload Screenshot") == "Upload Screenshot")
                {
                    await DialogService.DisplayAlertAsync("Screenshot", "Taking screenhot", "OK");
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

        #endregion Commands

        #region Properties

        private MissionDTO _mission;
        public MissionDTO Mission
        {
            get { return _mission; }
            set { SetProperty(ref _mission, value); }
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
                Title = $"Mission #{Mission.Id}";
            }
        }
    }
}
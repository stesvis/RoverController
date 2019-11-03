﻿using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using RoverController.Mobile.DTOs;
using RoverController.Mobile.Misc;
using RoverController.Mobile.Services;
using RoverController.Mobile.Services.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RoverController.Mobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        #region Commands

        private DelegateCommand _newMissionCommand;
        public DelegateCommand NewMissionCommand =>
            _newMissionCommand ?? (_newMissionCommand = new DelegateCommand(ExecuteNewMissionCommand, CanExecuteNewMissionCommand));

        #endregion Commands

        #region Properties

        private ObservableCollection<MissionDTO> _missions;
        public ObservableCollection<MissionDTO> Missions
        {
            get { return _missions; }
            set { SetProperty(ref _missions, value); }
        }

        #endregion Properties

        public MainPageViewModel(INavigationService navigationService, IModalNavigationService modalNavigationService, IPageDialogService dialogService, IAppService appService)
            : base(navigationService, modalNavigationService, dialogService, appService)
        {
            Missions = new ObservableCollection<MissionDTO>();

            // Reload the list if a new mission was created
            MessagingCenter.Subscribe<NewMissionPageViewModel, MissionDTO>(this, MessagingCenterMessages.NewMission, async (sender, mission) =>
            {
                try
                {
                    IsBusy = true;
                    await ReloadPage();
                }
                catch (Exception ex)
                {
                    base.DisplayExceptionMessage(ex);
                }
                finally
                {
                    IsBusy = false;
                }
            });
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            try
            {
                IsBusy = true;

                if (await CheckBasicSettings() == false)
                {
                    return;
                }

                using (Helper.Loading())
                {
                    await ReloadPage();
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

        #region NewMission Command

        private async void ExecuteNewMissionCommand()
        {
            if (await CheckBasicSettings() == true)
            {
                await NavigationService.NavigateAsync("NewMission");
            }
        }

        private bool CanExecuteNewMissionCommand()
        {
            return true;
        }

        #endregion NewMission Command

        private async Task ReloadPage()
        {
            var apiResponse = await AppService.Api.Missions.All();
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
                Missions = new ObservableCollection<MissionDTO>(apiResponse.Item1);
            }
        }

        private async Task<bool> CheckBasicSettings()
        {
            if (Preferences.Get(Misc.Settings.GridMaxX, 0) == 0 ||
                Preferences.Get(Misc.Settings.GridMaxY, 0) == 0)
            {
                await DialogService.DisplayAlertAsync("First Setup", "Before you can continue you need to set the Grid size.", "OK");
                using (Helper.Loading("Redirecting to Settings"))
                {
                    await Task.Delay(2000);
                    await NavigationService.NavigateAsync("Navigation/Settings", null, true);

                    return false;
                }
            }

            return true;
        }
    }
}
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using RoverController.Lib;
using RoverController.Mobile.DTOs;
using RoverController.Mobile.Misc;
using RoverController.Mobile.Services;
using RoverController.Mobile.Services.Navigation;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RoverController.Mobile.ViewModels
{
    public class NewMissionPageViewModel : ViewModelBase
    {
        #region Commands

        private DelegateCommand _submitCommand;
        public DelegateCommand SubmitCommand =>
            _submitCommand ?? (_submitCommand = new DelegateCommand(ExecuteSubmitCommand, CanExecuteSubmitCommand)
            .ObservesProperty(() => IsBusy)
            .ObservesProperty(() => Model.InitialX)
            .ObservesProperty(() => Model.InitialY)
            .ObservesProperty(() => Model.InitialDirection)
            .ObservesProperty(() => Model.Instructions));

        private DelegateCommand<string> _instructionCommand;
        public DelegateCommand<string> InstructionCommand =>
            _instructionCommand ?? (_instructionCommand = new DelegateCommand<string>(ExecuteInstructionCommand, CanExecuteInstructionCommand)
            .ObservesProperty(() => Model.Instructions));

        #endregion Commands

        #region Properties

        private MissionRequestDTO _model;
        public MissionRequestDTO Model
        {
            get { return _model; }
            set { SetProperty(ref _model, value); }
        }

        private int _maxX;
        public int MaxX
        {
            get { return _maxX; }
            set { SetProperty(ref _maxX, value); }
        }

        private int _maxY;
        public int MaxY
        {
            get { return _maxY; }
            set { SetProperty(ref _maxY, value); }
        }

        #endregion Properties

        public NewMissionPageViewModel(INavigationService navigationService, IModalNavigationService modalNavigationService, IPageDialogService dialogService, IAppService appService)
            : base(navigationService, modalNavigationService, dialogService, appService)
        {
            try
            {
                IsBusy = true;

                MaxX = Preferences.Get(Settings.GridMaxX, 0);
                MaxY = Preferences.Get(Settings.GridMaxY, 0);
                Model = new MissionRequestDTO { MaxX = MaxX, MaxY = MaxY };
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

        #region Instruction Command

        private void ExecuteInstructionCommand(string parameter)
        {
            try
            {
                IsBusy = true;

                if (parameter.IsEmpty())
                {
                    Model.Instructions = Model.Instructions.Remove(Model.Instructions.Length - 1);
                    return;
                }

                Model.Instructions += parameter;
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

        private bool CanExecuteInstructionCommand(string parameter)
        {
            return !parameter.IsEmpty() ||
                parameter.IsEmpty() && !Model.Instructions.IsEmpty();
        }

        #endregion Instruction Command

        #region Submit Command

        private async void ExecuteSubmitCommand()
        {
            try
            {
                IsBusy = true;

                using (Helper.Loading())
                {
                    var apiResponse = await AppService.Api.Missions.Create(Model);
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
                        var missionDTO = apiResponse.Item1;
                        MessagingCenter.Send(this, MessagingCenterMessages.NewMission, missionDTO);
                        Helper.Toast("Success!", ToastType.Success);
                        await NavigationService.NavigateAsync($"MissionDetails?id={missionDTO.Id}");
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
            }
        }

        private bool CanExecuteSubmitCommand()
        {
            // make sure that all the required fields are filled
            return IsNotBusy &&
                Model.InitialX.HasValue && Model.InitialX.Value <= MaxX &&
                Model.InitialY.HasValue && Model.InitialY.Value <= MaxY &&
                !Model.InitialDirection.IsEmpty() &&
                !Model.Instructions.IsEmpty();
        }

        #endregion Submit Command
    }
}
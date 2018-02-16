namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    class LandsViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private ObservableCollection<Land> lands;
        private string targetLand;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Propierties
        public ObservableCollection<Land> Lands
        {
            get
            {
                return this.lands;
            }
            set
            {
                SetValue(ref this.lands, value);
            }
        }

        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
            set
            {
                SetValue(ref this.isRunning, value);
            }
        }

        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
            set
            {
                SetValue(ref this.isEnabled, value);
            }
        }

        public string TargetLand
        {
            get
            {
                return this.targetLand;
            }
            set
            {
                SetValue(ref this.targetLand, value);
            }
        }
        #endregion

        #region Contructors
        public LandsViewModel()
        {
            this.IsRunning = false;
            this.IsEnabled = true;
            this.apiService = new ApiService();
            this.LoadLans();
        }
        #endregion

        #region Commands
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        private async void Search()
        {
            if (string.IsNullOrEmpty(this.TargetLand))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an land.",
                    "Accept");
                return;
            }            

            this.IsRunning = true;
            this.IsEnabled = false;            
        }
        #endregion

        #region Methods
        private async void LoadLans()
        {
            var response = await this.apiService.GetList<Land>(
                "http://restcountries.eu",
                "/rest",
                "/v2/all");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", 
                    response.Message, 
                    "Accept");
                return;
            }

            var list = (List<Land>)response.Result;
            this.Lands = new ObservableCollection<Land>(list);
        }
        #endregion
    }
}

﻿namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LandsViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private ObservableCollection<LandItemViewModel> lands;
        private bool isRefreshing;
        private string filter;
        #endregion

        #region Propierties
        public ObservableCollection<LandItemViewModel> Lands
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

        public bool IsRefreshing
        {
            get
            {
                return this.isRefreshing;
            }
            set
            {
                SetValue(ref this.isRefreshing, value);
            }
        }

        public string Filter
        {
            get
            {
                return this.filter;
            }
            set
            {
                SetValue(ref this.filter, value);
                this.Search();
            }
        }
        #endregion

        #region Contructors
        public LandsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadLans();
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadLans);
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }
        #endregion

        #region Methods
        
        private async void LoadLans()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();

            if(!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                   "Error",
                   connection.Message,
                   "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                this.IsRefreshing = false;
                return;
            }

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
                this.IsRefreshing = false;
                return;
            }

            MainViewModel.GetInstance().LandsList = (List<Land>)response.Result;
            this.Lands = new ObservableCollection<LandItemViewModel>(
                this.ToLandItemViewModel());
            this.IsRefreshing = false;
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(Filter))
            {
                this.Lands = new ObservableCollection<LandItemViewModel>(
                     this.ToLandItemViewModel());
            }
            else
            {
                this.Lands = new ObservableCollection<LandItemViewModel>(
                     this.ToLandItemViewModel().Where(
                        l => l.Name.ToLower().Contains(this.Filter.ToLower()) ||
                             l.Capital.ToLower().Contains(this.Filter.ToLower())));
            }
        }

        private IEnumerable<LandItemViewModel> ToLandItemViewModel()
        {
            return MainViewModel.GetInstance().LandsList.Select(l => new LandItemViewModel
            {
                Alpha2Code = l.Alpha2Code,
                Alpha3Code = l.Alpha3Code,
                AltSpellings = l.AltSpellings,
                Area = l.Area,
                Borders = l.Borders,
                CallingCodes = l.CallingCodes,
                Capital = l.Capital,
                Cioc = l.Cioc,
                Currencies = l.Currencies,
                Demonym = l.Demonym,
                Flag = l.Flag,
                Gini = l.Gini,
                Languages = l.Languages,
                Latlng = l.Latlng,
                Name = l.Name,
                NativeName = l.NativeName,
                NumericCode = l.NumericCode,
                Population = l.Population,
                Region = l.Region,
                RegionalBlocs = l.RegionalBlocs,
                Subregion = l.Subregion,
                Timezones = l.Timezones,
                TopLevelDomain = l.TopLevelDomain,
                Translations = l.Translations
            });
        }
        #endregion
    }
}

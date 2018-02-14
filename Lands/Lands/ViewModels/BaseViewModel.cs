namespace Lands.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.CompilerServices;
    using System.ComponentModel;

    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(propertyName)));
        }

        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
            {
                return;
            }

            backingField = value;
            OnPropertyChanged(propertyName);
        }
    }
}

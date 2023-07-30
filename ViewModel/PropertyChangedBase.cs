using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace V2Grupp.ViewModel
{
    public class PropertyChangedBase : INotifyPropertyChanged
    {
        protected virtual void OnPropertyChanged(string Name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

        protected bool SetProperty<T>(ref T store, T value,
                                 [CallerMemberName] string prop = "")
        {
            if (EqualityComparer<T>.Default.Equals(store, value))
                return false;

            store = value;
            OnPropertyChanged(prop);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CommonCSharp.Models
{
    [Serializable]
    public abstract class BindableBase : INotifyPropertyChanged
    {
        #region < OnPropertyChanged >

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string pPropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(pPropertyName));
        }
        protected void SetProperty<T>(ref T pField, T pValue, [CallerMemberName] string pPropertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(pField, pValue))
            {
                pField = pValue;
                OnPropertyChanged(pPropertyName);
            }
        }
        #endregion
    }
}

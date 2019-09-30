using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CommonCSharp.Models
{
    [Serializable]
    public abstract class BindableBase : INotifyPropertyChanged
    {
        private Dictionary<string, object> dict = new Dictionary<string, object>();

        private static string GetProperyName(string methodName)
        {
            if (methodName.StartsWith("get_") || methodName.StartsWith("set_") ||
                methodName.StartsWith("put_"))
            {
                return methodName.Substring("get_".Length);
            }
            throw new Exception(methodName + " not a method of Property");
        }

        protected virtual void OnPropertyChanged(string pPropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(pPropertyName));
        }
        protected void SetValue(object value)
        {
            string propertyName = GetProperyName(new StackTrace(true).GetFrame(1).GetMethod().Name);

            if (dict.ContainsKey(propertyName))
            {
                if (dict[propertyName] != value)
                {
                    if (dict[propertyName] == null || !dict[propertyName].Equals(value))
                    {
                        dict[propertyName] = value;
                        OnPropertyChanged(propertyName);
                    }
                }
            }
            else
            {
                dict.Add(propertyName, value);
                OnPropertyChanged(propertyName);
            }
        }

        protected object GetValue()
        {
            string propertyName = GetProperyName(new StackTrace(true).GetFrame(1).GetMethod().Name);
            if (dict.ContainsKey(propertyName))
            {
                return dict[propertyName];
            }
            else
            {
                return null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

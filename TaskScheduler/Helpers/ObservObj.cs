using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaskScheduler
{
    public class ObservObj : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Better way to send property change
        /// </summary>
        /// <typeparam name="T">Generic Type Parameter</typeparam>
        /// <param name="proprerty">Property what will change</param>
        /// <param name="value">New value</param>
        /// <param name="propertyName">Allows you to obtain the method or property name of the caller to the method</param>
        public void Set<T>(ref T proprerty, T value, [CallerMemberName] String propertyName = "")
        {
            proprerty = value;
            var handler = PropertyChanged;
            if (handler != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

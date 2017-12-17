using System;

namespace no.trainer.personal.Interfaces
{
    internal interface INotifyPropertyChanged
    {
        event EventHandler PropertyChanged;
    }
}
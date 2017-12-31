using System;

namespace no.trainer.personal.Interfaces
{
    interface ISelectDate
    {
        void SelectDate(DateTime dateTime,Action<DateTime> callback);
    }
}
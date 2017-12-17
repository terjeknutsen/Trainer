using System;
using System.Collections.Generic;

namespace ViewModels
{
    public sealed class MeasurementViewModel : IViewModel
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Unit { get; set; }
        public Dictionary<DateTime, double> Values { get; set; } = new Dictionary<DateTime, double>(); 
    }
}

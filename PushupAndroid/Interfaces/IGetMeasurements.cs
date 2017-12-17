using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace no.trainer.personal.Interfaces
{
    public interface IGetMeasurements
    {
        Task<IEnumerable<MeasurementViewModel>> Measurements { get; }
    }
}
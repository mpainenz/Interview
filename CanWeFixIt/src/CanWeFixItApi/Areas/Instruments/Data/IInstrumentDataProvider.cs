using System.Collections.Generic;
using System.Threading.Tasks;

using CanWeFixItService;

namespace CanWeFixItApi.Areas.Instruments.Data
{
    public interface IInstrumentDataProvider
    {
        Task<IEnumerable<Instrument>> GetInstruments();
    }
}

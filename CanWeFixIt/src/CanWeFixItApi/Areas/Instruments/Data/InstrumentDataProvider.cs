using System.Collections.Generic;
using System.Threading.Tasks;

using CanWeFixItService;

namespace CanWeFixItApi.Areas.Instruments.Data
{
    public class InstrumentDataProvider: IInstrumentDataProvider
    {

        IDatabaseService _db;

        public InstrumentDataProvider(IDatabaseService db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Instrument>> GetInstruments()
        {
            return await _db.Instruments();
        }
    }

}

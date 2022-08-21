using System.Collections.Generic;
using System.Threading.Tasks;

using CanWeFixItService;

namespace CanWeFixItApi.Areas.MarketData.Data
{
    public class MarketDataDataProvider: IMarketDataDataProvider
    {

        IDatabaseService _db;

        public MarketDataDataProvider(IDatabaseService db)
        {
            _db = db;
        }

        public async Task<IEnumerable<CanWeFixItService.MarketData>> GetMarketData()
        {
            return await _db.MarketData();
        }
    }

}

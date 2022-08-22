using System.Collections.Generic;
using System.Threading.Tasks;

using CanWeFixItService;

namespace CanWeFixItApi.Areas.MarketValuation.Data
{
    public class MarketValuationDataProvider: IMarketValuationDataProvider
    {

        IDatabaseService _db;

        public MarketValuationDataProvider(IDatabaseService db)
        {
            _db = db;
        }

        public async Task<IEnumerable<CanWeFixItService.MarketValuation>> GetMarketValuation()
        {
            return await _db.MarketValuation();
        }
    }

}

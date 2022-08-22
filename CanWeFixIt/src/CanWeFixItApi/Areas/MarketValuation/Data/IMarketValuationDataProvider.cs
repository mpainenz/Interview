using System.Collections.Generic;
using System.Threading.Tasks;

using CanWeFixItService;

namespace CanWeFixItApi.Areas.MarketValuation.Data
{
    public interface IMarketValuationDataProvider
    {
        Task<IEnumerable<CanWeFixItService.MarketValuation>> GetMarketValuation();
    }
}

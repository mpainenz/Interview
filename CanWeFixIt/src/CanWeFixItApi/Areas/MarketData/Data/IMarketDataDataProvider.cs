using System.Collections.Generic;
using System.Threading.Tasks;

using CanWeFixItService;

namespace CanWeFixItApi.Areas.MarketData.Data
{
    public interface IMarketDataDataProvider
    {
        Task<IEnumerable<CanWeFixItService.MarketData>> GetMarketData();
    }
}

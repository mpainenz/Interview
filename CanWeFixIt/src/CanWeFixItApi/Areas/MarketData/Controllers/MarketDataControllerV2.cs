using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using CanWeFixItApi.Areas.MarketData.Data;
using CanWeFixItService;

namespace CanWeFixItApi.Areas.MarketData.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("v{version:apiVersion}/marketdata")]
    public class MarketDataControllerV2 : ControllerBase
    {
        private readonly IMarketDataDataProvider _dp;
        
        public MarketDataControllerV2(IMarketDataDataProvider dp)
        {
            _dp = dp;
        }
        
        [HttpGet]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<IEnumerable<CanWeFixItService.MarketData>>> Get()
        {   
            var marketData = await _dp.GetMarketData();
            return Ok(marketData);
        }
    }
}
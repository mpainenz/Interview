using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using CanWeFixItApi.Areas.MarketData.Data;
using CanWeFixItService;

namespace CanWeFixItApi.Areas.MarketData.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/marketdata")]
    public class MarketDataControllerV1 : ControllerBase
    {
        private readonly IMarketDataDataProvider _dp;
        public MarketDataControllerV1(IMarketDataDataProvider dp)
        {
            _dp = dp;
        }
        
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<CanWeFixItService.MarketDataDto>>> Get()
        {   
            var marketData = await _dp.GetMarketData();
            return Ok(marketData);
        }
    }
}
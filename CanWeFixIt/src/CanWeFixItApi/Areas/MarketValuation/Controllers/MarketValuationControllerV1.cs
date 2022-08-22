using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using CanWeFixItApi.Areas.MarketValuation.Data;
using CanWeFixItService;

namespace CanWeFixItApi.Areas.MarketValuation.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/valuations")]
    public class MarketValuationController : ControllerBase
    {

        private readonly IMarketValuationDataProvider _dp;
        
        public MarketValuationController(IMarketValuationDataProvider dp)
        {
            _dp = dp;
        }
        
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<CanWeFixItService.MarketValuation>>> Get()
        {   
            var marketValuation = await _dp.GetMarketValuation();
            return Ok(marketValuation);
        }
    }
}
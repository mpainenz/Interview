using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using CanWeFixItApi.Areas.Instruments.Data;
using CanWeFixItService;

namespace CanWeFixItApi.Areas.Instruments.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/instruments")]
    public class InstrumentController : ControllerBase
    {

        private readonly IInstrumentDataProvider _dp;
        
        public InstrumentController(IInstrumentDataProvider dp)
        {
            _dp = dp;
        }
        
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<Instrument>>> Get()
        {   
            var instruments = await _dp.GetInstruments();
            return Ok(instruments);
        }
    }
}
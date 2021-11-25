using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using weekly.Models;
using weekly.Services;

namespace weekly.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GubiController : ControllerBase
    {
        private readonly GubiService _gubiService;
        public GubiController(GubiService gubiService)
        {
            _gubiService = gubiService;
        }

        [HttpGet]
        public ActionResult<List<Gubi>> Get() =>
           _gubiService.Get();

        [HttpGet("{id:length(24)}", Name = "GetGubi")]
        public ActionResult<Gubi> Get(string id)
        {
            var gubi = _gubiService.Get(id);

            if (gubi == null)
            {
                return NotFound();
            }

            return gubi;
        }

        [HttpPost]
        public ActionResult<Gubi> Create(Gubi gubi)
        {
            _gubiService.Create(gubi);

            return CreatedAtRoute("GetGubi", new { id = gubi.Id.ToString() }, gubi);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Gubi gubiIn)
        {
            var gubi = _gubiService.Get(id);

            if (gubi == null)
            {
                return NotFound();
            }

            _gubiService.Update(id, gubiIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var gubi = _gubiService.Get(id);

            if (gubi == null)
            {
                return NotFound();
            }

            _gubiService.Remove(gubi.Id);

            return NoContent();
        }
    }
}

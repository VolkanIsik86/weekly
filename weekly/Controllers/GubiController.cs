using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult<Gubi> Get()
        {
            Gubi gubi = _gubiService.Get().First();

            if (gubi == null)
            {
                return NotFound();
            }

            return gubi;
        }
           

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
        public ActionResult<Gubi> Create()
        {
            Gubi gubi = new Gubi();
            gubi.Name = "time";
            long unixTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            gubi.Timestamp = unixTimestamp + (60*60*24*3);
            _gubiService.Create(gubi);

            return CreatedAtRoute("GetGubi", new { id = gubi.Id.ToString() }, gubi);
        }

        [HttpPut("{name:length(4)}")]
        public IActionResult Update(string name)
        {
            List<Gubi> gubi = _gubiService.Get();

            if (gubi == null)
            {
                return NotFound();
            }

            foreach (Gubi item in gubi)
            {
                if (item.Name.Contains(name))
                {
                    Gubi gubiIn = new Gubi();
                    gubiIn.Name = item.Name;
                    gubiIn.Id = item.Id;
                    long unixTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
                    gubiIn.Timestamp = unixTimestamp + (60 * 60 * 24 * 3);
                    _gubiService.Update(gubiIn.Id, gubiIn);
                }
            }

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

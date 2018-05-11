using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyManagement.Models;

/** * @author Umair Naeem */
namespace PropertyManagementApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Properties")]
    public class PropertiesController : Controller
    {

        private readonly PMContext _context;

        public PropertiesController(PMContext context)
        {
            _context = context;

        }

        // GET api/Properties
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Property>), 200)]
        public IActionResult Get()
        {

            return Ok(_context.Properties.ToList());
        }

        // GET api/Properties/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Property), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
            var property = _context.Properties.Find(id);
            if (property == null)
            {
                return NotFound();
            }
            return  Ok(property);
        }

        // POST api/Properties
        [HttpPost]
        [ProducesResponseType(typeof(Property), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody]Property property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.AddAsync(property);

            return CreatedAtAction(nameof(GetById),
                new { id = property.ID }, property);
        }

        // PUT api/Properties/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Property), 200)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody]Property property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _context.Properties.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            _context.Update<Property>(property);

            return CreatedAtAction(nameof(GetById),
                new { id = property.ID }, property);

        }

        // DELETE api/Properties/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var property = _context.Properties.Find(id);
            if (property == null)
            {
                return NotFound();
            }

            _context.Remove<Property>(property);

            return new NoContentResult();
        }
    }
}

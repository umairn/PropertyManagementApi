using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyManagement.Models;

namespace PropertyManagementApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Categories")]
    public class CategoriesController : Controller
    {
        private readonly PMContext _context;

        public CategoriesController(PMContext context)
        {
            _context = context;

        }

        // GET api/Properties
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Category>), 200)]
        public IActionResult Get()
        {

            return Ok(_context.Properties.ToList());
        }

        // GET api/Properties/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Category), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST api/Properties
        [HttpPost]
        [ProducesResponseType(typeof(Category), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody]Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.AddAsync(category);

            return CreatedAtAction(nameof(GetById),
                new { id = category.ID }, category);
        }

        // PUT api/Properties/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Category), 200)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody]Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _context.Categories.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            _context.Update<Category>(category);

            return CreatedAtAction(nameof(GetById),
                new { id = category.ID }, category);

        }

        // DELETE api/Properties/5
        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Remove<Category>(category);

            return new NoContentResult();
        }
    }
}
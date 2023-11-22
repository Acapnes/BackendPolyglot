using API_MSSQL.Data;
using API_MSSQL.Models;
using API_MSSQL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API_MSSQL.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class EntiriesController : Controller
    {

        private readonly IRepository<Entry> _entryIRepository;
        private readonly EntriesRepository _entryRepository;

        public EntiriesController(IRepository<Entry> entryIRepository, EntriesRepository entryRepository)
        {
            _entryIRepository = entryIRepository;
            _entryRepository = entryRepository;
        }

        [HttpGet]
        public IEnumerable<Entry> GetAllEntries()
        {
            return _entryIRepository.GetAll();
        }

        [HttpGet("/[controller]/author")]
        public IEnumerable<Entry> GetAllEntriesWithAuthor()
        {
            return _entryRepository.GetAllWithAuthor();
        }

        [HttpGet("{id}")]
        public IActionResult GetEntry(int id)
        {
            var entry = _entryIRepository.Get(id);
            if (entry == null)
            {
                return NotFound();
            }
            return Ok(entry);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEntry([FromBody] Entry entry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _entryIRepository.Add(entry);

            return CreatedAtAction("GetEntry", new { id = entry.Id }, entry);
        }

        [HttpPut]
        public IActionResult UpdateEntry(int id, [FromBody] Entry entry)
        {
            if (entry == null || entry.Id != id)
            {
                return BadRequest();
            }
            var existingEntry = _entryIRepository.Get(id);
            if (existingEntry == null)
            {
                return NotFound();
            }
            _entryIRepository.Update(entry);
            return Ok(entry);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEntry(int id)
        {
            var entry = _entryIRepository.Get(id);
            if (entry == null)
            {
                return NotFound();
            }
            _entryIRepository.Delete(entry);
            return NoContent();
        }
    }
}


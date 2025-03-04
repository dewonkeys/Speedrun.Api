using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Speedrun.Dtos;
using Speedrun.Services.SRSystemService;

namespace Speedrun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SRSystemController : ControllerBase
    {
        private readonly ISRSystemService _srSystemService;

        public SRSystemController(ISRSystemService srSystemService)
        {
            _srSystemService = srSystemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SRSystemDto>>> GetSRSystems()
        {
            var srSystems = await _srSystemService.GetAllSRSystems();
            return Ok(srSystems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SRSystemDto>> GetSRSystem(int id)
        {
            var srSystem = await _srSystemService.GetSRSystemById(id);
            if (srSystem == null) return NotFound();
            return Ok(srSystem);
        }

        [HttpPost]
        public async Task<ActionResult<SRSystemDto>> CreateSRSystem(SRSystemDto srSystemDto)
        {
            var createdSRSystem = await _srSystemService.CreateSRSystem(srSystemDto);
            return CreatedAtAction(nameof(GetSRSystem), new { id = createdSRSystem.Id }, createdSRSystem);
        }

        [HttpPut("{srSystemId}")]
        public async Task<IActionResult> UpdateSRSystem(int srSystemId, SRSystemDto srSystemDto)
        {
            var updatedSRSystem = await _srSystemService.UpdateSRSystem(srSystemId, srSystemDto);
            if (updatedSRSystem == null) return NotFound();
            return Ok(updatedSRSystem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSRSystem(int id)
        {
            var deleted = await _srSystemService.DeleteSRSystem(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Speedrun.Dtos;
using Speedrun.Services.SRStrainService;

namespace Speedrun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SRStrainController : ControllerBase
    {
        private readonly ISRStrainService _srStrainService;

        public SRStrainController(ISRStrainService srStrainService)
        {
            _srStrainService = srStrainService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SRStrainDto>>> GetSRStrains()
        {
            var srStrains = await _srStrainService.GetAllSRStrains();
            return Ok(srStrains);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SRStrainDto>> GetSRStrain(int id)
        {
            var srStrain = await _srStrainService.GetSRStrainById(id);
            if (srStrain == null) return NotFound();
            return Ok(srStrain);
        }
        [HttpGet("srsystem/{srSystemId}")]
        public async Task<ActionResult<IEnumerable<SRStrainDto>>> GetStrainsBySystemId(int srSystemId)
        {
            var srStrains = await _srStrainService.GetSRStrainsBySRSystemId(srSystemId);
            return Ok(srStrains);
        }

        [HttpPost]
        public async Task<ActionResult<SRStrainDto>> CreateSRStrain(SRStrainDto srStrainDto)
        {
            try
            {
                var createdSRStrain = await _srStrainService.CreateSRStrain(srStrainDto);
                return CreatedAtAction(nameof(GetSRStrain), new { id = createdSRStrain.Id }, createdSRStrain);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSRStrain(int id, SRStrainDto srStrainDto)
        {
            var updatedSRStrain = await _srStrainService.UpdateSRStrain(id, srStrainDto);
            if (updatedSRStrain == null) return NotFound();
            return Ok(updatedSRStrain);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSRStrain(int id)
        {
            var deleted = await _srStrainService.DeleteSRStrain(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
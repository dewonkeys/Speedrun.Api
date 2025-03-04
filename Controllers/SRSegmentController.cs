using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Speedrun.Dtos;
using Speedrun.Services.SRSegmentService;

namespace Speedrun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SRSegmentController : ControllerBase
    {
        private readonly ISRSegmentService _srSegmentService;

        public SRSegmentController(ISRSegmentService srSegmentService)
        {
            _srSegmentService = srSegmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SRSegmentDto>>> GetSRSegments()
        {
            var srSegments = await _srSegmentService.GetAllSRSegments();
            return Ok(srSegments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SRSegmentDto>> GetSRSegment(int id)
        {
            var srSegment = await _srSegmentService.GetSRSegmentById(id);
            if (srSegment == null) return NotFound();
            return Ok(srSegment);
        }
        [HttpGet("srstrain/{srStrainId}")]
        public async Task<ActionResult<IEnumerable<SRSegmentDto>>> GetSRSegmentBySRStrainId(int srStrainId)
        {
            try
            {
                var srSegments = await _srSegmentService.GetSRSegmentBySRStrainId(srStrainId);
                return Ok(srSegments);
            }
            catch (ArgumentException ex)
            {
                // Handle the specific exception for non-existent strain
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<SRSegmentDto>> CreateSRSegment(SRSegmentDto srSegmentDto)
        {
            try
            {
                var createdSRSegment = await _srSegmentService.CreateSRSegment(srSegmentDto);
                return CreatedAtAction(nameof(GetSRSegment), new { id = createdSRSegment.Id }, createdSRSegment);

            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSRSegment(int srSegmentId, SRSegmentDto srSegmentDto)
        {
            var updatedSRSegment = await _srSegmentService.UpdateSRSegment(srSegmentId, srSegmentDto);
            if (updatedSRSegment == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSRSegment(int id)
        {
            var deleted = await _srSegmentService.DeleteSRSegment(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Speedrun.Dtos;
using Speedrun.Models;

namespace Speedrun.Services.SRSegmentService
{
    public class SRSegmentService : ISRSegmentService
    {
        private readonly SRDbContext _context;

        public SRSegmentService(SRDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SRSegmentDto>> GetAllSRSegmentsAsync()
        {
            var srSegments = await _context.SRSegment.ToListAsync();
            return srSegments.Select(s => new SRSegmentDto { Id = s.Id, Name = s.Name });
        }

        public async Task<SRSegmentDto> GetSRSegmentByIdAsync(int id)
        {
            var srSegment = await _context.SRSegment.FindAsync(id);
            if (srSegment == null) return null;
            return new SRSegmentDto { Id = srSegment.Id, Name = srSegment.Name };
        }

        public async Task<SRSegmentDto> CreateSRSegmentAsync(SRSegmentDto srSegmentDto)
        {
            var srSegment = new SRSegment { Name = srSegmentDto.Name };
            _context.SRSegment.Add(srSegment);
            await _context.SaveChangesAsync();
            srSegmentDto.Id = srSegment.Id;
            return srSegmentDto;
        }

        public async Task<SRSegmentDto> UpdateSRSegmentAsync(int id, SRSegmentDto srSegmentDto)
        {
            var srSegment = await _context.SRSegment.FindAsync(id);
            if (srSegment == null) return null;
            srSegment.Name = srSegmentDto.Name;
            await _context.SaveChangesAsync();
            return srSegmentDto;
        }

        public async Task<bool> DeleteSRSegmentAsync(int id)
        {
            var srSegment = await _context.SRSegment.FindAsync(id);
            if (srSegment == null) return false;
            _context.SRSegment.Remove(srSegment);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<IEnumerable<SRSegmentDto>> GetAllSRSegments()
        {
            return GetAllSRSegmentsAsync();
        }

        public Task<SRSegmentDto> GetSRSegmentById(int id)
        {
            return GetSRSegmentByIdAsync(id);
        }

        public Task<SRSegmentDto> CreateSRSegment(SRSegmentDto srSegmentDto)
        {
            return CreateSRSegmentAsync(srSegmentDto);
        }

        public Task<SRSegmentDto> UpdateSRSegment(int id, SRSegmentDto srSegmentDto)
        {
            return UpdateSRSegmentAsync(id, srSegmentDto);
        }

        public Task<bool> DeleteSRSegment(int id)
        {
            return DeleteSRSegmentAsync(id);
        }
    }
}
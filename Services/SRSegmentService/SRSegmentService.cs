using System;
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
            return srSegments.Select(s => new SRSegmentDto { Id = s.Id, Name = s.Name, MsDuration = s.MsDuration, SRStrainId = s.SRStrainId });
        }

        public async Task<SRSegmentDto> GetSRSegmentByIdAsync(int srSegmentId)
        {
            var srSegment = await _context.SRSegment.FindAsync(srSegmentId);
            if (srSegment == null) return null;
            return new SRSegmentDto { Id = srSegment.Id, Name = srSegment.Name, MsDuration = srSegment.MsDuration, SRStrainId = srSegment.SRStrainId };
        }
        public async Task<IEnumerable<SRSegmentDto>> GetSRSegmentBySRStrainIdAsync(int srStrainId)
        {
            // First check if the SRStrain exists
            var srStrain = await _context.SRStrain.FindAsync(srStrainId);
            if (srStrain == null)
            {
                throw new ArgumentException($"SRStrain with ID {srStrainId} does not exist");
            }

            // If SRStrain exists, proceed to get segments
            var srSegments = await _context.SRSegment.Where(s => s.SRStrainId == srStrainId).ToListAsync();
            return srSegments.Select(s => new SRSegmentDto
            {
                Id = s.Id,
                Name = s.Name,
                SRStrainId = s.SRStrainId,
                MsDuration = s.MsDuration
            });
        }


        public async Task<SRSegmentDto> CreateSRSegmentAsync(SRSegmentDto srSegmentDto)
        {
            // First check if the SRStrain exists
            var srStrain = await _context.SRStrain.FindAsync(srSegmentDto.SRStrainId);
            if (srStrain == null)
            {
                throw new ArgumentException($"SRStrain with ID {srSegmentDto.SRStrainId} does not exist");
            }

            // If SRStrain exists, proceed to create segment
            var srSegment = new SRSegment { Name = srSegmentDto.Name, MsDuration = srSegmentDto.MsDuration, SRStrainId = srSegmentDto.SRStrainId };
            _context.SRSegment.Add(srSegment);
            await _context.SaveChangesAsync();
            srSegmentDto.Id = srSegment.Id;
            return srSegmentDto;
        }

        public async Task<SRSegmentDto> UpdateSRSegmentAsync(int srSegmentId, SRSegmentDto srSegmentDto)
        {
            var srSegment = await _context.SRSegment.FindAsync(srSegmentId);
            if (srSegment == null) return null;
            srSegment.Name = srSegmentDto.Name ?? srSegment.Name;
            srSegment.MsDuration = srSegmentDto.MsDuration;
            await _context.SaveChangesAsync();
            return srSegmentDto;
        }

        public async Task<bool> DeleteSRSegmentAsync(int srSegmentId)
        {
            var srSegment = await _context.SRSegment.FindAsync(srSegmentId);
            if (srSegment == null) return false;
            _context.SRSegment.Remove(srSegment);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<IEnumerable<SRSegmentDto>> GetAllSRSegments()
        {
            return GetAllSRSegmentsAsync();
        }

        public Task<SRSegmentDto> GetSRSegmentById(int srSegmentId)
        {
            return GetSRSegmentByIdAsync(srSegmentId);
        }
        public Task<IEnumerable<SRSegmentDto>> GetSRSegmentBySRStrainId(int srStrainId)
        {
            return GetSRSegmentBySRStrainIdAsync(srStrainId);
        }

        public Task<SRSegmentDto> CreateSRSegment(SRSegmentDto srSegmentDto)
        {
            return CreateSRSegmentAsync(srSegmentDto);
        }

        public Task<SRSegmentDto> UpdateSRSegment(int srSegmentId, SRSegmentDto srSegmentDto)
        {
            return UpdateSRSegmentAsync(srSegmentId, srSegmentDto);
        }

        public Task<bool> DeleteSRSegment(int srSegmentId)
        {
            return DeleteSRSegmentAsync(srSegmentId);
        }
    }
}
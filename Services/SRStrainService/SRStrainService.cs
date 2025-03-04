using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Speedrun.Dtos;
using Speedrun.Models;

namespace Speedrun.Services.SRStrainService
{
    public class SRStrainService : ISRStrainService
    {
        private readonly SRDbContext _context;

        public SRStrainService(SRDbContext context)
        {
            _context = context;
        }

        public async Task<SRStrainDto> CreateSRStrainAsync(SRStrainDto srStrainDto)
        {
            // Check if the SRSystemId exists
            var srSystem = await _context.SRSystem.FindAsync(srStrainDto.SRSystemId);
            if (srSystem == null)
            {
                throw new ArgumentException("Invalid SRSystemId");
            }

            var srStrain = new SRStrain { Name = srStrainDto.Name, SRSystemId = srStrainDto.SRSystemId };
            _context.SRStrain.Add(srStrain);
            await _context.SaveChangesAsync();
            srStrainDto.Id = srStrain.Id;
            return srStrainDto;
        }

        public async Task<IEnumerable<SRStrainDto>> GetAllSRStrainsAsync()
        {
            var srStrains = await _context.SRStrain.ToListAsync();
            return srStrains.Select(s => new SRStrainDto { Id = s.Id, Name = s.Name, SRSystemId = s.SRSystemId });
        }

        public async Task<SRStrainDto> GetSRStrainByIdAsync(int id)
        {
            var srStrain = await _context.SRStrain.FindAsync(id);
            if (srStrain == null) return null;
            return new SRStrainDto { Id = srStrain.Id, Name = srStrain.Name, SRSystemId = srStrain.SRSystemId };
        }

        public async Task<IEnumerable<SRStrainDto>> GetSRStrainsBySRSystemIdAsync(int srSystemId)
        {
            var srStrains = await _context.SRStrain.Where(s => s.SRSystemId == srSystemId).ToListAsync();
            return srStrains.Select(s => new SRStrainDto { Id = s.Id, Name = s.Name, SRSystemId = s.SRSystemId });
        }

        public async Task<SRStrainDto> UpdateSRStrainAsync(int id, SRStrainDto srStrainDto)
        {
            var srStrain = await _context.SRStrain.FindAsync(id);
            if (srStrain == null) return null;
            srStrain.Name = srStrainDto.Name;
            await _context.SaveChangesAsync();

            // Dto doesn't have an Id, so we need to set it here before returning
            srStrainDto.Id = srStrain.Id;
            srStrainDto.SRSystemId = srStrain.SRSystemId;

            return srStrainDto;
        }

        public async Task<bool> DeleteSRStrainAsync(int id)
        {
            var srStrain = await _context.SRStrain.FindAsync(id);
            if (srStrain == null) return false;
            _context.SRStrain.Remove(srStrain);
            await _context.SaveChangesAsync();
            return true;
        }

        // Interface implementations
        public Task<SRStrainDto> CreateSRStrain(SRStrainDto srStrainDto) { return CreateSRStrainAsync(srStrainDto); }
        public Task<IEnumerable<SRStrainDto>> GetAllSRStrains() { return GetAllSRStrainsAsync(); }
        public Task<SRStrainDto> GetSRStrainById(int id) { return GetSRStrainByIdAsync(id); }
        public Task<IEnumerable<SRStrainDto>> GetSRStrainsBySRSystemId(int srSystemId) { return GetSRStrainsBySRSystemIdAsync(srSystemId); }
        public Task<SRStrainDto> UpdateSRStrain(int id, SRStrainDto srStrainDto) { return UpdateSRStrainAsync(id, srStrainDto); }
        public Task<bool> DeleteSRStrain(int id) { return DeleteSRStrainAsync(id); }
    }
}
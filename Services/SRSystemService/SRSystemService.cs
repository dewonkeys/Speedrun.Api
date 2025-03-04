using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Speedrun.Dtos;
using Speedrun.Models;

namespace Speedrun.Services.SRSystemService
{
    public class SRSystemService : ISRSystemService
    {
        private readonly SRDbContext _context;

        public SRSystemService(SRDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SRSystemDto>> GetAllSRSystemsAsync()
        {
            var srSystems = await _context.SRSystem.ToListAsync();
            return srSystems.Select(s => new SRSystemDto { Id = s.Id, Name = s.Name });
        }

        public async Task<SRSystemDto> GetSRSystemByIdAsync(int id)
        {
            var srSystem = await _context.SRSystem.FindAsync(id);
            if (srSystem == null) return null;
            return new SRSystemDto { Id = srSystem.Id, Name = srSystem.Name };
        }

        public async Task<SRSystemDto> CreateSRSystemAsync(SRSystemDto srSystemDto)
        {
            var srSystem = new SRSystem { Name = srSystemDto.Name };
            _context.SRSystem.Add(srSystem);
            await _context.SaveChangesAsync();
            srSystemDto.Id = srSystem.Id;
            return srSystemDto;
        }

        public async Task<SRSystemDto> UpdateSRSystemAsync(int srSystemId, SRSystemDto srSystemDto)
        {
            var srSystem = await _context.SRSystem.FindAsync(srSystemId);
            if (srSystem == null) return null;
            srSystem.Name = srSystemDto.Name;

            await _context.SaveChangesAsync();

            // Dto doesn't have an Id, so we need to set it here before returning
            srSystemDto.Id = srSystem.Id;
            return srSystemDto;
        }

        public async Task<bool> DeleteSRSystemAsync(int id)
        {
            var srSystem = await _context.SRSystem.FindAsync(id);
            if (srSystem == null) return false;
            _context.SRSystem.Remove(srSystem);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<IEnumerable<SRSystemDto>> GetAllSRSystems()
        {
            return GetAllSRSystemsAsync();
        }

        public Task<SRSystemDto> GetSRSystemById(int id)
        {
            return GetSRSystemByIdAsync(id);
        }

        public Task<SRSystemDto> CreateSRSystem(SRSystemDto srSystemDto)
        {
            return CreateSRSystemAsync(srSystemDto);
        }

        public Task<SRSystemDto> UpdateSRSystem(int id, SRSystemDto srSystemDto)
        {
            return UpdateSRSystemAsync(id, srSystemDto);
        }

        public Task<bool> DeleteSRSystem(int id)
        {
            return DeleteSRSystemAsync(id);
        }
    }
}
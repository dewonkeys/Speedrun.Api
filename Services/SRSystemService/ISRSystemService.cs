using System.Collections.Generic;
using System.Threading.Tasks;
using Speedrun.Dtos;

namespace Speedrun.Services.SRSystemService
{
    public interface ISRSystemService
    {
        public Task<IEnumerable<SRSystemDto>> GetAllSRSystems();
        public Task<SRSystemDto> GetSRSystemById(int id);
        public Task<SRSystemDto> CreateSRSystem(SRSystemDto srSystemDto);
        public Task<SRSystemDto> UpdateSRSystem(int id, SRSystemDto srSystemDto);
        public Task<bool> DeleteSRSystem(int id);
    }
}
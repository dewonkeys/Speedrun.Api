using System.Collections.Generic;
using System.Threading.Tasks;
using Speedrun.Dtos;

namespace Speedrun.Services.SRStrainService
{
    public interface ISRStrainService
    {
        Task<IEnumerable<SRStrainDto>> GetAllSRStrains();
        Task<SRStrainDto> GetSRStrainById(int id);
        Task<SRStrainDto> CreateSRStrain(SRStrainDto srStrainDto);
        Task<SRStrainDto> UpdateSRStrain(int id, SRStrainDto srStrainDto);
        Task<bool> DeleteSRStrain(int id);
        Task<IEnumerable<SRStrainDto>> GetSRStrainsBySRSystemId(int srSystemId);

    }
}
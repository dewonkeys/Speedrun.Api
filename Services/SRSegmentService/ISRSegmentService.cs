using System.Collections.Generic;
using System.Threading.Tasks;
using Speedrun.Dtos;

namespace Speedrun.Services.SRSegmentService
{
    public interface ISRSegmentService
    {
        Task<IEnumerable<SRSegmentDto>> GetAllSRSegments();
        Task<SRSegmentDto> GetSRSegmentById(int id);
        Task<SRSegmentDto> CreateSRSegment(SRSegmentDto srSegmentDto);
        Task<SRSegmentDto> UpdateSRSegment(int id, SRSegmentDto srSegmentDto);
        Task<bool> DeleteSRSegment(int id);
    }
}
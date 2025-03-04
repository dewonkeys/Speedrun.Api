using System.Collections.Generic;
using System.Threading.Tasks;
using Speedrun.Dtos;

namespace Speedrun.Services.SRSegmentService
{
    public interface ISRSegmentService
    {
        Task<IEnumerable<SRSegmentDto>> GetAllSRSegments();
        Task<SRSegmentDto> GetSRSegmentById(int srSegmentId);
        Task<IEnumerable<SRSegmentDto>> GetSRSegmentBySRStrainId(int srStrainId);
        Task<SRSegmentDto> CreateSRSegment(SRSegmentDto srSegmentDto);
        Task<SRSegmentDto> UpdateSRSegment(int srSegmentId, SRSegmentDto srSegmentDto);
        Task<bool> DeleteSRSegment(int srSegmentId);
    }
}
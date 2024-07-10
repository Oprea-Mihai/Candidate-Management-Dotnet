using CandidateManagement.Repository.Entities;

namespace CandidateManagement.Service.Interfaces
{
    public interface ICandidateService
    {
        Task AddOrUpdateCandidateAsync(Candidate candidate);
    }
}

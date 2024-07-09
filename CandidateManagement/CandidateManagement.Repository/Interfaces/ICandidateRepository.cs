using CandidateManagement.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagement.Repository.Interfaces
{
    public interface ICandidateRepository : IBaseRepository<Candidate>
    {
        Task<Candidate> GetCandidateByEmailAsync(string email);
    }
}

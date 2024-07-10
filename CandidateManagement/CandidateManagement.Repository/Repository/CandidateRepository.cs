using CandidateManagement.Repository.Entities;
using CandidateManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagement.Repository.Repository
{
    public class CandidateRepository : BaseRepository<Candidate>, ICandidateRepository
    {
        public CandidateRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<Candidate?> GetCandidateByEmailAsync(string email)
        {
            Candidate? candidate =  await _context.Candidates.SingleOrDefaultAsync(c => c.Email == email);
            return candidate;
        }


    }
}

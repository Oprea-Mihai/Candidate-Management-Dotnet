using CandidateManagement.Repository.Entities;
using CandidateManagement.Repository.Interfaces;
using CandidateManagement.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagement.Service
{
    public class CandidateService : ICandidateService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CandidateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddOrUpdateCandidateAsync(Candidate candidate)
        {
            candidate = CorrectTimes(candidate);

            Candidate? existingCandidate = await _unitOfWork.CandidateRepository.GetCandidateByEmailAsync(candidate.Email);
            if (existingCandidate != null)
            {
                existingCandidate.FirstName = candidate.FirstName;
                existingCandidate.LastName = candidate.LastName;
                existingCandidate.PhoneNumber = candidate.PhoneNumber;
                existingCandidate.CallAvailabilityStart = candidate.CallAvailabilityStart;
                existingCandidate.CallAvailabilityEnd = candidate.CallAvailabilityEnd;
                existingCandidate.LinkedInUrl = candidate.LinkedInUrl;
                existingCandidate.GitHubUrl = candidate.GitHubUrl;
                existingCandidate.FreeTextComment = candidate.FreeTextComment;
                await _unitOfWork.CandidateRepository.UpdateAsync(existingCandidate);
            }
            else
            {
                await _unitOfWork.CandidateRepository.AddAsync(candidate);
            }
            await _unitOfWork.CompleteAsync();
        }

        ///<summary>
        ///Ensures that CallAvailabilityStart is before CallAvailabilityEnd.
        /// Swaps the times if they are not in the correct order.
        ///</summary>
        public Candidate CorrectTimes(Candidate candidate)
        {
            
            if (candidate.CallAvailabilityEnd != null && candidate.CallAvailabilityStart != null)
            {
                TimeSpan startTime = TimeSpan.Parse(candidate.CallAvailabilityStart);
                TimeSpan endTime = TimeSpan.Parse(candidate.CallAvailabilityEnd);
                if (endTime < startTime)
                {
                    string endTimeString =candidate.CallAvailabilityStart;
                    candidate.CallAvailabilityStart = candidate.CallAvailabilityEnd;
                    candidate.CallAvailabilityEnd = endTimeString;
                }
            }
            return candidate;
        }
    }
}
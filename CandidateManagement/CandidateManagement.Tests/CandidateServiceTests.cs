using CandidateManagement.Repository.Entities;
using CandidateManagement.Repository.Interfaces;
using CandidateManagement.Service.Interfaces;
using CandidateManagement.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace CandidateManagement.Tests
{

    [TestFixture]
    public class CandidateServiceTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<ICandidateRepository> _mockCandidateRepository;
        private ICandidateService _candidateService;
        private Candidate candidate;

        [SetUp]
        public void SetUp()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCandidateRepository = new Mock<ICandidateRepository>();
            _mockUnitOfWork.Setup(u => u.CandidateRepository).Returns(_mockCandidateRepository.Object);
            _candidateService = new CandidateService(_mockUnitOfWork.Object);
            candidate = new Candidate
            {
                Email = "existingcandidate@email.com",
                FirstName = "FirstName",
                LastName = "LastName",
                FreeTextComment = "Text comment",
                CallAvailabilityStart = "09:00",
                CallAvailabilityEnd = "17:00",
            };
        }

        [Test]
        public async Task AddOrUpdateCandidateAsync_AddsNewCandidate_WhenCandidateDoesNotExist()
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.CandidateRepository.
            GetCandidateByEmailAsync(candidate.Email)).ReturnsAsync((Candidate)null);

            // Act
            await _candidateService.AddOrUpdateCandidateAsync(candidate);

            // Assert
            _mockCandidateRepository.Verify(repo => repo.AddAsync(candidate), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Test]
        public async Task AddOrUpdateCandidateAsync_UpdatesExistingCandidate_WhenCandidateExists()
        {
            // Arrange
            var updatedCandidate = new Candidate
            {
                Email = "existingcandidate@email.com",
                FirstName = "Mike",
                LastName = "LastName",
                FreeTextComment = "Updated comment"
            };
            _mockUnitOfWork.Setup(uow => uow.CandidateRepository.GetCandidateByEmailAsync(candidate.Email)).ReturnsAsync(candidate);

            // Act
            await _candidateService.AddOrUpdateCandidateAsync(updatedCandidate);

            // Assert
            _mockCandidateRepository.Verify(repo => repo.UpdateAsync(It.Is<Candidate>(c =>
                c.Email == updatedCandidate.Email &&
                c.FirstName == updatedCandidate.FirstName &&
                c.LastName == updatedCandidate.LastName &&
                c.FreeTextComment == updatedCandidate.FreeTextComment
            )), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }




        [Test]
        public void CorrectTimes_SwapsTimes_WhenEndIsBeforeStart()
        {
            // Arrange
            Candidate wrongCandidate = new Candidate
            {
                Email = "existingcandidate@email.com",
                FirstName = "Mike",
                LastName = "LastName",
                FreeTextComment = "Updated comment",
                CallAvailabilityStart = "17:00",
                CallAvailabilityEnd = "09:00"
            };

            // Act
            Candidate correctedCandidate = _candidateService.CorrectTimes(wrongCandidate);

            // Assert
            Assert.That(correctedCandidate.CallAvailabilityStart, Is.EqualTo("09:00"));
            Assert.That(correctedCandidate.CallAvailabilityEnd, Is.EqualTo("17:00"));
        }

        [Test]
        public void CorrectTimes_DoesNotSwapTimes_WhenEndIsAfterStart()
        {

            // Act
            var correctedCandidate = _candidateService.CorrectTimes(candidate);

            // Assert
            Assert.That(correctedCandidate.CallAvailabilityStart, Is.EqualTo("09:00"));
            Assert.That(correctedCandidate.CallAvailabilityEnd, Is.EqualTo("17:00"));
        }
    }
}
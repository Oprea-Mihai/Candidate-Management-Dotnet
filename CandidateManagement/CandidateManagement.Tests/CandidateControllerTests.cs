using CandidateManagement.Controllers;
using CandidateManagement.Repository.Entities;
using CandidateManagement.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagement.Tests
{
    [TestFixture]
    public class CandidateControllerTests
    {
        private Mock<ICandidateService> _mockCandidateService;
        private CandidateController _candidateController;

        [SetUp]
        public void SetUp()
        {
            _mockCandidateService = new Mock<ICandidateService>();
            _candidateController = new CandidateController(_mockCandidateService.Object);
        }

        [Test]
        public async Task AddOrUpdateCandidate_ModelStateInvalid_ReturnsBadRequest()
        {
            // Arrange
            _candidateController.ModelState.AddModelError("error", "some error");

            // Act
            var result = await _candidateController.AddOrUpdateCandidate(new Candidate());

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task AddOrUpdateCandidate_ValidModelState_CallsAddOrUpdateCandidateAsync()
        {
            // Arrange
            var candidate = new Candidate();

            // Act
            var result = await _candidateController.AddOrUpdateCandidate(candidate);

            // Assert
            _mockCandidateService.Verify(service => service.AddOrUpdateCandidateAsync(candidate), Times.Once);
            Assert.IsInstanceOf<OkResult>(result);
        }
    }

}

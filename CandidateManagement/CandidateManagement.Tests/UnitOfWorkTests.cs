using CandidateManagement.Repository.Repository;
using CandidateManagement.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagement.Tests
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        private Mock<AppDbContext> _mockContext;
        private UnitOfWork _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            _mockContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
            _unitOfWork = new UnitOfWork(_mockContext.Object);
        }

        [Test]
        public async Task CompleteAsync_WhenCalled_SavesChanges()
        {
            // Arrange
            _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            await _unitOfWork.CompleteAsync();

            // Assert
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public void CandidateRepository_WhenCalled_ReturnsCandidateRepository()
        {
            // Act
            var repository = _unitOfWork.CandidateRepository;

            // Assert
            Assert.IsNotNull(repository);
            Assert.IsInstanceOf<CandidateRepository>(repository);
        }

        [Test]
        public void Dispose_WhenCalled_DisposesContext()
        {
            // Act
            _unitOfWork.Dispose();

            // Assert
            _mockContext.Verify(c => c.Dispose(), Times.Once);
        }

        [Test]
        public async Task DisposeAsync_WhenCalled_DisposesContextAsync()
        {
            // Act
            await _unitOfWork.DisposeAsync();

            // Assert
            _mockContext.Verify(c => c.DisposeAsync(), Times.Once);
        }
    }
}

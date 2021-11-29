using System;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.Core.Entities;
using WebAutopark.Core.Exceptions;
using WebAutopark.Tests.Fixtures;
using Xunit;

namespace WebAutopark.Tests.Repositories
{
    public class DetailRepositoryTests : IDisposable
    {
        private readonly DetailRepositoryFixture _fixture;

        public DetailRepositoryTests()
        {
            _fixture = new DetailRepositoryFixture();
        }

        [Fact]
        public async Task GetAll_OK()
        {
            var detailsFromContext = _fixture.Connection.Details;
            // Act
            var result = _fixture.Repository.GetAll().AsQueryable();
            
            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(detailsFromContext.Count(), result.Count());
        }

        [Fact]
        public async Task GetById_OK()
        {
            // Act
            var result = await _fixture.Repository.GetById(_fixture.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_fixture.Id, result.Id);
            Assert.Equal("Name", result.Name);
        }

        [Fact]
        public async Task GetById_EmptyId_Fail()
        {
            // Arrange
            var id = Guid.Empty;
            // Act, Assert
            await Assert.ThrowsAsync<InvalidArgumentException>(() => _fixture.Repository.GetById(id));
        }

        [Fact]
        public async Task GetById_NotExistDirection_Fail()
        {
            // Arrange
            var id = Guid.Parse("99999999-9999-9999-9999-999999999999");

            // Act, Assert
            await Assert.ThrowsAsync<ObjectNotFoundException>(() => _fixture.Repository.GetById(id));
        }

        [Fact]
        public async Task Create_OK()
        {
            // Arrange
            var entity = new Detail
                         {
                             Name = "Name",
                         };

            // Act
            var result = await _fixture.Repository.Create(entity);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(default, result);
            Assert.Equal(entity.Name, result.Name);
        }

        [Fact]
        public async Task Create_EmptyEntity_Fail()
        {
            // Arrange Act, Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Repository.Create(null));
        }

        [Fact]
        public async Task Update_OK()
        {
            // Arrange
            var entity = new Detail
                         {
                             Id = _fixture.Id,
                             Name = "Name",
                         };

            // Act
            var result = _fixture.Repository.Update(entity);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(default, result.Id);
            Assert.Equal(entity.Name, result.Name);
        }

        [Fact]
        public void Update_EmptyEntity_Fail()
        {
            // Arrange Act, Assert
            Assert.Throws<ObjectNotFoundException>(() => _fixture.Repository.Update(null));
        }

        [Fact]
        public async Task Delete_OK()
        {
            // Act
            await _fixture.Repository.Delete(_fixture.Id);
            await _fixture.Connection.SaveChangesAsync();
            
            // Assert
            var deletedEntity = await _fixture.Connection.Details.FindAsync(_fixture.Id);
            
            Assert.Null(deletedEntity);
        }

        [Fact]
        public async Task Delete_EmptyId_Fail()
        {
            // Arrange
            var id = Guid.Empty;

            // Act, Assert
            await Assert.ThrowsAsync<ObjectNotFoundException>(() => _fixture.Repository.Delete(id));
        }
        public void Dispose() => _fixture.Dispose();
    }
}
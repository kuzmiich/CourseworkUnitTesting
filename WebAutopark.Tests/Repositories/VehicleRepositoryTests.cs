using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAutopark.Core.Entities;
using WebAutopark.Core.Exceptions;
using WebAutopark.Tests.Fixtures;
using Xunit;

namespace WebAutopark.Tests.Repositories
{
    public class VehicleRepositoryTests
    {
        private readonly VehicleRepositoryFixture _fixture;

        public VehicleRepositoryTests()
        {
            _fixture = new VehicleRepositoryFixture();
        }

        [Fact]
        public async Task GetAll_OK()
        {
            var detailsFromContext = _fixture.Connection.Vehicles;
            // Act
            var result = await _fixture.Repository.GetAll().ToListAsync();
            
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
            Assert.Equal("Name", result.ModelName);
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
            var entity = new Vehicle
                         {
                             ModelName = "Name",
                         };

            // Act
            var result = await _fixture.Repository.Create(entity);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(default, result);
            Assert.Equal(entity.ModelName, result.ModelName);
        }

        [Fact]
        public async Task Create_EmptyEntity_Fail()
        {
            // Arrange Act, Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Repository.Create(null));
        }

        [Fact]
        public void Update_OK()
        {
            // Arrange
            var entity = new Vehicle
                         {
                             Id = _fixture.Id,
                             ModelName = "Name",
                         };

            // Act
            var result = _fixture.Repository.Update(entity);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(default, result.Id);
            Assert.Equal(entity.ModelName, result.ModelName);
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
            var deletedEntity = await _fixture.Connection.Vehicles.FindAsync(_fixture.Id);
            
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
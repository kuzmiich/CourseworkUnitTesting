using System;
using System.Data.Common;
using System.Threading.Tasks;
using WebAutopark.Core.Entities;
using WebAutopark.Tests.Fixtures;
using Xunit;
using System.Linq;

namespace WebAutopark.Tests.Repositories
{
    public class DetailRepositoryTests : IDisposable
    {
        private readonly DetailRepositoryFixture _fixture;

        public DetailRepositoryTests(DbConnection dbConnection)
        {
            _fixture = new DetailRepositoryFixture(dbConnection);
        }

        [Fact]
        // Note: GetAll will work always in our case. It can be thrown into EF Core.
        // But it is implementation details of EF Core so we mustn't test these cases.
        public async Task GetAll_OK()
        {
            // Act
            var result = await _fixture.Repository.GetAll();

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(1, result.Count());
        }

        [Fact]
        public async Task GetById_OK()
        {
            // Act
            //var result = await _fixture.Repository.GetById(_fixture.DetailId);

            // Assert
            //Assert.NotNull(result);
            //Assert.Equal(_fixture.DirectionId, result.DirectionId);
            //Assert.Equal("Test Name", result.Name);
        }

        [Fact]
        public async Task GetById_EmptyId_Fail()
        {
            // Arrange
            const int id = 0;

            // Act, Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => _fixture.Repository.GetById(id));
        }

        [Fact]
        public async Task GetById_NotExistDirection_Fail()
        {
            // Arrange
            const int id = int.MaxValue;

            // Act, Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => _fixture.Repository.GetById(id));
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
            Assert.NotEqual(default, result.DirectionId);
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
                             DetailId = _fixture.DetailId,
                             Name = "Name",
                         };

            // Act
            var result = _fixture.Repository.Update(entity);
            await _fixture.Context.SaveChangesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(default, result.DirectionId);
            Assert.Equal(entity.Name, result.Name);
        }

        [Fact]
        public void Update_EmptyEntity_Fail()
        {
            // Arrange Act, Assert
            //Assert.Throws<ArgumentNullException>(() => _fixture.Repository.Update(null));
        }

        [Fact]
        public async Task Delete_OK()
        {
            // Act
            /*await _fixture.Repository.Delete(_fixture.DirectionId);
            await _fixture.Context.SaveChangesAsync();*/

            // Assert
            //var deletedEntity = await _fixture..Directions.FindAsync(_fixture.DirectionId);
            //Assert.Null(deletedEntity);
        }

        [Fact]
        public async Task Delete_EmptyId_Fail()
        {
            // Arrange
            const int id = 0;

            // Act, Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Repository.Delete(id));
        }
        public void Dispose() => _fixture.Dispose();
    }
}
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
    public class OrderRepositoryTests
    {
        private readonly OrderRepositoryFixture _fixture;

        public OrderRepositoryTests()
        {
            _fixture = new OrderRepositoryFixture();
        }

        [Fact]
        public async Task GetAll_OK()
        {
            // Arrange
            var ordersFromContext = _fixture.Connection.Orders;
            
            // Act
            var result = await _fixture.Repository.GetAll().ToListAsync();
            
            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(ordersFromContext.Count(), result.Count());
        }

        [Fact]
        public async Task GetById_OK()
        {
            // Act
            var result = await _fixture.Repository.GetById(_fixture.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_fixture.Id, result.Id);
            Assert.Equal("Name", result.Description);
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
            var entity = new Order
                         {
                             Description = "Name",
                         };

            // Act
            var result = await _fixture.Repository.Create(entity);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(default, result);
            Assert.Equal(entity.Description, result.Description);
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
            var entity = new Order
                         {
                             Id = _fixture.Id,
                             Description = "Name",
                         };

            // Act
            var result = _fixture.Repository.Update(entity);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(default, result.Id);
            Assert.Equal(entity.Description, result.Description);
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
            var deletedEntity = await _fixture.Connection.Orders.FindAsync(_fixture.Id);
            
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
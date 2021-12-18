﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAutopark.Core.Entities;
using WebAutopark.Core.Exceptions;
using WebAutopark.Tests.Fixtures;
using Xunit;

namespace WebAutopark.Tests.Repositories
{
    public class ProductRepositoryTests
    {
        private readonly ProductRepositoryFixture _fixture;

        public ProductRepositoryTests()
        {
            _fixture = new ProductRepositoryFixture();
        }

        [Fact]
        public async Task GetAll_OK()
        {
            var detailsFromContext = _fixture.Connection.Products;
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
            Assert.Equal("Name", result.Description);
        }

        [Fact]
        public async Task GetById_EmptyId_Fail()
        {
            // Arrange
            var id = -1;
            // Act, Assert
            await Assert.ThrowsAsync<InvalidArgumentException>(() => _fixture.Repository.GetById(id));
        }

        [Fact]
        public async Task GetById_NotExistDirection_Fail()
        {
            // Arrange
            var id = int.MaxValue;

            // Act, Assert
            await Assert.ThrowsAsync<ObjectNotFoundException>(() => _fixture.Repository.GetById(id));
        }

        [Fact]
        public async Task Create_OK()
        {
            // Arrange
            var entity = new Product
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
            // Arrange, Act, Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Repository.Create(null));
        }

        [Fact]
        public void Update_OK()
        {
            // Arrange
            var entity = new Product
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
            var deletedEntity = await _fixture.Connection.Products.FindAsync(_fixture.Id);
            
            Assert.Null(deletedEntity);
        }

        [Fact]
        public async Task Delete_EmptyId_Fail()
        {
            // Arrange
            var id = 0;

            // Act, Assert
            await Assert.ThrowsAsync<ObjectNotFoundException>(() => _fixture.Repository.Delete(id));
        }
        
        public void Dispose() => _fixture.Dispose();
    }
}
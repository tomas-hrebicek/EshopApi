using AutoMapper;
using Sample.Api.DTOs;
using Sample.Core.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Sample.Test.Controllers
{
    public class ProductTest
    {
        private IMapper _mapper;

        public ProductTest()
        {
            _mapper = new MapperConfiguration(mc => mc.AddMaps(typeof(Api.Profiles.ProductProfile).Assembly)).CreateMapper();
        }

        [Fact]
        public void ListReturnsData()
        {
            // Arrange
            var repository = new Mock<Core.Interfaces.IProducts>();
            repository.Setup(x => x.List()).Returns(new List<Product>()
            {
                new Product
                {
                    Id = 1,
                    Name = "product name 1",
                    ImgUri = "img uri",
                    Price = 123M
                },
                new Product
                {
                    Id = 2,
                    Name = "product name 2",
                    ImgUri = "img uri",
                    Price = 321M
                },
                new Product
                {
                    Id = 3,
                    Name = "product name 3",
                    ImgUri = "img uri",
                    Price = 132M
                }
            });

            var controller = new Api.Controllers.ProductController(repository.Object, _mapper);

            // Act
            var result = controller.List();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
        }

        [Fact]
        public void ListPaginationReturnsData()
        {
            // Arrange
            var repository = new Mock<Core.Interfaces.IProducts>();
            repository.Setup(x => x.Query()).Returns(new List<Product>()
            {
                new Product
                {
                    Id = 1,
                    Name = "product name 1",
                    ImgUri = "img uri",
                    Price = 123M
                },
                new Product
                {
                    Id = 2,
                    Name = "product name 2",
                    ImgUri = "img uri",
                    Price = 321M
                },
                new Product
                {
                    Id = 3,
                    Name = "product name 3",
                    ImgUri = "img uri",
                    Price = 132M
                }
            }.AsQueryable());

            var controller = new Api.Controllers.ProductController(repository.Object, _mapper);

            PaginationDTO pagination = new PaginationDTO()
            {
                PageNumber = 1,
                PageSize = 2
            };

            // Act
            var result = controller.ListPagination(pagination);

            // Assert
            result.Should().NotBeNull();
            result.Item.Should().NotBeNull().And.HaveCount(pagination.PageSize);
            result.Paging.Should().NotBeNull();
            result.Paging.TotalPages.Should().Be(2);
            result.Paging.TotalItems.Should().Be(3);
        }

        [Fact]
        public void GetReturnsProduct()
        {
            // Arrange
            var repository = new Mock<Core.Interfaces.IProducts>();
            repository.Setup(x => x.Get(It.IsAny<int>())).Returns((int id) => new Product()
            {
                Id = id,
                Name = "product name",
                ImgUri = "img uri",
                Price = 123M
            });

            var controller = new Api.Controllers.ProductController(repository.Object, _mapper);

            int expectedProductId = 123;

            // Act
            var actionResult = controller.Get(expectedProductId);

            // Assert
            actionResult.Should().NotBeNull();
            actionResult.Should().BeOfType<OkObjectResult>();

            var model = (actionResult as OkObjectResult)!.Value as ProductDTO;

            model.Should().NotBeNull();
            model.Id.Should().Be(expectedProductId);
        }

        [Fact]
        public void GetReturnsNotFound()
        {
            // Arrange
            var repository = new Mock<Core.Interfaces.IProducts>();
            repository.Setup(x => x.Get(It.IsAny<int>())).Returns((Product)null);

            var controller = new Api.Controllers.ProductController(repository.Object, _mapper);

            // Act
            var actionResult = controller.Get(123);

            // Assert
            actionResult.Should().NotBeNull();
            actionResult.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void UpdateDescriptionReturnsItemWithNewDescription()
        {
            // Arrange
            int productId = 123;
            ProductDescriptionDTO description = new ProductDescriptionDTO() { Description = "test description" };

            var repository = new Mock<Core.Interfaces.IProducts>();
            repository.Setup(x => x.Update(It.IsAny<Product>()));
            repository.Setup(x => x.Get(It.IsAny<int>())).Returns((int id) => new Product()
            {
                Id = id,
                Name = "testing product",
                ImgUri = "img uri",
                Price = 123M,
                Description = description.Description
            });

            var controller = new Api.Controllers.ProductController(repository.Object, _mapper);

            // Act
            var actionResult = controller.UpdateDescription(productId, description);

            // Assert
            actionResult.Should().NotBeNull();
            actionResult.Should().BeOfType<OkObjectResult>();

            var model = (actionResult as OkObjectResult)!.Value as ProductDTO;

            model.Should().NotBeNull();
            model.Id.Should().Be(productId);
            model.Description.Should().Be(description.Description);
        }

        [Fact]
        public void UpdateDescriptionBadProductIdReturnsNotFound()
        {
            // Arrange
            int productId = 123;
            ProductDescriptionDTO description = new ProductDescriptionDTO() { Description = "test description" };

            var repository = new Mock<Core.Interfaces.IProducts>();
            repository.Setup(x => x.Update(It.IsAny<Product>()));
            repository.Setup(x => x.Get(It.IsAny<int>())).Returns((Product)null);

            var controller = new Api.Controllers.ProductController(repository.Object, _mapper);

            // Act
            var actionResult = controller.UpdateDescription(productId, description);

            // Assert
            actionResult.Should().NotBeNull();
            actionResult.Should().BeOfType<NotFoundResult>();
        }
    }
}
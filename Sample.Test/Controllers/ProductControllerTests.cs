using AutoMapper;
using Sample.Api.DTOs;
using Sample.Core.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Sample.Core.Specification;
using System.Collections.Generic;

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
        public async void ListReturnsData()
        {
            // Arrange
            var repository = new Mock<Core.Interfaces.IProducts>();
            repository.Setup(x => x.ListAsync()).ReturnsAsync(new List<Product>()
            {
                new Product
                {
                    Id = 1,
                    Name = "product name 1",
                    ImgUri = new Uri("https://www.img.test"),
                    Price = 123M
                },
                new Product
                {
                    Id = 2,
                    Name = "product name 2",
                    ImgUri = new Uri("https://www.img.test"),
                    Price = 321M
                },
                new Product
                {
                    Id = 3,
                    Name = "product name 3",
                    ImgUri = new Uri("https://www.img.test"),
                    Price = 132M
                }
            });

            var controller = new Api.Controllers.v1.ProductController(repository.Object, _mapper);

            // Act
            var actionResult = await controller.List();
            
            // Assert
            var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;
            var value = okResult.Value.Should().NotBeNull().And.BeAssignableTo<IEnumerable<ProductDTO>>().Subject;
            value.Should().NotBeNull();
            value.Should().HaveCount(3);
        }

        [Fact]
        public async void ListPaginationReturnsData()
        {
            // Arrange
            List<Product> items = new List<Product>()
            {
                new Product
                {
                    Id = 1,
                    Name = "product name 1",
                    ImgUri = new Uri("https://www.img.test"),
                    Price = 123M
                },
                new Product
                {
                    Id = 2,
                    Name = "product name 2",
                    ImgUri = new Uri("https://www.img.test"),
                    Price = 321M
                },
                new Product
                {
                    Id = 3,
                    Name = "product name 3",
                    ImgUri = new Uri("https://www.img.test"),
                    Price = 132M
                }
            };

            var repository = new Mock<Core.Interfaces.IProducts>();
            repository.Setup(x => x.ListAsync(It.IsAny<Pagination>())).ReturnsAsync((Pagination pagination) => new PagedList<Product>(
                items.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize), new PagingInformation()
            {
                    TotalItems = items.Count,
                    PageSize = pagination.PageSize,
                    PageNumber = pagination.PageNumber
            }));

            var controller = new Api.Controllers.v2.ProductController(repository.Object, _mapper);

            PaginationDTO pagination = new PaginationDTO()
            {
                PageNumber = 1,
                PageSize = 2
            };

            // Act
            var actionResult = await controller.ListPagination(pagination);

            // Assert
            var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;
            var value = okResult.Value.Should().NotBeNull().And.BeOfType<PagedList<ProductDTO>>().Subject;
            value.Should().NotBeNull();
            value.Item.Should().NotBeNull().And.HaveCount(pagination.PageSize);
            value.Paging.Should().NotBeNull();
            value.Paging.TotalPages.Should().Be(2);
            value.Paging.TotalItems.Should().Be(3);
        }

        [Fact]
        public async void GetReturnsProduct()
        {
            // Arrange
            var repository = new Mock<Core.Interfaces.IProducts>();
            repository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync((int id) => new Product()
            {
                Id = id,
                Name = "product name",
                ImgUri = new Uri("https://www.img.test"),
                Price = 123M
            });

            var controller = new Api.Controllers.v1.ProductController(repository.Object, _mapper);

            int expectedProductId = 123;

            // Act
            var actionResult = await controller.Get(expectedProductId);

            // Assert
            var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;
            var value = okResult.Value.Should().NotBeNull().And.BeOfType<ProductDTO>().Subject;
            value.Should().NotBeNull();
            value.Id.Should().Be(expectedProductId);
        }

        [Fact]
        public async void GetReturnsNotFound()
        {
            // Arrange
            var repository = new Mock<Core.Interfaces.IProducts>();
            repository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync((Product)null);

            var controller = new Api.Controllers.v1.ProductController(repository.Object, _mapper);

            // Act
            var actionResult = await controller.Get(123);

            // Assert
            actionResult.Should().NotBeNull();
            actionResult.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void UpdateDescriptionReturnsItemWithNewDescription()
        {
            // Arrange
            int productId = 123;
            ProductDescriptionDTO description = new ProductDescriptionDTO() { Description = "test description" };

            var repository = new Mock<Core.Interfaces.IProducts>();
            repository.Setup(x => x.UpdateAsync(It.IsAny<Product>()));
            repository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync((int id) => new Product()
            {
                Id = id,
                Name = "testing product",
                ImgUri = new Uri("https://www.img.test"),
                Price = 123M,
                Description = description.Description
            });

            var controller = new Api.Controllers.v1.ProductController(repository.Object, _mapper);

            // Act
            var actionResult = await controller.UpdateDescription(productId, description);

            // Assert
            var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;
            var value = okResult.Value.Should().NotBeNull().And.BeOfType<ProductDTO>().Subject;
            value.Should().NotBeNull();
            value.Id.Should().Be(productId);
            value.Description.Should().Be(description.Description);
        }

        [Fact]
        public async void UpdateDescriptionBadProductIdReturnsNotFound()
        {
            // Arrange
            int productId = 123;
            ProductDescriptionDTO description = new ProductDescriptionDTO() { Description = "test description" };

            var repository = new Mock<Core.Interfaces.IProducts>();
            repository.Setup(x => x.UpdateAsync(It.IsAny<Product>()));
            repository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync((Product)null);

            var controller = new Api.Controllers.v1.ProductController(repository.Object, _mapper);

            // Act
            var actionResult = await controller.UpdateDescription(productId, description);

            // Assert
            actionResult.Should().NotBeNull();
            actionResult.Should().BeOfType<NotFoundResult>();
        }
    }
}
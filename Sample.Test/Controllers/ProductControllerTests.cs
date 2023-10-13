using AutoMapper;
using Sample.Core.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Sample.Core.Base;
using Sample.Infrastructure;
using Sample.Application.DTOs;

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
            var repository = new Mock<Application.Interfaces.IProductsService>();
            repository.Setup(x => x.ListAsync()).ReturnsAsync(new List<ProductDTO>()
            {
                new ProductDTO
                {
                    Id = 1,
                    Name = "product name 1",
                    ImgUri = new Uri("https://www.img.test"),
                    Price = 123M
                },
                new ProductDTO
                {
                    Id = 2,
                    Name = "product name 2",
                    ImgUri = new Uri("https://www.img.test"),
                    Price = 321M
                },
                new ProductDTO
                {
                    Id = 3,
                    Name = "product name 3",
                    ImgUri = new Uri("https://www.img.test"),
                    Price = 132M
                }
            });

            var controller = new Api.Controllers.v1.ProductController(repository.Object);

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
            List<ProductDTO> items = new List<ProductDTO>()
            {
                new ProductDTO
                {
                    Id = 1,
                    Name = "product name 1",
                    ImgUri = new Uri("https://www.img.test"),
                    Price = 123M
                },
                new ProductDTO
                {
                    Id = 2,
                    Name = "product name 2",
                    ImgUri = new Uri("https://www.img.test"),
                    Price = 321M
                },
                new ProductDTO
                {
                    Id = 3,
                    Name = "product name 3",
                    ImgUri = new Uri("https://www.img.test"),
                    Price = 132M
                }
            };

            var repository = new Mock<Application.Interfaces.IProductsService>();
            repository.Setup(x => x.ListAsync(It.IsAny<PaginationSettingsDTO>())).ReturnsAsync((PaginationSettingsDTO pagination) =>
            {
                PaginationSettings p = _mapper.Map<PaginationSettingsDTO, PaginationSettings>(pagination);
                var query = items.AsQueryable();
                var totalCount = query.Count();
                var list = query.Skip((p.PageNumber - 1) * p.PageSize).Take(p.PageSize).ToList();
                return new PagedList<ProductDTO>(list, totalCount, p.PageNumber, p.PageSize);
            });

            var controller = new Api.Controllers.v2.ProductController(repository.Object);

            PaginationSettingsDTO pagination = new PaginationSettingsDTO()
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
            value.Items.Should().NotBeNull().And.HaveCount(pagination.PageSize);
            value.PageCount.Should().Be(2);
            value.TotalCount.Should().Be(3);
        }

        [Fact]
        public async void GetReturnsProduct()
        {
            // Arrange
            var repository = new Mock<Application.Interfaces.IProductsService>();
            repository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync((int id) => new ProductDTO()
            {
                Id = id,
                Name = "product name",
                ImgUri = new Uri("https://www.img.test"),
                Price = 123M
            });

            var controller = new Api.Controllers.v1.ProductController(repository.Object);

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
            var repository = new Mock<Application.Interfaces.IProductsService>();
            repository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync((ProductDTO)null);

            var controller = new Api.Controllers.v1.ProductController(repository.Object);

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
            var repository = new Mock<Application.Interfaces.IProductsService>();
            repository.Setup(x => x.UpdateDescriptionAsync(It.IsAny<int>(), It.IsAny<ProductDescriptionDTO>())).ReturnsAsync((int id, ProductDescriptionDTO description) => new ProductDTO()
            {
                Id = id,
                Name = "testing product",
                ImgUri = new Uri("https://www.img.test"),
                Price = 123M,
                Description = description.Description
            });

            int productId = 123;
            ProductDescriptionDTO description = new ProductDescriptionDTO() { Description = "test description" };

            var controller = new Api.Controllers.v1.ProductController(repository.Object);

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
            var repository = new Mock<Application.Interfaces.IProductsService>();
            repository.Setup(x => x.UpdateDescriptionAsync(It.IsAny<int>(), It.IsAny<ProductDescriptionDTO>())).ReturnsAsync((int id, ProductDescriptionDTO description) => (ProductDTO)null);

            int productId = 123;
            ProductDescriptionDTO description = new ProductDescriptionDTO() { Description = "test description" };

            var controller = new Api.Controllers.v1.ProductController(repository.Object);

            // Act
            var actionResult = await controller.UpdateDescription(productId, description);

            // Assert
            actionResult.Should().NotBeNull();
            actionResult.Should().BeOfType<NotFoundResult>();
        }
    }
}
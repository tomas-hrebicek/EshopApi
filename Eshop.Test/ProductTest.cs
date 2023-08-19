using AutoMapper;
using Eshop.Api.DTOs;
using Eshop.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Eshop.Test
{
    public class ProductTest
    {
        [Fact]
        public void GetReturnsProduct()
        {
            // Arrange
            IMapper mapper = new AutoMapper.MapperConfiguration(mc => mc.AddMaps(typeof(Eshop.Api.Profiles.ProductProfile).Assembly)).CreateMapper();

            var repository = new Mock<Eshop.Core.Interfaces.IProducts>();
            repository.Setup(x => x.Get(It.IsAny<int>())).Returns((int id) => new Product()
            {
                Id = id,
                Name = "product name",
                ImgUri = "img uri",
                Price = 123M
            });

            var controller = new Eshop.Api.Controllers.ProductController(repository.Object, mapper);
            
            // Act
            var result = controller.Get(123) as ObjectResult;
            var resultObject = result.Value as ProductDTO;
            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result!.StatusCode);
            Assert.NotNull(resultObject);
            Assert.Equal(resultObject.Id, 123);
        }
    }
}
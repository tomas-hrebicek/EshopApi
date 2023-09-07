namespace Sample.Test.Validations
{
    public class ProductValidationTest : ValidationTests
    {
        [Theory]
        [InlineData(1, null, null, 0, null, false)]
        [InlineData(1, "name", null, 0, null, false)]
        [InlineData(1, "name", "img", 0, null, true)]
        [InlineData(-1, "name", "img", 0, null, false)]
        [InlineData(1, "name", "img", 0, "desc", true)]
        public void TestModelValidation(int id, string name, string imgUri, decimal price, string description, bool isValid)
        {
            var owner = new Sample.Api.DTOs.ProductDTO()
            {
                Id = id,
                Name = name,
                ImgUri = imgUri,
                Price = price,
                Description = description
            };

            Assert.Equal(isValid, ValidateModel(owner));
        }
    }
}

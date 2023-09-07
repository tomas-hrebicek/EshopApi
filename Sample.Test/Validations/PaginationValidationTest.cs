namespace Sample.Test.Validations
{
    public class PaginationValidationTest : ValidationTests
    {
        [Theory]
        [InlineData(0, 1, false)]
        [InlineData(1, 0, false)]
        [InlineData(1, 1, true)]
        [InlineData(1, 1000, true)]
        [InlineData(1, 1001, false)]
        public void TestModelValidation(int pageNumber, int pageSize, bool isValid)
        {
            var owner = new Sample.Api.DTOs.PaginationDTO()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            Assert.Equal(isValid, ValidateModel(owner));
        }
    }
}

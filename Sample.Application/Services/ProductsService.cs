using AutoMapper;
using Sample.Application.DTOs;
using Sample.Application.Interfaces;
using Sample.Domain.Domain;
using Sample.Domain.Entities;
using Sample.Domain.Interfaces;

namespace Sample.Application.Services
{
    internal class ProductsService : IProductsService
    {
        public ProductsService(IProductsRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private readonly IMapper _mapper;
        private readonly IProductsRepository _repository;

        public async Task<Result<ProductDTO>> GetAsync(int id)
        {
            var product = await _repository.GetAsync(id);

            if (product is null)
            {
                return Result.Failure<ProductDTO>(new NotFoundError($"product id = {id}"));
            }
            else
            {
                return Result.Success(_mapper.Map<Product, ProductDTO>(product));
            }
        }

        public async Task<Result<IEnumerable<ProductDTO>>> ListAsync()
        {
            var products = await _repository.ListAsync();
            return Result.Success(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products));
        }

        public async Task<Result<PagedList<ProductDTO>>> ListAsync(PaginationSettingsDTO paginationSettings)
        {
            var settings = _mapper.Map<PaginationSettingsDTO, PaginationSettings>(paginationSettings);
            var products = await _repository.ListAsync(settings);
            return Result.Success(_mapper.Map<PagedList<Product>, PagedList<ProductDTO>>(products));
        }

        public async Task<Result<ProductDTO>> UpdateDescriptionAsync(int productId, ProductDescriptionDTO description)
        {
            var product = await _repository.GetAsync(productId);

            if (product is null)
            {
                return Result.Failure<ProductDTO>(new NotFoundError($"product id = {productId}"));
            }
            else
            {
                _mapper.Map(description, product);
                _repository.UpdateAsync(product);
                product = await _repository.GetAsync(productId);
                return Result.Success(_mapper.Map<Product, ProductDTO>(product));
            }
        }
    }
}
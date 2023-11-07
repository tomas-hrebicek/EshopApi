using AutoMapper;
using Sample.Application.DTOs;
using Sample.Application.Interfaces;
using Sample.Domain.Domain;
using Sample.Domain.Entities;
using Sample.Domain.Interfaces;

namespace Sample.Application.Services
{
    internal class ProductService : IProductsService
    {
        public ProductService(IProductsRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private readonly IMapper _mapper;
        private readonly IProductsRepository _repository;

        public async Task<ProductDTO> GetAsync(int id)
        {
            var product = await _repository.GetAsync(id);
            return _mapper.Map<Product, ProductDTO>(product);
        }

        public async Task<IEnumerable<ProductDTO>> ListAsync()
        {
            var products = await _repository.ListAsync();
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
        }

        public async Task<PagedList<ProductDTO>> ListAsync(PaginationSettingsDTO paginationSettings)
        {
            var settings = _mapper.Map<PaginationSettingsDTO, PaginationSettings>(paginationSettings);
            var products = await _repository.ListAsync(settings);
            return _mapper.Map<PagedList<Product>, PagedList<ProductDTO>>(products);
        }

        public async Task<ProductDTO> UpdateDescriptionAsync(int productId, ProductDescriptionDTO description)
        {
            var product = await _repository.GetAsync(productId);

            if (product is null)
            {
                return null;
            }
            else
            {
                _mapper.Map(description, product);
                _repository.UpdateAsync(product);
                product = await _repository.GetAsync(productId);
                return _mapper.Map<Product, ProductDTO>(product);
            }
        }
    }
}
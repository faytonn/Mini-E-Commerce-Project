using Mini_E_Commerce_Project.DTO.GetDTO.UserAccessedDTO;
using Mini_E_Commerce_Project.Exceptions;
using Mini_E_Commerce_Project.Repositories.Implementations;
using Mini_E_Commerce_Project.Repositories.Interfaces;
using Mini_E_Commerce_Project.Services.AdminInterfaces;

namespace Mini_E_Commerce_Project.Services.AdminImplementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService()
        {
            _productRepository = new ProductRepository();
        }


        public async Task<GetProductDTO> GetProductById(int id)
        {
            var product = await _productRepository.GetSingleAsync(p => p.Id == id);

            if (product == null)
            {
                throw new NotFoundException("Product not found.");
            }

            return new GetProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description
            };

        }

        public async Task<List<GetProductDTO>> GetProducts()
        {
            var products = await _productRepository.GetAllAsync();

            var productDTOs = new List<GetProductDTO>();

            foreach (var product in products)
            {
                var productDTO = new GetProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Stock = product.Stock,
                    Description = product.Description,

                };
                productDTOs.Add(productDTO);
            }
            return productDTOs;
        }
    }
}

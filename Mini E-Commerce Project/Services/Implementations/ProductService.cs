using Mini_E_Commerce_Project.DTO.DTOModels;
using Mini_E_Commerce_Project.DTO.ServiceDTO;
using Mini_E_Commerce_Project.Exceptions;
using Mini_E_Commerce_Project.Models;
using Mini_E_Commerce_Project.Repositories.Implementations;
using Mini_E_Commerce_Project.Repositories.Interfaces;
using Mini_E_Commerce_Project.Services.Interfaces;

namespace Mini_E_Commerce_Project.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public ProductService()
        {
            _productRepository = new ProductRepository();
            _userRepository = new UserRepository();
        }

        public async Task CreateProduct(CreateProductDTO newProduct)
        {
            var DoesProductExist = await _productRepository.ExistsAsync(p => p.Name == newProduct.Name);

            if (DoesProductExist)
            {
                throw new InvalidProductException("This product already exists.");
            }
            Product product = new()
            {
                Name = newProduct.Name,
                Price = newProduct.Price,
                Stock = newProduct.Stock,
                Description = newProduct.Description,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            };

            await _productRepository.CreateAsync(product);
            await _productRepository.SaveChangesAsync();  
        }

        public Task DeleteProduct(int id)
        {

        }

        public Task<ProductDTO> GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDTO>> GetProducts()
        {
            throw new NotImplementedException();
        }

        public Task UpdateProduct(int id, ProductDTO updatedProduct)
        {
            throw new NotImplementedException();
        }
    }
}

using Mini_E_Commerce_Project.DTO;
using Mini_E_Commerce_Project.Models;
using Mini_E_Commerce_Project.Repositories.Interfaces;
using Mini_E_Commerce_Project.Services.Interfaces;

namespace Mini_E_Commerce_Project.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository  _productRepository;
        private readonly IUserRepository _userRepository;

        public ProductService(IProductRepository productRepository, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public async Task CreateProduct(ProductDTO newProduct)
        {
           var DoesProductExist
        }

        public Task DeleteProduct(int id)
        {
            throw new NotImplementedException();
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

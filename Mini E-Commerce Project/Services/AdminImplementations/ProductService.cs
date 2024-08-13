using Mini_E_Commerce_Project.DTO.GetDTO.UserAccessedDTO;
using Mini_E_Commerce_Project.DTO.InsertDTO;
using Mini_E_Commerce_Project.DTO.ServiceDTO;
using Mini_E_Commerce_Project.Exceptions;
using Mini_E_Commerce_Project.Models;
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



        public async Task DeleteProduct(int id)
        {
            var product = await _productRepository.GetSingleAsync(p => p.Id == id);

            if (product is null)
            {
                throw new NotFoundException("Product not found.");
            }
            _productRepository.Delete(product);
            await _productRepository.SaveChangesAsync();
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

        public async Task UpdateProduct(int id, InsertProductDTO updatedProduct)
        {
            var product = await _productRepository.GetSingleAsync(p => p.Id == id);

            if (product == null)
            {
                throw new NotFoundException("Product not found.");
            }
            if (string.IsNullOrWhiteSpace(updatedProduct.Name) || updatedProduct.Price < 0)
            {
                throw new InvalidProductException("Invalid product data");
            }

            updatedProduct.Id = product.Id;
            updatedProduct.Name = product.Name;
            updatedProduct.Price = product.Price;
            updatedProduct.Stock = product.Stock;
            updatedProduct.Description = product.Description;

            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();
        }

    }
}

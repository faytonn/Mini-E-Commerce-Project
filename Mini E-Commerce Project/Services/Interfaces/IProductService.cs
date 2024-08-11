using Mini_E_Commerce_Project.DTO;

namespace Mini_E_Commerce_Project.Services.Interfaces
{
    public interface IProductService
    {
        Task CreateProduct(ProductDTO newProduct);
        Task UpdateProduct(int id, ProductDTO updatedProduct);
        Task DeleteProduct(int id);
        Task<List<ProductDTO>> GetProducts();
        Task<ProductDTO> GetProductById(int id);
    }
}

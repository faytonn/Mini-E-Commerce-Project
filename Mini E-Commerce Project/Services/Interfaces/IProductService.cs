using Mini_E_Commerce_Project.DTO.GetDTO;
using Mini_E_Commerce_Project.DTO.InsertDTO;
using Mini_E_Commerce_Project.DTO.ServiceDTO;

namespace Mini_E_Commerce_Project.Services.Interfaces
{
    public interface IProductService
    {
        Task CreateProduct(CreateProductDTO newProduct);
        Task UpdateProduct(int id, InsertProductDTO updatedProduct);
        Task DeleteProduct(int id);
        Task<List<GetProductDTO>> GetProducts();
        Task<GetProductDTO> GetProductById(int id);
    }
}

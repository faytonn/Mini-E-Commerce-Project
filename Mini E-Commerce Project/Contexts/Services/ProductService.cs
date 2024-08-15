using Mini_E_Commerce_Project.DTO.GetDTO.UserAccessedDTO;
using Mini_E_Commerce_Project.Services.AdminInterfaces;

namespace Mini_E_Commerce_Project.Contexts.Services
{
    public class ProductService : IProductService
    {
        public Task<GetProductDTO> GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetProductDTO>> GetProducts()
        {
            throw new NotImplementedException();
        }
    }
}

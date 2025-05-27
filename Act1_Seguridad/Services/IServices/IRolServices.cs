using Domain.DTO;
using Domain.Entities;

namespace Act1_Seguridad.Services.IServices
{
    public interface IRolServices
    {
        public Task<Response<List<Rol>>> GetAll();
        public Task<Response<Rol>> GetById(int id);
        public Task<Response<Rol>> Create(RolRequest request);
        public Task<Response<Rol>> Update(RolRequest request, int id);
        public Task<Response<Rol>> Delete(int id);
    }
}

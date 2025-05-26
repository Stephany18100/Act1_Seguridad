using Domain.DTO;
using Domain.Entities;

namespace Act1_Seguridad.Services.IServices
{
    public interface IUsuarioServices
    {
        public Task<Response<List<Usuario>>> GetAll();
        public Task<Response<Usuario>> GetById(int id);
        public Task<Response<Usuario>> Create(UsuarioRequest request);

        public Task<Response<Usuario>> Update(UsuarioRequest request, int id);
        public Task<Response<Usuario>> Delete(int id);
    }
}

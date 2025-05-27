using Act1_Seguridad.Context;
using Act1_Seguridad.Services.IServices;
using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Act1_Seguridad.Services.Services
{
    public class RolServices : IRolServices
    {
        private readonly ApplicationDbContext _context;
        public RolServices(ApplicationDbContext context)
        {
            _context = context;
        }

        //Lista de usuarios
        public async Task<Response<List<Rol>>> GetAll()
        {
            try
            {

                List<Rol> response = await _context.Roles.ToListAsync();

                return new Response<List<Rol>>(response);

            }
            catch (Exception ex)
            {

                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }

        public async Task<Response<Rol>> GetById(int id)
        {
            try
            {
                Rol rol = await _context.Roles.FindAsync(id);

                return new Response<Rol>(rol);
            }
            catch (Exception ex)
            {

                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }

        public async Task<Response<Rol>> Create(RolRequest request)
        {
            try
            {
                Rol response = new Rol()
                {
                    Nombre = request.Nombre
                };
                _context.Roles.Add(response);
                await _context.SaveChangesAsync();
                return new Response<Rol>(response, "Se agregó correctamente");
            }
            catch (Exception e)
            {

                throw new Exception("Ocurrio un error: " + e.Message);
            }
        }
        public async Task<Response<Rol>> Update(RolRequest request, int id)
        {
            try
            {
                var response = _context.Roles.Find(id);
                response.Nombre = request.Nombre;

                _context.Entry(response).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new Response<Rol>(response, "Se actualizo correctamente");

            }
            catch (Exception e)
            {

                throw new Exception("ocurrio un error: " + e.Message);
            }
        }

        public async Task<Response<Rol>> Delete(int id)
        {
            try
            {
                Rol response = _context.Roles.Find(id);
                _context.Roles.Remove(response);
                await _context.SaveChangesAsync();

                return new Response<Rol>(response, "Se elimino correctamente");
            }
            catch (Exception e)
            {
                throw new Exception("Ocurrio un error " + e.Message);
            }
        }
    }
}
